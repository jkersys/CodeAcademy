namespace TowerOfHanoi.Models
{
    public class Tower
    {
        public int Height { get; set; }
        public bool IsActive { get; set; }

        public List<Disc> Discs { get; set; }

        public Tower(int height)
        { 
            Height = height;
            Discs = new List<Disc>(height - 1);
        }
    }
}
