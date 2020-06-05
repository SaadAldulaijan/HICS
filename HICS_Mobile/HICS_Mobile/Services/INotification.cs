using System;
using System.Collections.Generic;
using System.Text;

namespace HICS_Mobile.Services
{
    public interface INotification
    {
        void SendNewNotification(string title, string body);
    }
}
