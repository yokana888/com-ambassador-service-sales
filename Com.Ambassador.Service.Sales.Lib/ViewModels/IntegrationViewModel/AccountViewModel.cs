using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        [MaxLength(1000)]
        public string UserName { get; set; }

        /*profile*/
        [MaxLength(1000)]
        public string FirstName { get; set; }
        [MaxLength(1000)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Gender { get; set; }
        public string _id { get; set; }
        public ProfileViewModel profile { get; set; }

        //public virtual ICollection<RolesViewModel> Roles { get; set; }

    }

    public class ProfileViewModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
