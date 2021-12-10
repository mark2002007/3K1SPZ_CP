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
    public class CommentsCommand : ICommand
    {
        OrdersViewModel _orderViewModel;
        public CommentsCommand(OrdersViewModel ordersViewModel)
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
            var comments = _orderViewModel._commentServices.GetCommentsOfUser(Context.User.Id).Where(c => c.OrderId == _orderViewModel.SelectedOrder.Id);
            string res = String.Empty;
            foreach (var comment in comments)
                res += $"{comment.InsertTime} : {comment.Text}\n";
            MessageBox.Show(res);
        }
    }
}
