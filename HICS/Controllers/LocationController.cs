using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using HICS.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HICS.Controllers
{
    public class LocationController : Controller
    {
        private readonly IUnitOfWork<Location> _locaion;
        private readonly IUnitOfWork<Device> _device;

        public LocationController(IUnitOfWork<Location> locaion , IUnitOfWork<Device> device)
        {
            _locaion = locaion;
            _device = device;
        }
        public IActionResult Index()
        {
            ViewData["LocationId"] = new SelectList(_locaion.Entity.GetAll(), "LocationId", "LocationName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Location location , Device device)
        {
            //should save in cookies. 
            //should be passed to activation form.

            if (ModelState.IsValid)
            {
                //Save Cookies.
                var selectedItem = _locaion.Entity.GetById(location.LocationId);
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
                _device.Entity.Insert(device);
                //_device.Save();
                //Redirect to activation page
                return RedirectToAction("Index", "Activation");
            }

            ViewData["LocationId"] = new SelectList(
                _locaion.Entity.GetAll(), 
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