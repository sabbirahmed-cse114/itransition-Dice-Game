
namespace Task3
{
    public class GameController : IGameController
    {
        private readonly IDiceSet _diceSet;
        private readonly Random _random;
        private readonly IConsoleTableGenerator _consoleTable;

        public IDice UserDice { get; private set; }
        public IDice ComputerDice { get; private set; } 

        public GameController(IDiceSet diceSet)
        {
            _diceSet = diceSet;
            _random = new Random();
            _consoleTable = new ConsoleTableGenerator();
        }

        public void HandleTurnFunction(bool isUserFirst)
        {
            if (isUserFirst)
            {
                Console.WriteLine("Choose your dice:");

                while (true)
                {
                    ShowAvailableDice();
                    string input = GetUserInput();

                    if (input == "x") 
                    {
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0); 
                    }
                    else if (input == "?") 
                    { 
                        ShowHelpTable(); 
                    }
                    else if (int.TryParse(input, out int selectedIndex) && IsValidIndex(selectedIndex))
                    {
                        UserDice = _diceSet.GetDice(selectedIndex);
                        Console.WriteLine($"You choose the [{string.Join(",", UserDice.Faces)}] dice.");
                        _diceSet.RemoveDiceAt(selectedIndex);
                        break;
                    }
                    else Console.WriteLine("Invalid input. Try again.");
                }

                ComputerDice = PickDiceForComputer();
                Console.WriteLine($"I selected the [{string.Join(",", ComputerDice.Faces)}] dice.");
            }
            else
            {
                int compIndex = _random.Next(_diceSet.Count);
                ComputerDice = _diceSet.GetDice(compIndex);
                Console.WriteLine($"I make the first move and choose the [{string.Join(",", ComputerDice.Faces)}] dice.");
                _diceSet.RemoveDiceAt(compIndex);

                Console.WriteLine("Choose your dice:");
                while (true)
                {
                    ShowAvailableDice();
                    string input = GetUserInput();

                    if (input == "x") 
                    {
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0); 
                    }
                    else if (input == "?") 
                    { 
                        ShowHelpTable(); 
                    }
                    else if (int.TryParse(input, out int selectedIndex) && IsValidIndex(selectedIndex))
                    {
                        UserDice = _diceSet.GetDice(selectedIndex);
                        Console.WriteLine($"You choose the [{string.Join(",", UserDice.Faces)}] dice.");
                        break;
                    }
                    else Console.WriteLine("Invalid input. Try again");
                }
            }
        }

        private void ShowAvailableDice()
        {
            for (int i = 0; i < _diceSet.Count; i++)
            {
                var die = _diceSet.GetDice(i);
                Console.WriteLine($"{i} - {string.Join(",", die.Faces)}");
            }
            Console.WriteLine("x - exit");
            Console.WriteLine("? - help");
        }

        private string GetUserInput()
        {
            Console.Write("Your selection: ");
            return Console.ReadLine()?.Trim().ToLower();
        }

        private void ShowHelpTable()
        {
            _consoleTable.ShowConsoleHelpTable(_diceSet);
            Console.WriteLine("Enter the number corresponding to your preferred dice:");
        }

        private bool IsValidIndex(int index) => index >= 0 && index < _diceSet.Count;

        private IDice PickDiceForComputer()
        {
            int index = _random.Next(_diceSet.Count);
            IDice selected = _diceSet.GetDice(index);
            _diceSet.RemoveDiceAt(index);
            return selected;
        }
    }
}