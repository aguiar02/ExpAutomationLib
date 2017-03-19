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
	public class IfActionTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			IfAction ifAction = new IfAction
			{
				Parser = Parser,
				ConditionExpression = "1=1$",
				IfTrueAction = new DummyAction
				{
					Parser = Parser
				}
			};
			ifAction.ExecuteAction();
			Assert.AreEqual(1, (ifAction.IfTrueAction as DummyAction).Count);

		}
	}
}
