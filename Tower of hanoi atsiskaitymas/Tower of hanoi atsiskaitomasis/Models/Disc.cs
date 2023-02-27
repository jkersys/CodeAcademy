namespace TowerOfHanoi.Models
{
    public class Disc
    {
        public int Size { get; private set; }

        public Disc(int size)
        {
            Size = size;
        }

        public override string ToString()
        {
            return "|".PadLeft(Size + 1, '#')
                .PadRight(2 * Size + 1, '#');
        }
    }
}