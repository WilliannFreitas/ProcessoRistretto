using ProcessoRistretto.Models;
using System.Collections.Generic;

namespace ProcessoRistretto.Repository
{
    public interface IFuncionarioRepository
    {
        public bool Inserir(Funcionario param);
        public bool Alterar(Funcionario param);

        public List<Funcionario> Consultar(FuncionarioParam funcionario, bool EAlteracao = false);

    }
}
