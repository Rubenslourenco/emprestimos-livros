using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using emprestimos_livros.data;
using emprestimos_livros.Models;
using emprestimos_livros.Services.EmprestimosService;
using emprestimos_livros.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace emprestimos_livros.Controllers
{
    public class EmprestimoController : Controller
    {


        readonly private ISessaoInterface _sessaoInterface;
        private readonly IEmprestimosInterface _emprestimosInterface;

        public EmprestimoController(ApplicationDbContext db, IEmprestimosInterface emprestimosInterface, ISessaoInterface sessaoInterface)
        {

            _sessaoInterface = sessaoInterface;
            _emprestimosInterface = emprestimosInterface;
        }
        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // IEnumerable<EmprestimosModel> emprestimos = _db.Emprestimos;
            var emprestimos = await _emprestimosInterface.BuscarEmprestimos();
            return View(emprestimos.Dados);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {

            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimosInterface.BuscarEmprestimoPorId(id);

            return View(emprestimo.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimosInterface.BuscarEmprestimoPorId(id);

            return View(emprestimo);

        }

        public async Task<IActionResult> Exportar()
        {
            var dados = await _emprestimosInterface.BuscarDadosEmprestimosExcel();

            using (XLWorkbook workBook = new XLWorkbook())
            {

                workBook.AddWorksheet(dados, "Dados Emprestimos");

                using (MemoryStream ms = new MemoryStream())
                {

                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimos.xlsx");
                }

            }

        }



        [HttpPost]
        public async Task<IActionResult> Cadastrar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid)
            {

                var emprestimoResult = await _emprestimosInterface.CadastrarEmprestimo(emprestimos);

                if (emprestimoResult.Status)
                {
                    TempData["MensagemSucesso"] = emprestimoResult.Mensagem;

                }
                else
                {
                    TempData["MensagemErro"] = emprestimoResult.Mensagem;
                    return View(emprestimoResult);
                }

                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar o cadastro!";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Editar(EmprestimosModel emprestimo)
        {

            if (ModelState.IsValid)
            {
                var emprestimoResult = await _emprestimosInterface.EditarEmprestimo(emprestimo);

                if (!emprestimoResult.Status)
                {
                    if (emprestimoResult.Status)
                    {
                        TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                    }
                    else
                    {
                        TempData["MensagemErro"] = emprestimoResult.Mensagem;
                    }



                }
                return RedirectToAction("Index");

            }
            TempData["MensagemErro"] = "Algum erro ocorreu al realizar a ediçao!";

            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(EmprestimosModel emprestimo)
        {

            if (emprestimo == null)
            {
                TempData["MensagemErro"] = "Emprestimo não localizado";
                return RedirectToAction("Index");
            }

            var emprestimoResult = await _emprestimosInterface.RemoveEmprestimo(emprestimo);

            if (emprestimoResult.Status)
            {
                TempData["MensagemSucesso"] = emprestimoResult.Mensagem;

            }
            else
            {
                TempData["MensagemErro"] = emprestimoResult.Mensagem;
            }
            return RedirectToAction("Index");


        }

    }
}