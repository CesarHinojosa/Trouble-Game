using Trouble.ConsoleApp;

internal class Program
{
    private static string DrawMenu()
    {
        Console.WriteLine("Which operation do you wish to preform?");
        Console.WriteLine("Connect to a channel (c)");
        Console.WriteLine("Send a message to the channel (s)");
        Console.WriteLine("Exit (x)");

        string operation = Console.ReadLine();
        return operation;
    }

    private static void Main(string[] args)
    {
        string user = "Luke";
        string hubAddress = "https://localhost:7081/TroubleHub";
        //string hubAddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";
    string operation = DrawMenu();

        var signalRConnection = new SignalRConnection(hubAddress);

        while (operation != "x")
        {
            switch (operation)
            {
                case "x":
                    break;

                case "c":
                    signalRConnection.ConnectToChannel(user);
                    break;

                case "s":
                    break;
            }
            operation = DrawMenu();
        }
    }
}
