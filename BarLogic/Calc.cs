using System;
using NUnit.Framework;
using Moq;
using UnitTests;
using AutofacContrib.Moq;

namespace BarLogicTests
{
	[TestFixture]
	public class Calc
	{
		[Test]
		public void InvalidActionTest()
		{
			BarLogic barLogic = new BarLogic(null, null);

			Assert.Throws(typeof(ArgumentException), ()=>{
				barLogic.Calc('*', 0, 0);
			});
		}

		[Test]
		public void PlusNotShow()
		{
			int a = 0, b = 0;

			var calculateServiceMock = new Mock<ICalculateService>(MockBehavior.Strict);
			calculateServiceMock.Setup(m => m.Plus(It.IsAny<int>(), It.IsAny<int>())).Returns(() => a + b);

			BarLogic barLogic = new BarLogic(null, calculateServiceMock.Object);

			a = 1;
			b = 2;
			Assert.AreEqual(barLogic.Calc('+', a, b), a + b);

			a = 5;
			b = 10;
			Assert.AreEqual(barLogic.Calc('+', a, b), a + b);

			calculateServiceMock.VerifyAll();
		}

		[Test]
		public void MinusWithShow()
		{
			var autoMock = AutoMock.GetStrict();

			autoMock.Mock<ICalculateService>().Setup(m => m.Minus(It.IsAny<int>(), It.IsAny<int>())).Returns(7);
			autoMock.Mock<IOutputService>().SetupProperty(m => m.OutputBufer);
			autoMock.Mock<IOutputService>().Setup(m => m.Show());

			var barLogic = autoMock.Create<BarLogic>();

			Assert.AreEqual(barLogic.Calc('-', 12, 5, true), 7);

			autoMock.Mock<IOutputService>().VerifySet(m => m.OutputBufer = "7");
			autoMock.Dispose();
		}
	}
}

