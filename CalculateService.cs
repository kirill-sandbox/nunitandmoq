using System;

namespace UnitTests
{
	public class CalculateService : ICalculateService
	{
		public CalculateService()
		{
		}

		#region ICalculateService implementation

		public int Plus(int a, int b)
		{
			return a + b;
		}

		public int Minus(int a, int b)
		{
			return a - b;
		}

		#endregion
	}
}

