using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace UpStart.Model.Helper
{
    public static class Extraction
    {






        public static  IEnumerable<IEnumerable<HtmlNode>> ExtractLink(HtmlDocument content,string name)
        {
            var classes = content.DocumentNode.Descendants("div")
                                 .Where(div => div.GetAttributeValue("class", "").Equals(name))
                                 .Select(div => div.Descendants("a"));
            return classes;
        }

        public static IEnumerable<HtmlNode> ExtractClass(HtmlDocument content, string name)
        {
            var classes = content.DocumentNode.Descendants("div")
                                 .Where(div => div.GetAttributeValue("class", "").Equals(name));
                                
            return classes;
        }


        public static IEnumerable<HtmlNode> ExtractID(HtmlDocument content, string name)
        {
            var classes = content.DocumentNode.Descendants("div")
                                 .Where(div => div.GetAttributeValue("id", "").Equals(name));

            return classes;
        }



        public static IEnumerable<HtmlNode> ExtractCustom(HtmlNode content, string attribute)
        {
            var classes = content.Descendants(attribute);
            return classes;
        }

        public static IEnumerable<HtmlNode> ExtractCustomClass(HtmlDocument content, string tag, string Class)
        {
            var classes = content.DocumentNode.Descendants(tag)
                                 .Where(tage => tage.GetAttributeValue("class", "").Equals( Class));

            return classes;
        }



        
    }
}
