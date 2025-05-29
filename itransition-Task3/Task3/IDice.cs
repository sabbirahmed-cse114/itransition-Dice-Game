
namespace Task3
{
    public interface IDice
    {
        List<int> Faces { get; }
        int Roll(Random rng);
    }
}