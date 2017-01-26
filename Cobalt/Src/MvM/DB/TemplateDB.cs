using Cobalt.MvM.Element;
using System.Collections.Generic;
using System.IO;

namespace Cobalt.MvM.DB
{
    public class TemplateDB
    {
        //템플릿 보관
        private List<TFBot> m_Templates;

        //쿼리시 필터링
        private TFBot.TFClass[] filter_Class;
        private string filter_ContainName;
        private string filter_TemplateFile;
        private bool filter_Gatebot;

        public TemplateDB()
        {
            if (!Directory.Exists(Properties.Settings.Default.PATH_BASE))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PATH_BASE); //디렉토리 생성
                File.WriteAllText(Properties.Settings.Default.PATH_BASE+"robot_standard.pop",
                    Properties.Resources.robot_standard);
                File.WriteAllText(Properties.Settings.Default.PATH_BASE + "robot_giant.pop",
                    Properties.Resources.robot_giant);
                File.WriteAllText(Properties.Settings.Default.PATH_BASE + "robot_gatebot.pop",
                    Properties.Resources.robot_gatebot);
            }
                
            string[] files = Directory.GetFiles(Properties.Settings.Default.PATH_BASE);
            foreach(string file in files)
            {
                if (!file.EndsWith(".pop"))
                    continue;

                addFile(getFilename(file));
            }
        }

        //파일 추가
        public bool addFile(string name)
        {
            if(true)
                return false;
        }

        public TFBot[] querry()
        {
            return new TFBot[3];
        }

        public List<TFBot> querryAll(){return m_Templates;}

        private string getFilename(string url)
        {
            string[] splited = url.Split('/');
            return splited[splited.Length - 1];
        }
    }
}
