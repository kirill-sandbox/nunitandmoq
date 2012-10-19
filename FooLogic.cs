using System;

namespace UnitTests
{
	public class FooLogic
	{
		private string _prefix;

		public FooLogic(string prefix)
		{
			_prefix = prefix;
		}

		public string AddPrefix(string text)
		{
			if(string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}
			return string.Format("{0}{1}", _prefix, text);
		}
	}
}

