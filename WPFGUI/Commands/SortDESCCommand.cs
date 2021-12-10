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
    public class SortDESCCommand: ICommand
    {
        OrdersViewModel _ordersViewModel;
        public SortDESCCommand(OrdersViewModel ordersViewModel)
        {
            _ordersViewModel = ordersViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _ordersViewModel.Sort = ESort.DESC;
        }
    }
}
