using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExpCalculatorLib;
using ExpCalculatorLib.Expression;

namespace ExpAutomationLib.Action
{
	public class HTTPAction : IAction
	{
		public string URLServerPortExpression { get; set; }
		private string url;

		public int Timeout { get; set; }
		public string ContentType { get; set; }
		public string Method { get; set; }
		public string VariableName { get; set; }

		public HTTPAction()
		{
			Timeout = 10000;
			ContentType = "text/html";
			Method = "GET";
		}

		public void ExecuteAction()
		{
			url = ExpHelper.EvalToString(Parser, URLServerPortExpression);

			var request = System.Net.HttpWebRequest.Create(url);
			request.Timeout = Timeout;
			request.ContentType = ContentType;
			request.Method = Method;
			using (var response = request.GetResponse())
			using (var stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
			{
				Parser.ParsingContext.Parameters.Add(VariableName, Parameter.NewParameter(reader.ReadToEnd()));
			}
			
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
