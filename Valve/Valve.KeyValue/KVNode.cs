using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valve.KeyValue
{
    public class KVNode
    {
        public KVNode Parent { get; set; }
        private KVNodeType type;
        public KVNodeType Type
        {
            get
            {
                return type;
            }
        }
        public string KeyName { get; set; }
        private List<KVNode> Child { get; }
        
        private object value;
        public enum KVNodeType
        {
            Root,
            String,
            Float,
            Int,
            Parent,
            None
        }

        public KVNode(string name, bool root = false) //as root, parent
        {
            KeyName = name;
            if (root)
                type = KVNodeType.Root;
            else
                type = KVNodeType.None;
            Child = new List<KVNode>();
        }
        public KVNode(string name, params KVNode[] children) //as root, parent
        {
            KeyName = name;
            this.type = KVNodeType.Parent;
            Child = new List<KVNode>();
            foreach(var c in children)
            {
                AppendNode(c);
            }
        }
        public KVNode(string name, string value) //as keyvalue
        {
            KeyName = name;
            this.type = KVNodeType.String;
            this.value = value;
            Child = new List<KVNode>();
        }
        public KVNode(string name, float value) //as keyvalue
        {
            KeyName = name;
            this.type = KVNodeType.Float;
            this.value = value;
            Child = new List<KVNode>();
        }
        public KVNode(string name, int value) //as keyvalue
        {
            KeyName = name;
            this.type = KVNodeType.Int;
            this.value = value;
            Child = new List<KVNode>();
        }

        public void AppendNode(KVNode node)
        {
            if(type != KVNodeType.Root)
                type = KVNodeType.Parent;
            node.Parent = this;
            if(node.Type != KVNodeType.Root && node.Type != KVNodeType.Parent)
            {
                if (!Child.Exists(x=>x.KeyName.Equals(node.KeyName)))
                {
                    Child.Add(node);
                }
            }
            else
            {
                Child.Add(node);
            }
        }

        public bool SetValue(int value, string key = null, bool create = true)
        {
            if (key == null)
            {
                this.value = value;
                type = KVNodeType.Int;
                return true;
            }
            var bring = FindNode(key, KVNodeType.Int);
            if (bring != null)
            {
                bring.SetValue(value);
                return true;
            }
            else if(create == true)
            {
                AppendNode(new KVNode(key, value));
            }
            return false;
        }

        public bool SetValue(float value, string key = null, bool create = true)
        {
            if (key == null && type != KVNodeType.Parent && type != KVNodeType.Root)
            {
                this.value = value;
                type = KVNodeType.Float;
                return true;
            }
            var bring = FindNode(key, KVNodeType.Float);
            if (bring != null)
            {
                bring.SetValue(value);
                return true;
            }
            else if (create == true)
            {
                AppendNode(new KVNode(key, value));
            }
            return false;
        }

        public bool SetValue(string value, string key = null, bool create = true)
        {
            if (key == null && type != KVNodeType.Parent && type != KVNodeType.Root)
            {
                this.value = value;
                type = KVNodeType.String;
                return true;
            }
            var bring = FindNode(key, KVNodeType.String);
            if (bring != null)
            {
                bring.SetValue(value);
                return true;
            }
            else if (create == true)
            {
                AppendNode(new KVNode(key, value));
            }
            return false;
        }

        public int GetInt(string key, int def = -1)
        {
            if (key == null && type == KVNodeType.Int)
            {
                return (int)value;
            }
            KVNode bring = FindNode(key, KVNodeType.Int);
            if (bring != null)
            {
                return bring.GetInt(null, def);
            }
            return def;
        }

        public float GetFloat(string key, float def = -1.0f)
        {
            if (key == null && type == KVNodeType.Float)
            {
                return (float)value;
            }
            KVNode bring = FindNode(key, KVNodeType.Float);
            if (bring != null)
            {
                return bring.GetFloat(null, def);
            }
            return def;
        }

        public string GetString(string key = null, string def = null)
        {
            if(key == null && type == KVNodeType.String)
            {
                return (string)value;
            }
            KVNode bring = FindNode(key, KVNodeType.String);
            if (bring != null)
            {
                return bring.GetString(null, def);
            }
            return def;
        }

        public string Get(string key = null)
        {
            if (type != KVNodeType.Root && type != KVNodeType.Parent)
            {
                return value.ToString();
            }
            KVNode bring = FindNode(key, KVNodeType.String);
            if (bring != null)
            {
                return bring.Get(null).ToString();
            }
            return null;
        }

        private KVNode FindNode(string key, KVNodeType type)
        {
            try
            {
                return Child.Find(x => x.Type == type && x.KeyName.Equals(key));
            }
            catch
            {
                return null;
            }
        }

        public KVNode[] FindNodes(string key)
        {
            try
            {
                return Child.FindAll(x => x.KeyName.Equals(key)).ToArray();
            }
            catch
            {
                return null;
            }
        }

        public KVNode[] FindNodes()
        {
            try
            {
                return Child.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public void DeleteThis()
        {
            if(Type != KVNodeType.Root)
            {
                Parent.Child.Remove(this);
            }
            foreach(var c in Child)
            {
                Delete(c);
            }
            
        }

        public void DeleteKey(string key)
        {
            foreach (var node in Child.FindAll(x => x.KeyName.Equals(key)))
            {
                Delete(node);
            }
        }

        public void Delete(KVNode node)
        {
            node.Parent = null;
            if(node.Type == KVNodeType.Parent)
            {
                node.DeleteThis();
            }
        }
    }
}
