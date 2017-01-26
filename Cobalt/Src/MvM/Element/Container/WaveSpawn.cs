using System;

namespace Cobalt.MvM.Element
{
    class WaveSpawn
    {
        public enum SquadType {Squad, TFBot, Tank, RandomChoice}

        public string Name;
        public string WaitForAllDead;
        public string WaitForAllSpawned;
        public short WaitBeforeStarting = 0;
        public short WaitBetweenSpawns = 0;

        public short TotalCount = 5;
        public short MaxActive = 5;
        public short SpawnCount = 5;

        public int TotalCurrency = 500;
        public string[] Where;

        public string FirstSpawnOutput = null;

        public bool Support = false;
        public SquadType SpawnerType = SquadType.TFBot;
        public Child[] Spawner;

        public WaveSpawn()
        {
            this.Spawner[0] = new Tank();
            if(Spawner[0] is Tank)
            {

            }
        }
    }
}
