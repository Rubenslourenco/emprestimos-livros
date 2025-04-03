using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace emprestimos_livros.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite nome do recebedor")]
        public string Recebedor { get; set; }
        [Required(ErrorMessage = "Digite  nome do fornecedor")]
        public string Fornecedor { get; set; }
        [Required(ErrorMessage = "Digite nome do livro")]
        public string LivroEmprestado { get; set; }
        public DateTime? dataUltimaAtualizacao { get; set; }














    }
}