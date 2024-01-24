using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoRistretto.Models;

namespace ProcessoRistretto.Repository
{
    public interface IEmpresaRepository
    {
        public bool Inserir(Empresa param);
        public bool Alterar(Empresa param);

        public List<Empresa> Consultar(Empresa empresa, bool EAlteracao = false);

    }
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly RistrettoContext db;

        public EmpresaRepository(RistrettoContext _db)
        {
            db = _db;
        }
        public bool Inserir(Empresa empresa)
        {
            try
            {
                db.Add(empresa);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Alterar(Empresa empresa)
        {
            try
            {
                db.Update(empresa);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Empresa> Consultar(Empresa empresa, bool EAlteracao = false)
        {
            try
            {
                using (var context = db)
                {

                    var teste = context.Empresas.ToList();

                    if (empresa.IdEmpresa > 0)
                        teste = teste.Where(banco => banco.IdEmpresa == empresa.IdEmpresa).ToList();

                    if (!String.IsNullOrWhiteSpace(empresa.NomeEmpresarial))
                        teste = teste.Where(banco => banco.NomeEmpresarial.ToUpper().Contains(empresa.NomeEmpresarial.ToUpper())).ToList();

                    if (empresa.DddTelefone > 0)
                        teste = teste.Where(banco => banco.DddTelefone == empresa.DddTelefone).ToList();

                    if (!String.IsNullOrWhiteSpace(empresa.Url))
                        teste = teste.Where(banco => banco.Url.ToUpper().Contains(empresa.Url.ToUpper())).ToList();

                    return teste;
                }

            }
            catch (Exception ex)
            {
                return new List<Empresa>();
            }
        }
    }
}
