using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        public FuncionarioController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]

        public IEnumerable<FuncionarioDomain> Get()
        {
            return _funcionarioRepository.Listar();
        }

        [HttpPost]

        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            _funcionarioRepository.Cadastrar(novoFuncionario);

            return StatusCode(201);
        }

        [HttpPut("{id}")]

        public IActionResult PutId(int id, FuncionarioDomain funcionarioAtualizado)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionário não encontrado",
                            erro = true
                        }
                    );
            }

            try
            {
                _funcionarioRepository.AtualizarId(id, funcionarioAtualizado);

                
                return NoContent();
            }

            catch (Exception erro)
            {
                return BadRequest(erro);

            }
            
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);
            if (funcionarioBuscado == null)
            {
                return NotFound("Funcionário não encontrado");
            }

            return Ok(funcionarioBuscado);

        }

        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            _funcionarioRepository.Deletar(id);

            return Ok("Funcionário Deletado");
        }

    }
}