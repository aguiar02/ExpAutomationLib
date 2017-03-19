using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using ExpCalculatorLib;
using System.Threading;
using System.Management;

namespace ExpAutomationLib.Trigger
{
    public class DrivePlug2Trigger : IEventTrigger
    {
        public event TriggerEvent OnTriggerEvent;
        ManagementEventWatcher watcher = new ManagementEventWatcher();

        public string VolumeLabelExpression { get; set; }
        private string volumeLabel;

        public ExpCalculatorLib.ExpressionParser Parser { get; set; }

        public void SetParameters()
        {
            Parser.ParsingContext.Parameters.Add("DrivePlug2Letter", Parameter.NewParameter(typeof(string)));
        }


        public void Setup()
        {
            volumeLabel = ExpHelper.EvalToString(Parser, VolumeLabelExpression);

            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcher.Query = query;
            watcher.Start();
            watcher.WaitForNextEvent();
        }

        public void ShutDown()
        {
            watcher.Dispose();
        }



        private void watcher_EventArrived(object source, EventArrivedEventArgs e)
        {
            Parser.ParsingContext.Parameters["DrivePlug2Letter"].ParameterValue = e.NewEvent.Properties["DriveName"].Value.ToString();
            OnTriggerEvent.Invoke();

        }
    }
}
