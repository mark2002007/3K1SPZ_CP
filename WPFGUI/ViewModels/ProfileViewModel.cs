using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFGUI.Helpers;

namespace WPFGUI.ViewModels
{
    public class ProfileViewModel : Screen
    {
        public ProfileViewModel()
        {
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Login = Context.User?.Login;
            Password = Context.User?.ClearPassword;
            DisplayName = Context.User?.DispName;
            return new Task(() => {});
        }

        private string _login;
        private string _password; 
        private string _displayName;

        public string Login
        {
            get { return _login; }
            set { 
                _login = value;
                NotifyOfPropertyChange(() => Login);
            }
        }
        public string Password
        {
            get { return _password; }
            set { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        public string DisplayName
        {
            get { return _displayName; }
            set { 
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

    }
}
