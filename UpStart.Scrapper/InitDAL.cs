using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using UpStart.Model;

namespace UpStart.Scrapper
{
    public static class InitDAL
    {
        public static void Init()
        {
            var connectionString = @"Data Source=SOFIANE-PC\EUREQUAT;Initial Catalog=DB_OSI;Integrated Security=SSPI;" + ";XpoProvider=MSSqlServer";

            DevExpress.Xpo.Metadata.XPDictionary dict =
            new DevExpress.Xpo.Metadata.ReflectionDictionary();
            // Initialize the XPO dictionary.
            dict.GetDataStoreSchema(typeof(Article), typeof(LaterArticle), typeof(Support));


            DevExpress.Xpo.DB.IDataStore store = DevExpress.Xpo.XpoDefault.GetConnectionProvider(connectionString,
                DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            DevExpress.Xpo.XpoDefault.DataLayer = new DevExpress.Xpo.ThreadSafeDataLayer(dict, store);
            DevExpress.Xpo.XpoDefault.Session = null;

            /*
            var support = new Support(new Session());
            support.libelle = "echourouk";
            support.Save();
            support = new Support(new Session());
            support.libelle = "el watan";
            support.Save();
             * */
        }

    }
}
