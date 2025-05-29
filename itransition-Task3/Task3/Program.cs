using Task3;

try
{
    IDiceConfigure diceConfigure = new DiceConfigure();
    List<List<int>> configureDice = diceConfigure.ConfigureDiceArguments(args);

    IDiceSet diceSet = new DiceSet();
    for (int i = 0; i < configureDice.Count; i++)
    {
        IDice dice = new Dice(configureDice[i]);
        diceSet.AddDice(dice);
    }

    IFairRandomGenerator fairGenerator = new FairRandomGenerator();
    string hmac = fairGenerator.generateRandomNumber(2);

    Console.WriteLine("Let's determine who makes the first move.");
    Console.WriteLine($"I selected a random value in the range 0..1");
    Console.WriteLine($"(HMAC={hmac}).");
    Console.WriteLine("Try to guess my selection.");
    Console.WriteLine("0 - 0\n1 - 1\nx - exit\n? - help");

    string guessInput;
    int userGuess = -1;

    while (true)
    {
        Console.Write("Your selection: ");
        guessInput = Console.ReadLine()?.Trim();

        if (guessInput == "0" || guessInput == "1")
        {
            userGuess = int.Parse(guessInput);
            break;
        }
        else if (guessInput?.ToLower() == "x")
        {
            Console.WriteLine("Exiting...");
            return;
        }
        else if (guessInput == "?")
        {
            Console.WriteLine("Guess (0 or 1) to predict who will go first.");
        }
        else
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    (string key, int actualValue) = fairGenerator.Reveal();
    Console.WriteLine($"My selection: {actualValue}");
    Console.WriteLine($"(KEY={key}).");

    bool isUserFirst = false;

    if (fairGenerator.Verify(hmac, key, actualValue))
    {
        if (userGuess == actualValue)
        {
            isUserFirst = true;
            Console.WriteLine("You make the first move.");
        }
    }
    else
    {
        Console.WriteLine("HMAC verification failed.");
        return;
    }

    IGameController gameController = new GameController(diceSet);
    gameController.HandleTurnFunction(isUserFirst);

    Roller roller = new Roller();

    int userIndex = roller.Roll("your");
    int userRoll = gameController.UserDice.Faces[userIndex];
    Console.WriteLine($"Your roll result is {userRoll}.");

    int compIndex = roller.Roll("my");
    int compRoll = gameController.ComputerDice.Faces[compIndex];
    Console.WriteLine($"My roll result is {compRoll}.");

    if (userRoll > compRoll)
    {
        Console.WriteLine($"\nYou win ({userRoll} > {compRoll})!");
    }
    else if (userRoll < compRoll)
    {
        Console.WriteLine($"\nI win ({compRoll} > {userRoll})!");
    }
    else
    {
        Console.WriteLine($"\nIt's a draw ({userRoll} = {compRoll})!");
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine("Error: " + ex.Message);
    Console.WriteLine("Example usage:");
    Console.WriteLine("dotnet run 1,2,3,4,5,6 6,5,4,3,2,1 1,1,2,2,3,3");
}