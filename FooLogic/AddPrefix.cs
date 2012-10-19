using System;
using NUnit.Framework;
using UnitTests;

namespace FooLogicTests
{
	[TestFixture]
	public class AddPrefix
	{
		private FooLogic _foo;
		private string _prefix;

		[SetUp]
		public void Setup()
		{
			_prefix = RndString();
			_foo = new FooLogic(_prefix);
		}

		[Test]
		public void InvalidArgimentTest()
		{
			try
			{
				_foo.AddPrefix(null);
			}
			catch(ArgumentNullException)
			{

			}
		}

		[Test]
		public void PrefixAddedTest()
		{
			var result = _foo.AddPrefix("some text");

			Assert.AreEqual(result, _prefix + "some text");
		}

		public static string RndString()
		{
			return Guid.NewGuid().ToString().Replace("-", "");
		}
	}
}

