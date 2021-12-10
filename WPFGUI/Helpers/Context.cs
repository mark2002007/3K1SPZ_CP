using Caliburn.Micro;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFGUI.Events;

namespace WPFGUI.Helpers
{
    public static class Context
    {
        static IEventAggregator _eventAggregator;
        public static UserModel User { get; set; } = new UserModel();
        public static OrderDTO CurrentOrder { set; get; } = new OrderDTO();
    }
}
