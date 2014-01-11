using System.Net;
using HtmlAgilityPack;

namespace UpStart.Scrapper
{    
    public class EchouroukScrawler
    {

        EchouroukScrawler()
        {
            var url = "http://www.echoroukonline.com/ara/watani/";
            var webGet = new HtmlWeb();
            var document = default(HtmlDocument);
            try
            {
                document = webGet.Load(url);
                var htmlcontent = document.DocumentNode.OuterHtml;
                //var stream = new System.IO.StreamWriter(@"D:\echourouk\Actualite\article");
                //stream.Write(htmlcontent);
            }
            catch (WebException e)
            {
                var d = 2;
            }

            var temp = document;
        }


    }
}
