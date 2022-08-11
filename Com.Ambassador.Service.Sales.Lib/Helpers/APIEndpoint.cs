using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Helpers
{
    public static class APIEndpoint
    {
        public static string Purchasing { get; set; }
        public static string AzurePurchasing { get; set; }
        public static string Core { get; set; }
        public static string AzureCore { get; set; }
		public static string StorageAccountName { get; set; }
		public static string StorageAccountKey { get; set; }
        public static string ConnectionString { get; set; }
        public static string Production { get; set; }
        public static string Finance { get; set; }
        public static string PackingInventory { get; set; }
    }
}
