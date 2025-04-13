
using emprestimos_livros.Models;

namespace emprestimos_livros.Services.EmprestimosService
{
    public interface IEmprestimosInterface
    {
        Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos();
        Task<ResponseModel<EmprestimosModel>> BuscarEmprestimoPorId(int? id);

    }
}