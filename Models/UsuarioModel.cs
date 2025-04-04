using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emprestimos_livros.Models
{
    public class UsuarioModel
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }

    }
}