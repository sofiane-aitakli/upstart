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

namespace UpStart.Scrapper.Archive.Francophone
{
    public class ElWatan
    {
        readonly HtmlWeb webGet = new HtmlWeb();
        private static int k = 0;


        private static int j = 0;
        private static DateTime date;
        static Dictionary<int, string> site=new Dictionary<int, string>() {{1,"&rub=ew:w:une:actualite"},{2,"&rub=ew:w:une:culture"},{3,"&rub=ew:w:une:economie"},{4,"&rub=ew:w:une:international"},{5,"&rub=ew:w:regions:actu-regions"},{6,"&rub=ew:w:une:sports"},{7,"&rub=ew:w:regions:kabylie:actu-kabylie"}};
        static Dictionary<int, string> localisation=new Dictionary<int, string>(){{1,"actualite"},{2,"culture"},{3,"economie"},{4,"internationl"},{5,"regional"},{6,"sport"},{7,"kabyle"}};
        static Dictionary<int, string> DateActuelle = new Dictionary<int, string>() { { 1, "Dateactualite" }, { 2, "Dateculture" }, { 3, "Dateeconomie" }, { 4, "Dateinternationl" }, { 5, "Dateregional" }, { 6, "Datesport" }, { 7, "Datekabyle" } };
        const string path = @"H:\Donnees\elWatan";
        private int category = 1;

        public ElWatan()
        {
           while (category < 8)
           {
               var pathSec = @"H:\Donnees\elWatan\" + localisation[category];
               pathSec+=@"\";
               pathSec+= DateActuelle[category] + ".txt";
               string text = System.IO.File.ReadAllText(pathSec);

               string simpleTime = text;
               date = DateTime.Parse(simpleTime);
               while (date<DateTime.Now)
               {
                   var pathSource=@"H:\Donnees\elWatan\" + localisation[category];
                   pathSource += @"\source\";
                   pathSource += j + ".art";

                   if (!File.Exists(pathSource))
                   {
                       var url = string.Format("http://www.elwatan.com/archives/rubrique.php?ed={0}{1}", date.ToString("yyyy-MM-dd"), site[category]);
                       var document=Download(url, pathSource);
                       ExtractArticle(document, localisation[category]);

                   }
                   date = date.AddDays(1);
                   j++;
               }
           }
        }



        private void ExtractArticle(HtmlDocument document,string category)
        {
            var content = Extraction.ExtractClass(document, "ventre-col-1").First();
            var classes1 = Extraction.ExtractCustom(content, "h1");
            var pathSource = @"H:\Donnees\elWatan\" + category;
            pathSource += @"\"+k + ".art";
            foreach (var htmlNode in classes1)
            {
                var url = htmlNode.Descendants("a").First().GetAttributeValue("href", string.Empty);
                var item = Download("http://www.elwatan.com" + url, pathSource);

                var titrefinale = Extraction.ExtractClass(item, "article").First();
                var titres = Extraction.ExtractCustom(content, "h1");

               /* using (var uow = new UnitOfWork())
                {
                    var exist = uow.FindObject<Article>(new BinaryOperator("Titre", titrefinale));
                    if (exist != null) return false;
                    var article = new Article(uow) { DatePublication = dateTime, Path = nom, Titre = titrefinale };
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
                   

                }
                */
                k++;
            }
            
            


            var classes2 = Extraction.ExtractCustom(content, "h2");


            var classes3 = Extraction.ExtractCustom(content, "h3");

            var classes4 = Extraction.ExtractCustom(content, "h4");
        }


        /*

    
        if( os.path.exists(path+str(localisation[enum])+"/source/date_" +date.isoformat()+'.art')):
            
            
            extractlink(path+str(localisation[enum])+"/source/date_" +date.isoformat()+'.art',date,date.isoformat(),localisation[enum],fichierDate);
        else: 
            print "the source doesn't exist " 
    
    
        date=date+deltatime
    if(date<datetime.date.today()+deltatime):
        enum=enum+1;
        print enum
 * */


        public HtmlDocument Download(string url, string path)
        {


            try
            {
                var document = webGet.Load(url );
                var stream = new StreamWriter(path);
                stream.Write(document.DocumentNode.OuterHtml);
                j++;
                return document;
            }
            catch (WebException e)
            {
                using (var uow = new UnitOfWork())
                {
                    var articleProblem = new LaterArticle(uow);
                    articleProblem.Article = url;
                    uow.CommitChanges();
                }
                return null;
            }
            


        }

      
    }
}

/*
 * 
 * 
 * 
 
 
 # coding=utf-8
import os
import datetime
import urllib

from lxml.html import  fromstring



def download(url,nom,libelle):
    """Copy the contents of a file from a given URL
     to a local file.
     """
    webFile = urllib.urlopen(url)

    
    localFile = open(path+libelle+"/source/date_" +nom+'.art', 'w')
    
    
    

    localFile.write(webFile.read())
    webFile.close()
    localFile.close()




def extractlink(page,date,nom,libelle,localDate):
    """

    """
    print date
    l=1
    while(l<4):


        #print "*********************************************************"
        localFile = open(page, 'r')
        #print(localFile.read())
        soup = fromstring(localFile.read())
        i=0;
        cssSelector="h"+str(l)+" a"
        for link in soup.cssselect(cssSelector):
            





            if link.get('href')!= 'http://www.emploitic.com' :

                import urllib2
                opener = urllib2.build_opener()
                opener.addheaders.append(('Cookie', '__utma=2354788.1367889603.1342046542.1342208493.1342298745.11; __utmz=2354788.1342298745.11.3.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=el%20watan; WatanPreP_SESSION=1af742987eea43b51ebcaef1fe91caa5; WatanPreP_Acces_1=%7B%22Acces%22%3A%7B%22178374%22%3A%7B%22id_abonnement%22%3A%22178374%22%2C%22id_sso%22%3A%22178351%22%2C%22id_formule%22%3A%22IDENT%22%2C%22conso_initial%22%3Anull%2C%22conso_restant%22%3Anull%2C%22conso_duree%22%3Anull%2C%22conso_nbrecons%22%3Anull%2C%22date_debut%22%3A%2220120507182232%22%2C%22date_expiration%22%3A%2220120507182232%22%2C%22complement%22%3A%22%22%2C%22id_mouvement%22%3A%22178369%22%2C%22options%22%3A%22%22%2C%22date_creation%22%3A%2220120507182232%22%2C%22numero_abonnement%22%3A%22%22%7D%7D%7D; WatanPreP_SSO_1=%7B%22Infos%22%3A%7B%22id_sso%22%3A%22178351%22%2C%22civilite%22%3Anull%2C%22nom%22%3A%22rex%22%2C%22prenom%22%3A%22termandae%22%2C%22mail%22%3A%22rex%40yopmail.com%22%2C%22societe%22%3Anull%2C%22pseudo%22%3A%22rexade%22%2C%22type_compte%22%3A%22U%22%2C%22check_string%22%3A%22a89d9303e85bda1489df8f454cc9a0a2%22%7D%7D; rotation_index=0; __utmb=2354788.1.10.1342298745; __utmc=2354788'))
        
        
                try:
                    f = opener.open("http://www.elwatan.com"+link.get('href'))
                except urllib2.HTTPError, e:
                    print 'The server couldn\'t fulfill the request.'
                    print 'Error code: ', e.code
                except urllib2.URLError, e:
                    print 'We failed to reach a server.'
                    print 'Reason: ', e.reason
                else:
    
    
                    filename=path+libelle+"/"+str(l)+"/date_"+nom+"_%d"%i+".art"
                    
                    
                    
                    localFile = open(filename ,'w')
                    fileLocal=f.read();
                    localFile.write(fileLocal)
                    localFile.close()
                    subSoup = fromstring(fileLocal)
                    cssSubSelector='.signature'
                    if(len(subSoup.cssselect(cssSubSelector))<>0):
                        print subSoup.cssselect(cssSubSelector)[0].text
                        filenameAuteur=path+libelle+"/"+str(l)+"/date_"+nom+"_%d"%i+"_auteur.art"
                        localFileAuteur = open(filenameAuteur ,'w')
                        sig=subSoup.cssselect(cssSubSelector)[0]

                        localFileAuteur.write(sig.text.encode('utf8'))
                        localFileAuteur.close()
                    cssMessageSelector='#2REACS'
                    if(len(subSoup.cssselect(cssMessageSelector))<>0):
                        print subSoup.cssselect(cssMessageSelector)[0].contents
                        filenameMessage=path+libelle+"/"+str(l)+"/date_"+nom+"_%d"%i+"_Commentaire.art"
                        localMessage = open(filenameMessage ,'w')
                        localMessage.write(subSoup.cssselect(cssMessageSelector)[0].text)
                        localMessage.close()
                        
                    i=i+1;
    
        #webFile = urllib.urlopen("http://www.elwatan.com"+link.get('href'),cookie='__utma=2354788.1367889603.1342046542.1342208493.1342298745.11; __utmz=2354788.1342298745.11.3.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=el%20watan; WatanPreP_SESSION=1af742987eea43b51ebcaef1fe91caa5; WatanPreP_Acces_1=%7B%22Acces%22%3A%7B%22178374%22%3A%7B%22id_abonnement%22%3A%22178374%22%2C%22id_sso%22%3A%22178351%22%2C%22id_formule%22%3A%22IDENT%22%2C%22conso_initial%22%3Anull%2C%22conso_restant%22%3Anull%2C%22conso_duree%22%3Anull%2C%22conso_nbrecons%22%3Anull%2C%22date_debut%22%3A%2220120507182232%22%2C%22date_expiration%22%3A%2220120507182232%22%2C%22complement%22%3A%22%22%2C%22id_mouvement%22%3A%22178369%22%2C%22options%22%3A%22%22%2C%22date_creation%22%3A%2220120507182232%22%2C%22numero_abonnement%22%3A%22%22%7D%7D%7D; WatanPreP_SSO_1=%7B%22Infos%22%3A%7B%22id_sso%22%3A%22178351%22%2C%22civilite%22%3Anull%2C%22nom%22%3A%22rex%22%2C%22prenom%22%3A%22termandae%22%2C%22mail%22%3A%22rex%40yopmail.com%22%2C%22societe%22%3Anull%2C%22pseudo%22%3A%22rexade%22%2C%22type_compte%22%3A%22U%22%2C%22check_string%22%3A%22a89d9303e85bda1489df8f454cc9a0a2%22%7D%7D; rotation_index=0; __utmb=2354788.1.10.1342298745; __utmc=2354788')

        #print "*********************************************************"
    #for link in soup.find_all('a'):
    #    print(link)


        l=l+1;
    localFile = open(fichierDate ,'w')
    deltatime=datetime.timedelta(days=1);
    Tomorrow=date+deltatime
    localFile.write(Tomorrow.strftime('%d/%m/%y'))
    localFile.close()






    

 */