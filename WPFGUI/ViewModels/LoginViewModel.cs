using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;
using BLL;
using System.Threading;
using WPFGUI.Helpers;
using WPFGUI.Events;

namespace WPFGUI.ViewModels
{
    public class LoginViewModel : Screen 
    {

        private string _login;
        private string _password;
        private IUserServices _userServices;
        private IEventAggregator _eventAggregator;
        public LoginViewModel(IUserServices userServices, IEventAggregator eventAggregator)
        {
            _userServices = userServices;
            _eventAggregator = eventAggregator;
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                NotifyOfPropertyChange(() => Login);
                NotifyOfPropertyChange(() => CanSignIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanSignIn);
            }
        }
        public void SignIn()
        {
            if (_userServices.CheckPassword(Login, Password))
            {
                Context.User = new UserModel(_userServices.Get(Login));
                Context.User.ClearPassword = Password;
                Login = "";
                Password = "";
                _eventAggregator.PublishOnUIThreadAsync(new LogInEvent());
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }

        public bool CanSignIn
        {
            get
            {
                if (Login?.Length > 0 && Password?.Length > 0)
                {
                    return true;
                }
                return false;
            }
        }

        
    }
}
