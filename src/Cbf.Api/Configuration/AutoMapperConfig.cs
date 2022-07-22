using AutoMapper;
using Cbf.Api.ViewModels;
using Cbf.Business.Models;

namespace Cbf.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Time, TimeViewModel>().ReverseMap();
            CreateMap<Jogador, JogadorViewModel>().ReverseMap();
            CreateMap<Transferencia, TransferenciaViewModel>().ReverseMap();
            CreateMap<Torneio, TorneioViewModel>().ReverseMap();
        }
    }
}
