using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowaria.Population
{
    public class Properties<T>
    {
        private List<T> properties;
        
        public Properties(int propsize, params T[] value)
        {
            properties = new List<T>();
            foreach(T val in value)
            {
                properties.Add(val);
            }
        }
        public T GetProperties(int prop)
        {
            return properties[prop];
        }
        public void SetProperties(int prop, T value)
        {
            properties[prop] = value;
        }
    }
}
