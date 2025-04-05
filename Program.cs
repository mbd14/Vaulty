namespace Vaulty
{
    internal class Program
    {
        // Instance of the Vaulty App
        public App.Vaulty vaulty;

        // Main method for the program
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        // Main method for async execution of the bot   
        static async Task MainAsync()
        {
            // Start the App and wait forever in the main thread
            App.Vaulty vaulty = new App.Vaulty();
            vaulty.Start();
            await Task.Delay(-1);
        }
    }
}
