using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using emprestimos_livros.Models;

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

}
