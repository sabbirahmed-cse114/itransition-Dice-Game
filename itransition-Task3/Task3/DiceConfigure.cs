
namespace Task3
{
    public class DiceConfigure : IDiceConfigure
    {
        public List<List<int>> ConfigureDiceArguments(string[] args)
        {
            if (args.Length < 3)
            {
                ShowError("At least 3 dice must be provided.");
            }

            var diceList = new List<List<int>>();

            for (int i = 0; i < args.Length; i++)
            {
                var faceStrings = args[i].Split(',');

                if (faceStrings.Length != 6)
                {
                    ShowError($"Each dice {i + 1} must contain exactly 6 values. Example: 1,2,3,4,5,6");
                }

                var die = new List<int>();
                foreach (var face in faceStrings)
                {
                    if (!int.TryParse(face.Trim(), out int value))
                    {
                        ShowError($"All face values must be integers. Found invalid value: '{face}' in Dice {i + 1}");
                    }
                    die.Add(value);
                }

                diceList.Add(die);
            }

            return diceList;
        }

        private void ShowError(string message)
        {
            Console.WriteLine("Error: " + message);
            Console.WriteLine("Example usage:");
            Console.WriteLine("dotnet run \"1,2,3,4,5,6\" \"6,5,4,3,2,1\" \"2,3,4,5,6,7\"");
            Environment.Exit(1);
        }
    }
}