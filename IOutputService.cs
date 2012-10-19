using System;

namespace UnitTests
{
	public interface IOutputService
	{
		string OutputBufer { get; set; }
		void Show();
	}
}

