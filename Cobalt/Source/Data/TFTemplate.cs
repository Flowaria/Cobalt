using Cobalt.Enums;
using Cobalt.Population.Element;
using System.Collections.Generic;

namespace Cobalt.Data
{
    public class TFTemplate
    {
        //Static 템플릿 파일 목록
        public static List<TFTemplate> Templates = new List<TFTemplate>();

        public static void AddTemplate(TFTemplate template)
        {
            if (!Templates.Exists(x => x.FileName.Equals(template.FileName)))
            {
                Templates.Add(template);
            }
        }

        //
        public TFTemplate(string filename, bool official)
        {
            FileName = filename;
            IsOfficial = official;
        }

        private static List<TFBot> Bots = new List<TFBot>();
        public string FileName = "robot_standard.pop";
        public bool IsOfficial = false;

        public void AddTemplate(string name, TFBot bot)
        {
            bot.BaseTemplate = name;
            bot.BaseTemplateFile = FileName;
            if(!Bots.Exists(x => x.BaseTemplate.Equals(name)))
            {
                Bots.Add(bot);
            }
        }

        public void RemoveTemplate(string name)
        {
            Bots.RemoveAll(x => x.BaseTemplate.Equals(name));
        }

        public List<TFBot> querry(TFClass filter_Class, string filter_ContainName, bool filter_Gatebot)
        {
            return new List<TFBot>();
        }
    }
}
