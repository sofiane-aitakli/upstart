using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using HtmlAgilityPack;
using UpStart.Model;
using UpStart.Model.Helper;

namespace UpStart.Scrapper.Archive.arabophone
{
    public class Echourouk
    {
        List<String> BrokenLink=new List<string>();
        HtmlWeb webGet = new HtmlWeb();
        static string actualite = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MToiMyI7fXM6MjQ6InNlYXJjaF9hdXRob3JfZXhhY3RfbmFtZSI7czoxOiIxIjtzOjE0OiJnZW5lcmljX3NlYXJjaCI7YTowOnt9fXM6MTQ6InNlYXJjaF9zb3J0X2J5IjtzOjk6Im9yZGVyX251bSI7czoxMjoic2VhcmNoX29yZGVyIjtzOjEwOiJkZXNjZW5kaW5nIjtzOjY6Im9mZnNldCI7aTowO3M6MTQ6InNlYXJjaF9hcmNoaXZlIjtpOjE7czoxODoic2VhcmNoX2RvX2FkdmFuY2VkIjtiOjE7fQ&pg=";
        static string regional = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MToiNCI7fXM6MjQ6InNlYXJjaF9hdXRob3JfZXhhY3RfbmFtZSI7czoxOiIxIjtzOjE0OiJnZW5lcmljX3NlYXJjaCI7YTowOnt9fXM6MTQ6InNlYXJjaF9zb3J0X2J5IjtzOjk6Im9yZGVyX251bSI7czoxMjoic2VhcmNoX29yZGVyIjtzOjEwOiJkZXNjZW5kaW5nIjtzOjY6Im9mZnNldCI7aTowO3M6MTQ6InNlYXJjaF9hcmNoaXZlIjtpOjE7czoxODoic2VhcmNoX2RvX2FkdmFuY2VkIjtiOjE7fQ&pg=";
        static string economie = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MToiNSI7fXM6MjQ6InNlYXJjaF9hdXRob3JfZXhhY3RfbmFtZSI7czoxOiIxIjtzOjE0OiJnZW5lcmljX3NlYXJjaCI7YTowOnt9fXM6MTQ6InNlYXJjaF9zb3J0X2J5IjtzOjk6Im9yZGVyX251bSI7czoxMjoic2VhcmNoX29yZGVyIjtzOjEwOiJkZXNjZW5kaW5nIjtzOjY6Im9mZnNldCI7aTowO3M6MTQ6InNlYXJjaF9hcmNoaXZlIjtpOjE7czoxODoic2VhcmNoX2RvX2FkdmFuY2VkIjtiOjE7fQ&pg=";
        static string international = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MToiNyI7fXM6MjQ6InNlYXJjaF9hdXRob3JfZXhhY3RfbmFtZSI7czoxOiIxIjtzOjE0OiJnZW5lcmljX3NlYXJjaCI7YTowOnt9fXM6MTQ6InNlYXJjaF9zb3J0X2J5IjtzOjk6Im9yZGVyX251bSI7czoxMjoic2VhcmNoX29yZGVyIjtzOjEwOiJkZXNjZW5kaW5nIjtzOjY6Im9mZnNldCI7aTowO3M6MTQ6InNlYXJjaF9hcmNoaXZlIjtpOjE7czoxODoic2VhcmNoX2RvX2FkdmFuY2VkIjtiOjE7fQ&pg=";
        static string sport = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiMzciO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg";
        static string sportLocal = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiMzgiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg";
        static string sportInter = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiNzkiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg";
        static string faitDivers = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiODAiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg";
        static string avis = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiMjAiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string autre = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiNjEiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string societe = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiNTYiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string cultureArt = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiNTciO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string civilisation = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiNTgiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string dossier = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiODYiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string chouroukiete = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiODciO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        static string dernier = "http://www.echoroukonline.com/ara/archive/index.html?search_options=YTo3OntzOjEyOiJzZWFyY2hfbGltaXQiO2k6MTA7czoxNDoic2VhcmNoX29wdGlvbnMiO2E6NTp7czoyMToic2VhcmNoX2NyZWF0ZWRfZmlsdGVyIjtpOjE7czoxOToic2VhcmNoX3N0YXR1c19saW1pdCI7aToxO3M6MTA6InNlYXJjaF9jaWQiO2E6MTp7aTowO3M6MjoiMTIiO31zOjI0OiJzZWFyY2hfYXV0aG9yX2V4YWN0X25hbWUiO3M6MToiMSI7czoxNDoiZ2VuZXJpY19zZWFyY2giO2E6MDp7fX1zOjE0OiJzZWFyY2hfc29ydF9ieSI7czo5OiJvcmRlcl9udW0iO3M6MTI6InNlYXJjaF9vcmRlciI7czoxMDoiZGVzY2VuZGluZyI7czo2OiJvZmZzZXQiO2k6MDtzOjE0OiJzZWFyY2hfYXJjaGl2ZSI7aToxO3M6MTg6InNlYXJjaF9kb19hZHZhbmNlZCI7YjoxO30&pg=";
        private static int k = 0;
        const string path = @"H:\Donnees\echourouk\";
        static Dictionary<int, string> site = new Dictionary<int, string>() { { 1, actualite }, { 2, regional }, { 3, economie }, { 4, international }, { 5, sport }, { 6, sportLocal }, { 7, sportInter }, { 8, faitDivers }, { 9, avis }, { 10, autre }, { 11, societe }, { 12, cultureArt }, { 13, civilisation }, { 14, dossier }, { 15, chouroukiete }, { 16, dernier } };
        static Dictionary<int, string> localisation = new Dictionary<int, string>() { { 1, "actualite" }, { 2, "regional" }, { 3, "economie" }, { 4, "international" }, { 5, "sport" }, { 6, "sportLocal" }, { 7, "sportInter" }, { 8, "faitDivers" }, { 9, "avis" }, { 10, "autre" }, { 11, "societe" }, { 12, "cultureArt" }, { 13, "civilisation" }, { 14, "dossier" }, { 15, "chouroukiete" }, { 16, "dernier" } };
        const string rootLink = "http://www.echoroukonline.com/ara/";
        private int i;
        public void Actualite()
        {



            i = 1;

            while (i < 17)
            {
                int j = 1;
                var url = site[i] ;

                while (true)
                {
                    try
                    {
                        var document = webGet.Load(url + j);

                        if (ReachedEnd(document))
                        {
                            i++;
                            j = 1;
                            break;
                        }
                        ExtractionLink(document, i);


                        var htmlcontent = document.DocumentNode.OuterHtml;

                        var stream = new System.IO.StreamWriter(path + localisation[i] + "\\Source\\00\\article_" + j);
                        stream.Write(htmlcontent);


                    }
                    catch (WebException e)
                    {
                        j++;
                        break;
                    }
                    j++;
                }
                if (i == 17)
                    break;


            }


        }

        private Boolean ReachedEnd(HtmlDocument document)
        {
            var temp = Extraction.ExtractCustomClass(document, "Span", "pagination_total");
            var text = temp.First().InnerText;
            var page = Regex.Match(text, "[0-9]+").Length;
            return page <= 0;
        }

        void ExtractionLink(HtmlDocument content, int i)
        {

            var classes = Extraction.ExtractLink(content, "short");




            foreach (var htmlNode in classes)
            {
                var link = htmlNode.First().GetAttributeValue("href", "NULL");
                HtmlDocument documentLink;
                try
                {
                    documentLink = webGet.Load(rootLink + link);
                }
                catch (Exception)
                {
                    

                    using (var uow = new UnitOfWork())
                    {
                        var articleProblem = new LaterArticle(uow);
                        articleProblem.Article = rootLink + link;
                        uow.CommitChanges();
                    }



                    break;
                }
                
                
                var cheminDd = path + localisation[i] + "\\00\\article_" + k + ".art";
                while (File.Exists(cheminDd))
                {
                    k++;
                    cheminDd = path + localisation[i] + "\\00\\article_" + k + ".art";
                }
                if (ExtractInformation(documentLink, cheminDd, localisation[i] + "\\00\\article_" + k + ".art"))
                {
                    var htmlcontent = documentLink.DocumentNode.OuterHtml;
                    var test = Extraction.ExtractID(documentLink, "dzarticleBody");
                    var list = Regex.Split(test.First().OuterHtml, "&");
                    string texte = list.Aggregate("", (current, s) => current + Regex.Replace(s, "#.*;", ""));

                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex(@"[ ]{2,}", options);
                    texte = regex.Replace(texte, @" ");
                    var stream = new System.IO.StreamWriter(cheminDd);
                    stream.Write(texte);
                    var imagePath = @"H:\Donnees\echourouk\"+localisation[i]+@"\Images\article_" + k + ".jpg"; 
                    var imageContent = Extraction.ExtractClass(documentLink, "image widthD");
                    if (imageContent != null && imageContent.Any()) 
                    {
                        var img=(imageContent.First().Descendants("img").First().GetAttributeValue("src",""));
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(@img, imagePath);
                    }

                  

                    
                }
                k++;
            }

        }

        public Boolean ExtractInformation(HtmlDocument content, string chemin, string nom)
        {
            var classes = Extraction.ExtractClass(content, "article_head");
            var head = Extraction.ExtractCustom(classes.First(), "h1");
            var titrePrincipale = head.First().InnerText;
            var list = Regex.Split(titrePrincipale, "&");
            string titrefinale = list.Aggregate("", (current, s) => current + Regex.Replace(s, "#.*;", ""));



            var dateClass = Extraction.ExtractClass(content, "article_information").First();
            var date = dateClass.Descendants("span").First().InnerText;
            var dateTime = DateTime.ParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            using (var uow = new UnitOfWork())
            {
                var exist = uow.FindObject<Article>(new BinaryOperator("Titre", titrefinale));
                if (exist != null) return false;
                var article = new Article(uow) {Oid=Guid.NewGuid(),DatePublication = dateTime, Path = nom, Titre = titrefinale};
                article.Support = uow.FindObject<Support>(new BinaryOperator("libelle", "echourouk"));
                #region switch type
                switch (i)
                {
                    case 1:
                        article.TypeArticle = TypeArticle.actualite;
                        break;
                    case 2:
                        article.TypeArticle = TypeArticle.regional;
                        break;
                    case 3:
                        article.TypeArticle = TypeArticle.economie;
                        break;
                    case 4:
                        article.TypeArticle = TypeArticle.international;
                        break;
                    case 5:
                        article.TypeArticle = TypeArticle.sport;
                        break;
                    case 6:
                        article.TypeArticle = TypeArticle.sportLocal;
                        break;
                    case 7:
                        article.TypeArticle = TypeArticle.sportInter;
                        break;
                    case 8:
                        article.TypeArticle = TypeArticle.faitDivers;
                        break;
                    case 9:
                        article.TypeArticle = TypeArticle.avis;
                        break;
                    case 10:
                        article.TypeArticle = TypeArticle.autre;
                        break;
                    case 11:
                        article.TypeArticle = TypeArticle.societe;
                        break;
                    case 12:
                        article.TypeArticle = TypeArticle.cultureArt;
                        break;
                    case 13:
                        article.TypeArticle = TypeArticle.civilisation;
                        break;
                    case 14:
                        article.TypeArticle = TypeArticle.dossier;
                        break;
                    case 15:
                        article.TypeArticle = TypeArticle.chouroukiete;
                        break;
                    case 16:
                        article.TypeArticle = TypeArticle.dernier;
                        break;

                }
                #endregion
                uow.CommitChanges();
                return true;

            }
        }

    }
}
