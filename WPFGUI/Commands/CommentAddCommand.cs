using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFGUI.Helpers;
using WPFGUI.ViewModels;

namespace WPFGUI.Commands
{
    public class CommentAddCommand : ICommand
    {
        OrdersViewModel _orderViewModel;
        public CommentAddCommand(OrdersViewModel ordersViewModel)
        {
            _orderViewModel = ordersViewModel;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Context.CurrentOrder = _orderViewModel.SelectedOrder;
            ((HomeViewModel)_orderViewModel.Parent).LoadCommentPrompt();
        }
    }
}
