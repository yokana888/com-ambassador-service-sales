using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Services
{
    public interface IIdentityService
    {
        string Username { get; set; }
        string Token { get; set; }
        int TimezoneOffset { get; set; }
    }
}
