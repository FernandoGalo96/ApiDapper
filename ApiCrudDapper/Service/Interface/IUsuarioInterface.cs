using ApiCrudDapper.Dto;
using ApiCrudDapper.Models;

namespace ApiCrudDapper.Service.Interface;

public interface IUsuarioInterface
{
    Task<ResponseModel<List<UsuarioListarDto>>> BuscarTodosUsuarios();

    Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId);

    Task<ResponseModel<List<UsuarioListarDto>>> AdicionarUsuario(UsuarioAdicionarDto usuarioAdicionarDto);

    Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);

    Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId);
}