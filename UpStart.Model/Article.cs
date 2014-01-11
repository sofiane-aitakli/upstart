using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace UpStart.Model
{
    public class Article : XPBaseObject
    {

        public Article()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Article(Session session)
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

        
        private Guid _Oid;
        [Key]
        public Guid Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
            }
        }
        // Fields...

        private string _Titre;
        [Size(SizeAttribute.Unlimited)]
        public string Titre 
        {
            get
            {
                return _Titre;
            }
            set
            {
                SetPropertyValue("Titre", ref _Titre, value);
            }   
        }

        private string _Resume;
        public string Resume
        {
            get
            {
                return _Resume;
            }
            set
            {
                SetPropertyValue("Resume", ref _Resume, value);
            }
        }
        private string _SousTitre;
        public string SousTitre
        {
            get
            {
                return _SousTitre;
            }
            set
            {
                SetPropertyValue("SousTitre", ref _SousTitre, value);
            }
        }
        private DateTime _DatePublication;
        public DateTime DatePublication
        {
            get
            {
                return _DatePublication;
            }
            set
            {
                SetPropertyValue("DatePublication", ref _DatePublication, value);
            }
        }
        private List<String> _MotsCle;
        public List<String> MotsCle
        {
            get
            {
                return _MotsCle;
            }
            set
            {
                SetPropertyValue("MotsCle", ref _MotsCle, value);
            }
        }

        private String _Path;
        public String Path
        {
            get
            {
                return _Path;
            }
            set
            {
                SetPropertyValue("Path", ref _Path, value);
            }
        }


        private TypeArticle _typeArticle;
        public TypeArticle TypeArticle
        {
            get
            {
                return _typeArticle;
            }
            set
            {
                SetPropertyValue("TypeArticle", ref _typeArticle, value);
            }
        }



        private Support _Support;
        [Association("Articles-Support")]
        public Support Support
        {
            get
            {
                return _Support;
            }
            set
            {
                SetPropertyValue("Support", ref _Support, value);
            }
        }
    }
}
