using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFGUI.ViewModels
{
    public class RegisterViewModel : Screen
    {
        private string _login;
        private string _password;
        private string _repeatPassword;
        private string _displayName;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                NotifyOfPropertyChange(() => Login);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                NotifyOfPropertyChange(() => RepeatPassword);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public bool CanSignUp
        {
            get
            {
                if (Login?.Length > 0 && Password?.Length > 0 && RepeatPassword?.Length > 0 && DisplayName?.Length > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public void SignUp()
        {
            MessageBox.Show("SignedUP");
        }
    }
}
