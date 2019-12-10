using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Autofac;
using FriendsRank.Services.Interfaces;
using FriendsRank.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FriendsRank.ViewModels
{
	public class MainPageViewModel:ViewModelBase
	{
		private readonly ISocialNetwork _VkSocialNetwork;

		private string _login;
		public string UserLogin
		{
			get { return _login; }
			set
			{
				_login = value;
				RaisePropertyChanged();
			}
		}

		private string _password;
		public string UserPassword
		{
			get { return _password; }
			set
			{
				_password = value;
				RaisePropertyChanged();
			}
		}

		private List<string> _friendsNames;
		public List<string> FriendsNames
		{
			get
			{
				return _friendsNames ?? (_friendsNames = new List<string>());
			}
			set
			{
				_friendsNames = value;
				RaisePropertyChanged();
			}
		}

		private List<string> _music;

		public List<string> Music
		{
			get
			{
				return _music ?? (_music = new List<string>());
			}
			set
			{
				_music = value;
				RaisePropertyChanged();
			}
		}

		private List<string> _friendsNamesRank;
		public List<string> FriendsNamesRank
		{
			get
			{
				return _friendsNamesRank ?? (_friendsNamesRank = new List<string>());
			}
			set
			{
				_friendsNamesRank = value;
				RaisePropertyChanged();
			}
		}

		private List<string> _currentView;
		public List<string> CurrentView
		{
			get
			{
				return _currentView ?? (_currentView = new List<string>());

			}
			set
			{
				_currentView = value;
				base.RaisePropertyChanged();
			}
		}

		private string _greeting;
		public string Greeting
		{
			get { return _greeting; }
			set
			{
				_greeting = value;
				RaisePropertyChanged();
			}
		}

		private bool _load;
		public bool Load
		{
			get { return _load; }
			set
			{
				_load = value;
				RaisePropertyChanged();
			}
		}

		private bool _showList = false;

		public bool ShowList
		{
			get { return _showList; }
			set
			{
				_showList = value;
				RaisePropertyChanged();
			}
		}

		private bool _show=true;
		public bool Show 
		{
			get { return _show; }
			set
			{
				_show = value;
				RaisePropertyChanged();
			}
		}

		public ICommand LoginCommand { get; set; }
		public ICommand LogoutCommand { get; set; }
		public ICommand GetFriendsCommand { get; set; }
		public ICommand GetFriendsRankCommand { get; set; }
		public ICommand GetPopularMusicCommand { get; set; }
		public ICommand GetReccomendedMusicCommand { get; set; }

		private async void Login()
		{
			Load = true;
			string currentUserLogin = UserLogin;
			string currentUserPassword = UserPassword;

			await Task.Run(() =>
			{
				_VkSocialNetwork.Login(currentUserLogin, currentUserPassword);
			});
			UserLogin = "";
			UserPassword = "";

			Show = false;
			ShowList = true;
			Greeting = "Welcome";
			Load = false;
		}

		private async void Logout()
		{
			await Task.Run(() => _VkSocialNetwork.Logout());
			Greeting = "Welcome";
			CurrentView = null;
			ShowList = false;
			Show = true;
		}

		private async void GetFriends()
		{
			CurrentView = null;
			Load = true;
			FriendsNames = await Task.Run(() => _VkSocialNetwork.GetFriendsNames());
			Greeting = "Current user's friends";
			CurrentView = FriendsNames;
			Load = false;
		}

		private async void GetFriendsRank()
		{
			CurrentView = null;
			Load = true;
			FriendsNamesRank = await Task.Run(() => _VkSocialNetwork.GetFriendsNamesRank());
			Greeting = "Friends's names rank";
			CurrentView = FriendsNamesRank;
			Load = false;
		}

		private async void GetPopularMusic()
		{
			CurrentView = null;
			Load = true;
			Music = await Task.Run(() => _VkSocialNetwork.GetPopularMusic());
			Greeting = "Popular music";
			CurrentView = Music;
			Load = false;
		}

		private async void GetReccomendedMusic()
		{
			CurrentView = null;
			Load = true;
			Music = await Task.Run(() => _VkSocialNetwork.GetReccomendedMusic());
			Greeting = "Reccomended music";
			CurrentView = Music;
			Load = false;
		}

		public MainPageViewModel()
		{
			_VkSocialNetwork = ViewModelLocator.GetContainer().Resolve<ISocialNetwork>();

			LoginCommand = new RelayCommand(Login);

			LogoutCommand  = new RelayCommand(Logout);

			GetFriendsCommand = new RelayCommand(GetFriends);

			GetFriendsRankCommand = new RelayCommand(GetFriendsRank);

			GetPopularMusicCommand = new RelayCommand(GetPopularMusic);

			GetReccomendedMusicCommand = new RelayCommand(GetReccomendedMusic);

		}


	}
}
