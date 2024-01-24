using Microsoft.Graph;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessoRistretto.Models
{
    [Table(name: "EMPRESA")]
    public class Empresa
    {
     
        [Key]
        [Column("ID_EMPRESA")]
        public Int64 IdEmpresa { get; set;}

        [Column("NOME_EMPRESARIAL")]
        public string NomeEmpresarial { get; set; }

        [RegularExpression(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Telefone Inválido")]
        [Column("TELEFONE_1")]
        [Display(Name = "(DDD) Telefone Celular")]
        [DisplayFormat(DataFormatString = @"{0:(##) # ####-####}")]
        public long DddTelefone { get; set; }

        [Column("URL")]
        public string Url { get; set; }

    }

    public class EmpresaParam
    {
        public Int64? IdEmpresa { get; set; }
        public string NomeEmpresarial { get; set; }
        public long? DddTelefone { get; set; }
        public string Url { get; set; }
    }
}
