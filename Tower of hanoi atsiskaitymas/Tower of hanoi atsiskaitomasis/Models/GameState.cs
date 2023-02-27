namespace TowerOfHanoi.Models
{
    public enum GameState
    {
        Initial,
        DiskInHand,
        NoDisksInSelectedTower,
        DiskDoesNotFit,
        InvalidInputTower,
        InvalidDestinationTower,
        Won
        
    }
}
