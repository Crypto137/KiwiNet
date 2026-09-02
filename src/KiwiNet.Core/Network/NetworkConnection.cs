using KiwiNet.Core.Logging;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KiwiNet.Core.Network
{
    public class NetworkConnection
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        // This is based on the client implementation. The fields follow the same order as client code, which is why we don't use auto properties (for now).
        private const int BufferSize = 1024 * 128;  // 0x20000

        private readonly Socket _socket;

        private bool _isActive;
        private bool _isConnected;
        private bool _isTruncated;
        private bool _throwIfWouldBlock;

        private readonly byte[] _writeBuffer = new byte[BufferSize];
        private int _writePosition;

        private readonly byte[] _readBuffer = new byte[BufferSize];
        private int _readPosition;
        private int _lastConfirmedReadPosition;
        private int _receivePosition;

        public bool IsActive { get => _isActive; }
        public bool IsConnected { get => _isConnected; }
        public bool IsTruncated { get => _isTruncated; }

        public NetworkConnection(Socket socket)
        {
            _socket = socket;
            _socket.Blocking = false;

            _isActive = true;
            _isConnected = true;
        }

        public void Disconnect()
        {
            _socket.Disconnect(false);
            _isActive = false;
            _isConnected = false;
        }

        public void Receive()
        {
            if (_isConnected == false)
                return;

            int length = GetAvailableReceiveCapacity();
            if (length == 0)
                goto End;

            int bytesReceived;
            int errorCode;

            try
            {
                bytesReceived = _socket.Receive(_readBuffer, _receivePosition, length, SocketFlags.None);
                errorCode = 0;
            }
            catch (SocketException e)
            {
                bytesReceived = -1;
                errorCode = e.ErrorCode;    // Replacement for WSAGetLastError()
            }

            if (bytesReceived == 0)
            {
                _isConnected = false;
                return;
            }
            else if (bytesReceived == -1)
            {
                if (errorCode != (int)SocketError.WouldBlock)
                {
                    _isActive = false;
                    _isConnected = false;
                }

                return;
            }

            _receivePosition += bytesReceived;
        End:
            if (_receivePosition == BufferSize)
                _receivePosition = 0;
        }

        public void Read(Span<byte> destination)
        {
            if (_isTruncated)
                return;

            int count = destination.Length;

            if (count >= BufferSize)
            {
                Disconnect();
                return;
            }

            int available;
            if (_readPosition > _receivePosition)
                available = _receivePosition - _readPosition + BufferSize;
            else
                available = _receivePosition - _readPosition;

            if (count > available)
            {
                _isTruncated = true;
                return;
            }

            if (_readPosition + count < BufferSize)
            {
                // No wrapping needed
                _readBuffer.AsSpan(_readPosition, count).CopyTo(destination);
                _readPosition += count;
            }
            else
            {
                // Read in two chunks (pre and post wrap)
                int countPreWrap = BufferSize - _readPosition;
                int countPostWrap = count - countPreWrap;
                _readBuffer.AsSpan(_readPosition, countPreWrap).CopyTo(destination);
                _readBuffer.AsSpan(0, countPostWrap).CopyTo(destination[countPreWrap..]);
                _readPosition = countPostWrap;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Read<T>() where T: unmanaged
        {
            Debug.Assert(typeof(T).IsPrimitive);

            Span<byte> bytes = stackalloc byte[Unsafe.SizeOf<T>()];
            Read(bytes);
            bytes.Reverse();    // BE -> LE
            return MemoryMarshal.Read<T>(bytes);
        }

        public void ConfirmRead()
        {
            _lastConfirmedReadPosition = _readPosition;
        }

        public void CancelRead()
        {
            _readPosition = _lastConfirmedReadPosition;
            _isTruncated = false;
        }

        public int GetAvailableReceiveCapacity()
        {
            if (_lastConfirmedReadPosition <= _receivePosition)
                return BufferSize - _receivePosition - (_lastConfirmedReadPosition == 0 ? 1 : 0);
            else
                return _lastConfirmedReadPosition - _receivePosition - 1;
        }

        public void Write(ReadOnlySpan<byte> source)
        {
            int count = source.Length;

            if (count > BufferSize)
            {
                Disconnect();
                return;
            }

            if (_writePosition + count > BufferSize)
                Flush();

            source.CopyTo(_writeBuffer.AsSpan(_writePosition, count));
            _writePosition += count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(T value) where T: unmanaged
        {
            Debug.Assert(typeof(T).IsPrimitive);

            int count = Unsafe.SizeOf<T>();

            // No BufferSize Check for primitive type writes, same as the client.

            if (_writePosition + count > BufferSize)
                Flush();

            Span<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
            bytes.Reverse();    // LE -> BE
            bytes.CopyTo(_writeBuffer.AsSpan(_writePosition, count));
            _writePosition += count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(byte value)
        {
            if (_writePosition + 1 > BufferSize)
                Flush();

            _writeBuffer[_writePosition++] = value;
        }

        public void Flush(bool arg = true)
        {
            if (_writePosition == 0)
                return;

            int totalBytesSent = 0;
            int errorCode;

            try
            {
                while (totalBytesSent < _writePosition)
                {
                    int bytesSent = _socket.Send(_writeBuffer, totalBytesSent, _writePosition - totalBytesSent, SocketFlags.None);
                    totalBytesSent += bytesSent;
                }

                _writePosition = 0;
                return;
            }
            catch (SocketException e)
            {
                errorCode = e.ErrorCode;
            }

            // Error handling
            if (errorCode != (int)SocketError.WouldBlock)
            {
                _writePosition = 0;
                _isActive = false;
                return;
            }

            // Partial flush
            if (totalBytesSent > 0)
            {
                Buffer.BlockCopy(_writeBuffer, totalBytesSent, _writeBuffer, 0, _writePosition - totalBytesSent);
                _writePosition -= totalBytesSent;
            }

            if (arg == false)
            {
                // TODO: more stuff here?
                return;
            }

            if (_throwIfWouldBlock)
                throw new Exception("Network::OperationWouldBlock");

            // Not sure if this part is correct (or even reachable)
            try
            {
                Socket[] sockets = [_socket];
                Socket.Select(null, sockets, null, 0);
            }
            catch (SocketException e)
            {
                if (e.ErrorCode != (int)SocketError.Interrupted)
                {
                    Logger.Error($"Select returned a fatal error. Error Code: {e.ErrorCode}");
                    Disconnect();
                }
            }
        }
    }
}
