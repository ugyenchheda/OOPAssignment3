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
    Console.WriteLine("Welcome to JSON file processing: Select an action(1 - 5)");
    Console.WriteLine("Enter 1 if you want to show all:");
    Console.WriteLine("Enter 2 if you want to add a new team member:");
    Console.WriteLine("Enter 3 if you want to modify information.");
    Console.WriteLine("Enter 4 if you want to remove information.");
    Console.WriteLine("Enter 5 if you want to do nothing.");
    Console.WriteLine();
    Console.Write("Select an action: ");

    string received = Console.ReadLine();
    while (!Int32.TryParse(received, out choice) || choice < 1 || choice > 5)
    {
        Console.Write("Not valid, try again: ");
        received = Console.ReadLine();
    }

    switch (choice)
    {
        case 1:
            Console.WriteLine("Your choice: Show All");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("The field was either left empty or not correct, try again!");
                }

            } while (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase));


            allPlayers[0].PrintTheContents(path);
            break;

        case 2:
            Console.WriteLine("Your choice: Add a new team member.");
            do
            {

                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("The field was either left empty or not correct, try again!");
                }

            } while (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase));


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
            Console.WriteLine("Enter Mobile Number in +358XXXXXXXXXX format");
            string phone = Console.ReadLine();
            string phonePattern = @"^(\+|00)358\d{9}$";

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
                if (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("The field was either left empty or not correct, try again!");
                }

            } while (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase));

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

            Console.WriteLine("Your choice: Remove Player Information.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("The field was either left empty or not correct, try again!");
                }

            } while (String.IsNullOrEmpty(path) || !Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase));


            do
            {
                Console.Write("Enter the Player's name: ");
                playerName = Console.ReadLine();
                if (String.IsNullOrEmpty(playerName))
                    Console.WriteLine("The field was left empty, try again!");
            } while (String.IsNullOrEmpty(playerName));
            allPlayers[0].RemovePlayer(path, playerName);

            break;

        case 5:

            Console.WriteLine("Your choice: Do Nothing...");
            break;
        default:
            Console.WriteLine("Your choice: Let it rest...");
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
