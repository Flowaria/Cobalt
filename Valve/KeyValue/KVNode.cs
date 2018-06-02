using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valve.KeyValue
{
    public class KVNode
    {
        public KVNode Parent { get; private set; }
        public KVNodeType Type { get; private set; }
        public string KeyName { get; private set; }
        private List<KVNode> Child { get; }
        private object value;
        public enum KVNodeType
        {
            Root,
            KeyValue,
            Parent,
            None
        }

        public KVNode(string name, bool root = false) //as root, parent
        {
            KeyName = name;
            if (root)
                Type = KVNodeType.Root;
            else
                Type = KVNodeType.None;
            Child = new List<KVNode>();
        }

        public KVNode(string name, params KVNode[] children) //as root, parent
        {
            KeyName = name;
            Type = KVNodeType.Parent;
            Child = new List<KVNode>();
            foreach(var c in children)
            {
                AppendNode(c);
            }
        }

        public KVNode(string name, object value) //as keyvalue
        {
            KeyName = name;
            Type = KVNodeType.KeyValue;
            this.value = value;
            Child = new List<KVNode>();
        }

        public void AppendNode(KVNode node)
        {
            if(Type != KVNodeType.Root)
                Type = KVNodeType.Parent;
            node.SetParent(this);
            value = null;
            if(node.Type == KVNodeType.KeyValue)
            {
                if (!Child.Exists(x=>x.KeyName.Equals(node.KeyName)))
                {
                    Child.Add(node);
                }
                else
                {
                    SetValue(node.GetValue(), node.KeyName);
                }
            }
            else
            {
                Child.Add(node);
            }
        }

        public void SetParent(KVNode node)
        {
            Parent = node;
        }

        public bool SetValue(object value, string key = null, bool create = true)
        {
            if (key == null && this.Type != KVNodeType.Parent && this.Type != KVNodeType.Root)
            {
                this.value = value;
                Type = KVNodeType.KeyValue;
                return true;
            }
            var bring = FindSingleNode(key, KVNodeType.KeyValue);
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

        public object GetValue(string key = null)
        {
            if (Type != KVNodeType.Root && Type != KVNodeType.Parent)
            {
                return value.ToString();
            }
            KVNode bring = FindSingleNode(key, KVNodeType.KeyValue);
            if (bring != null)
            {
                return bring.GetValue(null);
            }
            return null;
        }

        public KVNode FindSingleNode(string key, KVNodeType type)
        {
            try
            {
                return Child.Find(x => x.Type == type && x.KeyName.ToLower().Equals(key.ToLower()));
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
                return Child.FindAll(x => x.KeyName.ToLower().Equals(key.ToLower())).ToArray();
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
