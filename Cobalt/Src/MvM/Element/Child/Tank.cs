using System;

namespace Cobalt.MvM.Element
{
    public class Tank : Child
    {
        public Boolean Skin = false;
        public int Speed = 75;
        public string StartingPathTrackNode = "boss_tank_path_a1_01";
        public string OnKilledOutput = "boss_dead_relay:Trigger";
        public string OnBombDroppedOutput = "boss_deploy_relay:Trigger";

        public Tank()
        {
            this.Health = 2500;
            this.Name = "Tank";
        }  
    }
}
