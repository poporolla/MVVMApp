using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMApp
{
	class Phone : ICloneable, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
		public object Clone()
		{
			return new Phone() 
				{
				Title = this.Title, 
				Company = this.company, 
				Price = this.price 
				};
		}
		private string title;
		private string company;
		private int price;
		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}
		public string Company
		{
			get
			{
				return company;
			}
			set
			{
				company = value;
				OnPropertyChanged("Company");
			}
		}
		public int Price
		{
			get
			{
				return price;
			}
			set
			{
				price = value;
				OnPropertyChanged("Price");
			}
		}
	}
}
