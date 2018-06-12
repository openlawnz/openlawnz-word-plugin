using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.IO;

namespace OpenLawNZ
{
   
    public partial class ThisAddIn
    {
        private UserControl1 myUserControl1;
        private Microsoft.Office.Tools.CustomTaskPane myCustomTaskPane;

        // https://stackoverflow.com/questions/43213777/custom-ribbon-xml-tab-not-showing-in-word-vsto-add-in?rq=1
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon1();
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

            myUserControl1 = new UserControl1();
            myCustomTaskPane = this.CustomTaskPanes.Add(myUserControl1, "OpenLaw NZ");
            myCustomTaskPane.Visible = false;
            myCustomTaskPane.DockPosition =
            Office.MsoCTPDockPosition.msoCTPDockPositionRight;
            myCustomTaskPane.Width = 450;

#if DEBUG
            string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            this.Application.Documents.Open(solutionPath + @"\TestFiles\Test.docx");
#endif
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}