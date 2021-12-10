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
    public class FilterOrdersCommand : ICommand
    {
        OrdersViewModel _ordersVM;
        public FilterOrdersCommand(OrdersViewModel ordersViewModel)
        {
            _ordersVM = ordersViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _ordersVM.Orders = _ordersVM._orderServices.GetOrderHistory(Context.User.Id).Where(o => o.ProductName.Contains(_ordersVM.Search));
            if (_ordersVM.Sort == ESort.ASC)
                _ordersVM.Orders = _ordersVM.Orders.OrderBy(o => o.Id);
            else
                _ordersVM.Orders = _ordersVM.Orders.OrderByDescending(o => o.Id);

        }
    }
}
