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
	public class SetPropertyValueActionTest
	{
		private class DummyClass
		{
			[EnableProperty]
			public double X { get; set; }
			[EnableProperty]
			public double Y { get; set; }
		}

		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			Parser.ParsingContext.Parameters.Add("x", Parameter.NewParameter(new DummyClass
			{
				X = 0,
				Y = 0
			}));
			SetPropertyValueAction action = new SetPropertyValueAction
			{
				PropertyExpression = "x.X$",
				ValueExpression = "100$",
				Parser = Parser
			};
			action.ExecuteAction();
			Assert.AreEqual(100.0, (double)(Parser.ParsingContext.Parameters["x"].ParameterValue as DummyClass).X);
		}
	}
}
