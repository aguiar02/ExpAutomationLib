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
	public class ForActionTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			Parser.ParsingContext.Parameters.Add("x", Parameter.NewParameter(0));
			Parser.ParsingContext.Parameters.Add("c", Parameter.NewParameter(new List<int>{ 1, 2, 3 }));
			DummyAction dummy = new DummyAction();
			ForAction action = new ForAction
			{
				Parser = Parser,
				CollectionExpression = "c$",
				ItemVarName = "i",
				InnerAction = new BlockAction
				{
					ActionList = new List<IAction>
					{
						dummy,
						new SetVariableAction
						{
							VariableName = "x",
							Expression = "x + i$"
						}
					}
				}
			};
			action.ExecuteAction();
			Assert.AreEqual(3, dummy.Count);
			Assert.AreEqual(6.0, (double)Parser.ParsingContext.Parameters["x"].ParameterValue);
		}
	}
}
