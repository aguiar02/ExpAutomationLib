using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpCalculatorLib;
using ExpAutomationLib.Action;

namespace ExpAutomationLibTest.Action
{
	[TestClass]
	public class HTTPActionTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			HTTPAction action = new HTTPAction
			{
				Parser = Parser,
				URLServerPortExpression = "'http://npaa1215/rve1'$",
				VariableName = "html"
			};
			action.ExecuteAction();
			Assert.IsTrue(Parser.ParsingContext.Parameters.ContainsKey("html"));
		}
	}
}
