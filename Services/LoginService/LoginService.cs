using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emprestimos_livros.data;
using emprestimos_livros.Dto;
using emprestimos_livros.Models;

namespace emprestimos_livros.Services.LoginService
{
    public class LoginService : ILoginInterface
    {


        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;

        }

        public Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {

            }
            catch (Exception ex)
            {


            }
        }
    }