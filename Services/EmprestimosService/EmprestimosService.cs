using System.Data;
using Azure;
using emprestimos_livros.data;
using emprestimos_livros.Models;
using Microsoft.EntityFrameworkCore;

namespace emprestimos_livros.Services.EmprestimosService
{
    public class EmprestimosService : IEmprestimosInterface
    {

        private readonly ApplicationDbContext _context;

        public EmprestimosService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos()

        {
            ResponseModel<List<EmprestimosModel>> response = new ResponseModel<List<EmprestimosModel>>();

            try
            {
                var emprestimo = await _context.Emprestimos.ToListAsync();

                if (emprestimo == null || emprestimo.Count == 0)
                {
                    response.Mensagem = "Nenhum empréstimo encontrado.";
                    response.Status = false;
                    return response;
                }

                response.Dados = emprestimo;
                response.Mensagem = "Dados coletados com sucesso";

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<EmprestimosModel>> BuscarEmprestimoPorId(int? id)
        {
            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {
                if (id == null)
                {
                    response.Mensagem = "Emprestimo não encontrado!";
                    response.Status = false;
                    return response;
                }

                var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(x => x.Id == id);

                if (emprestimo == null)
                {
                    response.Mensagem = "Emprestimo não encontrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = emprestimo;
                response.Mensagem = "Dados coletados com sucesso";
                return response;
            }
            catch (Exception ex)
            {

                response.Mensagem = ex.Message;
                response.Status = false;
                return response;

            }
        }


        public async Task<DataTable> BuscarDadosEmprestimosExcel()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados do emprestimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data Emprestimos", typeof(DateTime));

            var emprestimos = await BuscarEmprestimos();

            if (emprestimos.Dados.Count > 0)
            {
                emprestimos.Dados.ForEach(emprestimo =>
            {
                dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.dataUltimaAtualizacao);
            });
            }
            return dataTable;
        }

        public async Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimo(EmprestimosModel emprestimosModel)
        {
            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {
                _context.Add(emprestimosModel);
                await _context.SaveChangesAsync();

                response.Mensagem = "Cadastro realizado com secesso";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<EmprestimosModel>> EditarEmprestimo(EmprestimosModel emprestimosModel)
        {
            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {
                var emprestimo = await BuscarEmprestimoPorId(emprestimosModel.Id);

                if (emprestimo.Status == false)
                {
                    return emprestimo;
                }

                emprestimo.Dados.LivroEmprestado = emprestimosModel.LivroEmprestado;
                emprestimo.Dados.Fornecedor = emprestimosModel.Fornecedor;
                emprestimo.Dados.Recebedor = emprestimosModel.Recebedor;

                _context.Add(emprestimo);
                await _context.SaveChangesAsync();

                response.Mensagem = "Edição realizada com sucesso";


                return response;
            }
            catch (Exception ex)
            {

                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}