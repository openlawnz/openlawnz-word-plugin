using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Net;
using System.Net.Http; 
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json.Linq;
using Microsoft.Office.Core;

namespace OpenLawNZ
{

	public partial class UserControl1 : UserControl
	{

		private static readonly HttpClient client = new HttpClient();

		private string LinkRef = "ref=OpenLawNZWordAddIn";

		private BindingSource resultsBindingSource = new BindingSource();
		
		async private Task<String> GraphQLQuery(Dictionary<string, string> values)
		{
			HttpResponseMessage response = await client.PostAsync("https://api.openlaw.nz/graphql", new FormUrlEncodedContent(values));
			return await response.Content.ReadAsStringAsync();
		}

		async private System.Threading.Tasks.Task ProcessLocalCitation(ResultDataGridItem gridItem, bool courtOfAppeal = false, bool appelant = false)
		{
			
			string apiResultString = await GraphQLQuery(new Dictionary<string, string>
			{
                 { "query","{ caseCitations(condition:{citation: \"" + gridItem.citation + "\"}) { case { pdf { pdfDbKey } } } }" } 
            });

			JObject apiResult = JObject.Parse(apiResultString);
			if (apiResult["data"]["caseCitations"].HasValues)
			{
                string fileName = (string)apiResult["data"]["caseCitations"][0]["case"]["pdf"]["pdfDbKey"];
                string FilePath = Directory.GetParent(Globals.ThisAddIn.Application.ActiveDocument.FullName).FullName;
				string RelativePath;
				if (courtOfAppeal)
				{
					if (appelant)
					{
						RelativePath = "Auth\\App Auth";
						
					}
					else
					{
						RelativePath = "Auth\\Resp Auth";
						
					}
					
				} else
				{
					RelativePath = "References";
				}

				string absolutePath = FilePath + "\\" + RelativePath;
				string absoluteFilePath = absolutePath + "\\" + fileName;
				
				System.IO.Directory.CreateDirectory(absolutePath);

				if (!File.Exists(absoluteFilePath))
					{
                    string URL = $"https://s3-ap-southeast-2.amazonaws.com/openlawnz-pdfs/{fileName}";
                    System.Net.WebClient Client = new WebClient();
						Client.DownloadFile(URL, absoluteFilePath);
					}

				gridItem.url = RelativePath + "\\" + fileName;
				gridItem.status = "Linked";

				gridItem.ranges.ForEach(range =>
				{
					range.Hyperlinks.Add(range, gridItem.url, LinkRef);
				});

			} else
			{
				gridItem.status = "Not found";
			}
		}

		async private System.Threading.Tasks.Task ProcessRemoteCitation(ResultDataGridItem gridItem)
		{

			string apiResultString = await GraphQLQuery(new Dictionary<string, string>
			{
                { "query","{ caseCitations(condition:{citation: \"" + gridItem.citation + "\"}) { case { id } } }" }
            });

			JObject apiResult = JObject.Parse(apiResultString);
			
			if (apiResult["data"]["caseCitations"].HasValues)
			{
                string caseID = (string)apiResult["data"]["caseCitations"][0]["case"]["id"];

				string DestinationPath;

				DestinationPath = "https://www.openlaw.nz/case/" + caseID;

				gridItem.ranges.ForEach(range =>
				{
					range.Hyperlinks.Add(range, DestinationPath, LinkRef);
				});

				gridItem.url = DestinationPath;

				gridItem.status = "Linked";

			}
			else
			{
				gridItem.status = "Not found";
			}

		}

		public MatchCollection matchRegex(string text)
		{
			return Regex.Matches(text, @"(\[|\()\d{4}(\]|\))[\s\S](\d{0,3}[\s\S])\w{1,5}[\s\S]\d{1,5}(([\s\S]\(\w*\))?)");
		}

		private DocumentCitation makeCitationFromMatch(Match m)
		{
			return new DocumentCitation { range = Globals.ThisAddIn.Application.ActiveDocument.Range(m.Index, m.Index + m.Length), value = m.Value };
		}

		private List<DocumentCitation> findCitations()
		{
			List<DocumentCitation> contentCitations = new List<DocumentCitation>();

			var footnotes = Globals.ThisAddIn.Application.ActiveDocument.Footnotes;
			var content = Globals.ThisAddIn.Application.ActiveDocument.Content;

			//content.Text = "Text [2017] NZHC 2017 text text [2017] NZHC 2017 test test [2016] NZHC 2010 test \ntest [2016] NZHC 2010 text text test";

			//------------------------------------------------------------------
			// Content matches
			//------------------------------------------------------------------

			var contentMatches = matchRegex(content.Text);
			contentCitations.AddRange(contentMatches.OfType<Match>().Select(match => makeCitationFromMatch(match)));

			//------------------------------------------------------------------
			// Footnote matches
			//------------------------------------------------------------------

			List<DocumentCitation> footnoteMatches = new List<DocumentCitation>();
			foreach (Word.Footnote footnote in footnotes)
			{

				var text = footnote.Range.Text;
				var matches = matchRegex(text);


				foreach (Match m in matches)
				{
					Word.Range rng = footnote.Range.Duplicate;
					rng.Start = rng.Start + m.Index;
					rng.End = rng.Start + m.Value.Length;

					footnoteMatches.Add(new DocumentCitation { range = rng, value = m.Value });

				}

				contentCitations.AddRange(footnoteMatches);

			}

			return contentCitations;

			//contentCitations.ForEach(contentCitation => ProcessCitation(contentCitation));
		}


		public UserControl1()
		{
			InitializeComponent();
			resultsGridView.AutoGenerateColumns = false;
			resultsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			resultsGridView.ReadOnly = true;
			resultsGridView.RowHeadersVisible = false;

			DataGridViewColumn citationColumn = new DataGridViewTextBoxColumn();
			citationColumn.DataPropertyName = "citation";
			citationColumn.Name = "Citation";
			resultsGridView.Columns.Add(citationColumn);

			DataGridViewColumn countColumn = new DataGridViewTextBoxColumn();
			countColumn.DataPropertyName = "count";
			countColumn.Name = "Count";
			resultsGridView.Columns.Add(countColumn);

			DataGridViewColumn statusColumn = new DataGridViewTextBoxColumn();
			statusColumn.DataPropertyName = "status";
			statusColumn.Name = "Status";
			resultsGridView.Columns.Add(statusColumn);

			DataGridViewColumn urlColumn = new DataGridViewLinkColumn();
			urlColumn.DataPropertyName = "url";
			urlColumn.Name = "URL";
			//urlColumn.UseColumnTextForLinkValue = true;
			//urlColumn.Text = "Delete";
			resultsGridView.Columns.Add(urlColumn);
			
			resultsGridView.DataSource = resultsBindingSource;

		}

		private List<ResultDataGridItem> createResultDataGridItems()
		{
			
			List<DocumentCitation> foundCitations = findCitations();

			return foundCitations
				.GroupBy(c => c.value)
				.Select(g => new ResultDataGridItem
				{
					citation = g.First().value,
					ranges = g.Select(r => r.range).ToList(),
					count = g.Distinct().Count(),
					status = "Processing"
				})
				.ToList();
		}

		private void linkToPDFButton_Click(object sender, EventArgs e)
		{
			removeCitations();
			resultsBindingSource.Clear();

			List<ResultDataGridItem> gridItems = createResultDataGridItems();

			gridItems.ForEach(gridItem =>
			{
				resultsBindingSource.Add(gridItem);
			});

			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			gridItems.ForEach(gridItem =>
			{
				tasks.Add(ProcessRemoteCitation(gridItem));
			});

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			resultsGridView.Refresh();
			resultsGridView.AutoResizeColumns();

		}

		private void downloadPDFButton_Click(object sender, EventArgs e)
		{
			removeCitations();
			resultsBindingSource.Clear();
			List<ResultDataGridItem> gridItems = createResultDataGridItems();

			gridItems.ForEach(gridItem =>
			{
				resultsBindingSource.Add(gridItem);
			});

			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			string comboBoxValue = (string)folderStructureComboBox.SelectedItem;
			bool isCourtOfAppeal = !String.IsNullOrEmpty(comboBoxValue);
			bool isAppelant = isCourtOfAppeal && comboBoxValue.Contains("Appellant");

			gridItems.ForEach(gridItem =>
			{
				tasks.Add(ProcessLocalCitation(gridItem, isCourtOfAppeal, isAppelant));
			});

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			resultsGridView.Refresh();
			resultsGridView.AutoResizeColumns();
		}

		private void removeCitations()
		{
			List<Hyperlink> links = new List<Hyperlink>();

			Hyperlinks contentLinks = Globals.ThisAddIn.Application.ActiveDocument.Content.Hyperlinks;
			for (int i = 0; i < contentLinks.Count; i++)
			{
				links.Add(contentLinks[i + 1]);
			}

			Footnotes footnotes = Globals.ThisAddIn.Application.ActiveDocument.Footnotes;
			foreach (Word.Footnote footnote in footnotes)
			{
				for (int i = 0; i < footnote.Range.Hyperlinks.Count; i++)
				{
					links.Add(footnote.Range.Hyperlinks[i + 1]);
				}
				
			}
			
			links.Where(l => l.SubAddress == LinkRef).ToList().ForEach(l =>
			{
				l.Delete();
			});

		}

		private void removeCitationButton_Click(object sender, EventArgs e)
		{
			removeCitations();
			resultsBindingSource.Clear();
		}
	}

	// Must appear under UserControl1 class
	// https://developercommunity.visualstudio.com/content/problem/44160/open-form-designer-from-solution-explorer.html
	
	internal class CitationDataFields
	{
		public string case_id { get; set; }
		public string citation { get; set; }
	}

	internal class DocumentCitation
	{
		public Range range { get; set; }
		public string value { get; set; }
	}

	public class ResultDataGridItem
	{
		public string citation { get; set; }
		public int count { get; set; }
		public List<Range> ranges { get; set; }
		public string status { get; set; }
		public string url { get; set; }
	}
}
