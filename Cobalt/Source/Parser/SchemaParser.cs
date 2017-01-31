using Cobalt.Data;
using Cobalt.Enums;
using Cobalt.TFItems;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cobalt.Parser
{
    class Root
    {
        public Result result { get; set; }
    }

    class Result
    {
        public List<Items> items { get; set; }
        public List<Attributes> attributes { get; set; }
    }

    class Items
    {
        public string name { get; set; } //Def Name
        public int defindex { get; set; } //Def Index
        public string item_class { get; set; } //Item Class
        public string item_name { get; set; } //Local Name
        public string item_slot { get; set; } //Item Slot Raw
        public int item_quality { get; set; } //Item Quality
        public string image_url { get; set; } //Image URL

        public List<string> used_by_classes { get; set; }
        public List<DefaultAttribute> attributes { get; set; }
    }

    class DefaultAttribute
    {
        public string name { get; set; }
        public float value { get; set; }
    }

    class Attributes
    {
        public string name { get; set; }
        public int defindex { get; set; }
        public string description_format { get; set; }
        public string effect_type { get; set; }
    }

    public class SchemaParser
    {
        //API 주소
        //추후 상수가 아닌 변수로 변경할것
        public string API_URL;

        //초기화
        public SchemaParser()
        {
            API_URL = String.Format(Properties.Settings.Default.Format_Schema,
                Properties.Settings.Default.API_KEY,
                Properties.Settings.Default.API_LANG);
        }

        //데이터 불러오기
        public async Task parse()
        {
            //JSON 파싱
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            String content = await wc.DownloadStringTaskAsync(API_URL);
            Root root = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Root>(content));

            //태그 DB 등록
            TFAttribute iAttribute;
            foreach (Attributes attribute in root.result.attributes)
            {
                iAttribute = new TFAttribute();
                iAttribute.Name = attribute.name;
                iAttribute.DefId = attribute.defindex;
                iAttribute.Description = attribute.description_format;
                //Attributes 추가
                ItemsData.Attributes.Add(iAttribute);
            }

            //아이템 DB 등록
            TFItem iItem;
            foreach (Items item in root.result.items)
            {
                //유효성 검사
                if((item.image_url == null || item.item_slot == null) //URL 없을시, Slot 없을시
                    || (!item.item_class.Contains("tf_weapon") && !item.item_class.Contains("tf_wearable") //무기와 의장이 아닐시
                    || item.item_slot.Contains("action"))) //액션 아이템일시
                    continue;

                //새 객체 생성
                iItem = new TFItem();
                iItem.Classname = item.item_class;
                iItem.Name = item.item_name;
                iItem.DefName = item.name;
                iItem.DefId = item.defindex;
                iItem.ImageURL = item.image_url;
                iItem.Quality = (ItemQuality)Enum.Parse(typeof(ItemQuality), item.item_quality.ToString());
                //iItem.ItemSlot = (ItemSlot)Enum.Parse(typeof(ItemSlot), item.item_slot);

                //Attributes 유효성 검사
                if (item.attributes != null)
                {
                    //객체에 Attributes 집어넣기
                    foreach (DefaultAttribute attribute in item.attributes)
                    {
                        //이름이 같은 Attributes 를 찾는다.
                        iItem.addAttribute(ItemsData.Attributes.Find(x => x.Name.Equals(attribute.name)), attribute.value);
                    }
                }
                //아이템 추가
                ItemsData.Items.Add(iItem);
            }
        }
    }
}
