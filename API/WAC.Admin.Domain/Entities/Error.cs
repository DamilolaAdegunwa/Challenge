using System;
using FWK.Domain.Entities;

namespace WAC.Admin.Domain.Entities
{
    public class Error: Entity<Int64>
    {
        public DateTime ErrorDate { get; set; }

        public String UserName { get; set; }

        public String ErrorMessage { get; set; }

        public String SessionId { get; set; }

        public string StackTrace { get; set; }

    }
}
