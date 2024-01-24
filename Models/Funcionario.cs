using NSwag.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessoRistretto.Models
{
    [Table(name:"FUNCIONARIO")]
    public class Funcionario
    {
        [Key]
        [SwaggerIgnore]
        [Column("ID_FUNCIONARIO")]
        public Int64 IdFuncionario { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }

        [Column("SOBRENOME")]
        public string Sobrenome { get; set; }

        [Column("LOGIN")]
        public string Login { get; set; }

        [Column("SENHA")]
        public Int64 Senha { get; set; }

        [Column("DT_NASCIMENTO")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        [Column("STATUS")]
        [Display(Name = "Funcionário Ativo?")]
        public bool Status { get; set; }

        //[Column("DT_INCLUSAO")]
        //public DateTime DataInclusao { get; set; }

        //[Column("DT_ALTERACAO")]
        //public DateTime? DataAlteracao { get; set; }

    }

    public class FuncionarioParam
    {
        public Int64 IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Login { get; set; }
        public Int64 Senha { get; set; }
        public bool Status { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
