using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAC.Admin.AppService.ModelDto;
using WAC.Domain.Entities;
using WAC.Domain.Model;

namespace WAC.WebService.Admin
{

    public class MappingProfile : Profile
    {
        //  where TModel : Entity<TPrimaryKey>, new()
        //where TDto : EntityDto<TPrimaryKey>, new()
        public MappingProfile()
        {

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto  > ();

            CreateMap<LocationDto, Domain.Entities.Location>();
            CreateMap<Domain.Entities.Location, LocationDto>();


            CreateMap<Result, User>()
                   .ForMember(d => d.IdValue, o => o.MapFrom(s => GetId(s.id)))
                   .ForMember(d => d.Gender, o => o.MapFrom(s => s.gender))
                   .ForMember(d => d.FirstName, o => o.MapFrom(s => s.name.first))
                   .ForMember(d => d.LastName, o => o.MapFrom(s => s.name.last))
                   .ForMember(d => d.Email, o => o.MapFrom(s => s.email))
                   .ForMember(d => d.BirthDate, o => o.MapFrom(s => s.registered.date))
                   .ForMember(d => d.Uuid, o => o.MapFrom(s => s.login.uuid))
                   .ForMember(d => d.UserName, o => o.MapFrom(s => s.login.username));


            CreateMap<Domain.Model.Location, Domain.Entities.Location>()
                   .ForMember(d => d.IdValue, o => o.MapFrom(s =>  Guid.NewGuid()))
                   .ForMember(d => d.City, o => o.MapFrom(s => s.city))
                   .ForMember(d => d.Street, o => o.MapFrom(s => s.street))
                   .ForMember(d => d.PostCode, o => o.MapFrom(s => s.postcode))
                   .ForMember(d => d.Street, o => o.MapFrom(s => s.street));


        }

        private static string GetId(Id id) {
            var result = id != null ? string.Format("{0}{1}", id.value, id.name) : Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(result))
            {
                result = Guid.NewGuid().ToString();
            }
            return result;
        }
    }
}
