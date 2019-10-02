using Microsoft.AspNet.SignalR;

namespace HMS.Web.Hubs
{
    public class dashboardHub : Hub
    {
        public void refreshCalender()
        {
            GlobalHost.ConnectionManager.GetHubContext<dashboardHub>().Clients.All.RefreshCalender();
        }
    }
}