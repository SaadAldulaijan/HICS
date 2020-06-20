using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLibrary.Services
{
    public class ReportingHub: Hub
    {
        public async Task BroadcastData()
        {
            await Clients.All.SendAsync("Reports");
        }
    }
}
