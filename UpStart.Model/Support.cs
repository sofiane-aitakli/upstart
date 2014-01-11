using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace UpStart.Model
{
    public class Support: XPBaseObject
    {

        public Support()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Support(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        [Key(AutoGenerate = true)]
        public Guid Oid;
        // Fields..

        private string _libelle;
        public string libelle
        {
            get
            {
                return _libelle;
            }
            set
            {
                SetPropertyValue("libelle", ref _libelle, value);
            }
        }



        [Association("Articles-Support")]
        public XPCollection<Article> Articles
        {
            get { return GetCollection<Article>("Articles"); }
        }

    }
}
