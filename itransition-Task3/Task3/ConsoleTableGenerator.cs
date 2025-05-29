using ConsoleTables;

namespace Task3
{
    public class ConsoleTableGenerator : IConsoleTableGenerator
    {
        public void ShowConsoleHelpTable(IDiceSet diceSet)
        {
            int n = diceSet.Count;
            string[] headers = new string[n + 1];
            headers[0] = "User dice vs";

            for (int i = 0; i < n; i++)
            {
                headers[i + 1] = string.Join(",", diceSet.GetDice(i).Faces);
            }

            var table = new ConsoleTable(headers);

            for (int i = 0; i < n; i++)
            {
                List<string> row = new List<string>();
                string DiceName = string.Join(",", diceSet.GetDice(i).Faces);
                row.Add(DiceName);

                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        IDice A = diceSet.GetDice(i);
                        IDice B = diceSet.GetDice(j);
                        double Prob = CalculateProbability(A, B);
                        row.Add($"- ({Prob:F4})");
                        continue;
                    }

                    IDice diceA = diceSet.GetDice(i);
                    IDice diceB = diceSet.GetDice(j);
                    double winProb = CalculateProbability(diceA, diceB);
                    row.Add($"{winProb:F4}");
                }

                table.AddRow(row.ToArray());
            }

            Console.WriteLine("\nC#\nProbability of the win fоr the user:");
            table.Write(Format.Alternative);
        }

        private double CalculateProbability(IDice diceA, IDice diceB)
        {
            int winCount = 0;
            int total = diceA.Faces.Count * diceB.Faces.Count;

            foreach (int faceA in diceA.Faces)
            {
                foreach (int faceB in diceB.Faces)
                {
                    if (faceA > faceB) winCount++;
                }
            }
            return (double)winCount / total;
        }
    }
}