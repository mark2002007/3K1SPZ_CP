using BLL;
using Caliburn.Micro;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFGUI.Events;

namespace WPFGUI.ViewModels
{
    public class LobbyViewModel : Conductor<object>, IHandle<LogOutEvent>
    {
        private LoginViewModel _loginViewModel;

        private RegisterViewModel _registerViewModel;
        private IEventAggregator _eventAggregator;

        public LobbyViewModel(LoginViewModel loginViewModel, RegisterViewModel registerViewModel, IEventAggregator eventAggregator)
        {
            _loginViewModel = loginViewModel;
            _registerViewModel = registerViewModel;
            _eventAggregator = eventAggregator;
            LoadLoginPage();
        }

        public Task HandleAsync(LogOutEvent message, CancellationToken cancellationToken)
        {
            LoadLoginPage();
            return new Task(() => { });
        }

        public async void LoadLoginPage()
        {
            await ActivateItemAsync(_loginViewModel);
        }

        public async void LoadRegisterPage()
        {
            await ActivateItemAsync(_registerViewModel);
        }


    }
}
