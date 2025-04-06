using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emprestimos_livros.Services.SenhaService
{
    public interface ISenhaInterface
    {
        void CriarSenha(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}