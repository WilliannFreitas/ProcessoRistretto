﻿using Microsoft.AspNetCore.Authorization;
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

namespace ProcessoRistretto.Controllers
{
    //[Authorize]
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
        public IActionResult ConsultarEmpresa([FromQuery] Empresa empresa)
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