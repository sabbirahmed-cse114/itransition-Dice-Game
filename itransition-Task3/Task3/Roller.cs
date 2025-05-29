
namespace Task3
{
    public class Roller
    {
        private readonly FairRandomGenerator _fairGenerator;
        private readonly Random _random;

        public Roller()
        {
            _fairGenerator = new FairRandomGenerator();
            _random = new Random();
        }

        public int Roll(string player)
        {
            Console.WriteLine($"It's time for {player} roll.");

            string hmac = _fairGenerator.generateRandomNumber(6);
            Console.WriteLine($"I selected a random value in the range 0..5");
            Console.WriteLine($"(HMAC={hmac.ToUpper()})");

            int userInput;
            while (true)
            {
                Console.WriteLine("Add your number modulo 6.\n0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - exit\n? - help");
                Console.Write("Your selection: ");
                string input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out userInput) && userInput >= 0 && userInput <= 5)
                    break;
                else if (input == "x") 
                {
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                }
                else if (input == "?") 
                {
                    Console.WriteLine("Please enter a number between 0 to 5.\n");
                }
                else
                    Console.WriteLine("Invalid input! Try again.");
            }

            (string key, int compValue) = _fairGenerator.Reveal();
            Console.WriteLine($"My number is {compValue}");
            Console.WriteLine($"(KEY={key}");

            if (!_fairGenerator.Verify(hmac, key, compValue))
            {
                Console.WriteLine("HMAC verification failed.");
                Environment.Exit(1);
            }

            int resultIndex = (compValue + userInput) % 6;
            Console.WriteLine($"The fair number generation result is {compValue} + {userInput} = {(compValue+userInput) % 6} (mod 6).");
            return resultIndex;
        }
    }
}