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
        static string AccomplishedFilePath = "accomplished.txt"; //for history
        static string ReadNotificationsFilePath = "readnotifications.txt"; //forlogin notifications
        static string pad = new string('\t', 11);
        static string smallpad = new string('\t', 10);
        static string Apad = new string('\t', 8);
        static string Bpad = new string('\t', 7);
        static string Cpad = new string('\t', 6);
        static string Dpad = new string('\t', 5);
        static string Epad = new string('\t', 4);

        static void title()
        {
            string asciiArt = $@"
{Dpad} _    _      _                            _                 
{Dpad}| |  | |    | |                          | |                
{Dpad}| |  | | ___| | ___ ___  _ __ ___   ___  | |_ ___           
{Dpad}| |/\| |/ _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \          
{Dpad}\  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) |         
{Dpad} \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/                 
{Dpad}______                       _      ______      _           
{Dpad}|  _  \                     ( )     |  _  \    (_)          
{Dpad}| | | |___  _ __   ___  _ __|/ ___  | | | |_ __ ___   _____ 
{Dpad}| | | / _ \| '_ \ / _ \| '__| / __| | | | | '__| \ \ / / _ \
{Dpad}| |/ / (_) | | | | (_) | |    \__ \ | |/ /| |  | |\ V /  __/
{Dpad}|___/ \___/|_| |_|\___/|_|    |___/ |___/ |_|  |_| \_/ \___| 
{Dpad}════════════════════════════════════════════════════════════";
            Console.WriteLine(asciiArt);

        }
        static void logindisplay()
        {
            string art = $@"                                                                                                                                                                                                                                                                                                                   
{Bpad}   _                 _       
{Bpad}  | |               (_)      
{Bpad}  | |     ___   __ _ _ _ __  
{Bpad}  | |    / _ \ / _` | | '_ \ 
{Bpad}  | |___| (_) | (_| | | | | |
{Bpad}  \_____/\___/ \__, |_|_| |_|
{Bpad}                __/ |        
{Bpad}               |___/     

{Bpad}  ═══════════════════════════";
            Console.WriteLine(art);
        }
        static void Accountcreationdisplay()
        {
            string art = $@"
{Dpad}  ___                            _      
{Dpad} / _ \                          | |     
{Dpad}/ /_\ \ ___ ___ ___  _   _ _ __ | |_    
{Dpad}|  _  |/ __/ __/ _ \| | | | '_ \| __|   
{Dpad}| | | | (_| (_| (_) | |_| | | | | |_    
{Dpad}\_| |_/\___\___\___/ \__,_|_| |_|\__|                                     
{Dpad} _____                _   _             
{Dpad}/  __ \              | | (_)            
{Dpad}| /  \/_ __ ___  __ _| |_ _  ___  _ __  
{Dpad}| |   | '__/ _ \/ _` | __| |/ _ \| '_ \ 
{Dpad}| \__/\ | |  __/ (_| | |_| | (_) | | | |
{Dpad} \____/_|  \___|\__,_|\__|_|\___/|_| |_|

{Dpad} ═══════════════════════════════════════";
            Console.WriteLine(art);
        }
        static void Notification()
        {
            string notifart = $@"                                                                          
{Bpad}                :--:                  
{Bpad}             .------.                
{Bpad}           .------------.             
{Bpad}          :-------------=:            
{Bpad}         ----------------=-           
{Bpad}         ---------------===.          
{Bpad}        .-------------=====.          
{Bpad}        .==================.          
{Bpad}        .==================.          
{Bpad}        .==================.          
{Bpad}        -==================-          
{Bpad}     .-====================-.        
{Bpad}     -========================-.      
{Bpad}     .--------========--------.       
{Bpad}              =++++++=                
{Bpad}               .=++=.  ";
            Console.WriteLine(notifart);
        }
        static void Main(string[] args)
        {
            List<string> RequestListmaker = new List<string>();
            if (File.Exists(RequestsFilePath))
            {
                string[] savedTickets = File.ReadAllText(RequestsFilePath)
                                            .Split(new string[] { " =========================\n" }, StringSplitOptions.RemoveEmptyEntries);

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
                    CheckLoginNotifications();
                    Console.ReadKey();
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
                                ExecuteViewAccomplishedRequests();
                                break;
                            case 6:
                                ExecuteProfile();
                                break;
                            case 7:
                                DeleteRequests(RequestListmaker);
                                break;
                            case 8:
                                Console.WriteLine("\nLogging out... Returning to Authentication screen.");
                                Console.ReadKey();
                                currentLoggedInUser = ""; // reset tracker for consistency
                                userIsLoggedIn = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        static bool MainMenu()
        {
            Console.Clear();
            title();
            Console.Write($"\n{Dpad}\t ---->  Do you have an account? (Y/N): ");
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

                Console.WriteLine($"\n{Bpad}Invalid input. Please type Y or N.");
            }
            catch (Exception)
            {
                Console.WriteLine($"\n {Bpad}Invalid input. Please type exactly one character.");
            }
            Console.WriteLine($"{Bpad}Press any key to try again...");
            Console.ReadKey();
            Console.Clear();
            return true;
        }
        static void SignUpProcess()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Accountcreationdisplay();
                Console.Write($"\n{Dpad} Would you like to sign up? (Y/N) :");
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

                    Console.WriteLine($"\n{Bpad} Invalid input. Please type Y or N.");
                }
                catch (Exception)
                {
                    Console.WriteLine($"\n{Bpad} Invalid input. Please type exactly one character.");
                }
                Console.WriteLine($"\n{Bpad} Press any key to try again...");
                Console.ReadKey();
            }
            while (true)
            {
                Console.Clear();
                Accountcreationdisplay();
                Console.WriteLine($"\n\n{Cpad}[Creating New Account]");
                Console.Write($"\n\n{Cpad}Enter Username: ");
                string user = Console.ReadLine()?.Trim();

                Console.Write($"\n\n{Cpad}Enter Password: ");
                string pass = Console.ReadLine()?.Trim();

                if (userDatabase.ContainsKey(user) || string.IsNullOrEmpty(user))
                {
                    Console.WriteLine($"\n\n{Dpad} Username invalid or already exists. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                userDatabase.Add(user, new string[] { pass, "0" });
                File.AppendAllText(DatabaseFilePath, $"{user},{pass},0\n");

                Console.WriteLine($"\n\n{Dpad}Registration Successful! Press any key to continue");
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
                logindisplay();
                Console.WriteLine($"\n\n{Apad}[Login Session]");
                Console.Write($"\n\n{Apad}Enter Username: ");
                string user = Console.ReadLine().Trim();

                Console.Write($"\n\n{Apad}Enter Password: ");
                string pass = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    Console.WriteLine($"\n\n{Bpad}Fields cannot be empty. Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                if (userDatabase.ContainsKey(user) && userDatabase[user][0] == pass)
                {
                    currentLoggedInUser = user;
                    Console.WriteLine($"\n\n{Bpad}Login Successful! Redirecting to main dashboard...");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    Console.WriteLine($"\n\n{Bpad}[ Invalid Details ] Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }
        static int DisplayMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n{Cpad}User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");

            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine($"{Dpad}╔══════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}║              MAIN MENU               ║");
            Console.WriteLine($"{Dpad}╠══════════════════════════════════════╣");
            Console.WriteLine($"{Dpad}║ [1] Donate                           ║");
            Console.WriteLine($"{Dpad}║ [2] View Status & Ranking            ║");
            Console.WriteLine($"{Dpad}║ [3] View Requests                    ║");
            Console.WriteLine($"{Dpad}║ [4] Make Request                     ║");
            Console.WriteLine($"{Dpad}║ [5] Completed Requests               ║");
            Console.WriteLine($"{Dpad}║ [6] Profile                          ║");
            Console.WriteLine($"{Dpad}║ [7] Delete Request                   ║");
            Console.WriteLine($"{Dpad}║ [8] Logout                           ║");
            Console.WriteLine($"{Dpad}╚══════════════════════════════════════╝");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice >= 1 && choice <= 9)
                {
                    return choice;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{pad}\nInvalid input. Please type 1 to 6.");
                    Console.ResetColor();
                    Console.WriteLine($"{pad}Press any key to try again...");
                    Console.ReadKey();
                    return 0;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{pad}\nInvalid input. Please type a valid number.");
                Console.ResetColor();
                Console.WriteLine($"{pad}Press any key to try again...");
                Console.ReadKey();
                return 0;
            }
        }
        static void ExecuteDonation(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.WriteLine($" User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
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

            for (int i = 0; i < RequestListmaker.Count; i++)
            {
                string cleanRequest = RequestListmaker[i].Replace("=========================", "").Trim();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine($"REQUEST #{i + 1}");
                Console.WriteLine("╠═══════════════════════════════════════╣");

                Console.WriteLine(cleanRequest);
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.Write("Enter the number of the request you want to fulfill (or 0 to cancel): ");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 0)
                {
                    return;
                }

                int targetIndex = choice - 1;
                if (targetIndex >= 0 && targetIndex < RequestListmaker.Count)
                {
                    string selectedTicket = RequestListmaker[targetIndex];
                    string originalRequester = "Someone";
                    string[] ticketLines = selectedTicket.Split('\n');
                    if (ticketLines.Length > 0 && ticketLines[0].Contains("Posted by:"))
                    {
                        originalRequester = ticketLines[0].Replace("Posted by:", "").Trim();
                    }

                    if (originalRequester == currentLoggedInUser)
                    {
                        Console.WriteLine("\nYou cannot fulfill your own request.");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        return;
                    }

                    //adds it to accomplished
                    Console.Write("Leave a message: ");
                    string message = Console.ReadLine();

                    File.AppendAllText(AccomplishedFilePath,
                        $"{originalRequester}|{currentLoggedInUser}|Donation Pack|Completed|{message}\n");

                    //removes the accomplished item
                    RequestListmaker.RemoveAt(targetIndex);

                    // rewriter
                    File.WriteAllText(RequestsFilePath, "");
                    foreach (string ticket in RequestListmaker)
                    {
                        File.AppendAllText(RequestsFilePath, ticket + "=========================\n");
                    }

                    // add points
                    int currentPoints = Convert.ToInt32(userDatabase[currentLoggedInUser][1]);
                    currentPoints += 10;
                    userDatabase[currentLoggedInUser][1] = currentPoints.ToString();

                    // Update the file
                    File.WriteAllText(DatabaseFilePath, "");
                    foreach (var entry in userDatabase)
                    {
                        File.AppendAllText(DatabaseFilePath, $"{entry.Key},{entry.Value[0]},{entry.Value[1]}\n");
                    }

                    Console.WriteLine("\nThank you for donating! You completed the request.");
                    Console.WriteLine("You gained +10 points! Check rankings to see your spot.\n");
                    if (currentPoints == 10)
                    {
                        Console.WriteLine("Achievement Unlocked: First Donation!");
                    }
                    else if (currentPoints == 50)
                    {
                        Console.WriteLine("Achievement Unlocked: Helping Hand!");
                    }
                    else if (currentPoints == 80)
                    {
                        Console.WriteLine("Achievement Unlocked: Fellow Volunteers!");
                    }
                    else if (currentPoints == 100)
                    {
                        Console.WriteLine("Achievement Unlocked: Community Helper!");
                    }
                    else if (currentPoints == 200)
                    {
                        Console.WriteLine("Achievement Unlocked: Contributor to the Need!");
                    }
                    else if (currentPoints == 300)
                    {
                        Console.WriteLine("Achievement Unlocked: The Benefactor!");
                    }
                    else if (currentPoints == 400)
                    {
                        Console.WriteLine("Achievement Unlocked: A Patron to Society!");
                    }
                    else if (currentPoints == 500)
                    {
                        Console.WriteLine("Achievement Unlocked: The Philanthropist!");
                    }
                    else if (currentPoints == 650)
                    {
                        Console.WriteLine("Achievement Unlocked: The Altruistic Friend!");
                    }
                    else if (currentPoints == 800)
                    {
                        Console.WriteLine("Achievement Unlocked: The Helping Champion!");
                    }
                    else if (currentPoints == 1000)
                    {
                        Console.WriteLine("Achievement Unlocked: Mr Beast!");
                    }
                    else if (currentPoints == 1500)
                    {
                        Console.WriteLine("Achievement Unlocked: Greatest of All Time!");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid selection number.");
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Expected a valid number entry.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to return to dashboard...");
            Console.ReadKey();
        }
        static void ExecuteViewStatus()
        {
            int points = int.Parse(userDatabase[currentLoggedInUser][1]);

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
                int points2 = int.Parse(player.Value[1]);

                Console.WriteLine(
                    $"{rank}. {player.Key,-12} : {points} pts | {Rankings(points)}"
                );

                rank++;
            }
            Console.WriteLine("=================================");

            Console.WriteLine("\nPress any key to return to dashboard...");
            Console.ReadKey();
        }
        static void ExecuteViewRequests(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.WriteLine($" User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
            Console.WriteLine("=================================");
            Console.WriteLine("       ACTIVE REQUESTS           ");
            Console.WriteLine("=================================");

            if (RequestListmaker.Count == 0)
            {
                Console.WriteLine("\nNo active requests found at this time.");
            }
            else
            {
                Console.WriteLine($"Found ({RequestListmaker.Count}) Active Request(s):\n");
                for (int i = 0; i < RequestListmaker.Count; i++)
                {
                    string cleanRequest = RequestListmaker[i].Replace("=========================", "").Trim();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("╔═══════════════════════════════════════╗");
                    Console.WriteLine($"REQUEST #{i + 1}");
                    Console.WriteLine("╠═══════════════════════════════════════╣");

                    Console.WriteLine(cleanRequest);
                    Console.WriteLine("╚═══════════════════════════════════════╝");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            Console.ReadKey();
        }
        static void ExecuteMakeRequest(List<string> RequestListmaker)
        {
            string title = "";
            string description = "";
            List<string> itemsList = new List<string>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($" User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
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

                        if (string.Equals(itemInput.ToUpper(), "X"))
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

                    RequestListmaker.Add(finalizedTicket);

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option. Press any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }

        }
        static void CheckLoginNotifications()
        {
            int unreadCount = 0;

            if (File.Exists(AccomplishedFilePath))
            {
                List<string> readEntries = new List<string>();

                if (File.Exists(ReadNotificationsFilePath))
                {
                    readEntries = File.ReadAllLines(ReadNotificationsFilePath).ToList();
                }

                foreach (string line in File.ReadAllLines(AccomplishedFilePath))
                {
                    string[] parts = line.Split('|');

                    if (parts.Length >= 4 &&
                        parts[0] == currentLoggedInUser &&
                        !readEntries.Contains(line))
                    {
                        unreadCount++;
                    }
                }
            }

            Notification();
            Console.WriteLine();
            if (unreadCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Bpad}     [NOTIFICATION] You have {unreadCount} new completed request(s).");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{Bpad}     [NOTIFICATION] No new updates.");
            }
            Console.ResetColor();
        }
        static void ExecuteViewAccomplishedRequests()
        {
            Console.Clear();
            Console.WriteLine("=== YOUR ACCOMPLISHED REQUESTS ===");

            if (File.Exists(AccomplishedFilePath))
            {
                foreach (string line in File.ReadAllLines(AccomplishedFilePath))
                {
                    string[] parts = line.Split('|');

                    // Check if this belongs to user
                    if (parts.Length >= 4 && parts[0] == currentLoggedInUser)
                    {
                        Console.WriteLine($"Donor : {parts[1]}");
                        Console.WriteLine($"Type  : {parts[2]}");
                        Console.WriteLine($"Status: {parts[3]}");
                        if (parts.Length >= 5)
                        {
                            Console.WriteLine($"Message: {parts[4]}");
                        }
                        Console.WriteLine("--------------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("No requests have been completed yet.");
            }

            if (File.Exists(AccomplishedFilePath))
            {
                foreach (string line in File.ReadAllLines(AccomplishedFilePath))
                {
                    string[] parts = line.Split('|');

                    if (parts.Length >= 4 && parts[0] == currentLoggedInUser)
                    {
                        File.AppendAllText(ReadNotificationsFilePath, line + Environment.NewLine);
                    }
                }
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
        static string Rankings(int points)
        {
            if (points >= 2000)
            {
                return "Global Donor";
            }
            else if (points >= 1000)
            {
                return "Diamond Donor";
            }
            else if (points >= 500)
            {
                return "Emerald Donor";
            }
            else if (points >= 200)
            {
                return "Platinum Donor";
            }
            else if (points >= 100)
            {
                return "Gold Donor";
            }
            else if (points >= 50)
            {
                return "Silver Donor";
            }
            else
            {
                return "Bronze Donor";
            }
        }
        static List<string> GetAchievements(int points)
        {
            List<string> achievements = new List<string>();

            if (points >= 10)
            {
                achievements.Add("First Donation");
            }

            if (points >= 50)
            {
                achievements.Add("Helping Hand");
            }

            if (points >= 80)
            {
                achievements.Add("Fellow Voluntees");
            }

            if (points >= 100)
            {
                achievements.Add("Community Helper");
            }

            if (points >= 200)
            {
                achievements.Add("Contributor to the need");
            }

            if (points >= 300)
            {
                achievements.Add("The Benefactor");
            }

            if (points >= 400)
            {
                achievements.Add("A patron to society");
            }

            if (points >= 500)
            {
                achievements.Add("The Philanthropist");
            }

            if (points >= 650)
            {
                achievements.Add("The Altruistic friend");
            }

            if (points >= 800)
            {
                achievements.Add("The Helping Champion");
            }

            if (points >= 1000)
            {
                achievements.Add("Mr Beast");
            }

            if (points >= 1500)
            {
                achievements.Add("Greatest of All time");
            }

            return achievements;
        }
        static void ExecuteProfile()
        {
            Console.Clear();

            int points = int.Parse(userDatabase[currentLoggedInUser][1]);

            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine($" Username : {currentLoggedInUser}");
            Console.WriteLine($" Points   : {points}");
            Console.WriteLine($" Rank     : {Rankings(points)}");
            Console.WriteLine("└──────────────────────────────┘");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAchievements");
            Console.ResetColor();

            foreach (string achievement in GetAchievements(points))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ {achievement}");
                Console.ResetColor();
            }

            Console.WriteLine("\n=== RANK TIERS ===");
            Console.WriteLine("Bronze Donor   - 0 Points");
            Console.WriteLine("Silver Donor   - 50 Points");
            Console.WriteLine("Gold Donor     - 100 Points");
            Console.WriteLine("Platinum Donor - 200 Points");
            Console.WriteLine("Emerald Donor  - 500 Points");
            Console.WriteLine("Diamond Donor  - 1000 Points");
            Console.WriteLine("Global Donor   - 2000 Points\n\n");

            string[] achievements =
                {
                        "First Donation|10",
                        "Helping Hand|50",
                        "Fellow Volunteers|80",
                        "Community Helper|100",
                        "Contributor to the Need|200",
                        "The Benefactor|300",
                        "A Patron to Society|400",
                        "The Philanthropist|500",
                        "The Altruistic Friend|650",
                        "The Helping Champion|800",
                        "Mr Beast|1000",
                        "Greatest of All Time|1500"
                    };

            foreach (string achievement in achievements)
            {
                string[] parts = achievement.Split('|');
                string name = parts[0];
                int requiredPoints = int.Parse(parts[1]);

                string status = points >= requiredPoints ? "[UNLOCKED]" : "[LOCKED]";

                Console.WriteLine($"{status,-11} {name} ({requiredPoints} pts)");
            }


            Console.ReadKey();
        }
        static void DeleteRequests(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.WriteLine("Choose a request you made that you want to remove:");

            List<int> userRequestIndexes = new List<int>();

            for (int i = 0; i < RequestListmaker.Count; i++)
            {
                string[] ticketLines = RequestListmaker[i].Split('\n');

                if (ticketLines.Length > 0 &&
                    ticketLines[0].Contains("Posted by:") &&
                    ticketLines[0].Replace("Posted by:", "").Trim() == currentLoggedInUser)
                {
                    userRequestIndexes.Add(i);

                    Console.WriteLine($"\nRequest #{userRequestIndexes.Count}");
                    Console.WriteLine(RequestListmaker[i]);
                }
            }

            if (userRequestIndexes.Count == 0)
            {
                Console.WriteLine("\nYou have no requests to delete.");
                Console.ReadKey();
                return;
            }

            Console.Write("\nEnter request number to delete (0 to cancel): ");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 0)
                {
                    return;
                }

                if (choice < 1 || choice > userRequestIndexes.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid selection.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                int targetIndex = userRequestIndexes[choice - 1];

                // Remove request
                RequestListmaker.RemoveAt(targetIndex);

                // Rewrite requests file
                File.WriteAllText(RequestsFilePath, "");

                foreach (string ticket in RequestListmaker)
                {
                    File.AppendAllText(RequestsFilePath,
                        ticket + "=========================\n");
                }

                Console.WriteLine("\nRequest deleted successfully.");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Expected a valid number entry.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
