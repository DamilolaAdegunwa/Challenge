using FWK.AppService.Interface;
using FWK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WAC.Admin.AppService.ModelDto
{
    public class UserDto : EntityDto<string>
    {  
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Uuid { get; set; }
        public string UserName { get; set; } 
        public LocationDto Location { get; set; }
    }

    public class LocationDto : EntityDto<Guid>
    {
        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }

}
