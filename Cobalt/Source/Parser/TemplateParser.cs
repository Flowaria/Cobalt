using Cobalt.FileIO;
using Cobalt.Properties;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cobalt.Parser
{
    public class TemplateParser
    {
        public TemplateParser()
        {
            FileFunction.ExportString(Resources.robot_standard, Settings.Default.PATH_BASE, "robot_standard.pop");
            FileFunction.ExportString(Resources.robot_giant, Settings.Default.PATH_BASE, "robot_giant.pop");
            FileFunction.ExportString(Resources.robot_gatebot, Settings.Default.PATH_BASE, "robot_gatebot.pop");
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

        public async Task Parse(string content)
        {
            foreach (string line in content.Split('\n'))
            {
                //앞부분 탭과 주석 제거
                string cut = Regex.Replace(line, @"^\s*|\/\/.*", "");
                Match inString = Regex.Match(cut, @""".*""");
                if (inString.Success) //스트링 내부일경우
                {
                    //공간을 § 문자로 변경하고 "를 공백으로 변경
                    cut = cut.Replace(inString.Value, Regex.Replace(inString.Value, @"\s", "§").Replace(@"""", ""));
                }

                //공백 기준으로 Split
                bool isValue = false;
                foreach (string str in Regex.Split(cut, @"\s").Where(x => !string.IsNullOrEmpty(x)))
                {
                    string value = str.Replace("§", " ");
                }
            }
        }

        private string getFilename(string url)
        {
            string[] splited = url.Split('/');
            return splited[splited.Length - 1];
        }
    }
}
