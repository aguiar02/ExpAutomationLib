using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Trigger;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using ExpAutomationLib.Action;
using ExpCalculatorLib;
using System.Collections.Concurrent;
using ExpAutomationLib.Serialization;

namespace ExpAutomationLib
{
	public class AutomationService
	{
		public IList<IWindowsMessageTrigger> RegisteredWinMessage { get; set; }
		public IntPtr FormHandle { get; set; }
		private ConcurrentQueue<IAction> ActionQueue { get; set; }
		private Thread serviceThread;

		public List<Profile> Profiles { get; set; }
	
		public bool IsServiceExecuting { get; private set; }

		private bool isShuttingDown = false;

		public AutomationService()
		{
			RegisteredWinMessage = new List<IWindowsMessageTrigger>();
			ActionQueue = new ConcurrentQueue<IAction>();
			Profiles = new List<Profile>();
		}

		public void ExecuteService()
		{
			foreach (var profile in Profiles)
			{
				Thread thread;
				ExpressionParser profileParser;
				if (profile.Enabled)
				{
					profileParser = ExpressionParser.CreateParser();
					profile.ActionToExecute.Parser = profileParser;
					if (profile.EventTrigger == null)
						profile.EventTrigger = new DefaultTrigger();

					profile.EventTrigger.Parser = profileParser;
					profile.EventTrigger.SetParameters();
					if (profile.EventTrigger is IWindowsMessageTrigger)
					{
						IWindowsMessageTrigger wmt = (IWindowsMessageTrigger)profile.EventTrigger;
						wmt.FormHandle = FormHandle;
						RegisteredWinMessage.Add(wmt);
					}

					var prof = profile;
					prof.EventTrigger.OnTriggerEvent += () =>
					{
						if (prof.StateTriggers.Count == 0 || prof.StateTriggers.All(st => st.IsStateActive))
						{
							prof.IsAllStateActive = true;
							ActionQueue.Enqueue(prof.ActionToExecute);
						}
						else if (prof.EventTrigger is DefaultTrigger)
						{
							//executa ação de término do estado
							if (prof.IsAllStateActive && prof.ActionOnExitState != null)
							{
								ActionQueue.Enqueue(prof.ActionOnExitState);
							}
							prof.IsAllStateActive = false;
						}
					};

					profile.EventTrigger.Setup();

					foreach (var trigger in profile.StateTriggers)
					{
						trigger.Parser = profileParser;
						trigger.SetParameters();
						thread = new Thread(trigger.ExecuteMonitor);
						thread.Start();
					}
				}
			}
			serviceThread = new Thread(ProcessActionQueue);
			serviceThread.ApartmentState = ApartmentState.STA;
			serviceThread.Start();
			IsServiceExecuting = true;
		}

		[STAThread]
		public void ProcessActionQueue()
		{
			while (!isShuttingDown || !ActionQueue.IsEmpty)
			{
				Thread.Sleep(500);
				IAction action = null;
				while (ActionQueue.TryDequeue(out action))
				{
					action.ExecuteAction();
				}
			}
		}

		public void ShutDownService()
		{
			foreach (var profile in Profiles)
			{
				profile.EventTrigger.ShutDown();

			}
			isShuttingDown = true;
			serviceThread.Join();
			IsServiceExecuting = false;
		}

		public void WndProc(ref Message m)
		{
			foreach (var trigger in RegisteredWinMessage)
			{
				trigger.WindowsMessageReceived(ref m);
			}
		}
	}
}
