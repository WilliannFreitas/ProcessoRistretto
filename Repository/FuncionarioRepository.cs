using ProcessoRistretto.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoRistretto.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly RistrettoContext db;

        public FuncionarioRepository(RistrettoContext _db)
        {
            db = _db;
        }

        public bool Inserir(Funcionario funcionario)
        {
            try
            {
                db.Add(funcionario);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Alterar(Funcionario funcionario)
        {
            try
            {
                db.Update(funcionario);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<Funcionario> Consultar(FuncionarioParam funcionarioParam, bool EAlteracao = false)
        {
            try
            {

                Funcionario funcionario = new Funcionario()
                {
                    IdFuncionario = (long)funcionarioParam.IdFuncionario,
                    Cargo = funcionarioParam.Cargo,
                    DataNascimento = (DateTime)funcionarioParam.DataNascimento,
                    Email = funcionarioParam.Email,
                    Login = funcionarioParam.Login,
                    Nome = funcionarioParam.Nome,
                    Senha = funcionarioParam.Senha,
                    Sobrenome = funcionarioParam.Sobrenome,
                    StatusFuncionario = (bool)funcionarioParam.StatusFuncionario
                };

                using (var context = db)
                {

                    var teste = context.Funcionarios.ToList();

                    if (funcionario.IdFuncionario > 0)
                        teste = teste.Where(banco => banco.IdFuncionario == funcionario.IdFuncionario).ToList();

                    if (!EAlteracao)
                    {
                        if (!String.IsNullOrWhiteSpace(funcionario.Nome))
                            teste = teste.Where(banco => banco.Nome.ToUpper().Contains(funcionario.Nome.ToUpper())).ToList();

                        if (!String.IsNullOrWhiteSpace(funcionario.Sobrenome))
                            teste = teste.Where(banco => banco.Sobrenome.ToUpper().Contains(funcionario.Sobrenome.ToUpper())).ToList();

                        if (!String.IsNullOrWhiteSpace(funcionario.Login))
                            teste = teste.Where(banco => banco.Login.ToUpper().Contains(funcionario.Login.ToUpper())).ToList();

                        if (funcionario.Senha.Length > 6 && funcionario.Senha.Length < 12)
                            teste = teste.Where(banco => banco.Senha == funcionario.Senha).ToList();

                        if (!String.IsNullOrWhiteSpace(funcionario.Email))
                            teste = teste.Where(banco => banco.Email.ToUpper().Contains(funcionario.Email.ToUpper())).ToList();

                        if (!String.IsNullOrWhiteSpace(funcionario.Cargo))
                            teste = teste.Where(banco => banco.Cargo.ToUpper().Contains(funcionario.Cargo.ToUpper())).ToList();

                        if (funcionario.DataNascimento < DateTime.Now.AddYears(-115))
                            teste = teste.Where(banco => banco.DataNascimento == funcionario.DataNascimento).ToList();

                        teste = teste.Where(banco => banco.StatusFuncionario == funcionario.StatusFuncionario).ToList();

                    }

                    return teste;
                }

            }
            catch (Exception ex)
            {
                return new List<Funcionario>();
            }
        }
    }
}
