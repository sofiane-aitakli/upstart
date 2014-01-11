using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using UpStart.Model.Helper;

namespace UpStart.Scrapper.Archive.arabophone
{
    public class AlJazeera
    {
        static string Arabi =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:1E011A2F32674807ADE84C3C6D414D67|Channel:84AD340701C24F699597D698D2547472%29";

        static String douwali =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:B1ED88A7E5D54F60B88CC8F847DEF585|Channel:F6B13CE485A6494CBF553AA05B2DAA04%29";

        static String sport =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:378F4226808B4B388268E21E1570B3E9|Channel:55285935E87F4199A2C99F83E489E6BA%29&sort=date:D:R:d1";

        static String culture =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:C2D958DF06DB4452B56D4CE7A8D08FF8|Channel:A72E90ED1CE6471393EFA98E7B8B8283%29&sort=date:D:R:d1";

        static String sante =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:8458B8E4F80549BE80790BC892D68163|Channel:8BCADC5517DA4F529CE8D45D46BCAA0C%29&sort=date:D:R:d1";

        static String autre =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:0E2812CA80624B0296423954321C12B5|Channel:6321324B2B7043A9A8911A1072EA5C48%29&sort=date:D:R:d1";

        static String interview =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:8D3974C3AA0C4C939B8CE932A4749660|Channel:5D97E418DB6A4E83A5112E846BEEC7CB%29&sort=date:D:R:d1";

        static String presse =
            "&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:AF115CF9D37E474591CA27281D5314BC|Channel:5B40659874834467AFF4E188D5C95177%29&sort=date:D:R:d1";

        HtmlWeb webGet = new HtmlWeb();
        WebClient webClient=new WebClient();
        private string path = "D:/OpenSourceIntelligence/AlJazzera/";
         string RootPath2="&client=default_frontend&output=xml_no_dtd&start=0&lr=lang_ar&sort=date%3AD%3AL%3Ad1";

        private static Dictionary<int, string> site = new Dictionary<int, string>()
            {
                {1, Arabi},
                {2, douwali},
                {3, sport},
                {4, culture},
                {5, sante},
                {6, autre},
                {7, interview},
                {8, presse}
            };

        private static Dictionary<int, string> localisation = new Dictionary<int, string>()
            {
                {1, "Arabe"},
                {2, "autre"},
                {3, "culture"},
                {4, "international"},
                {5, "interview"},
                {6, "Presse"},
                {7, "sante"},
                {8, "Sport"}
            };

        private static Dictionary<int, string> DateActuelle =new Dictionary<int, string>()
            {
                {1, "DateArabe"},
                {2, "DateAutre"},
                {3, "DateCulture"},
                {4, "DateInternational"},
                {5, "DateInterview"},
                {6, "DatePresse"},
                {7, "DateSante"},
                {8, "DateSport"}
            };

        private int _enu = 1;
        private DateTime now = DateTime.Now;
        private static string RootPath1 = "http://search.aljazeera.net/search?q=*++daterange%3A";
        private static readonly string PathImage = "http://www.aljazeera.net/"; 
        public AlJazeera()
        {
            
            var datedebut = new DateTime(2000, 11, 18).Date;
            var datefin = new DateTime(2000, 11, 18).Date;

            while (datefin <= DateTime.Now)
            {
                for (int i = 1; i < 9; i++)
                {

                    var rootdate = datedebut.ToString("yyyy-MM-dd") + ".. " + datefin.ToString("yyyy-MM-dd");
                    var url = RootPath1 + rootdate + site[i];
                    var source = webClient.DownloadString(url);
                    //var xmlelement = new XmlDocument();
                    var elements=XElement.Parse(source);
                    var listeUrl = from item in elements.Descendants("U")
                                   select item.Value;

                    foreach (var urle in listeUrl)
                    {
                        GetPage(urle);
                    }
                }

                
                datefin = datefin.AddDays(1);
            }
        }


        Boolean GetPage(String url)
        {
            var page = webGet.Load(url);

            var extractionExtractCustomClass = Extraction.ExtractCustomClass(page, "p", "detailedArticleTitle");
            if (extractionExtractCustomClass.Any())
            {
                var Titre = extractionExtractCustomClass.First().InnerText.Trim();
            }

            var article = Extraction.ExtractClass(page, "level2Font").First().InnerText.Trim();


            var image = Extraction.ExtractCustom(Extraction.ExtractClass(page, "level2Font").First(), "img");
            var urlImage = image.First().GetAttributeValue("src","");
            webClient.DownloadFile(PathImage+urlImage,"");
            return true;
        }
    }
}
/*
 
 # -*- coding: utf-8 -*-
import codecs
import os
import datetime
import urllib
from lxml.html import  fromstring



def download(url,nom,libelle,path,fichierDate):
    """Copy the contents of a file from a given URL
     to a local file.
     """
    webFile = urllib.urlopen(url)
    print webFile.read()

    localFile = codecs.open(path+libelle+"/source/date_" +nom.strftime("%d_%m_%y")+'.art', 'w',encoding='utf-8')
    
    
    
    
    localFile.write(webFile.read())
    print webFile.read()
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
    
    
                    filename="AlJazzera/"+libelle+"/"+str(l)+"/date_"+nom+"_%d"%i+".art"
                    
                    
                    
                    localFile = open(filename ,'w')
                    fileLocal=f.read();
                    localFile.write(fileLocal)
                    localFile.close()
                    subSoup = fromstring(fileLocal)
                    cssSubSelector='.signature'
                    if(len(subSoup.cssselect(cssSubSelector))<>0):
                        print subSoup.cssselect(cssSubSelector)[0].text
                        filenameAuteur="data/ElWatan/"+libelle+"/"+str(l)+"/date_"+nom+"_%d"%i+"_auteur.art"
                        localFileAuteur = open(filenameAuteur ,'w')
                        localFileAuteur.write(subSoup.cssselect(cssSubSelector)[0].text)
                        localFileAuteur.close()
                    cssMessageSelector='#2REACS'
                    if(len(subSoup.cssselect(cssMessageSelector))<>0):
                        print subSoup.cssselect(cssMessageSelector)[0].contents
                        filenameMessage="data/ElWatan/"+libelle+"/"+str(l)+"/date_"+nom+"_%d"%i+"_Commentaire.art"
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



Arabi= '&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:1E011A2F32674807ADE84C3C6D414D67|Channel:84AD340701C24F699597D698D2547472%29'
douwali= '&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:B1ED88A7E5D54F60B88CC8F847DEF585|Channel:F6B13CE485A6494CBF553AA05B2DAA04%29'
sport='&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:378F4226808B4B388268E21E1570B3E9|Channel:55285935E87F4199A2C99F83E489E6BA%29&sort=date:D:R:d1'
culture='&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:C2D958DF06DB4452B56D4CE7A8D08FF8|Channel:A72E90ED1CE6471393EFA98E7B8B8283%29&sort=date:D:R:d1'
sante='&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:8458B8E4F80549BE80790BC892D68163|Channel:8BCADC5517DA4F529CE8D45D46BCAA0C%29&sort=date:D:R:d1'
autre='&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:0E2812CA80624B0296423954321C12B5|Channel:6321324B2B7043A9A8911A1072EA5C48%29&sort=date:D:R:d1'
interview='&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:8D3974C3AA0C4C939B8CE932A4749660|Channel:5D97E418DB6A4E83A5112E846BEEC7CB%29&sort=date:D:R:d1'
presse ='&requiredfields=searchablePC:searchablePC.%28Site:4E190DB91C9243A3A2098E5DE79CC343|Site:57258A24E91A42B0B2399DCCF49CF8C1%29.%28Channel:AF115CF9D37E474591CA27281D5314BC|Channel:5B40659874834467AFF4E188D5C95177%29&sort=date:D:R:d1'

RootPath1='http://search.aljazeera.net/search?q=*++daterange%3A'


path="D:/OpenSourceIntelligence/AlJazzera/"
RootPath2='&client=default_frontend&output=xml_no_dtd&start=0&lr=lang_ar&sort=date%3AD%3AL%3Ad1'
site={1:Arabi,2:douwali,3:sport,4:culture,5:sante,6:autre,7:interview,8:presse}
localisation={1:"Arabe",2:"autre",3:"culture",4:"international",5:"interview",6:"Presse",7:"sante",8:"Sport"}
DateActuelle={1:"DateArabe",2:"DateAutre",3:"DateCulture",4:"DateInternational",5:"DateInterview",6:"DatePresse",7:"DateSante",8:"DateSport"}


enum=1
now = datetime.datetime.today()
while(enum<9) :
   
    fichierDate=path+DateActuelle[enum]+'.txt'
    localFile = open(fichierDate ,'r')
    temp= localFile.read()
    deltatime=datetime.timedelta(days=1);
    localFile.close()

    datedebut=datetime.datetime.strptime(temp, "%d/%m/%y")
    dateFin=datedebut+deltatime
    RootDate=str(datedebut.date())+"..+"+str(dateFin.date())
    print RootDate
    
    print datedebut 
    while(datedebut.date()<datetime.date.today()):
        if( not os.path.exists(path+str(localisation[enum])+"/source/date_" +datedebut.isoformat()+'.art')):
            Url=RootPath1+RootDate+site[enum]
            print Url
            download(Url,datedebut,localisation[enum],path,fichierDate)
            print Url
            print("done");
       
          
        else :
            print "exist"
    
        if( os.path.exists(path+str(localisation[enum])+"/source/date_" +datedebut.isoformat()+'.art')):
            
            
            extractlink("data/AlJazzera/"+str(localisation[enum])+"/source/date_" +datedebut.isoformat()+'.art',datedebut,datedebut.isoformat(),localisation[enum],fichierDate);
        else: 
            print "the source doesn't exist " 
    
    
        datedebut=datedebut+deltatime
    if(datedebut<datetime.date.today()+deltatime):
        enum=enum+1;
        print enum

 
 
 
 */