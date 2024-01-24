using ProcessoRistretto.Models;
using System.Collections.Generic;

namespace ProcessoRistretto.Repository.Interface
{
    public interface IEmpresaRepository
    {
        public bool Inserir(Empresa param);
        public bool Alterar(Empresa param);

        public List<Empresa> Consultar(EmpresaParam empresa, bool EAlteracao = false);

    }
}
