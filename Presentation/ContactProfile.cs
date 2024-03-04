using AutoMapper;
using BusinessLogic.DTO;
using DataAccess.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapper
{
    internal class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDto, ContactEntity>();
            CreateMap<ContactEntity, ContactDto>();
            CreateMap<ContactEntity, ContactDetailDto>();
        }
    }
}
