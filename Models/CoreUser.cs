using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace testmysql.Models
{
    public partial class CoreUser
    {
        public CoreUser()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string TempPassword { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
