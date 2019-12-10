using GalaSoft.MvvmLight;
using Autofac;
using FriendsRank.Services.Classes;
using FriendsRank.Services.Interfaces;

namespace FriendsRank.ViewModels
{
	class ViewModelLocator
	{
		private static readonly IContainer _container;

		public static MainPageViewModel MainPageInstance =>
			_container.Resolve<MainPageViewModel>();

		public ViewModelLocator()
		{

		}

		static ViewModelLocator()
		{
			ContainerBuilder builder = new ContainerBuilder();

			ViewModelLocator.AddBindings(builder);

			_container = builder.Build();
		}

		public static IContainer GetContainer()
		{
			return _container;
		}

		private static void AddBindings(ContainerBuilder builder)
		{
			builder.RegisterType<VkSocialNetwork>().As<ISocialNetwork>();

			builder.RegisterType<MainPageViewModel>();
		}
	}
}
