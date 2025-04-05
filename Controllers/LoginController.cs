using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using emprestimos_livros.Models;
using emprestimos_livros.Dto;

namespace emprestimos_livros.Controllers;

public class LoginController : Controller
{


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrar(UsuarioRegisterDto usuarioRegisterDto)
    {
        return View(usuarioRegisterDto);
    }
}
