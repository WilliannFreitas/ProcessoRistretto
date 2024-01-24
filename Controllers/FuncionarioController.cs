using Microsoft.AspNetCore.Mvc;
using ProcessoRistretto.Models;
using ProcessoRistretto.Repository;
using System;
using System.Net;

namespace ProcessoRistretto.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepository repos;

        public FuncionarioController(FuncionarioRepository _repos)
        {
            repos = _repos;
        }

        [HttpGet]
        public IActionResult ConsultarFuncionario([FromQuery] FuncionarioParam funcionario)
        {
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

        [HttpPost]
        public IActionResult InserirAlterarFuncionario(Funcionario funcionario)
        {

            string campos = string.Empty;
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
                campos += " Nome,";
            if (string.IsNullOrWhiteSpace(funcionario.Sobrenome))
                campos += " Sobrenome,";
            if (string.IsNullOrWhiteSpace(funcionario.Login))
                campos += " Login,";
            if (string.IsNullOrWhiteSpace(funcionario.Email))
                campos += " Email,";
            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                campos += " Cargo,";
            if (string.IsNullOrWhiteSpace(funcionario.Senha) && funcionario.Senha.Length > 6 && funcionario.Senha.Length < 12)
                campos += " Senha entre 6 e 12 Digitos,";
            if (funcionario.DataNascimento < DateTime.Now.AddYears(-115))
                campos += " Data de Nascimento";

            if (funcionario.StatusFuncionario == null)
                campos += " Status Funcionario";

            if (!string.IsNullOrWhiteSpace(campos))
                return StatusCode((int)HttpStatusCode.NotAcceptable, $"O(s) campos(s){campos} são de preenchimento obrigatório!");

            try
            {
                if (funcionario.IdFuncionario <= 0)
                    repos.Inserir(funcionario);
                else
                {
                    funcionario.IdFuncionario = funcionario.IdFuncionario;
                    repos.Alterar(funcionario);
                }

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

    }

}

