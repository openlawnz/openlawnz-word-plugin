using Office = Microsoft.Office.Core;
using System.IO;
using System.Collections.Generic;
using Microsoft.Office.Tools;
using Microsoft.Office.Interop.Word;

namespace OpenLawNZ
{
   
    public partial class ThisAddIn
    {
		private Dictionary<int, CustomTaskPane> taskPanes = new Dictionary<int, CustomTaskPane>();

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
			string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
			
		}
		


		public CustomTaskPane ActivePane
		{
			get
			{
				if (!taskPanes.ContainsKey(Application.ActiveWindow.Hwnd))
				{
					UserControl1 taskPaneControl = new UserControl1();
					CustomTaskPane taskPanel = CustomTaskPanes.Add(taskPaneControl, "OpenLaw NZ", Application.ActiveWindow);
					taskPanel.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
					taskPanel.Width = 420;
					taskPanes[Application.ActiveWindow.Hwnd] = taskPanel;
				}
				return taskPanes[Application.ActiveWindow.Hwnd];
			}
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