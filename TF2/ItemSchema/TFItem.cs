using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Info;

namespace TF2.Items
{
    public class TFItem
    {
        private string classname;
        private int defid;
        private string name;
        private string displayname;
        private string image;
        private string image_url;
        private TFItemSlot slot;
        private bool[] allowedclass;
        public TFItem(string _classname, int _defid, string _name, TFItemSlot _slot, string _image, string _displayname, bool allclass)
        {
            classname = _classname;
            defid = _defid;
            name = _name;
            slot = _slot;
            image_url = _image;
            displayname = _displayname;
            if (allclass)
                allowedclass = new bool[10] { true, true, true, true, true, true, true, true, true, true };
            else
                allowedclass = new bool[10] { false, false, false, false, false, false, false, false, false, false };

            string[] splited = image_url.Split('/');
            var sp = splited[splited.Length - 1].Split('.');
            image = String.Join(".", sp[0], sp[2]);
        }

        public string GetClassname()
        {
            return classname;
        }

        public string GetName()
        {
            return name;
        }

        public int GetDefID()
        {
            return defid;
        }

        public TFItemSlot GetSlot()
        {
            return slot;
        }

        public string GetDisplayName()
        {
            return displayname;
        }

        public string GetImageURL()
        {
            return image_url;
        }

        public string GetImageName()
        {
            return image;
        }

        public void SetDisplayName(string newname)
        {
            displayname = newname;
        }

        public void UsedByClassToggle(TFClass c)
        {
            allowedclass[(int)c-1] ^= true;
        }

        public bool IsCosmetic()
        {
            return (slot == TFItemSlot.Misc);
        }

        public bool IsWearable()
        {
            return (classname.Contains("tf_wearable"));
        }

        public bool IsAllClass()
        {
            return (allowedclass[0] && allowedclass[1] && allowedclass[2]
                && allowedclass[3] && allowedclass[4] && allowedclass[5]
                && allowedclass[6] && allowedclass[7] && allowedclass[8]);
        }

        public void Debug()
        {
            Console.Out.WriteLine(String.Format("{0}:{1}:{2}", classname, name, displayname));
            Console.Out.WriteLine(String.Format(" {0}:{1}:{2}", defid, IsCosmetic(), IsWearable()));
        }
    }
}
