using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MVVMApp
{
	class ApplicationViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
		private Phone selectedPhone;

		private RelayCommand addCommand;
		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ?? (addCommand = new RelayCommand(obj =>
				{
					Phone phone = new Phone();
					Phones.Insert(0, phone);
					SelectedPhone = phone;
				}));
			}
		}

		private RelayCommand delCommand;
		public RelayCommand DelCommand
		{
			get
			{
				return delCommand ?? (delCommand = new RelayCommand(obj =>
				{
					Phone phone = SelectedPhone;
					Phones.Remove(phone);
					SelectedPhone = null;
				}));
			}
		}
		private RelayCommand copyCommand;
		public RelayCommand CopyCommand
		{
			get
			{
				return copyCommand ?? (copyCommand = new RelayCommand(obj =>
				{
					if(SelectedPhone != null)
					{
						Phone phone = (Phone)SelectedPhone.Clone();
						Phones.Insert(0, phone);
						SelectedPhone = phone;
					}
				}));
			}
		}

		public Phone SelectedPhone
		{
			get
			{
				return selectedPhone;
			}
			set
			{
				selectedPhone = value;
				OnPropertyChanged("SelectedPhone");
			}
		}
		public ObservableCollection<Phone> Phones { get; set; }
		public ApplicationViewModel()
		{
			Phones = new ObservableCollection<Phone>
			{
				new Phone { Title="iPhone 7", Company="Apple", Price=56000 },
				new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
				new Phone {Title="Elite x3", Company="HP", Price=56000 },
				new Phone {Title="Mi5S", Company="Xiaomi", Price=35000 }
			};
		}

	}
}
