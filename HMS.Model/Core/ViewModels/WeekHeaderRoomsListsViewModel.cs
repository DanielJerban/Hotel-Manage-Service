using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.ViewModels
{
    public class WeekHeaderRoomsListsViewModel
    {
        public List<WeeklyRoomViewModel> Saturday { get; set; }
        public List<WeeklyRoomViewModel> Sunday { get; set; }
        public List<WeeklyRoomViewModel> Monday { get; set; }
        public List<WeeklyRoomViewModel> Tuesday { get; set; }
        public List<WeeklyRoomViewModel> Wednesday { get; set; }
        public List<WeeklyRoomViewModel> Thursday { get; set; }
        public List<WeeklyRoomViewModel> Friday { get; set; }
    }
}
