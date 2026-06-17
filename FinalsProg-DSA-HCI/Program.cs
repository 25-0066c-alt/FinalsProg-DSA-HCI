using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace FinalsProg_DSA_HCI
{
    internal class Program
    {
        static string DatabaseFilePath = "database.txt"; //for login
        static Dictionary<string, string[]> userDatabase = new Dictionary<string, string[]>();
        static string currentLoggedInUser = ""; //for current user thats logged in
        static string RequestsFilePath = "requests.txt"; //storing requests

        static void Main(string[] args)
        {
            List<string> RequestListmaker = new List<string>();
            if (File.Exists(RequestsFilePath))
            {
                string[] savedTickets = File.ReadAllText(RequestsFilePath)
                                            .Split(new string[] { "=========================\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string ticket in savedTickets)
                {
                    RequestListmaker.Add(ticket.Trim() + "\n");
                }
            }

            while (true)
            {
                if (File.Exists(DatabaseFilePath))
                {
                    foreach (string line in File.ReadAllLines(DatabaseFilePath))
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 2)
                        {
                            string username = parts[0].Trim();
                            string password = parts[1].Trim();
                            string points = parts.Length >= 3 ? parts[2].Trim() : "0";

                            userDatabase[username] = new string[] { password, points };
                        }
                    }
                }
                while (true)
                {
                    bool MainmenuSession = true;
                    while (MainmenuSession)
                    {
                        MainmenuSession = MainMenu();
                    }
                    Console.Clear();

                    bool userIsLoggedIn = true;
                    while (userIsLoggedIn)
                    {

                        int userChoice = DisplayMenu();
                        switch (userChoice)
                        {
                            case 1:
                                ExecuteDonation(RequestListmaker);
                                break;
                            case 2:
                                ExecuteViewStatus();
                                break;
                            case 3:
                                ExecuteViewRequests(RequestListmaker);
                                break;
                            case 4:
                                ExecuteMakeRequest(RequestListmaker);
                                break;
                            case 5:
                                Console.WriteLine("\nLogging out... Returning to Authentication screen.");
                                Console.ReadKey();
                                userIsLoggedIn = false; // Breaks this loop, recycling back up to the login page
                                break;
                            default:
                                // Handles invalid entries or 0 returns smoothly by re-looping the menu
                                break;
                        }
                    }
                }
            }
        }

        static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  Welcome to the Donor's Drive");
            Console.WriteLine("=================================");

            Console.Write("Do you have an account? (Y/N): ");
            try
            {
                char hasAccount = char.ToUpper(Convert.ToChar(Console.ReadLine()));

                if (hasAccount == 'Y')
                {
                    if (LoginProcess())
                    {
                        return false;
                    }
                }
                else if (hasAccount == 'N')
                {
                    SignUpProcess();
                    return true; 
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
                Console.WriteLine("Would you like to sign up? (Y/N)");
                try
                {
                    char signupchoice = char.ToUpper(Convert.ToChar(Console.ReadLine()));

                    if (signupchoice == 'Y')
                    {
                        Console.Clear();
                        break;
                    }
                    else if (signupchoice == 'N')
                    {
                        Environment.Exit(0);
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
            }
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

                userDatabase.Add(user, new string[] { pass, "0" });
                File.AppendAllText(DatabaseFilePath, $"{user},{pass},0\n");

                Console.WriteLine("\nRegistration Successful! Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                return;
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

                if (userDatabase.ContainsKey(user) && userDatabase[user][0] == pass)
                {
                    currentLoggedInUser = user;
                    Console.WriteLine("\nLogin Successful! Redirecting to main dashboard...");
                    Console.ReadKey();
                    return true;
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
        static int DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Donor's Drive");
            Console.WriteLine("1. Donate");
            Console.WriteLine("2. View donor status and ranking");
            Console.WriteLine("3. View requests");
            Console.WriteLine("4. Make a request");
            Console.WriteLine("5. Log out");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice >= 1 && choice <= 5)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please type 1 to 5.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    return 0;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nInvalid input. Please type a valid number.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                return 0;
            }
        }
        static void ExecuteDonation(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("       FULFILL A REQUEST         ");
            Console.WriteLine("=================================");

            if (RequestListmaker.Count == 0)
            {
                Console.WriteLine("\nThere are no active help requests to fulfill right now.");
                Console.WriteLine("\nPress any key to return to dashboard...");
                Console.ReadKey();
                return;
            }

            // Display active requests formatted
            for (int i = 0; i < RequestListmaker.Count; i++)
            {
                Console.WriteLine($"--- Request #{i + 1} ---");
                Console.WriteLine(RequestListmaker[i]);
            }

            Console.Write("Enter the number of the request you want to fulfill (or 0 to cancel): ");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 0) return;

                int targetIndex = choice - 1;
                if (targetIndex >= 0 && targetIndex < RequestListmaker.Count)
                {
                    // 1. Remove the fulfilled item from memory
                    RequestListmaker.RemoveAt(targetIndex);

                    // 2. Rewrite the remaining items to requests.txt
                    File.WriteAllText(RequestsFilePath, "");
                    foreach (string ticket in RequestListmaker)
                    {
                        File.AppendAllText(RequestsFilePath, ticket + "=========================\n");
                    }

                    // 3. Update local dictionary points (index [1])
                    int currentPoints = Convert.ToInt32(userDatabase[currentLoggedInUser][1]);
                    currentPoints += 10;
                    userDatabase[currentLoggedInUser][1] = currentPoints.ToString();

                    // 4. Update the database file
                    File.WriteAllText(DatabaseFilePath, "");
                    foreach (var entry in userDatabase)
                    {
                        File.AppendAllText(DatabaseFilePath, $"{entry.Key},{entry.Value[0]},{entry.Value[1]}\n");
                    }

                    Console.WriteLine("\nThank you for donating! You completed the request.");
                    Console.WriteLine("✨ You gained +10 points! Check rankings to see your spot.");
                }
                else
                {
                    Console.WriteLine("\nInvalid selection number.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nInvalid input. Expected a valid number entry.");
            }

            Console.WriteLine("\nPress any key to return to dashboard...");
            Console.ReadKey();
        }

        static void ExecuteViewStatus()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("    DONOR STATUS & RANKINGS      ");
            Console.WriteLine("=================================");

            string currentPoints = userDatabase[currentLoggedInUser][1];
            Console.WriteLine($"Logged User: {currentLoggedInUser}");
            Console.WriteLine($"Your Score : {currentPoints} Points\n");

            Console.WriteLine("--- LEADERBOARD RANKINGS ---");

            //transfer for leaderboard
            var leaderboard = userDatabase.ToList();

            // Bubble sort for ranking
            for (int i = 0; i < leaderboard.Count - 1; i++)
            {
                for (int j = 0; j < leaderboard.Count - 1 - i; j++)
                {
                    int pointsCurrent = int.Parse(leaderboard[j].Value[1]);
                    int pointsNext = int.Parse(leaderboard[j + 1].Value[1]);

                    if (pointsNext > pointsCurrent)
                    {
                        var temp = leaderboard[j];
                        leaderboard[j] = leaderboard[j + 1];
                        leaderboard[j + 1] = temp;
                    }
                }
            }

            int rank = 1;
            foreach (var player in leaderboard)
            {
                Console.WriteLine($"{rank}. {player.Key,-12} : {player.Value[1]} pts");
                rank++;
            }
            Console.WriteLine("=================================");

            Console.WriteLine("\nPress any key to return to dashboard...");
            Console.ReadKey();
        }

        static void ExecuteViewRequests(List<string> Requestlistmaker)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("       ACTIVE REQUESTS           ");
            Console.WriteLine("=================================");

            if (Requestlistmaker.Count == 0)
            {
                Console.WriteLine("\nNo active requests found at this time.");
            }
            else
            {
                Console.WriteLine($"Found ({Requestlistmaker.Count}) Active Request(s):\n");
                for (int i = 0; i < Requestlistmaker.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Requestlistmaker[i]}");
                }
            }
            Console.ReadKey();
        }

        static void ExecuteMakeRequest(List<string> Requestlistmaker)
        {
            string title = "";
            string description = "";
            List<string> itemsList = new List<string>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("     CREATE A NEW REQUEST        ");
                Console.WriteLine("=================================");
                Console.WriteLine($"1. Title: {title}");
                Console.WriteLine($"2. Description: {description}");

                Console.Write("3. Items Needed: ");
                if (itemsList.Count == 0)
                {
                    Console.WriteLine("[None Added]");
                }
                else
                {
                    Console.WriteLine();
                    for (int i = 0; i < itemsList.Count; i++)
                    {
                        Console.WriteLine($"   {i + 1}. {itemsList[i]}");
                    }
                }
                Console.WriteLine("---------------------------------");
                Console.WriteLine("4. Submit and Save Request");
                Console.WriteLine("5. Cancel and Exit");
                Console.WriteLine("---------------------------------");
                Console.Write("Select an option (1-5): ");

                string choice = Console.ReadLine()?.Trim();

                if (choice == "1")
                {
                    Console.Write("\nEnter Title: ");
                    string input = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(input)) title = input;
                }
                else if (choice == "2")
                {
                    Console.Write("\nEnter Description: ");
                    string input = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(input)) description = input;
                }
                else if (choice == "3")
                {
                    itemsList.Clear();
                    Console.WriteLine("\nEnter items sequentially. Press [Enter] after each item.");
                    Console.WriteLine("Type 'X' and press [Enter] when finished.\n");

                    int itemCounter = 1;
                    while (true)
                    {
                        Console.Write($"{itemCounter}. ");
                        string itemInput = Console.ReadLine()?.Trim();

                        if (string.Equals(itemInput, "X", StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }

                        if (!string.IsNullOrEmpty(itemInput))
                        {
                            itemsList.Add(itemInput);
                            itemCounter++;
                        }
                    }
                }
                else if (choice == "4")
                {
                    string finalizedTicket = "Posted by: " + currentLoggedInUser + "\n" +
                                             "Title: " + title + "\n" +
                                             "Description: " + description + "\n" +
                                             "Items Needed:\n";

                    if (itemsList.Count == 0)
                    {
                        finalizedTicket += "   - None\n";
                    }
                    else
                    {
                        for (int i = 0; i < itemsList.Count; i++)
                        {
                            finalizedTicket += "   " + (i + 1) + ". " + itemsList[i] + "\n";
                        }
                    }

                    Requestlistmaker.Add(finalizedTicket);

                    string filePayload = finalizedTicket + "=========================\n";
                    File.AppendAllText(RequestsFilePath, filePayload);

                    Console.WriteLine("\nYour request has been saved successfully!");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;
                }
                else if (choice == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid option. Press any key to try again...");
                    Console.ReadKey();
                }
            }

        }
    }
}
