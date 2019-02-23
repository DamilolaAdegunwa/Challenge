using System;
using System.Collections.Generic;
using System.Text;
using FWK.Domain.Entities;

namespace WAC.Admin.Domain.Entities
{

    public partial class Logs : Entity<long>
    {
        public int Level { get; set; }

        public string LogMessage { get; set; }

        public DateTime LogDate { get; set; }

        public string UserName { get; set; }

        public string SessionId { get; set; }


    }

}
