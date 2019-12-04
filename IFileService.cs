using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMApp
{
	public interface IFileService
	{
		List<Phone> Open(string filename);
		void Save(string fileName, List<Phone> phonesList);
	}
}
