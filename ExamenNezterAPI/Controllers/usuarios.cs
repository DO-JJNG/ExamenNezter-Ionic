using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ExamenNezterAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class usuarios : ControllerBase
    {

        private readonly IDbConnection _conexion;
        public usuarios(IConfiguration config)
        {
            _conexion = new SqlConnection(config.GetConnectionString("DefaultConnection"));

        }

        // Get users -------------------------------------

        [HttpGet("GetUsuarios")]

        public async Task<ActionResult<List<usuariosN>>> GetAllUsuarios()
        {
            //var users = await _conexion.QueryAsync<usuariosN>("GetAllUsers", CommandType.StoredProcedure);
            //return Ok(users);
            var users = await _conexion.QueryAsync<usuariosN>("select * from USUARIOS");
            return Ok(users);
        }
        //-------------------------------------------------

        [HttpGet("ById/{idusuario}")]

        public async Task<ActionResult<usuariosN>> SelectIdUser(int idusuario)
        {

            var users = await _conexion.QueryAsync<usuariosN>("SelectIdUser",new 
            { @idusuario = idusuario }, commandType: CommandType.StoredProcedure);
            return Ok(users);
        }
        //------------------------------------------------- 
        [HttpPost("create")]

        public async Task<ActionResult<usuariosN>> CreateUsuario(usuariosN NewUser)
        {
           var users= await _conexion.ExecuteAsync("CreateUsuario", new
           {
              
               @nombre= NewUser.nombre,
               @direccion= NewUser.direccion,
               @telefono= NewUser.telefono,
               @codigoP= NewUser.codigoP,
               @estado= NewUser.estado,
               @ciudad= NewUser.ciudad,
               @TipoU = NewUser.TipoU,

           }, commandType: CommandType.StoredProcedure);
            return Ok(users);
        }

        //------------------------------------------------- revisar
           
        [HttpPut("actualizar")]

        public async Task<ActionResult<List<usuariosN>>> UpdateUsuario(usuariosN NewUser)
        {


            var users = await _conexion.ExecuteAsync("UpdateUsuarios", new
            {
                @idusuario = NewUser.idusuario,
                @nombre = NewUser.nombre,
                @direccion = NewUser.direccion,
                @telefono = NewUser.telefono,
                @codigoP = NewUser.codigoP,
                @estado = NewUser.estado,
                @ciudad = NewUser.ciudad,
                @TipoU = NewUser.TipoU,
                 
            }, commandType: CommandType.StoredProcedure);
            return Ok(users);   
        }
        //---------------------------------------------
        [HttpGet("eliminar/{idusuario}")] 
        public async Task<ActionResult<List<usuariosN>>> DeleteUsuario(int idusuario) 
        {
            var users = await _conexion.ExecuteAsync("DeleteUsuario", new { idusuario = @idusuario }, commandType: CommandType.StoredProcedure);
            return Ok(users);
        }
    }
}
