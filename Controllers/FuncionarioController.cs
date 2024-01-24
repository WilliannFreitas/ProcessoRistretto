using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ProcessoRistretto.Models;
using ProcessoRistretto.Repository;

namespace ProcessoRistretto.Controllers
// Este código contém um passo a passo litertal para melhor entendimento de sua construção.
//API's de consulta e inclusão do banco de dados via Link.
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class FuncionarioController : ControllerBase
    {

        private readonly FuncionarioRepository repos;

        public FuncionarioController(FuncionarioRepository _repos)
        {
            repos = _repos;
        }

        /// <summary>
        /// Método de consulta da API.
        /// </summary>
        /// <param name="funcionario">objeto usuário</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ConsultarFuncionario([FromQuery] Funcionario funcionario)
        {
            //setando no try e catch a variavel que recebe a consulta no banco de dados
            try
            {
                var funcionario_db = repos.Consultar(funcionario);
                return Ok(funcionario_db);
            }
            catch (Exception ex)
            {
                return Ok($" ERRO: {ex} - {ex.InnerException} ");
            }

        }
        //Método de inclusão de usuário da API.
        [HttpPost]
        public IActionResult InserirAlterarFuncionario(FuncionarioParam Param)
        {
            // filtros de validação de preenchimento de campos de pesquisa.
            string campos = string.Empty;
            if (string.IsNullOrWhiteSpace(Param.Nome))
                campos += " Nome,";
            if (string.IsNullOrWhiteSpace(Param.Sobrenome))
                campos += " Sobrenome,";
            if (string.IsNullOrWhiteSpace(Param.Login))
                campos += " Login,";
            if (Param.DataNascimento < DateTime.Now.AddYears(-100))
                campos += " Data de Nascimento";

            if (!string.IsNullOrWhiteSpace(campos))
                return StatusCode((int)HttpStatusCode.NotAcceptable, $"O(s) campos(s){campos} são de preenchimento obrigatório!");
            //-------------------------------------------------------------

            try
            {
                //atribuindo os valores de param para dentro do novo funcionario instanciado.
                Funcionario funcionario = new Funcionario();
                funcionario.Nome = Param.Nome;
                funcionario.Sobrenome = Param.Sobrenome;
                funcionario.Login = Param.Login;
                funcionario.Senha = Param.Senha;
                funcionario.Status = Param.Status;
                funcionario.DataNascimento = Param.DataNascimento;

                //atrinbuindo novo funcionario. 
                //if (Param.IdFuncionario <= 0)
                //{
                //    funcionario.DataInclusao = DateTime.Now;
                //    repos.Inserir(funcionario);
                //}
                //atrinbuindo alterações em um funcionario existente.
                //else
                //{
                //    funcionario.DataAlteracao = DateTime.Now;
                //    funcionario.IdFuncionario = Param.IdFuncionario;
                //    repos.Alterar(funcionario);
                //}

                //retornando funcionario instanciado acima.
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

    }

}

