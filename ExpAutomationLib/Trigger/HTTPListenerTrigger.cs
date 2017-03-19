using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using ExpCalculatorLib;

namespace ExpAutomationLib.Trigger
{
	public class HTTPListenerTrigger : IEventTrigger
	{
		public event TriggerEvent OnTriggerEvent;

		public string PrefixExpression { get; set; }
		private string prefix;

		System.Net.HttpListener listener;
		Thread triggerThread;
		bool isShutingDown = false;
		public void Setup()
		{
			prefix = ExpHelper.EvalToString(Parser, PrefixExpression);
			listener = new System.Net.HttpListener();
			listener.Prefixes.Add(prefix);
			listener.Start();
			triggerThread = new Thread(() =>
			{
				while (!isShutingDown)
				{
					try
					{
						HttpListenerContext context = listener.GetContext();
						HttpListenerRequest request = context.Request;
						// Obtain a response object.
						HttpListenerResponse response = context.Response;
						// Construct a response.
						string responseString = "OK";
						byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
						// Get a response stream and write the response to it.
						response.ContentLength64 = buffer.Length;
						System.IO.Stream output = response.OutputStream;
						output.Write(buffer, 0, buffer.Length);
						// You must close the output stream.
						output.Close();
						Parser.ParsingContext.Parameters["HLQuerystring"].ParameterValue = request.RawUrl;
						OnTriggerEvent.Invoke();
					}
					catch 
					{
					}
				}
				listener.Close();
			});
			triggerThread.Start();
			
		}

		public void ShutDown()
		{
			if (listener != null && listener.IsListening)
			{
				isShutingDown = true;
				listener.Abort();
			}
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			Parser.ParsingContext.Parameters.Add("HLQuerystring", Parameter.NewParameter(typeof(string)));
		}
	}
}
