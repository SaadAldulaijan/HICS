using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Entities
{
    public class Device
    {
        // i need to refactor the way that application register these information
        // use cases: 
        // if activation occurs, i need to know the MAC.
        // also MAC should be associated with location. 
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public string HostName { get; set; }

    }
}
