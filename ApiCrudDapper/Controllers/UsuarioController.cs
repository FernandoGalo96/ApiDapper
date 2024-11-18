using ApiCrudDapper.Dto;
using ApiCrudDapper.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioService)
        {
            _usuarioInterface = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarTodosUsuarios();

            if (usuarios.Status == false) return NotFound(usuarios);

            return Ok(usuarios);
        }

        [HttpGet("{usuariosId}")]
        public async Task<IActionResult> BuscarUsuariosId(int usuariosId)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(usuariosId);

            if (usuario.Status == false) return NotFound(usuario);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioAdicionarDto usuarioAdicionarDto)
        {
            var usuarios = await _usuarioInterface.AdicionarUsuario(usuarioAdicionarDto);

            if (usuarios.Status == false) return BadRequest(usuarios);
            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(usuarioEditarDto);
            if (usuarios.Status == false) return BadRequest(usuarios);
            return Ok(usuarios);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuarios = await _usuarioInterface.RemoverUsuario(usuarioId);
            if (usuarios.Status == false) return NotFound(usuarios);
            return Ok(usuarios);
        }
    }
}