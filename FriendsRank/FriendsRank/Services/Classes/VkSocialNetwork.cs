using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using FriendsRank.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.AudioBypassService;
using VkNet.AudioBypassService.Extensions;

namespace FriendsRank.Services.Classes
{
	class VkSocialNetwork:ISocialNetwork
	{
		private static IVkApi _api = new VkApi(new ServiceCollection().AddAudioBypass());

		public void Login(string login, string password)
		{
			_api.Authorize(new ApiAuthParams
			{
				ApplicationId = 7186665,
				Login = login,
				Password = password,
				Settings = Settings.All
			});

		}
		public void Logout()
		{
			_api.LogOut();
		}

		public List<string> GetFriendsNames()
		{
			var friendsInformation = _api.Friends.Get(new FriendsGetParams
			{
				UserId = _api.UserId,
				Fields = ProfileFields.All
			});

			var friends = friendsInformation.Select(x => x.FirstName + " " + x.LastName).ToList();

			return friends;
		}

		public List<string> GetFriendsNamesRank()
		{
			var NamesRank = new List<string>();

			var friendsInformation = _api.Friends.Get(new FriendsGetParams
			{
				UserId = _api.UserId,
				Fields = ProfileFields.All
			});

			var FriendsNames = friendsInformation.Select(x => x.FirstName);

			foreach (var name in FriendsNames.Distinct())
			{
				NamesRank.Add($"Name: {name}. It's rank: {FriendsNames.Count(x => x == name)}");
			}

			return NamesRank;

		}

		public List<string> GetPopularMusic()
		{
			var audios = _api.Audio.GetPopular();
			var result = new List<string>();

			foreach (var audio in audios)
			{
				result.Add($"Artist: {audio.Artist}. Title: {audio.Title}.");
			}

			return result;
		}

		public List<string> GetReccomendedMusic()
		{
			var audios = _api.Audio.GetRecommendations("", _api.UserId, 200, 1, true);
			var result = new List<string>();

			foreach (var audio in audios)
			{
				result.Add($"Artist: {audio.Artist}. Title: {audio.Title}.");
			}

			return result;
		}
	}
}
