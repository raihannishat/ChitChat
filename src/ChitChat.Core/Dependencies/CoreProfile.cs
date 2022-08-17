using AutoMapper;
using ChitChat.Core.BusinessObjects;
using ChitChat.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.Dependencies;
public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<Message, MessageBusinessObject>().ReverseMap();
    }
}
