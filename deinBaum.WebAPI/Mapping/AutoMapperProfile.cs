using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.FotoStruktur;
using deinBaum.Lib.PersonDaten;

namespace deinBaum.WebAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Baum, BaumDTO>();
            CreateMap<BaumDTO, Baum>();
            CreateMap<BaumDTO, BaumDTO>();

            CreateMap<BaumArtDTO, BaumArt>();
            CreateMap<BaumArt, BaumArtDTO>();
            CreateMap<BaumArtDTO, BaumArtDTO>();

            CreateMap<BaumMerkmalDTO, BaumMerkmal>();
            CreateMap<BaumMerkmal, BaumMerkmalDTO>();
            CreateMap<BaumMerkmalDTO, BaumMerkmalDTO>();

            CreateMap<BaumZustandDTO, BaumZustand>();
            CreateMap<BaumZustand, BaumZustandDTO>();
            CreateMap<BaumZustandDTO, BaumZustandDTO>();

            CreateMap<FeldmitarbeiterDTO, Feldmitarbeiter>();
            CreateMap<Feldmitarbeiter, FeldmitarbeiterDTO>();
            CreateMap<FeldmitarbeiterDTO, FeldmitarbeiterDTO>();

            CreateMap<FotoDTO, Foto>();
            CreateMap<Foto, FotoDTO>();
            CreateMap<FotoDTO, FotoDTO>();

            CreateMap<WaldeigentuemerDTO, Waldeigentuemer>();
            CreateMap<Waldeigentuemer, WaldeigentuemerDTO>();
            CreateMap<WaldeigentuemerDTO, WaldeigentuemerDTO>();

            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, UserDTO>();
        }
    }
}
