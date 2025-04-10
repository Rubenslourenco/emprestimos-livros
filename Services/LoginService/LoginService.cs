using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emprestimos_livros.data;
using emprestimos_livros.Dto;
using emprestimos_livros.Models;
using emprestimos_livros.Services.SenhaService;

namespace emprestimos_livros.Services.LoginService
{
    public class LoginService : ILoginInterface
    {


        private readonly ApplicationDbContext _context;
        private readonly ISenhaInterface _senhaInterface;


        public LoginService(ApplicationDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;

        }

        public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioLoginDto.Email);

                if (usuario == null)
                {
                    response.Mensagem = "Credenciais inválidas";
                    response.Status = false;
                    return response;
                }

                if (!_senhaInterface.VerificarSenha(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Credenciais inválidas";
                    response.Status = false;
                    return response;
                }
                {
                    response.Mensagem = "Credenciais inválidas";
                    response.Status = false;
                    return response;
                }

                response.Mensagem = "Login realizado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto)

        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                if (VerificarSeEmailExiste(usuarioRegisterDto))
                {
                    response.Mensagem = "Email já cadastro";
                    response.Status = false;
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioRegisterDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel()
                {
                    Nome = usuarioRegisterDto.Nome,
                    Sobrenome = usuarioRegisterDto.Sobrenome,
                    Email = usuarioRegisterDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuario cadastrado com sucesso";

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return response;

            }
        }

        private bool VerificarSeEmailExiste(UsuarioRegisterDto usuarioRegisterDto)
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