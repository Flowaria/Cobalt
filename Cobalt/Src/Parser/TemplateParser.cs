using Cobalt.Enums;
using Cobalt.MvM.Element;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cobalt.Parser
{
    public class TemplateParser
    {
        //템플릿 보관
        private List<TFBot> m_Templates;

        //쿼리시 필터링
        private TFClass filter_Class;
        private string filter_ContainName;
        private string filter_TemplateFile;
        private bool filter_Gatebot;

        public TemplateParser()
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
        }

        public async Task parse()
        {
            string[] files = Directory.GetFiles(Properties.Settings.Default.PATH_BASE);
            foreach (string file in files)
            {
                if (!file.EndsWith(".pop"))
                    continue;

                await parseFile(getFilename(file));
            }
        }

        //파일 추가
        public async Task parseFile(string name)
        {
            if(true)
                return;
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
