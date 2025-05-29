
namespace Task3
{
    public interface IDiceSet
    {
        int Count { get; }
        IDice GetDice(int index);
        void RemoveDiceAt(int index);
        void AddDice(IDice dice);
    }
}