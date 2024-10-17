using C__API_ASP.NET_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace C__API_ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarClientes()
        {
            List<ClienteModel> clientes = new List<ClienteModel>
            {
                new ClienteModel
                {
                    id = "1",
                    nombre = "Eric",
                    edad = 1,
                    correo = "eric@gmail.com"
                },
                new ClienteModel
                {
                    id = "2",
                    nombre = "Miguel",
                    edad = 23,
                    correo = "miguel@gmail.com"
                }
            };

            return clientes;
        }
        
        
        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(ClienteModel cliente)
        {
            cliente.id = "34";

            return new
            {
                success = true,
                messsage = "cliente registrado", 
                result = cliente
            };
        }

        [HttpGet]
        [Route("listar_id")]
        public dynamic listarClienteId(string _id)
        {
            List<ClienteModel> clientes = new List<ClienteModel>
            {
                new ClienteModel
                {
                    id = _id,
                    nombre = "Eric",
                    edad = 1,
                    correo = "eric@gmail.com"
                }
            };
            return clientes;
        }

        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarCliente(ClienteModel cliente)
        {
            string token = Request.Headers.Where(x => x.Key == "Auth").FirstOrDefault().Value;

            if (token != "1234")
            {
                return new
                {
                    success = false,
                    messsage = "Token incorrecto"
                };
            }

            return new
            {
                success = true,
                messsage = "cliente eliminado",
                result = cliente
            };
        }
    }
}
