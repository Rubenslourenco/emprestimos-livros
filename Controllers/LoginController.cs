using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using emprestimos_livros.Models;
using emprestimos_livros.Dto;
using emprestimos_livros.Services.LoginService;

namespace emprestimos_livros.Controllers;

public class LoginController : Controller
{

    private readonly ILoginInterface _loginInterface;

    public LoginController(ILoginInterface loginInterface)
    {
        _loginInterface = loginInterface;
    }


    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(UsuarioRegisterDto usuarioRegisterDto)
    {
        if (ModelState.IsValid)
        {

            var usuario = await _loginInterface.RegistrarUsuario(usuarioRegisterDto);

            if (usuario.Status)
            {
                TempData["MensagemSucesso"] = usuario.Mensagem;
            }
            else
            {

                TempData["MensagemSucesso"] = usuario.Mensagem;
                return View(usuarioRegisterDto);
            }

            return RedirectToAction("Index");

        }
        else
        {
            return View(usuarioRegisterDto);
        }

    }
    [HttpPost]
    public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
    {
        if (ModelState.IsValid)
        {
            var usuario = await _loginInterface.Login(usuarioLoginDto);

            if (usuario.Status)
            {
                TempData["MensagemSucesso"] = usuario.Mensagem;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["MensagemErro"] = usuario.Mensagem;
                return View("Index", usuarioLoginDto);
            }
        }
        else
        {
            return Redirect("Index");
        }
    }


}
