using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using HICS.Library.Models;
using HICS.Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HICS.Controllers
{
    public class LocationController : Controller
    {
        #region Properties and Constructor
        private readonly ILocationService _location;
        //private readonly IDeviceService _device;
        public LocationController(ILocationService location /*,IDeviceService device */)
        {
            _location = location;
            //_device = device;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var data = await _location.Get();
            ViewData["LocationId"] = new SelectList(data, "LocationId", "LocationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Location location, Device device)
        {
            if (ModelState.IsValid)
            {
                //Save Cookies.
                var selectedItem = await _location.GetById(location.LocationId);
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Append("LocationId", selectedItem.LocationId.ToString(), cookieOptions);

                //Get MAC Address
                string macAddress = "test"; //GetMACAddress();
                string hostName = Dns.GetHostName();
                string ipAddress = GetIP();
                device = new Device
                {
                    MACAddress = macAddress,
                    HostName = hostName,
                    IPAddress = ipAddress
                };

                // await _device.Post(device);
                return RedirectToAction("Index", "Activation");
            }
            ViewData["LocationId"] = new SelectList(
                await _location.Get(),
                "LocationId",
                "LocationName",
                location.LocationId);

            return BadRequest();
        }

        #region Helper Methods 

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public string GetIP()
        {
            string ip = "";
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in ips)
            {
                ip = string.Join(",", item);
            }
            return ip;
        }
        #endregion
    }
}