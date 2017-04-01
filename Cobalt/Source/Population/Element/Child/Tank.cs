namespace Cobalt.Population.Element
{
    public class Tank
    {
        public long Health;
        public string Name;
        public string ClassIcon;

        public bool Skin = false;
        public int Speed = 75;
        public Relay StartingPathTrackNode;
        public Relay OnKilledOutput;
        public Relay OnBombDroppedOutput;

        public Tank()
        {
            this.Health = 2500;
            this.Name = "Tank";
        }  
    }
}
