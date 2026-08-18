namespace KiwiNet.LoginServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "KiwiNet.LoginServer";
            LoginServerApp.Instance.Run();
        }
    }
}
