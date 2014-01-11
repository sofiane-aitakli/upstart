using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UpStart.Scrapper;
using UpStart.Scrapper.Archive;
using UpStart.Scrapper.Archive.Francophone;
using UpStart.Scrapper.Archive.arabophone;

namespace UpStart.Test
{
    [TestFixture]
    public class ScrawlerTest
    {
        [Test]
        public void ArticleDeEchoruouk()
        {
            InitDAL.Init();

            var AlJazeera = new AlJazeera();
          
            Assert.AreEqual(1,1);
        }


        [Test]
        public void ArticleElKhabar()
        {
            InitDAL.Init();
            var AlKhabar = new AlKhabar();
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void ArticleDeWatan()
        {
            InitDAL.Init();

            var watan = new ElWatan();
            Assert.AreEqual(1, 1);
        }
    }
}
