using BLL;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFGUI.Commands;

namespace WPFGUI.ViewModels
{
    public class CommentPromptViewModel :  Screen
    {
        public ICommentServices _commentServices;
        public ICommand SubmitCommentCommand { get; set; }
        public CommentPromptViewModel(ICommentServices commentServices)
        {
            _commentServices = commentServices;
            SubmitCommentCommand = new SubmitCommentCommand(this);
        }

        private string _responseText;

        public string ResponseText
        {
            get { return _responseText; }
            set { 
                _responseText = value;
                NotifyOfPropertyChange(() => ResponseText);
            }
        }
    }
}
