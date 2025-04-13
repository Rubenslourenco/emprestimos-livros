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
    }
}