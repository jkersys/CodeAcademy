using System.Text;
using TowerOfHanoi.Interface;
using TowerOfHanoi.Service;

namespace TowerOfHanoi.Models
{
    public class Game
    {
        public int TowersCount { get; set; }
        public int TowerHeight { get; set; }
        public int Moves { get; set; }

        public GameState State { get; private set; }

        public Disc? ActiveDisc { get; set; } = null;

        public List<Tower> Towers { get; set; } = new List<Tower>();
        public DateTime StartDate { get; set; } = DateTime.Now;

        private int TowerFromNumber { get; set; }
        private int TowerToNumber { get; set; }
        private ILog Logger { get; set; }
        
        public Game(ILog logger, int towersCount, int towerHeight)
        {
            this.Logger = logger;
            TowersCount = towersCount;
            TowerHeight = towerHeight;

            for (int i = 0; i < TowersCount; i++)
            {
                Towers.Add(new Tower(towerHeight));
            }

            for (int i = towerHeight; i > 1; i--)
            {
                Towers[0].Discs.Add(new Disc(i - 1));
            }
        }

        public void SetInvalidState()
        {
            if (State == GameState.Initial)
            {
                State = GameState.InvalidInputTower;
            }
            if (State == GameState.DiskInHand)
            {
                State = GameState.InvalidDestinationTower;
            }
        }


        public void MakeMove(int towerNumber)
        {
            if (State == GameState.Initial || State == GameState.NoDisksInSelectedTower || State == GameState.InvalidInputTower)
            {
                var tower = Towers[towerNumber - 1];
                if (tower.Discs.Count == 0)
                {
                    State = GameState.NoDisksInSelectedTower;
                    return;
                }
                else
                {
                    State = GameState.DiskInHand;
                    ActiveDisc = tower.Discs.Last();
                    tower.Discs.Remove(ActiveDisc);
                    TowerFromNumber = towerNumber;
                    SetActiveTower(tower);
                    return;
                }
            }
            
            if (State == GameState.DiskInHand || State == GameState.DiskDoesNotFit || State == GameState.InvalidDestinationTower)
            {
                var tower = Towers[towerNumber - 1];
                

                if (tower.Discs.Any() && ActiveDisc.Size > tower.Discs.Last().Size)
                {
                    State = GameState.DiskDoesNotFit;
                    return;
                }

                tower.Discs.Add(ActiveDisc);
                State = GameState.Initial;
                
                Moves++;
                TowerToNumber = towerNumber;
                Logger.Log(GetLogData());
                ActiveDisc = null;
                SetActiveTower(tower);
                IsWon();
            }
        }

        public string[] GetLogData()
        {
            string[] dataToString = new string[9];
            var discLocationMap = GetDiscLocationMap();

            dataToString[0] = StartDate.ToString("yyyy-MM-dd HH:mm");
            dataToString[1] = Moves.ToString();
            dataToString[2] = discLocationMap[0].ToString();
            dataToString[3] = discLocationMap[1].ToString();
            dataToString[4] = discLocationMap[2].ToString();
            dataToString[5] = discLocationMap[3].ToString();
            dataToString[6] = ActiveDisc.Size.ToString(); 
            dataToString[7] = TowerFromNumber.ToString();
            dataToString[8] = TowerToNumber.ToString();
            return dataToString;
        }

        public void DrawTower()
        {
            var width = 10;

            var builder = new StringBuilder();

            for (int i = 0; i < TowerHeight; i++)
            {
                for (int j = 0; j < TowersCount; j++)
                {
                    if (j == 0)
                    {
                        Console.Write($"{i + 1}eil.");
                    }

                    var currentTower = Towers[j];
                    var diskIndex = currentTower.Height - i - 1;

                    builder.Clear();

                    if (diskIndex < currentTower.Discs.Count)
                    {
                        var currentDisc = currentTower.Discs[diskIndex];
                        builder.Append(' ', width - currentDisc.Size);
                        builder.Append(currentDisc.ToString());
                        builder.Append(' ', width - currentDisc.Size - 2 + 1);
                        Console.Write(builder.ToString());
                    }
                    else
                    {
                        builder.Append(' ', width).Append('|').Append(' ', width - 1);
                        Console.Write(builder.ToString());
                    }
                }
                Console.WriteLine();
            }

            builder.Clear();
            builder.Append(' ', 5);

            for (int i = 0; i < TowersCount; i++)
            {
                var tower = Towers[i];
                if (tower.IsActive)
                    {
                        builder.Append('-', width - 3);
                        builder.Append($"^^[{i + 1}]^^");
                        builder.Append('-', width - 4);
                    }
                    else
                    {
                builder.Append('-', width - 1);
                builder.Append($"[{i + 1}]");
                builder.Append('-', width - 2);

                    }
            }

            //Console.Write(builder.ToString());

            //builder.Clear();
            //Console.WriteLine();

            //builder.Append(' ', 5);

            //foreach (var tower in Towers)
            //{
            //    builder.Append(' ', width - 1);
            //    if (tower.IsActive)
            //    {
            //        builder.Append($"^^[{i + 1}]^^");
            //    }
            //    else
            //    {
            //        builder.Append(' ', 3);
            //    }

            //    builder.Append(' ', width - 2);
            Console.Write(builder.ToString());
            }
        


        public void SetActiveTower(Tower tower)
        {
            foreach (var t in Towers)
            {
                t.IsActive = false;
            }

            tower.IsActive = true;
            if (ActiveDisc == null)
            {
                tower.IsActive = false;
            }
        }

        private void IsWon()
        {
            if (Towers[2].Discs.Count() == 4)
            {
                State = GameState.Won;
                
            }
                
        }
        private int[] GetDiscLocationMap()
        {
            int[] discLocation = new int[4];

            for (int j = 0; j < TowersCount; j++)
            {
                var currentTower = Towers[j];
                foreach (var disk in currentTower.Discs)
                {
                    discLocation[disk.Size - 1] = (j + 1);
                }
            }
            return discLocation;
        }
    }

}
