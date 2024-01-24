using Microsoft.AspNetCore.Mvc;
using ProcessoRistretto.Models;
using ProcessoRistretto.Repository.Interface;
using System;
using System.Net;

namespace ProcessoRistretto.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository repos;

        public EmpresaController(IEmpresaRepository _repos)
        {
            repos = _repos;
        }


        [HttpGet]
        public IActionResult ConsultarEmpresa([FromQuery] EmpresaParam empresa)
        {
            try
            {
                var empresa_db = repos.Consultar(empresa);
                return Ok(empresa_db);
            }
            catch (Exception ex)
            {
                return Ok($" ERRO: {ex} - {ex.InnerException} ");
            }

        }

        [HttpPost]
        public IActionResult InserirAlterarEmpresa(Empresa empresa)
        {
            string campos = string.Empty;
            if (string.IsNullOrWhiteSpace(empresa.NomeEmpresarial))
                campos += " Nome Empresarial,";
            if (string.IsNullOrWhiteSpace(empresa.Url))
                campos += " URL,";
            if (empresa.DddTelefone < 10)
                campos += " DDD e Telefone,";

            if (!string.IsNullOrWhiteSpace(campos))
                return StatusCode((int)HttpStatusCode.NotAcceptable, $"O(s) campos(s) {campos} obrigatórios, prescisam ser preenchidos corretamente!");

            try
            {
                if (empresa.IdEmpresa <= 0)
                    repos.Inserir(empresa);
                else
                {
                    empresa.IdEmpresa = empresa.IdEmpresa;
                    repos.Alterar(empresa);
                }

                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
