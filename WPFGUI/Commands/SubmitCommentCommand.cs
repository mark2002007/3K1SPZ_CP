using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFGUI.Helpers;
using WPFGUI.ViewModels;

namespace WPFGUI.Commands
{
    public class SubmitCommentCommand : ICommand
    {
        CommentPromptViewModel _commentPromptViewModel;
        public SubmitCommentCommand(CommentPromptViewModel commentPromptViewModel)
        {
            _commentPromptViewModel = commentPromptViewModel;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _commentPromptViewModel._commentServices.AddComment(Context.CurrentOrder.Id, _commentPromptViewModel.ResponseText);
            ((HomeViewModel)_commentPromptViewModel.Parent).LoadOrdersPage();
        }
    }
}
