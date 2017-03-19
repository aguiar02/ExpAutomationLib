using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExpCalculatorLib;
using ExpAutomationLib.Trigger;

namespace ExpAutomationLib.File.Trigger
{
	public class FileWatcherTrigger : IEventTrigger
	{
		public event TriggerEvent OnTriggerEvent;
		System.IO.FileSystemWatcher watcher;

		public string PathExpression { get; set; }
		private string path;

		public string FilterExpression { get; set; }
		private string filter;

		public bool HandleFileChange { get; set; }
		public bool HandleFileCreate { get; set; }
		public bool HandleFileDelete { get; set; }
		public bool HandleFileRename { get; set; }

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			Parser.ParsingContext.Parameters.Add("FWChangeType", Parameter.NewParameter(typeof(string)));
			Parser.ParsingContext.Parameters.Add("FWFullPath", Parameter.NewParameter(typeof(string)));
			Parser.ParsingContext.Parameters.Add("FWName", Parameter.NewParameter(typeof(string)));
			Parser.ParsingContext.Parameters.Add("FWOldFullPath", Parameter.NewParameter(typeof(string)));
			Parser.ParsingContext.Parameters.Add("FWOldName", Parameter.NewParameter(typeof(string)));
		}


		public void Setup()
		{
			path = ExpHelper.EvalToString(Parser, PathExpression);
			filter = ExpHelper.EvalToString(Parser, FilterExpression);

			watcher = new System.IO.FileSystemWatcher();
			watcher.Path = path;
			/* Watch for changes in LastAccess and LastWrite times, and
			   the renaming of files or directories. */
			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
			   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
			
			watcher.Filter = filter;

			// Add event handlers.
			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);
			watcher.Deleted += new FileSystemEventHandler(OnChanged);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);

			// Begin watching.
			watcher.EnableRaisingEvents = true;
		}

		public void ShutDown()
		{
			watcher.Dispose();
		}

		
		private void OnChanged(object source, FileSystemEventArgs e)
		{
			if ((this.HandleFileChange && e.ChangeType == WatcherChangeTypes.Changed)
				|| (this.HandleFileCreate && e.ChangeType == WatcherChangeTypes.Created)
				|| (this.HandleFileDelete && e.ChangeType == WatcherChangeTypes.Deleted))
			{
				Parser.ParsingContext.Parameters["FWChangeType"].ParameterValue = e.ChangeType.ToString();
				Parser.ParsingContext.Parameters["FWFullPath"].ParameterValue = e.FullPath;
				Parser.ParsingContext.Parameters["FWName"].ParameterValue = e.Name;
				OnTriggerEvent();
			}
		}

		private void OnRenamed(object source, RenamedEventArgs e)
		{
			if ((this.HandleFileChange && e.ChangeType == WatcherChangeTypes.Changed)
				|| (this.HandleFileCreate && e.ChangeType == WatcherChangeTypes.Created)
				|| (this.HandleFileDelete && e.ChangeType == WatcherChangeTypes.Deleted))
			{
				Parser.ParsingContext.Parameters["FWChangeType"].ParameterValue = e.ChangeType.ToString();
				Parser.ParsingContext.Parameters["FWFullPath"].ParameterValue = e.FullPath;
				Parser.ParsingContext.Parameters["FWName"].ParameterValue = e.Name;
				Parser.ParsingContext.Parameters["FWOldFullPath"].ParameterValue = e.OldFullPath;
				Parser.ParsingContext.Parameters["FWOldName"].ParameterValue = e.OldName;
				OnTriggerEvent();
			}
		}
	}
}
