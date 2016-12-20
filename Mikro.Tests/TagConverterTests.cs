using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Mikro.Tests
{
    [TestClass]
    public class TagConverterTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var content = "ostatnie <a href='/Tag/DisplayTagContent?tagId=konie'>#konie</a>";
            string html = string.Format("<html><head></head><body>{0}</body></html>", content);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            html.OptionAutoCloseOnEnd = true;

            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in doc.DocumentElement.SelectNodes("//a[@href]")
            {
                HtmlAttribute att = link["href"];
                att.Value = FixLink(att);
            }


        }
    }
}
