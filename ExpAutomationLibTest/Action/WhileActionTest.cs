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
	public class WhileActionTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			Parser.ParsingContext.Parameters.Add("x", Parameter.NewParameter(1));
			DummyAction dummy = new DummyAction();
			WhileAction action = new WhileAction
			{
				Parser = Parser,
				ConditionExpression = "x < 5$",
				InnerAction = new BlockAction
				{
					ActionList = new List<IAction>
					{
						dummy,
						new SetVariableAction
						{
							VariableName = "x",
							Expression = "x + 1$"
						}
					}
				}
			};
			action.ExecuteAction();
			Assert.AreEqual(4, dummy.Count);
		}
	}
}
