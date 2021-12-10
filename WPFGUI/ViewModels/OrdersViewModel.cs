using BLL;
using Caliburn.Micro;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFGUI.Commands;
using WPFGUI.Events;
using WPFGUI.Helpers;

namespace WPFGUI.ViewModels
{
    public class OrdersViewModel : Screen, IHandle<LogInEvent>, INotifyPropertyChanged
    {
        #region Private Prop
        private IEventAggregator _eventAggregator;
        public IOrderServices _orderServices;
        public ICommentServices _commentServices;
        private IEnumerable<OrderDTO> _orders;
        private OrderDTO _selectedOrder;
        private string _search;
        private bool _sortASC;
        private ICommand _filterOrdersCommand;
        private ESort _sort;
        #endregion
        #region Public Prop

        public OrdersViewModel(IOrderServices orderServices, ICommentServices commentServices, IEventAggregator eventAggregator)
        {
            _orderServices = orderServices;
            _commentServices = commentServices;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _filterOrdersCommand = new FilterOrdersCommand(this);
            SortASCCommand = new SortASCCommand(this);
            SortDESCCommand = new SortDESCCommand(this);
            RepeatOrderCommand = new RepeatOrderCommand(this);
            CommentsCommand = new CommentsCommand(this);
            CommentAddCommand = new CommentAddCommand(this);
            Search = String.Empty;
        }

        

        public OrderDTO SelectedOrder 
        {
            get { return _selectedOrder; }
            set { 
                _selectedOrder = value;
                NotifyOfPropertyChange(() => SelectedOrder);
                
            }
        }


        public ESort Sort
        {
            get { return _sort; }
            set { 
                _sort = value;
                NotifyOfPropertyChange(() => Sort);
                _filterOrdersCommand.Execute(null);
                NotifyOfPropertyChange(() => Orders);
            }
        }

        public ICommand SortASCCommand { get; set; }
        public ICommand SortDESCCommand { get; set; }
        public ICommand RepeatOrderCommand { get; set; }
        public ICommand CommentsCommand { get; set; }
        public ICommand CommentAddCommand { get; set; }

        public IEnumerable<OrderDTO> Orders
        {
            get { return _orders; }
            set { 
                _orders = value;
                NotifyOfPropertyChange(() => Orders);
            }

        }
        
        public string Search
        {
            get { return _search; }
            set { 
                _search = value;
                NotifyOfPropertyChange(() => Search);
                _filterOrdersCommand.Execute(null);
                NotifyOfPropertyChange(() => Orders);
            }
        }

        #endregion
        #region Other Methods
        public Task HandleAsync(LogInEvent message, CancellationToken cancellationToken)
        {
            RefreshOrders();
            return new Task(() => { });      
        }

        public void RefreshOrders() => Orders = _orderServices.GetOrderHistory(Context.User.Id);
        #endregion
    }
}
