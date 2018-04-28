using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowaria.PopulationFile
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
        private int[] PropertiesInt;
        private bool[] PropertiesBool;
        public int WaveLength
        {
            get
            {
                return wavelength;
            }
        }

        Population(int wave_length)
        {
            PropertiesInt = new int[4] {400,8,2000,15};
            PropertiesBool = new bool[4] { false,false,false,false };
            wavelength = wave_length;
        }

        public static Population ReadFile()
        {
            int wave_length = 3;
            return new Population(wave_length);
        }

        public int GetPropertiesInt(PropInt prop)
        {
            return PropertiesInt[(int)prop];
        }
        public bool GetPropertiesBool(PropBool prop)
        {
            return PropertiesBool[(int)prop];
        }
        public void SetPropertiesInt(PropInt prop, int value)
        {
            PropertiesInt[(int)prop] = value;
        }
        public void SetPropertiesBool(PropBool prop, bool value)
        {
            PropertiesBool[(int)prop] = value;
        }
    }
}
