using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using senai_peoples_webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_peoples_webApi.Controllers
{
   
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        
        public FuncionariosController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(_funcionarioRepository.ListarTodos());
        }

        
        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            if (novoFuncionario.Nome == null)
            {
                return BadRequest("O nome do funcionário é obrigatório!");
            }
            
            _funcionarioRepository.Cadastrar(novoFuncionario);

            
            return Created("http://localhost:5000/api/Funcionarios", novoFuncionario);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorID(id);
            if (funcionarioBuscado != null)
            {

                return Ok(funcionarioBuscado);
            }
            return NotFound("Nenhum funcionário encontrado para o identificador informado");
        }

     
        [HttpPut("{id}")]
        public IActionResult Put(int id, FuncionarioDomain funcionarioAtualizado)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorID(id);


            if (funcionarioBuscado != null)
            {

                try
                {

                    _funcionarioRepository.Atualizar(id, funcionarioAtualizado);


                    return NoContent();
                }
                catch (Exception erro)
                {

                    return BadRequest(erro);
                }

            }
            return NotFound
                (
                    new
                    {
                        mensagem = "Funcionário não encontrado",
                        erro = true
                    }
                );
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorID(id);


            if (funcionarioBuscado != null)
            {
                _funcionarioRepository.Deletar(id);

                
                return Ok($"O funcionário {id} foi deletado com sucesso!");
            }

            
            return NotFound("Nenhum funcionário encontrado para o identificador informado");
        }

        
        }

      

    }
