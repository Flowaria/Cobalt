using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cobalt.Parser
{
    public class KeyValueNode
    {
        public bool isValue = false; //?
        public string Name = null;
        public string Value = null;
        public int Depth = 0;

        public KeyValueNode(string[] _Data, int _Depth)
        {
            Depth = _Depth;
            if(_Data.Length == 1)
            {
                isValue = false;
                Name = _Data[0];
            }
            else if (_Data.Length == 2)
            {
                isValue = true;
                Name = _Data[0];
                Value = _Data[1];
            }
        }
    }

    public class KeyValueParser
    {
        // * Keytype
        // * Value
        // **1 List = Parent, { }
        // **2 List = Key with value
        // * Key Step
        public List<KeyValueNode> read(string Content)
        {
            var parsedList = new List<string[]>();
            var resultList = new List<KeyValueNode>();

            //한줄에 붙어있는 {}의 구분
            Content = Content.Replace("{", "\n{\n").Replace("}", "\n}\n");
            foreach (string line in Content.Split('\n'))
            {
                //앞부분 공백과, 주석 그리고 끝부분 공백 제거
                string cut = Regex.Replace(line, @"^\s*|\s$", "");
                cut = Regex.Replace(cut, @"(//)+.*", "");

                //문자열 안처리
                Match strMatch = Regex.Match(cut, @"""[^""]*""|'[^']*'");
                while (strMatch.Success)
                {
                    //문자열안 공백을 § 문자로 변경하고 "를 공백으로 변경
                    string strt = Regex.Replace(strMatch.Value, @"\s", "§");
                    if (strMatch.Value.StartsWith(@"""") && strMatch.Value.EndsWith(@""""))
                        strt = strt.Replace(@"""", "");

                    else if (strMatch.Value.StartsWith("'") && strMatch.Value.EndsWith("'"))
                        strt = strt.Replace(@"'", "");
                    cut = cut.Replace(strMatch.Value, strt);
                    strMatch = strMatch.NextMatch(); //다음으로
                }


                //다음과 같은 처리를 거쳤을때 비는 문자열인지 확인
                if (!string.IsNullOrEmpty(cut))
                {
                    string[] result = Regex.Split(cut, @"\s+").Select(x => x.Replace("§", " ")).ToArray();
                    //빈 공간을 기준으로 나눈후 §문자를 띄어쓰기 처리
                    if (1 < result.Length || result.Length < 2)
                        parsedList.Add(result);
                    else
                        return null;
                }
                    
            }

            int step = 0;
            foreach(string[] value in parsedList)
            {
                if (value.Length == 1)
                {
                    if(value[0] == "{")
                    {
                        step++;
                    }
                    else if(value[0] == "}")
                    {
                        step--;
                    }
                    else
                    {
                        resultList.Add(new KeyValueNode(value, step));
                    }
                }
                else if ( value.Length == 2)
                {
                    resultList.Add(new KeyValueNode(value, step));
                }      
            }
            return resultList;
        }

        public List<KeyValueNode> readChilds(List<KeyValueNode> collection, KeyValueNode key)
        {
            var result = new List<KeyValueNode>();
            int i = collection.IndexOf(key) + 1;
            int d = key.Depth;
            Console.Write("Find in {0} : ", key.Name);
            while (i < collection.Count && d < collection[i].Depth) //파일 끝보다 아직 포인터가 작고 아직 해당 블록 안일때
            {
                if (collection[i].isValue)
                    Console.Write(" {0} {1} /", collection[i].Name, collection[i].Value);
                else
                    Console.Write(" {0} /", collection[i].Name);
                Console.WriteLine();
                result.Add(collection[i]);
                i++;
            }
            return result;
        }
    }
}
