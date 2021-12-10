using BLL;
using Caliburn.Micro;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFGUI.Helpers;
using WPFGUI.ViewModels;

namespace WPFGUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            _container.Singleton<IWindowManager, WindowManager>()
                      .Singleton<IEventAggregator, EventAggregator>();

            _container.Singleton<IUserServices, UserServices>()
                      .Singleton<IOrderServices, OrderServices>()
                      .Singleton<ICommentServices, CommentServices>()
                      .Singleton<IUserRepository, UserRepository>()
                      .Singleton<IOrderRepository, OrderRepository>()
                      .Singleton<ICommentRepository, CommentRepository>();

            _container.PerRequest<ShellViewModel>()
                      .PerRequest<HomeViewModel>()
                      .PerRequest<ProfileViewModel>()
                      .PerRequest<OrdersViewModel>()
                      .PerRequest<LobbyViewModel>()
                      .PerRequest<LoginViewModel>()
                      .PerRequest<RegisterViewModel>()
                      .PerRequest<CommentPromptViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service); 
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}
