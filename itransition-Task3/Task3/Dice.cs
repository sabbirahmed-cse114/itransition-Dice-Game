
namespace Task3
{
    public class Dice : IDice
    {
        public List<int> Faces { get; private set; }

        public Dice(List<int> faces)
        {
            if (faces == null || faces.Count != 6)
                throw new ArgumentException("Each dice must have exactly 6 face values.");

            if (!faces.All(f => f is int))
                throw new ArgumentException("All face values must be integers.");

            Faces = faces;
        }

        public int Roll(Random rng)
        {
            int index = rng.Next(0, 6);
            return Faces[index];
        }

        public override string ToString()
        {
            return $"Dice({string.Join(", ", Faces)})";
        }
    }
}