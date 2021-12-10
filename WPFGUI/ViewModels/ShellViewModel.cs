using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFGUI.Events;

namespace WPFGUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogInEvent>, IHandle<LogOutEvent>
    {
        private LobbyViewModel _lobbyViewModel;
        private HomeViewModel _homeViewModel;
        private IEventAggregator _eventAggregator;

        public ShellViewModel(LobbyViewModel lobbyViewModel, HomeViewModel homeViewModel, IEventAggregator eventAggregator)
        {
            _lobbyViewModel = lobbyViewModel;
            _homeViewModel = homeViewModel;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            LoadLobbyPage();    
        }

        public async void LoadLobbyPage()
        {
            await ActivateItemAsync(_lobbyViewModel);
        }

        public async void LoadHomePage()
        {
            await ActivateItemAsync(_homeViewModel);
        }

        public Task HandleAsync(LogInEvent message, CancellationToken cancellationToken)
        {
            LoadHomePage();
            return new Task(() => { });
        }

        public Task HandleAsync(LogOutEvent message, CancellationToken cancellationToken)
        {
            LoadLobbyPage();
            return new Task(() => { });    
        }
    }
}
