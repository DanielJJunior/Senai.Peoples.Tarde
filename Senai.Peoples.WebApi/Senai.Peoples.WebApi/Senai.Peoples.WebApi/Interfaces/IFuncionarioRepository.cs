using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Peoples.WebApi.Domains;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionarioRepository
    {
        List<FuncionarioDomain> ListarTodos();

        FuncionarioDomain BuscarPorID(int id);

        void Cadastrar(FuncionarioDomain novoFuncionario);

        void Atualizar(int id, FuncionarioDomain funcionarioAtualizado);

        void Deletar(int id);
 
    }
}
