
namespace Task3
{
    public class DiceSet : IDiceSet
    {
        private readonly List<IDice> _diceList;

        public DiceSet()
        {
            _diceList = new List<IDice>();
        }

        public DiceSet(IEnumerable<IDice> dice)
        {
            _diceList = dice.ToList();
        }

        public int Count => _diceList.Count;

        public IDice GetDice(int index) => _diceList[index];

        public void RemoveDiceAt(int index)
        {
            _diceList.RemoveAt(index);
        }

        public void AddDice(IDice dice)
        {
            _diceList.Add(dice);
        }
    }
}