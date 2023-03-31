using Newtonsoft.Json;

namespace OOPAssignment3
{
    internal class Player
    {
        public Player(string? name, DateTime? dateOfBirth, string? email, string? mobile)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            ContactInformation = new List<ContactInfo>();
            if (!String.IsNullOrEmpty(email) || !String.IsNullOrEmpty(mobile))
            {
                ContactInformation.Add(new ContactInfo { Email = email, Mobile = mobile });
            }
        }

        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<ContactInfo> ContactInformation { get; set; }
        public class ContactInfo
        {
            public string Email { get; set; }
            public string Mobile { get; set; }
        }

        public void PrintTheContents(string path)
        {
            Console.WriteLine();
            List<Player> players;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
            }
            foreach (var item in players)
            {
                Console.Write("Name: {0}, Date of Birth: {1}", item.Name, item.DateOfBirth.Value.ToString("d"));
                if (item.ContactInformation.Any())
                {
                    Console.Write(", Email: {0}, Mobile: {1}", item.ContactInformation[0].Email, item.ContactInformation[0].Mobile);
                }
                Console.WriteLine();
            }
        }


        public void AddPlayer(string path, Player newPlayer)
        {
            Console.WriteLine();
            List<Player> players;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
            }
            players.Add(newPlayer);
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                string jsonString = JsonConvert.SerializeObject(players);
                streamWriter.Write(jsonString);
            }
        }



        public void ModifyInformation(string path, string name)
        {
            bool ifChanged = false;
            Console.WriteLine();
            List<Player> players;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                players = JsonConvert.DeserializeObject<List<Player>>(jsonString);

                var chosen = players.Where(cli => cli.Name.Contains(name));
                int numberOfPlayers = players.Count;

                if (chosen.Any())
                {
                    //You enter here if there were wanted names...
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        if (players[i].Name.Contains(name))
                        {
                            Console.WriteLine("There is a customer {0} in the file.",
                                players[i].Name);
                            Console.Write("Do you want to change the information? (Y/N): ");
                            string choice = Console.ReadLine().ToUpper();
                            if (choice.StartsWith("Y"))
                            {
                                //You are only here if changing data is selected
                                Console.WriteLine("Next, we go through all the information.");
                                Console.WriteLine("If you do not change any information,press ENTER at that point.");
                                Console.WriteLine("Current name is {0}.", players[i].Name);
                                Console.Write("Enter new name: ");
                                string newName = Console.ReadLine();
                                if (!String.IsNullOrEmpty(newName))
                                {
                                    players[i].Name = newName;
                                    ifChanged = true;
                                }

                                DateTime newBirthDate;
                                Console.WriteLine("Current Birth date is {0}.",
                                    players[i].DateOfBirth);
                                Console.Write("Enter new date of Birth: ");
                                string received = Console.ReadLine();
                                if (!String.IsNullOrEmpty(received))
                                {
                                    while (!DateTime.TryParse(received, out newBirthDate))
                                    {
                                        Console.Write("Not valid, try again: ");
                                        received = Console.ReadLine();
                                    }
                                    players[i].DateOfBirth = newBirthDate;
                                    ifChanged = true;
                                }
                            }
                        }
                    }
                    if (!ifChanged)
                        Console.WriteLine("You selected the change option, but no changes were made in the end.");
                }
                else
                {
                    Console.WriteLine("No customer with that name was found.");
                    Console.WriteLine();
                }
            }
            if (ifChanged)
            {

                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    string jsonString = JsonConvert.SerializeObject(players);
                    streamWriter.Write(jsonString);
                }
                Console.WriteLine("Editing of data in the file was successful.");
                Console.WriteLine();
            }
        }

        public void RemovePlayer(string path, string name)
        {
            bool ifChanged = false;
            Console.WriteLine();
            List<Player> players;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                players = JsonConvert.DeserializeObject<List<Player>>(jsonString);

                //Are there any clients with that name?
                var chosen = players.Where(cli => cli.Name.Contains(name));
                int numberOfPlayers = players.Count;

                if (chosen.Any())
                {
                    //You enter here if there were wanted names:
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        if (players[i].Name.Contains(name))
                        {
                            Console.WriteLine("There is a customer {0} in the file.",
                                players[i].Name);
                            Console.Write("Do you want to delete the customer's data? (Y/N): ");
                            string choice = Console.ReadLine().ToUpper();
                            if (choice.StartsWith("Y"))
                            {
                                //You are here only if data deletion is selected:
                                string toBeRemoved = players[i].Name;
                                bool succeeded = players.Remove(players[i]);
                                if (succeeded)
                                {
                                    Console.WriteLine("The information of client {0} was removed from the generic collection.",
                                        toBeRemoved);
                                    numberOfPlayers = numberOfPlayers - 1;
                                    i = i - 1;
                                    ifChanged = true;
                                }

                                else
                                    Console.WriteLine("The customer's {0} information could not be deleted from the file.",
                                    toBeRemoved);
                            }
                        }
                    }
                    if (!ifChanged)
                        Console.WriteLine("You chose the delete option, but nothing was done in the end.");
                }
                else
                {
                    Console.WriteLine("No customer with that name was found.");
                    Console.WriteLine();
                }
            }
            if (ifChanged)
            {
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    string jsonString = JsonConvert.SerializeObject(players);
                    streamWriter.Write(jsonString);
                }
                Console.WriteLine("Deleting of data in the file was successful.");
                Console.WriteLine();
            }
        }
    }
}
