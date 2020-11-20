using System;
using System.Reflection;
using Autofac;
using TinyMvvm;
using TinyMvvm.Autofac;
using TinyMvvm.IoC;
using TinyNavigationHelper;
using TinyNavigationHelper.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LiveSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();

            var navigation = new ShellNavigationHelper();
            navigation.RegisterViewsInAssembly(assembly);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssemblyTypes(assembly)
               .Where(x => x.IsSubclassOf(typeof(Page)));

            containerBuilder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsSubclassOf(typeof(ViewModelBase)));

            containerBuilder.RegisterInstance<INavigationHelper>(navigation);

            var container = containerBuilder.Build();

            Resolver.SetResolver(new AutofacResolver(container));



            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
