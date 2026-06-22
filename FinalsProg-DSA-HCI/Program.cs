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
        static void logo()
        {
            string header = @"
                                      @@@@@@@@@@@@                                    
                               @@@@@@@@@@@@@@@@@@@@@@@@@@                              
                           @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                          
                        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                      
                     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                    
                   @@@@@@@@@@@@@@@                    @@@@@@@@@@@@@@@                  
                 @@@@@@@@@@@@@                            @@@@@@@@@@@@@                
               @@@@@@@@@@@@                                  @@@@@@@@@@@              
              @@@@@@@@@@@                                      @@@@@@@@@@@            
            @@@@@@@@@@@  @@@@@@@@@@@@@            @@@@@@@@@@@@   @@@@@@@@@@            
           @@@@@@@@@@ @@@@@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@  @@@@@@@@@          
          @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@@@ @@@@@@@@@          
          @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        
         @@@@@@@@@@@@@@@@@@@@@@@                @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        
        @@@@@@@@@ @@@@@@@@@@@@@@  @@@  @@@ @@@@  @@@@@@@@@@@@@@@@@@@@@ @@@@@@@@        
        @@@@@@@@  @@@@@@@@@@@@@@  @@@      @@@@  @@@@@@@@@@@@@@@@@@@@@  @@@@@@@@      
       @@@@@@@@@ @@@@@@@@@@@@@@@  @@@@@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@@  @@@@@@@@      
       @@@@@@@@  @@@@@@@@@@@@@@@  @@@@@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@@  @@@@@@@@@      
       @@@@@@@@   @@@@@@     @@   @@@@@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@@   @@@@@@@@      
       @@@@@@@@   @@@@@@ @@@  @@@@@    @@@@@@@@  @@@@@@@@@@@@@@@@@@@@@   @@@@@@@@      
       @@@@@@@@   @@@@@@ @@@  @@  @@@@  @@@@@@@  @@@@@@@@@@@@@@@@@@@@    @@@@@@@@      
       @@@@@@@@    @@@@@ @@@  @@@@@@ @@@@@@    @@@@@@@@@@@@@@@@@@@@@@    @@@@@@@@      
       @@@@@@@@    @@@@@@   @@              @@@@@@@@@@@@@@@    @@@@@     @@@@@@@@      
       @@@@@@@@     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@           @@@ @@@@    @@@@@@@@      
        @@@@@@@@     @@@@@@@@@@@@@@@@@@@@@  @@@  @@@@@@@@@ @@@@ @@@     @@@@@@@@      
        @@@@@@@@@     @@@@@@@@@@@@@@@@@@@@            @@@@ @@@@ @      @@@@@@@@@      
         @@@@@@@@      @@@@@@@@@@@@@@@@@@@@     @@@@@@@        @      @@@@@@@@@        
         @@@@@@@@@       @@@@@@@@@@@@@@@@@@@@@@@@@    @@@@@@@@@       @@@@@@@@        
          @@@@@@@@@       @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@       @@@@@@@@@@        
           @@@@@@@@@@       @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@       @@@@@@@@@@          
            @@@@@@@@@@        @@@@@@@@@@@@@@@@@@@@@@@@@@@@        @@@@@@@@@            
             @@@@@@@@@@@        @@@@@@@@@@@@@@@@@@@@@@@@        @@@@@@@@@@            
               @@@@@@@@@@@        @@@@@@@@@@@@@@@@@@@@        @@@@@@@@@@@              
                @@@@@@@@@@@@@       @@@@@@@@@@@@@@@@       @@@@@@@@@@@@                
                  @@@@@@@@@@@@@@       @@@@@@@@@@      @@@@@@@@@@@@@@@                
                    @@@@@@@@@@@@@@@@@@   @@@@@@   @@@@@@@@@@@@@@@@@@                  
                       @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                      
                          @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                        
                             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@                            
                                   @@@@@@@@@@@@@@@@@@                                  
                                                                                       

            ";
            Console.WriteLine(header);
        }
        static void title()
        {
            string asciiArt = @"
                                                                            :@@@@@@*                                           :@@%          
                                                                            =@@@@@@@@@:                                        +@@@          
                                                                            =@@@  -#@@@.    ..          .        ..          . .@@    ..     
                                                                            =@@@   =@@@#.@@@@@@@@ =@@@@@@@@+ :@@@@@@@# +@@@@@@=    *@@@@@.   
                                                                            =@@@   +@@@*@@@- -#@@@=@@@ .*@@@=@@@  -%@@#+@@@ .     :%@@@#:    
                                                                            =@@@. @@@@*=@@@=  #@@@=@@@  +@@@+@@@. .@@@*+@@@        .-*@@@@   
                                                                            =@@@@@@@*   =@@@@@@@# =@@@  +@@@.+@@@@@@@= +@@@       =@@@@@@=        
                                                                                    -@@@@@@@@:           __                               
                                                                                    -@@@+=#@@@@          --                                 
                                                                                    -@@@=  =@@@#.###-%@#:##*=##*   +##+  +@@%+              
                                                                                    -@@@=  :%@@%+@@@@@@:*@@%=@@@# #@@%.@@@-=%@@-            
                                                                                    -@@@=  *@@@=+@@@    *@@# =%@@%@@% *@@@@@@@@*            
                                                                                    -@@@@@@@@@: +@@@    *@@#  -@@@@%  =@@@+.=@#             
                                                                                    -@@@@@@=    +@@@    *@@#   -%@@    :*@@@@%. ";
            Console.WriteLine(asciiArt);

        }
        static void logindisplay()
        {
            string art = @"                                                                                                                                                                                                                                                                                                                   
                                                                                                                
                                                            .*@@@@:                                                  +@@@@:                                 
                                                            :#@@@@:                                                 =%@@@@*                                 
                                                            :#@@@@:                                                 .=#%*                                   
                                                            :#@@@@:                                                                                         
                                                            :#@@@@:            @@@@@@@@-       #@@@@@@+@@@@=         *@@@@   %@@@%=@@@@@@-                  
                                                            :#@@@@:         *@@@@@@@@@@@@=  .%@@@@@@@@@@@@@=        :#@@@@  =@@@@@@@@@@@@@@                 
                                                            :#@@@@:       .#@@@@%:.-+%@@@@#:#@@@@%:.-*%@@@@=        :#@@@@  =@@@@@= .-#@@@@*                
                                                            :#@@@@:       =%@@@@     *@@@@@+@@@@@    .+@@@@=        :#@@@@  =@@@@#   .*@@@@*                
                                                            :#@@@@:       =%@@@@=    *@@@@@+%@@@@:    *@@@@=        :#@@@@  =@@@@#   .*@@@@*                
                                                            :#@@@@@@@@@@@@:*@@@@@@@@@@@@@@..*@@@@@@@@@@@@@@=        :#@@@@  =@@@@#   .*@@@@*                
                                                            :#@@@@@@@@@@@@ .=%@@@@@@@@@@+   :*@@@@@@@@@@@@@=        :#@@@@  =@@@@#   .*@@@@*                
                                                            .===========:     :=+*##*-        .=+###=:+@@@@=        .===:   :===.     -==-                  
                                                                                              =%%:   =@@@@@.                                                
                                                                                             *@@@@@@@@@@@@@=                                                 
                                                                                              .=#@@@@@@@@@-                                                   
                                                                                                   ...  ";
            Console.WriteLine(art);
        }
        static void Accountcreationdisplay()
        {
            string art = @"
                                                                              *@@@                                                                
                                                                             +@@@@%                                                    .@@@       
                                                                            -@@@@@@#                                                   =@@@       
                                                                           .@@@#+@@@.   *@@@@@- #@@@@@. #@@@@@@# =@@@  -%@@.+@@@@@@@@*+@@@@@@     
                                                                           #@@@**@@@@.:@@@* . -@@@+ . -@@@*.=#@@@=@@@  -%@@.+@@@.:*@@@.=@@@.      
                                                                          +@@@@@@@@@@@+@@@.   =@@@:   =@@@:  *@@@+@@@. -@@@.+@@@  +@@@ =@@@.      
                                                                         -@@@=    =@@@#*@@@@@@:#@@@@@@-#@@@@@@@@--%@@@@@@@@ +@@@  +@@@ -@@@@%     
                                                                        .***:     .***- :+%@%:  :+%@%:  :+#@@*    :=*%@@*   =**:  =**-  -*@%-     
                                                                                                                                                    
                                                                        -%@@@%=                                     =@=                        
                                                                      -@@@@@@@@@.                              #%# .*@@@                        
                                                                     #@@@*    .                               +@@@   .                          
                                                                    =@@@*        +@@@%@@@ :@@@@@%.  *@@@@@@@@+@@@@@@=@@@  :%@@@@@* .#@@@@@@@%   
                                                                    =@@@*        *@@@#++ #@@*.=@@@=@@@@=*@@@@-*@@@: +@@@ #@@@++@@@@:#@@@-+@@@#  
                                                                    :#@@@#       *@@@   -@@@@%%%%%#@@@   +@@@ +@@@  +@@@-@@@+  -@@@*#@@+ .#@@#  
                                                                     :*@@@@@@@@@-*@@@   :*@@@%*@@@=%@@@@@@@@@ +@@@@++@@@:*@@@@@@@@%.#@@+ .#@@#  
                                                                       .=#@@@@%=.*@@*     =#@@@%=  :*@@@%*@@# .+@@@#+@@*  -#@@@@*. .#@%- .#@@+  ";
            Console.WriteLine(art);
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


            Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t ---->  Do you have an account? (Y/N): ");
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
                Console.Clear();
                Accountcreationdisplay();
                Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t\tWould you like to sign up? (Y/N) :");
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

                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\tInvalid input. Please type Y or N.");
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\tInvalid input. Please type exactly one character.");
                }
                Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\tPress any key to try again...");
                Console.ReadKey();
            }
            while (true)
            {
                Console.Clear();
                Accountcreationdisplay();
                Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\t\t\t\t[Creating New Account]");
                Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t\t\tEnter Username: ");
                string user = Console.ReadLine()?.Trim();

                Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t\t\tEnter Password: ");
                string pass = Console.ReadLine()?.Trim();

                if (userDatabase.ContainsKey(user) || string.IsNullOrEmpty(user))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\t Username invalid or already exists. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                userDatabase.Add(user, new string[] { pass, "0" });
                File.AppendAllText(DatabaseFilePath, $"{user},{pass},0\n");

                Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\tRegistration Successful! Press any key to continue");
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
                Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\t\t\t\t[Login Session]");
                Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t\t\tEnter Username: ");
                string user = Console.ReadLine().Trim();

                Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t\t\tEnter Password: ");
                string pass = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\t\t\t\tFields cannot be empty. Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                if (userDatabase.ContainsKey(user) && userDatabase[user][0] == pass)
                {
                    currentLoggedInUser = user;
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\t\tLogin Successful! Redirecting to main dashboard...");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    Console.WriteLine("\n\n\t\t\t\t\t\t\t\t\t\t[ Invalid Details ] Press any key to try again.");
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
            Console.WriteLine("5. History of completed request from you");
            Console.WriteLine("6. View profile");
            Console.WriteLine("7. Log out");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice >= 1 && choice <= 7)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please type 1 to 6.");
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

            for (int i = 0; i < RequestListmaker.Count; i++)
            {
                Console.WriteLine($"--- Request #{i + 1} ---");
                Console.WriteLine(RequestListmaker[i]);
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
                Console.WriteLine("\nInvalid input. Expected a valid number entry.");
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

                        if (string.Equals(itemInput, "X"))
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

            if (unreadCount > 0)
            {
                Console.WriteLine($"[NOTIFICATION] You have {unreadCount} new completed request(s).");
            }
            else
            {
                Console.WriteLine("[NOTIFICATION] No new updates.");
            }
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

            Console.WriteLine("=== DONOR PROFILE ===");
            Console.WriteLine($"Username : {currentLoggedInUser}");
            Console.WriteLine($"Points   : {points}");
            Console.WriteLine($"Badge    : {Rankings(points)}");

            Console.WriteLine("\nAchievements:");

            foreach (string achievement in GetAchievements(points))
            {
                Console.WriteLine($"✓ {achievement}");
            }

            Console.ReadKey();
        }

        static void DeleteRequests(List<string> RequestListmaker)
        {
            Console.WriteLine("Choose a request you made that you want to remove:");
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

                    if (originalRequester != currentLoggedInUser)
                    {
                        Console.WriteLine("\nYou can only delete your own request.");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        return;
                    }

                    File.AppendAllText(AccomplishedFilePath,
                        $"{originalRequester}|{currentLoggedInUser}|Donation Pack|Completed|\n");

                    //removes the accomplished item
                    RequestListmaker.RemoveAt(targetIndex);

                    // rewriter
                    File.WriteAllText(RequestsFilePath, "");
                    foreach (string ticket in RequestListmaker)
                    {
                        File.AppendAllText(RequestsFilePath, ticket + "=========================\n");
                    }

                    // Update the file
                    File.WriteAllText(DatabaseFilePath, "");
                    foreach (var entry in userDatabase)
                    {
                        File.AppendAllText(DatabaseFilePath, $"{entry.Key},{entry.Value[0]},{entry.Value[1]}\n");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nInvalid input. Expected a valid number entry.");
            }
        }
    }
}
        
   


                
           
    
