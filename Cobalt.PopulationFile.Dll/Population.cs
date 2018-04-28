using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowaria.Population
{
    public enum PropInt
    {
        StartingCurrency,
        RespawnWaveTime,
        AddSentryBusterWhenDamageDealtExceeds,
        AddSentryBusterWhenKillCountExceeds
    }
    public enum PropBool
    {
        FixedRespawnWaveTime,
        CanBotsAttackWhileInSpawnRoom,
        Advanced,
        EventPopfile
    }

    public class Population
    {
        private int wavelength = 0;
        private Properties<int> PropertiesInt;
        private Properties<bool> PropertiesBool;
        public int WaveLength
        {
            get
            {
                return wavelength;
            }
        }

        Population(int wave_length)
        {
            PropertiesInt = new Properties<int>(4, 400, 8, 2000, 15);
            PropertiesBool = new Properties<bool>(4, false, false, false, false);
            wavelength = wave_length;
        }

        public static Population ReadFile()
        {
            int wave_length = 3;
            return new Population(wave_length);
        }
    }
}
