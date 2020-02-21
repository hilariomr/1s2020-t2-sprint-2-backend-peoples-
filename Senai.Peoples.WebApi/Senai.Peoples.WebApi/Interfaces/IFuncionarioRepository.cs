using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionarioRepository
    {

        List<FuncionarioDomain> Listar();

        void Cadastrar(FuncionarioDomain funcionario);

        FuncionarioDomain BuscarPorId(int id);

        void AtualizarId(int id, FuncionarioDomain funcionario);

        void Deletar(int id);
    }

}
