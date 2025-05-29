
namespace Task3
{
    public interface IGameController
    {
        IDice UserDice { get; }
        IDice ComputerDice { get; }
        void HandleTurnFunction(bool isUserFirst);
    }
}