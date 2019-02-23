using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WAC.Domain.Entities
{
    public class User : FWK.Domain.Entities.Entity<string>
    { 
       
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public Location Location { get; set; }
        public Guid LocationId { get; set; }

        //TODO: esto es para poder usar como PK otra propiedad , tambien se puede mapear por contexto
        //[NotMapped]
        //public override string IdValue { get => this.Id; set => this.Id = value; } 
        // public string Id { get; set; }
    }

    public class Location : FWK.Domain.Entities.Entity<Guid>
    {
        public Location(string state , string street , string city, string postCode) : base()
        {
            State = state;
            Street = street;
            City = city;
            PostCode = postCode;
        }

        public Location()
        {

        }


        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    } 
}
