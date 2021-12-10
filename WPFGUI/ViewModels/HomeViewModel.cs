using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGUI.Events;

namespace WPFGUI.ViewModels
{
    public class HomeViewModel : Conductor<object> 
    {
        ProfileViewModel _profileViewModel;
        OrdersViewModel _ordersViewModel;
        CommentPromptViewModel _commentPromptViewModel;
        IEventAggregator _eventAggregator;

        public HomeViewModel(ProfileViewModel profileViewModel, OrdersViewModel ordersViewModel, CommentPromptViewModel commentPromptViewModel, IEventAggregator eventAggregator)
        {
            _profileViewModel = profileViewModel;
            _ordersViewModel = ordersViewModel;
            _commentPromptViewModel = commentPromptViewModel; 
            _eventAggregator = eventAggregator;
            LoadProfilePage();
        }

        public async void LoadProfilePage()
        {
            await ActivateItemAsync(_profileViewModel);
        }

        public async void LoadOrdersPage()
        {
            await ActivateItemAsync(_ordersViewModel);
        }
        
        public async void LoadCommentPrompt()
        {
            await ActivateItemAsync(_commentPromptViewModel);
        }

        public async void LogOut()
        {
            _eventAggregator.PublishOnUIThreadAsync(new LogOutEvent());
        }
    }
}
