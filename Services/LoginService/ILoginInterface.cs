using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emprestimos_livros.Dto;
using emprestimos_livros.Models;

namespace emprestimos_livros.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto);
    }
}