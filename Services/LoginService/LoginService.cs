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

        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto)

        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                if (VerificacarSeEmailExiste(usarioRegisterDto))
                {
                    response.Mensagem = "Email já cadastro";
                    response.Status = "false";
                    return response;
                }
                else
                {
                    UsuarioModel usuario = new UsuarioModel()
                    {
                        Nome = usuarioRegisterDto.Nome,
                        Sobrenome = usuarioRegisterDto.Sobrenome,
                        Email = usuarioRegisterDto.Email,
                        SenhaHash = usuarioRegisterDto.SenhaHash,
                        SenhaSalt = usuarioRegisterDto.SenhaSalt
                    };

                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();

                    response.Dados = usuario;
                    response.Mensagem = "Usuario registrado com sucesso";
                    response.Status = "true";
                    return response;
                }


            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = "false";
                return response;

            }
        }

        private bool VerificacarSeEmailExiste(UsuarioRegisterDto usuarioRegisterDto)
        {

            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioRegisterDto.Email);
            if (usuario != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}