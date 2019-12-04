using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;


namespace MVVMApp
{
	public class JsonFileService : IFileService
	{
		public List<Phone> Open(string fileName)
		{
			List<Phone> phones = new List<Phone>();
			DataContractJsonSerializer jsonFormatter =
				new DataContractJsonSerializer(typeof(List<Phone>));
			using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				phones = jsonFormatter.ReadObject(fs) as List<Phone>;
			}

			return phones;
		}
		public void Save(string fileName, List<Phone> phonesList)
		{
			DataContractJsonSerializer jsonFormatter =
				new DataContractJsonSerializer(typeof(List<Phone>));
			using(FileStream fs = new FileStream(fileName, FileMode.Create))
			{
				jsonFormatter.WriteObject(fs, phonesList);
			}
		}
	}
}
