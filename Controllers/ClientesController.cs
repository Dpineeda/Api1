using Microsoft.AspNetCore.Mvc;
using Proyecto1Api.Models;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto1Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {

        private readonly DbEmpresaContext _context;
        public ClientesController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }

            [HttpGet("Listar usuarios")]
            public ActionResult<List<Cliente>> Listarclientes()
            {
                return _context.Clientes.ToList();
            }

        [HttpGet ("ObtenerUsuario/{id}")]
        public ActionResult<Cliente> ObtenerUsuario(int id)
        {
            return _context.Clientes.FirstOrDefault(e => e.IdCliente == id);
        }

        [HttpPost("CrearUsuario")]
        public ActionResult<Cliente> CrearUsuario([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObtenerUsuario), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("ActualizarUsuario/{id}")]
        public ActionResult ActualizarUsuario(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            var existente = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
            if (existente == null)
            {
                return NotFound();
            }

            existente.Nombre = cliente.Nombre;
            existente.Apellidos = cliente.Apellidos;
            existente.Telefono = cliente.Telefono;

            _context.SaveChanges();

            return NoContent();
        }


        }
    } 

