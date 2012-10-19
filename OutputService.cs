using System;

namespace UnitTests
{
	public class OutputService : IOutputService
	{
		public OutputService()
		{
		}

		#region IOutputService implementation

		public string OutputBufer { get; set; }

		public void Show()
		{
			Console.WriteLine(OutputBufer);
		}

		#endregion
	}
}

