using ApiCrudDapper.Dto;
using ApiCrudDapper.Models;
using AutoMapper;

namespace ApiCrudDapper.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioAdicionarDto>();
        CreateMap<Usuario, UsuarioListarDto>();
    }
}