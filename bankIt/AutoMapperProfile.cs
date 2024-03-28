using bankIt.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using MySqlConnector;
using System.Security.Cryptography.Xml;

namespace bankIt
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountDetailsDto>();
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>().ReverseMap();
            CreateMap<Transfer, TransferDto>().ReverseMap(); 
            CreateMap<MonthlyCharge, MonthlyChargeDto>().ReverseMap(); 
        }
    }
}