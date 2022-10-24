using MoneyWife.Models;

namespace MoneyWife
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Main());
            User? user = new User();
            MoneyWifeContext context = new MoneyWifeContext();
            user = context.Users.FirstOrDefault(u => u.Username == "oaiba" && u.Password == "123");
            if (user != null)
            {
                Application.Run(new Main(user));
            }
            else
            {
                Application.Run(new Login());
            }
        }
    }
}