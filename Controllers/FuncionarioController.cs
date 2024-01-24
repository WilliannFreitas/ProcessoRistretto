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
using Microsoft.Graph;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult ConsultarFuncionario([FromQuery] Funcionario funcionario)
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

