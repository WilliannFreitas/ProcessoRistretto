using NSwag.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessoRistretto.Models
{
    [Table(name: "FUNCIONARIO")]
    public class Funcionario
    {
        [Key]
        [Column("ID_FUNCIONARIO")]
        public Int64 IdFuncionario { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }

        [Column("SOBRENOME")]
        public string Sobrenome { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("CARGO")]
        public string Cargo { get; set; }

        [Column("LOGIN")]
        public string Login { get; set; }

        [Column("SENHA")]
        public string Senha { get; set; }

        [Column("DT_NASCIMENTO")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        [Column("STATUS_FUNCIONARIO")]
        [Display(Name = "Funcionário Ativo?")]
        public bool StatusFuncionario { get; set; }

    }

    public class FuncionarioParam
    {
        public Int64? IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool? StatusFuncionario { get; set; }
    }
}
