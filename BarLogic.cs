using System;

namespace UnitTests
{
	public class BarLogic
	{
		private IOutputService _outputService;
		private ICalculateService _calculateService;

		public BarLogic(IOutputService outputService, ICalculateService calculateService)
		{
			_outputService = outputService;
			_calculateService = calculateService;
		}

		public int Calc(char action, int a, int b, bool show = false)
		{
			var result = 0;
			switch(action)
			{
				case '+':
					result = _calculateService.Plus(a, b);
					break;
				case '-':
					result = _calculateService.Minus(a, b);
					break;
				default:
					throw new ArgumentException("action");
			}

			if(show)
			{
				_outputService.OutputBufer = result.ToString();
				_outputService.Show();
			}

			return result;
		}
	}
}

