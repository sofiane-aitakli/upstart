using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace UpStart.Model
{
    public class LaterArticle: XPBaseObject
    {

        public LaterArticle()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public LaterArticle(Session session)
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

         private string _Article;
        public string Article
        {
            get
            {
                return _Article;
            }
            set
            {
                SetPropertyValue("Article", ref _Article, value);
            }
        }


    }
}
