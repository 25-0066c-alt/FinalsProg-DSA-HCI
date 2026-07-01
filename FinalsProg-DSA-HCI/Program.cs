using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                RequestListmaker.Clear();

                string[] savedTickets = File.ReadAllText(RequestsFilePath)
                    .Split(new string[] { "=========================" },
                           StringSplitOptions.RemoveEmptyEntries);

                foreach (string ticket in savedTickets)
                {
                    string cleanTicket = ticket.Trim();

                    if (!string.IsNullOrWhiteSpace(cleanTicket))
                    {
                        RequestListmaker.Add(cleanTicket);
                    }
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
                                Thread.Sleep(2000);
                                currentLoggedInUser = ""; // reset tracker
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
            Console.Write($"\n{Dpad}Welcome to Donor's Drive! where you can:" +
                          $"\n{Dpad}Give: Donate to those in need." +
                          $"\n{Dpad}Receive: Request help from our community." +
                          $"\n{Dpad}Earn: Get points for every good deed." +
                          $"\n{Dpad}Compete: Top the worldwide leaderboards!" +
                          $"\n{Dpad}Contribute: To No Poverty (SDG 1)," +
                          $"\n{Dpad}Good Health (SDG 3), Reduced Inequalities(SDG 10)");
            Console.Write($"\n\n{Dpad} ---->  Do you have an account? (Y/N): ");
            try
            {
                char hasAccount = char.ToUpper(Convert.ToChar(Console.ReadLine()));

                if (hasAccount == 'Y')
                {
                    return !LoginProcess();
                }
                else if (hasAccount == 'N')
                {
                    if (SignUpProcess())
                    {
                        return false; 
                    }

                    return true; // User cancelled signup
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
        static bool SignUpProcess()
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
                Console.WriteLine($"\n{Dpad}Welcome to Donor's Drive!" +
                                  $"\r\n{Dpad}Let’s get you set up. Creating an account allows " +
                                  $"\r\n{Dpad}you to post requests, fulfill donations, and earn " +
                                  $"\r\n{Dpad}points on our community leaderboard. ");

                Console.WriteLine($"\n\n{Dpad}[Creating New Account]");
                Console.WriteLine($"\r\n{Dpad}Caution: Username and Password are Case Sensitive and don't use of commas\n{Dpad}Press 'x' to return");
                Console.Write($"\n{Dpad}Enter Username: ");
                string user = Console.ReadLine()?.Trim() ?? "";

                if (user.ToUpper() == "X")
                {
                    Console.WriteLine($"\n{Dpad}Returning to main menu...");
                    Thread.Sleep(2000);
                    return false; // Return to the main menu
                }

                Console.Write($"\n\n{Dpad}Enter Password: ");
                string pass = ReadPassword();
                if (pass.ToUpper() == "X")
                {
                    Console.WriteLine($"\n{Dpad}Cancelling account creation. Returning to main menu...");
                    Thread.Sleep(2000);
                    return false; // Return to the main menu
                }

                if (userDatabase.ContainsKey(user) || string.IsNullOrEmpty(user))
                {
                    Console.WriteLine($"\n\n{Dpad} Username invalid or already exists. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
                if (string.IsNullOrEmpty(pass))
                {
                    Console.WriteLine($"\n\n{Dpad} Password invalid. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
                userDatabase.Add(user, new string[] { pass, "0" });
                File.AppendAllText(DatabaseFilePath, $"{user},{pass},0\n");

                currentLoggedInUser = user;

                Console.WriteLine($"\n\n{Dpad}Registration Successful!");
                Console.WriteLine($"{Dpad}Logging you in...");
                Thread.Sleep(2000);

                return true;
            }
        }
        static bool LoginProcess()
        {
            Console.Clear();
            while (true)
            {
                logindisplay();
                Console.WriteLine($"\n{Bpad}Ready to make a difference today?" +
                                  $"\r\n{Bpad}Log in to view active requests and connect" +
                                  $"\r\n{Bpad}with your community.");
                                  
                Console.WriteLine($"\n\n{Bpad}[Login Session]");
                Console.WriteLine($"\r\n{Bpad}Caution: Username and Password are Case Sensitive\n{Bpad}Press 'x' to return");
                Console.Write($"\n{Bpad}Enter Username: ");
                string user = Console.ReadLine()?.Trim() ?? "";

                if (user.ToUpper() == "X")
                {
                    Console.WriteLine($"\n{Bpad}Returning to main menu...");
                    Thread.Sleep(2000);
                    return false; // Return to the main menu
                }

                Console.Write($"\n\n{Bpad}Enter Password: ");
                string pass = ReadPassword();

                if (pass.ToUpper() == "X")
                {
                    Console.WriteLine($"\n{Bpad}Returning to main menu...");
                    Thread.Sleep(2000);
                    return false;
                }

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

            Console.WriteLine($"\n{Dpad}Input from 1-8 to choose your destination");

            Console.WriteLine($"{Dpad}╔══════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}║              MAIN MENU               ║");
            Console.WriteLine($"{Dpad}╠══════════════════════════════════════╣");
            Console.WriteLine($"{Dpad}║ [1] Donate                           ║");
            Console.WriteLine($"{Dpad}║ [2] View Status & Ranking            ║");
            Console.WriteLine($"{Dpad}║ [3] View Requests                    ║");
            Console.WriteLine($"{Dpad}║ [4] Make Request                     ║");
            Console.WriteLine($"{Dpad}║ [5] Completed by History             ║");
            Console.WriteLine($"{Dpad}║ [6] Profile                          ║");
            Console.WriteLine($"{Dpad}║ [7] Delete Request                   ║");
            Console.WriteLine($"{Dpad}║ [8] Logout                           ║");
            Console.WriteLine($"{Dpad}╚══════════════════════════════════════╝");
            Console.Write($"\n{Dpad}Your input:");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice >= 1 && choice <= 8)
                {
                    return choice;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{Dpad}Invalid input. Please type 1 to 8.");
                    Console.ResetColor();
                    Console.WriteLine($"{Dpad}Press any key to try again...");
                    Console.ReadKey();
                    return 0;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{Dpad}Invalid input. Please type a valid number.");
                Console.ResetColor();
                Console.WriteLine($"{Dpad}Press any key to try again...");
                Console.ReadKey();
                return 0;
            }
        }
        static void ExecuteDonation(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Dpad}   User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
            Console.ResetColor();
            Console.WriteLine($"{Dpad}   =================================");
            Console.WriteLine($"{Dpad}         FULFILL A REQUEST         ");
            Console.WriteLine($"{Dpad}   =================================");

            if (RequestListmaker.Count == 0)
            {
                Console.WriteLine($"\n{Dpad}There are no active help requests to fulfill right now.");
                Console.WriteLine($"\n{Dpad}Press any key to return to dashboard...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < RequestListmaker.Count; i++)
            {
                string cleanRequest = RequestListmaker[i].Replace("=========================", "").Trim();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{Dpad}╔═══════════════════════════════════════╗");
                Console.WriteLine($"{Dpad}  REQUEST #{i + 1,-33}");
                Console.WriteLine($"{Dpad}╠═══════════════════════════════════════╣");
                PrintRequest(cleanRequest);
                Console.WriteLine($"{Dpad}╚═══════════════════════════════════════╝");
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.Write($"{Dpad}Enter the number of the request you want to fulfill (or 0 to cancel): ");
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

                    foreach (string line in ticketLines)
                    {
                        if (line.StartsWith("Posted by:"))
                        {
                            originalRequester = line.Replace("Posted by:", "").Trim();
                            break;
                        }
                    }

                    if (originalRequester == currentLoggedInUser)
                    {
                        Console.WriteLine($"\n{Dpad}You cannot fulfill your own request.");
                        Console.WriteLine($"{Dpad}Press any key to return...");
                        Console.ReadKey();
                        return;
                    }

                    // Confirmation before fulfilling
                    Console.Write($"\n{Dpad}Are you sure you want to fulfill this request? (Y/N): ");
                    string confirm = Console.ReadLine()?.Trim().ToUpper();

                    if (confirm != "Y")
                    {
                        Console.WriteLine($"{Dpad}Fulfillment cancelled.");
                        Console.WriteLine($"{Dpad}Press any key to return...");
                        Console.ReadKey();
                        return;
                    }

                    // Leave a message
                    Console.Write($"{Dpad}Leave a message (optional): ");
                    string message = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        message = "No message.";
                    }

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

                    Console.WriteLine($"\n{Dpad}Thank you for donating! You completed the request.");
                    Console.WriteLine($"{Dpad}You gained +10 points! you now have {currentPoints}. Check rankings to see your spot.\n");
                    if (currentPoints == 10)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: First Donation!");
                    }
                    else if (currentPoints == 50)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: Helping Hand!");
                    }
                    else if (currentPoints == 80)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: Fellow Volunteers!");
                    }
                    else if (currentPoints == 100)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: Community Helper!");
                    }
                    else if (currentPoints == 200)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: Contributor to the Need!");
                    }
                    else if (currentPoints == 300)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: The Benefactor!");
                    }
                    else if (currentPoints == 400)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: A Patron to Society!");
                    }
                    else if (currentPoints == 500)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: The Philanthropist!");
                    }
                    else if (currentPoints == 650)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: The Altruistic Friend!");
                    }
                    else if (currentPoints == 800)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: The Helping Champion!");
                    }
                    else if (currentPoints == 1000)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: Mr Beast!");
                    }
                    else if (currentPoints == 1500)
                    {
                        Console.WriteLine($"{Dpad}Achievement Unlocked: Greatest of All Time!");
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
                Console.WriteLine($"\n{Dpad}Invalid input. Expected a valid number entry.");
                Console.ResetColor();
            }

            Console.WriteLine($"\n{Dpad}Press any key to return to dashboard...");
            Console.ReadKey();
        }
        static void ExecuteViewStatus()
        {
            int points = int.Parse(userDatabase[currentLoggedInUser][1]);

            Console.Clear();
            Console.WriteLine($"{Dpad}   =================================");
            Console.WriteLine($"{Dpad}      DONOR STATUS & RANKINGS      ");
            Console.WriteLine($"{Dpad}   =================================");

            string currentPoints = userDatabase[currentLoggedInUser][1];
            Console.WriteLine($"{Dpad}╔══════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}  Logged User: {currentLoggedInUser}");
            Console.WriteLine($"{Dpad}  Your Score : {currentPoints} Points");
            Console.WriteLine($"{Dpad}╚════════ LEADERBOARD RANKINGS ════════╝");

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

                Console.WriteLine($"{Dpad}{rank}. {player.Key,-12} : {points2} pts | {Rankings(points2)}");

                rank++;
            }
            Console.WriteLine($"{Dpad}════════════════════════════════════════");

            Console.WriteLine($"\n{Dpad}Press any key to return to dashboard...");
            Console.ReadKey();
        }
        static void ExecuteViewRequests(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Dpad} User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
            Console.ResetColor();
            Console.WriteLine($"{Dpad}   =================================");
            Console.WriteLine($"{Dpad}          ACTIVE REQUESTS           ");
            Console.WriteLine($"{Dpad}   =================================");

            if (RequestListmaker.Count == 0)
            {
                Console.WriteLine($"\n{Dpad}No active requests found at this time.");
            }
            else
            {
                for (int i = 0; i < RequestListmaker.Count; i++)
                {
                    string cleanRequest = RequestListmaker[i].Replace("=========================", "").Trim();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{Dpad}╔═══════════════════════════════════════╗");
                    Console.WriteLine($"{Dpad}REQUEST #{i + 1}");
                    Console.WriteLine($"{Dpad}╠═══════════════════════════════════════╣");
                    PrintRequest(cleanRequest);
                    Console.WriteLine($"{Dpad}╚═══════════════════════════════════════╝");
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
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{Dpad} User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
                Console.ResetColor();
                Console.WriteLine($"{Dpad}   =================================");
                Console.WriteLine($"{Dpad}        CREATE A NEW REQUEST        ");
                Console.WriteLine($"{Dpad}   =================================");

                Console.WriteLine($"{Dpad}╔══════════════════════════════════════╗");
                PrintWrapped($"1. Title: ", title, 30);
                PrintWrapped($"2. Description: ", description, 30);
                Console.Write($"{Dpad}3. Items Needed: ");
                if (itemsList.Count == 0)
                {
                    Console.WriteLine("[None Added]");
                }
                else
                {
                    Console.WriteLine();
                    for (int i = 0; i < itemsList.Count; i++)
                    {
                        PrintWrapped($"{i + 1}. ", itemsList[i], 30);
                    }
                }
                Console.WriteLine($"{Dpad}╠══════════════════════════════════════╣");
                Console.WriteLine($"{Dpad}║ [4] Submit Request                   ║");
                Console.WriteLine($"{Dpad}║ [5] Cancel                           ║");
                Console.WriteLine($"{Dpad}╚══════════════════════════════════════╝");
                Console.Write($"{Dpad}Select an option (1-5): ");

                string choice = Console.ReadLine()?.Trim();

                if (choice == "1")
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"{Dpad}   =================================");
                        Console.WriteLine($"{Dpad}            CREATE A TITLE          ");
                        Console.WriteLine($"{Dpad}   =================================");
                        Console.Write($"\n{Epad}Enter Title (Max 50 characters): ");
                        string input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.WriteLine($"{Epad}Title cannot be empty.");
                            Console.ReadKey();
                            continue;
                        }

                        if (input.Length > 50)
                        {
                            Console.WriteLine($"{Epad}Title is too long! ({input.Length}/50)");
                            Console.ReadKey();
                            continue;
                        }

                        title = input;
                        break;
                    }
                }
                else if (choice == "2")
                {
                    while (true)
                    {
                        Console.Clear();

                        Console.WriteLine($"{Dpad}   =================================");
                        Console.WriteLine($"{Dpad}         CREATE A DESCRIPTION       ");
                        Console.WriteLine($"{Dpad}   =================================");

                        Console.Write($"\n{Epad}Enter Description (Max 300 characters): ");
                        string input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.WriteLine($"{Epad}Description cannot be empty.");
                            Console.WriteLine($"{Epad}Press any key to try again...");
                            Console.ReadKey();
                            continue;
                        }
                        if (input.Length > 300)
                        {
                            Console.WriteLine($"{Epad}Description is too long! ({input.Length}/300 characters)");
                            Console.WriteLine($"{Epad}Press any key to try again...");
                            Console.ReadKey();
                            continue;
                        }
                        description = input;
                        break;
                    }
                }
                else if (choice == "3")
                {
                    itemsList.Clear();
                    Console.Clear();
                    Console.WriteLine($"{Dpad}   =================================");
                    Console.WriteLine($"{Dpad}             CREATE A LIST          ");
                    Console.WriteLine($"{Dpad}   =================================");
                    Console.WriteLine($"\n{Epad}Enter items sequentially. Press [Enter] after each item.");
                    Console.WriteLine($"{Epad}Type 'X' and press [Enter] when finished.\n");

                    int itemCounter = 1;
                    while (true)
                    {
                        Console.Write($"{Epad}{itemCounter}. ");
                        string itemInput = Console.ReadLine()?.Trim();

                        if (string.Equals(itemInput, "X", StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(itemInput))
                        {
                            Console.WriteLine($"{Epad}Item cannot be empty.");
                            Console.WriteLine($"{Epad}Press any key to try again...");
                            Console.ReadKey();
                            continue;
                        }
                        if (itemInput.Length > 50)
                        {
                            Console.WriteLine($"{Epad}Item is too long! ({itemInput.Length}/50 characters)");
                            Console.WriteLine($"{Epad}Press any key to try again...");
                            Console.ReadKey();
                            continue;
                        }
                        if (itemsList.Count > 20)
                        {
                            Console.WriteLine($"{Epad}Item list is too long! ({itemInput.Length}/20 items)");
                            Console.WriteLine($"{Epad}Press any key to try again...");
                            Console.ReadKey();
                            continue;
                        }
                        else
                        {
                            itemsList.Add(itemInput);
                            itemCounter++;
                            continue;
                        }
                    }
                }
                else if (choice == "4")
                {
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n{Dpad}Title cannot be empty!");
                        Console.ResetColor();
                        Console.WriteLine($"{Dpad}Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(description))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n{Dpad}Description cannot be empty!");
                        Console.ResetColor();
                        Console.WriteLine($"{Dpad}Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    if (itemsList.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n{Dpad}Please add at least one item.");
                        Console.ResetColor();
                        Console.WriteLine($"{Dpad}Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

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

                    File.AppendAllText(RequestsFilePath, finalizedTicket.TrimEnd() + Environment.NewLine + "=========================" + Environment.NewLine);

                    Console.WriteLine($"\n{Dpad}Your request has been saved successfully!");
                    Console.WriteLine($"{Dpad}Press any key to return to menu...");
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
                    Console.WriteLine($"\n{Dpad}Invalid option. Press any key to try again...");
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
            Console.WriteLine($"{Bpad}       Press any key to continue");
        }
        static void ExecuteViewAccomplishedRequests()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Dpad} User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Dpad}========================================");
            Console.WriteLine($"{Dpad}        Completed by History            ");
            Console.WriteLine($"{Dpad}========================================");
            Console.ResetColor();
            Console.WriteLine();

            bool hasRequests = false;

            if (File.Exists(AccomplishedFilePath))
            {
                foreach (string line in File.ReadAllLines(AccomplishedFilePath))
                {
                    string[] parts = line.Split('|');

                    if (parts.Length >= 4 && parts[0] == currentLoggedInUser)
                    {
                        hasRequests = true;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{Dpad}╔══════════════════════════════════════╗");
                        Console.WriteLine($"{Dpad}║ Donor  : {parts[1],-28}║");
                        Console.WriteLine($"{Dpad}║ Type   : {parts[2],-28}║");
                        Console.WriteLine($"{Dpad}║ Status : {parts[3],-28}║");

                        if (parts.Length >= 5)
                        {
                            Console.WriteLine($"{Dpad}╠══════════════════════════════════════╣");
                            Console.WriteLine($"{Dpad}  Message:                            ");
                            Console.WriteLine($"{Dpad} {parts[4],-36} ║");
                        }

                        Console.WriteLine($"{Dpad}╚══════════════════════════════════════╝");
                        Console.ResetColor();
                        Console.WriteLine();

                        // Load already-read notifications
                        List<string> readEntries = new List<string>();

                        if (File.Exists(ReadNotificationsFilePath))
                        {
                            readEntries = File.ReadAllLines(ReadNotificationsFilePath).ToList();
                        }

                        // Save notification only if it hasn't been saved before
                        if (!readEntries.Contains(line))
                        {
                            File.AppendAllText(ReadNotificationsFilePath, line + Environment.NewLine);
                        }
                    }
                }
            }

            if (!hasRequests)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Dpad}No accomplished requests found.");
                Console.ResetColor();
            }

            Console.WriteLine($"\n{Dpad}Press any key to return...");
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
                achievements.Add("Fellow Volunteers");
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
            List<string> unlocked = GetAchievements(points);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Dpad}╔════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}║            USER PROFILE            ║");
            Console.WriteLine($"{Dpad}╠════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine($"{Dpad} Username : {currentLoggedInUser}");
            Console.WriteLine($"{Dpad} Points   : {points}");
            Console.WriteLine($"{Dpad} Rank     : {Rankings(points)}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Dpad}╚════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"{Dpad}Press any key to return");
            Console.WriteLine();

            // ACHIEVEMENTS SECTION HEADER
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Dpad}╔════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}║           ACHIEVEMENTS             ║");
            Console.WriteLine($"{Dpad}╚════════════════════════════════════╝");
            Console.ResetColor();

            if (unlocked.Count == 0)
            {
                Console.WriteLine($"{Dpad}No achievements unlocked yet.");
            }
            else
            {
                foreach (string achievement in GetAchievements(points))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Dpad} ✓ {achievement}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();

            // RANK TIERS HEADER
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Dpad}╔════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}║            RANK TIERS              ║");
            Console.WriteLine($"{Dpad}╚════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine($"{Dpad}Bronze Donor   - 0 Points");
            Console.WriteLine($"{Dpad}Silver Donor   - 50 Points");
            Console.WriteLine($"{Dpad}Gold Donor     - 100 Points");
            Console.WriteLine($"{Dpad}Platinum Donor - 200 Points");
            Console.WriteLine($"{Dpad}Emerald Donor  - 500 Points");
            Console.WriteLine($"{Dpad}Diamond Donor  - 1000 Points");
            Console.WriteLine($"{Dpad}Global Donor   - 2000 Points");

            Console.WriteLine();

            // FULL ACHIEVEMENT LIST
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Dpad}╔════════════════════════════════════╗");
            Console.WriteLine($"{Dpad}║        ALL ACHIEVEMENTS            ║");
            Console.WriteLine($"{Dpad}╚════════════════════════════════════╝");
            Console.ResetColor();

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

                Console.WriteLine($"{Dpad}{status,-12} {name} ({requiredPoints} pts)");
            }
            Console.ReadKey();
        }
        static void DeleteRequests(List<string> RequestListmaker)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Dpad} User: {currentLoggedInUser} | Points: {userDatabase[currentLoggedInUser][1]}");
            Console.ResetColor();
            Console.WriteLine($"{Dpad}   =================================");
            Console.WriteLine($"{Dpad}         DELETE A REQUEST         ");
            Console.WriteLine($"{Dpad}   =================================");
            Console.WriteLine($"{Dpad}Choose a request you made that you want to remove:");

            List<int> userRequestIndexes = new List<int>();

            for (int i = 0; i < RequestListmaker.Count; i++)
            {
                string cleanRequest = RequestListmaker[i].Replace("=========================", "").Trim();
                string[] ticketLines = RequestListmaker[i].Split('\n');

                string postedBy = "";

                foreach (string line in ticketLines)
                {
                    if (line.StartsWith("Posted by:"))
                    {
                        postedBy = line.Replace("Posted by:", "").Trim();
                        break;
                    }
                }

                if (postedBy == currentLoggedInUser)
                {
                    userRequestIndexes.Add(i);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{Dpad}╔═══════════════════════════════════════╗");
                    Console.WriteLine($"{Dpad} REQUEST #{userRequestIndexes.Count}");
                    Console.WriteLine($"{Dpad}╠═══════════════════════════════════════╣");

                    foreach (string line in cleanRequest.Split('\n'))
                    {
                        Console.WriteLine($"{Dpad}{line}");
                    }

                    Console.WriteLine($"{Dpad}╚═══════════════════════════════════════╝");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }

            if (userRequestIndexes.Count == 0)
            {
                Console.WriteLine($"\n{Dpad}You have no requests to delete.");
                Console.ReadKey();
                return;
            }

            Console.Write($"\n{Dpad}Enter request number to delete (0 to cancel): ");

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
                    Console.WriteLine($"{Dpad}\nInvalid selection.");
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
                    File.AppendAllText(
                        RequestsFilePath,
                        ticket.TrimEnd() +
                        Environment.NewLine +
                        "=========================" +
                        Environment.NewLine);
                }

                Console.WriteLine($"\n{Dpad}Request deleted successfully.");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{Dpad}Invalid input. Expected a valid number entry.");
                Console.ResetColor();
            }

            Console.WriteLine($"\n{Dpad}Press any key to return...");
            Console.ReadKey();
        }

        //Helper methods
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Ignore Enter
                if (key.Key == ConsoleKey.Enter)
                    break;

                // Handle Backspace
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }

            } while (true);

            Console.WriteLine();
            return password;
        }
        static void PrintWrapped(string prefix, string text, int width)
        {
            while (text.Length > width)
            {
                Console.WriteLine($"{Dpad}{prefix}{text.Substring(0, width)}");
                text = text.Substring(width);
                prefix = ""; // Indent next lines
            }

            Console.WriteLine($"{Dpad}{prefix}{text}");
        }
        static void PrintRequest(string request)
        {
            foreach (string line in request.Split('\n'))
            {
                if (line.StartsWith("Title: "))
                {
                    PrintWrapped("Title: ", line.Substring(7), 50);
                }

                else if (line.StartsWith("Description: "))
                {
                    PrintWrapped("Description: ", line.Substring(13), 50);
                }

                else
                {
                    Console.WriteLine($"{Dpad}{line}");
                }
            }
        }
    }
}
