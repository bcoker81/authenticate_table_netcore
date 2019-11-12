using System;
using System.Collections.Generic;

namespace testmysql.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
        public int? FkCoreUserId { get; set; }
        public int? Logins { get; set; }

        public virtual CoreUser FkCoreUser { get; set; }
    }
}
