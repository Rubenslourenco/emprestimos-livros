using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emprestimos_livros.Models;

namespace emprestimos_livros.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoveSessao();
    }
}