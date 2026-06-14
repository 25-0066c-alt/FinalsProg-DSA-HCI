using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalsProg_DSA_HCI
{
    internal class Program
    {
        static string DatabaseFilePath = "database.txt";
        static Dictionary<string, string> userDatabase = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            bool MainmenuSession = true;
            if (File.Exists(DatabaseFilePath))
            {
                foreach (string line in File.ReadAllLines(DatabaseFilePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2) userDatabase[parts[0].Trim()] = parts[1].Trim();
                }
            }
            while (MainmenuSession)
            {
                MainmenuSession = MainMenu();
            }
            Console.Clear();
            Console.Write("you should see this");
        }


        static bool MainMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("  Welcome to the Donor's Drive");
            Console.WriteLine("=================================");

            Console.Write("Do you have an account? (Y/N): ");
            try
            {
                char hasAccount = char.ToUpper(Convert.ToChar(Console.ReadLine()));

                if (hasAccount == 'Y')
                {
                    LoginProcess(); 
                    return false;
                }
                else if (hasAccount == 'N')
                {
                    SignUpProcess();
                    return false;
                }

                Console.WriteLine("\nInvalid input. Please type Y or N.");
            }
            catch (Exception)
            {
                Console.WriteLine("\nInvalid input. Please type exactly one character.");
            }
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey();
            Console.Clear();
            return true;
        }
        static void SignUpProcess()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\n[Creating New Account]");
                Console.Write("Enter Username: "); 
                string user = Console.ReadLine()?.Trim();

                Console.Write("Enter Password: "); 
                string pass = Console.ReadLine()?.Trim();

                if (userDatabase.ContainsKey(user) || string.IsNullOrEmpty(user))
                {
                    Console.WriteLine("Username invalid or already exists. Try again.");
                    continue;
                }

                userDatabase.Add(user, pass);
                File.AppendAllText(DatabaseFilePath, $"{user},{pass}\n");

                Console.WriteLine("\nRegistration Successful! Press any key to continue");
                Console.ReadKey();
                break; 
            }
        }
        static bool LoginProcess()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\n[Login Session]");
                Console.Write("Enter Username: "); 
                string user = Console.ReadLine().Trim();

                Console.Write("Enter Password: "); 
                string pass = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    Console.WriteLine("\nFields cannot be empty. Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                if (userDatabase.ContainsKey(user) && userDatabase[user] == pass)
                {
                    Console.WriteLine("\nLogin Successful! Redirecting to process 1.");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("\n[ Invalid Details ] Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }

    }
}
