﻿namespace Cobalt.MvM.Element
{
    class Mission
    {
        public enum Objective{DestroySentries/*SentryBuster*/, Spy, Sniper, Engineer}

        public short BeginAtWave = 1;
        public short RunForThisManyWaves = 3;
        public short InitialCooldown = 60;
        public short CooldownTime = 60;

        public short DesiredCount = 2;
        public string[] Where;

        public Child Spawner;

        public Mission()
        {

        }
    }
}
