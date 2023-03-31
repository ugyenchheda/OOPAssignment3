using System.Text.RegularExpressions;
using OOPAssignment3;

bool more = false;
int choice;
string path;
string playerName;
DateTime birthDate;
string emailId;
string mobileNo;
Player player;

List<Player> allPlayers = new List<Player>()
{
    new Player(null, null, null, null)
};

do
{
    Console.WriteLine();
    Console.WriteLine("Welcome to JSON file processing: Select an action(1 - 4)");
    Console.WriteLine("1. Print the contents of the file to the screen.");
    Console.WriteLine("2. Add a new Player.");
    Console.WriteLine("3. Modify Player information.");
    Console.WriteLine("4. Remove Player.");
    Console.WriteLine();
    Console.Write("Select an action: ");

    string received = Console.ReadLine();
    while (!Int32.TryParse(received, out choice) || choice < 1 || choice > 4)
    {
        Console.Write("Not valid, try again: ");
        received = Console.ReadLine();
    }

    switch (choice)
    {
        case 1:
            Console.WriteLine("Your choice: Print the contents of the file to the screen.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(path));


            allPlayers[0].PrintTheContents(path);
            break;
        case 2:
            Console.WriteLine("Your choice: Add a new Player.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(path));
            do
            {
                Console.Write("Enter the Player's name: ");
                playerName = Console.ReadLine();
                if (String.IsNullOrEmpty(playerName))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(playerName));

            Console.Write("Enter the date of birth: ");
            received = Console.ReadLine();
            while (!DateTime.TryParse(received, out birthDate))
            {
                Console.Write("Not valid, try again: ");
                received = Console.ReadLine();
            }
            Console.Write("Enter the Email: ");
            received = Console.ReadLine();
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            while (!Regex.IsMatch(received, pattern))
            {
                Console.Write("Not valid, try again: ");
                received = Console.ReadLine();
            }
            emailId = received; 
            Console.Write("Enter the Mobile: ");
            string phone = Console.ReadLine();
            string phonePattern = @"^[0-9]+$";
            while (!Regex.IsMatch(phone, phonePattern))
            {
                Console.Write("Not valid, try again: ");
                phone = Console.ReadLine();
            }
            mobileNo = phone;
            player = new Player(playerName, birthDate, emailId, mobileNo);
            player.ContactInformation.Add(new Player.ContactInfo() { Email = emailId, Mobile = mobileNo });
            allPlayers[0].AddPlayer(path, player);
            break;
        case 3:
            Console.WriteLine("Your choice: Modify Player information.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(path));

            do
            {
                Console.Write("Enter the Player's name: ");
                playerName = Console.ReadLine();
                if (String.IsNullOrEmpty(playerName))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(playerName));
            allPlayers[0].ModifyInformation(path, playerName);
            break;
        case 4:
            Console.WriteLine("Your choice: Remove Player.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");
            } while (String.IsNullOrEmpty(path));
            do
            {
                Console.Write("Enter the Player's name: ");
                playerName = Console.ReadLine();
                if (String.IsNullOrEmpty(playerName))
                    Console.WriteLine("The field was left empty, try again!");
            } while (String.IsNullOrEmpty(playerName));
            allPlayers[0].RemovePlayer(path, playerName);
            break;
        default:
            Console.WriteLine("Your choice: You'll never get there.");
            break;
    }
    Console.WriteLine();
    Console.Write("Do you continue with the new operation? (Y/N): ");
    received = Console.ReadLine().ToUpper();
    if (received.StartsWith("Y"))
        more = true;
    else
        more = false;
} while (more);