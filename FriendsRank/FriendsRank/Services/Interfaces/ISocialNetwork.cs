using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsRank.Services.Interfaces
{
	interface ISocialNetwork
	{
		void Login(string login, string password);
		void Logout();
		List<string> GetFriendsNames();
		List<string> GetFriendsNamesRank();
		List<string> GetPopularMusic();
		List<string> GetReccomendedMusic();
	}
}
