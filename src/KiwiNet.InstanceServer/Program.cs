namespace KiwiNet.InstanceServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "KiwiNet.InstanceServer";
            InstanceServerApp.Instance.Run();
        }
    }
}
