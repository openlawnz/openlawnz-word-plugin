using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon1();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.

using Word = Microsoft.Office.Interop.Word;
using System.Net;

namespace WordAddIn1
{
    internal class CitationSearchResultHits
    {
        public int found { get; set; }
        public int start { get; set; }
        public List<CitationSearchResultHit> hit { get; set; }
    }
    internal class CitationSearchResultHit {
        public string id { get; set; }
        public CitationDataFields fields { get; set; }
    }

    internal class CitationDataFields
    {
        public string case_id { get; set; }
        public string citation { get; set; }
    }

    internal class CitationSearchResult
    {
        public dynamic status { get; set; }
        public CitationSearchResultHits hits { get; set; }
    }

    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Ribbon1()
        {
        }

        

        private void parseCitation(Word.Range range, string value)
        {
            string text = range.Text;
           
            CitationSearchResult result;
            var webRequest = WebRequest.Create("https://jtme8klw2k.execute-api.ap-southeast-2.amazonaws.com/production?q=" + value) as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var searchResultJson = sr.ReadToEnd();
                    result = JsonConvert.DeserializeObject<CitationSearchResult>(searchResultJson);
                }
            }
            // For each match
            //content.Hyperlinks.Add()
            if (result.hits.found > 0)
            {
                range.Hyperlinks.Add(range, "https://www.openlaw.nz/case/" + result.hits.hit[0].fields.case_id);
            }
            
        }
        
        public MatchCollection matchRegex(string text)
        {
            return Regex.Matches(text, @"((?:\[\d{4}\]\s*)(?:(NZDC|NZFC|NZHC|NZCA|NZSC|NZEnvC|NZEmpC|NZACA|NZBSA|NZCC|NZCOP|NZCAA|NZDRT|NZHRRT|NZIACDT|NZIPT|NZIEAA|NZLVT|NZLCDT|NZLAT|NZSHD|NZLLA|NZMVDT|NZPSPLA|NZREADT|NZSSAA|NZSAAA|NZTRA))(?:\s*(\w{1,6})))");
        }

        public void OnAutoLinkCitations(Office.IRibbonControl control)
        {
            var footnotes = Globals.ThisAddIn.Application.ActiveDocument.Footnotes;
            var content = Globals.ThisAddIn.Application.ActiveDocument.Content;

            foreach(Word.Footnote footnote in footnotes)
            {

                var text = footnote.Range.Text;
                var matches = matchRegex(text);

                foreach (Match m in matches)
                {
                    parseCitation(footnote.Range, m.Value);
                }

                   
            }

            var contentMatches = matchRegex(content.Text);

            foreach (Match m in contentMatches)
            {
                //Word.Range range = Globals.ThisAddIn.Application.ActiveDocument.Range(m.Index, m.Index + m.Value.Length);
                //parseCitation(range, m.Value);


                Word.Range range = Globals.ThisAddIn.Application.ActiveDocument.Range(0, 0);
                if (range.Find.Execute(m.Value))
                {
                    parseCitation(range, m.Value);
                }
                /*
                // Do find for specific word
                content.Find.Text = m.Value;
                content.Find.Execute();
                while(content.Find.Found)
                {
                    // Highlight, then get range of highlight

                    Word.Range range = Globals.ThisAddIn.Application.ActiveDocument.Range(0, 0);
                    // Do find for specific word

                    if (range.Find.Execute(m.Value))
                    {
                        parseCitation(range, m.Value);
                    }

                    Word.Range range = Globals.ThisAddIn.Application.ActiveDocument.Range(0, 0);
                    if (range.Find.Execute("<table>"))
                    {
                        // range is now set to bounds of the word "<table>"
                    }

                    parseCitation(range, m.Value);
                }
                */

            }


        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("WordAddIn1.Ribbon1.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}








