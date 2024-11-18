using ApiCrudDapper.Dto;
using ApiCrudDapper.Models;
using ApiCrudDapper.Service.Interface;
using AutoMapper;
using Dapper;
using System.Data.SqlClient;

namespace ApiCrudDapper.Service;

public class UsuarioService : IUsuarioInterface
{
    private readonly IConfiguration _configuration;
    private IMapper _mapper;

    public UsuarioService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> AdicionarUsuario(UsuarioAdicionarDto usuarioAdicionarDto)
    {
        var response = new ResponseModel<List<UsuarioListarDto>>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuarioBanco = await connection.ExecuteAsync(
                "INSERT INTO Usuarios (NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) " +
                "VALUES (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioAdicionarDto);

            if (usuarioBanco == 0)
            {
                response.Mensagem = "Ocorreu um erro ao adicionar o usuário";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);
            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Adicionado com sucesso";
            response.Status = true;
        }

        return response;
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarTodosUsuarios()
    {
        var response = new ResponseModel<List<UsuarioListarDto>>();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuariosBanco = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

            if (!usuariosBanco.Any())
            {
                response.Mensagem = "Nenhum usuário localizado";
                response.Status = false;
                return response;
            }

            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Usuários localizados com sucesso!";
        }
        return response;
    }

    public async Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId)
    {
        var response = new ResponseModel<UsuarioListarDto>();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuarioBancoId = await connection.QueryFirstOrDefaultAsync<Usuario>("Select * from Usuarios where id = @Id", new { Id = usuarioId });

            if (usuarioBancoId == null)
            {
                response.Mensagem = "Usuário não existe";
                response.Status = false;
                return response;
            }

            var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuarioBancoId);
            response.Dados = usuarioMapeado;
            response.Mensagem = "Usuário localizado com sucesso";
        }
        return response;
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
    {
        var response = new ResponseModel<List<UsuarioListarDto>>();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuariosBanco = await connection.ExecuteAsync("update Usuarios set Nome = @NomeCompleto, Email = @Email, Cargo = @Cargo, " +
                "Salario = @Salario,Situacao = @Situacao, CPF = @CPF where Id = @Id", usuarioEditarDto);

            if (usuariosBanco == 0)
            {
                response.Mensagem = "Usuário não existe";
                response.Status = false;
                return response;
            }
            var usuarios = await ListarUsuarios(connection);
            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Editado com sucesso";
            response.Status = true;
        }

        return response;
    }

    public async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection sqlConnection)
    {
        return await sqlConnection.QueryAsync<Usuario>("select * from Usuarios");
    }

    public async Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId)
    {
        var response = new ResponseModel<List<UsuarioListarDto>>();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var usuarioBanco = await connection.ExecuteAsync("delete from usuarios where id = @id", new { Id = usuarioId });

            if (usuarioBanco == 0)
            {
                response.Mensagem = "Usuário não existe";
                response.Status = false;
                return response;
            }

            var usuarios = await ListarUsuarios(connection);
            var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

            response.Dados = usuariosMapeados;
            response.Mensagem = "Removido com sucesso";
            response.Status = true;
        }

        return response;
    }
}