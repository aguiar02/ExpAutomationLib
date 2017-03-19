using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLibTest.Action
{
	[TestClass]
	public class SetVariableActionTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			SetVariableAction action = new SetVariableAction
			{
				VariableName = "x",
				Expression = "100$",
				Parser = Parser
			};
			action.ExecuteAction();
			Assert.IsTrue(Parser.ParsingContext.Parameters.ContainsKey("x"));
		}
	}
}
