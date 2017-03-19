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
	public class BlockActionTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			ExpressionParser Parser = ExpressionParser.CreateParser();
			BlockAction action = new BlockAction
			{
				Parser = Parser,
				ActionList = new List<IAction>
				{
					new DummyAction(),
					new DummyAction()
				}
			};
			action.ExecuteAction();
			Assert.AreEqual(1, (action.ActionList[0] as DummyAction).Count);
			Assert.AreEqual(1, (action.ActionList[1] as DummyAction).Count);
		}
	}
}
