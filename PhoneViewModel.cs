using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMApp
{
	class PhoneViewModel : INotifyPropertyChanged
	{
		private Phone phone;
		public PhoneViewModel(Phone phone)
		{
			this.phone = phone;
		}
		public string Title
		{
			get
			{
				return phone.Title;
			}
			set
			{
				phone.Title = value;
				OnPropertyChanged("Title");
			}
		}
		public string Company
		{
			get
			{
				return phone.Company;
			}
			set
			{
				phone.Company = value;
				OnPropertyChanged("Company");
			}
		}
		public int Price
		{
			get
			{
				return phone.Price;
			}
			set
			{
				phone.Price = value;
				OnPropertyChanged("Price");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
