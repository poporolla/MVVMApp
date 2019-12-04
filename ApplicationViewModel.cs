using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMApp
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
		private Phone selectedPhone;
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
		public ApplicationViewModel(IFileService fileService, IDialogService dialogService)
		{
			this.fileService = fileService;
			this.dialogService = dialogService;

			Phones = new ObservableCollection<Phone>
			{
				new Phone() { Title="iPhone 7", Company="Apple", Price=56000 },
				new Phone() {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
				new Phone() {Title="Elite x3", Company="HP", Price=56000 },
				new Phone() {Title="Mi5S", Company="Xiaomi", Price=35000 }
			};
		}

		IFileService fileService;
		IDialogService dialogService;

		private RelayCommand saveCommand;
		public RelayCommand SaveCommand
		{
			get
			{
				return saveCommand ??
					(saveCommand = new RelayCommand(obj =>
					{
						try
						{
							if (dialogService.SaveFileDialog() == true)
							{
								fileService.Save(dialogService.FilePath, Phones.ToList());
								dialogService.ShowMessage("File saved");
							}
						}
						catch (Exception ex)
						{
							dialogService.ShowMessage(ex.Message);
						}
					}));
			}
		}

		private RelayCommand openCommand;
		public RelayCommand OpenCommand
		{
			get
			{
				return openCommand ??
					(openCommand = new RelayCommand(obj =>
					{
						try
						{
							if (dialogService.OpenFileDialog())
							{
								var phones = fileService.Open(dialogService.FilePath);
								Phones.Clear();
								foreach (var p in phones)
								{
									Phones.Add(p);
								}
								dialogService.ShowMessage("File opened");
							}
						}
						catch (Exception ex)
						{
							dialogService.ShowMessage(ex.Message);
						}
					}));
			}
		}

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
				return delCommand ??
					(delCommand = new RelayCommand(obj =>
					{
						Phone phone = obj as Phone;
						if (phone != null)
						{
							Phones.Remove(phone);
						}
					},
					(obj) => 
					{
						return (Phones.Count > 0 && obj != null);
					}
					));
			}
		}
		private RelayCommand copyCommand;
		public RelayCommand CopyCommand
		{
			get
			{
				return copyCommand ??
					(copyCommand = new RelayCommand(obj =>
					{
						Phone phone = obj as Phone;
						if(phone != null)
						{
							Phone phonecopy = (Phone)phone.Clone();
							Phones.Insert(0, phonecopy);
						}
					}));
			}
		}
	}
}
