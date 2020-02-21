using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {

        private string stringConexao = "Data Source=LAPTOP-6GS7ET34; initial catalog=M_Peoples_T_Peoples; user Id=sa; pwd=123456";

        public void AtualizarId(int id, FuncionarioDomain funcionario)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Funcionarios SET Nome = @Nome WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);

                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdFuncionario, Nome FROM funcionarios WHERE IdFuncionario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    
                    cmd.Parameters.AddWithValue("@ID", id);

                    
                    rdr = cmd.ExecuteReader();

                    
                    if (rdr.Read())
                    {
                       
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                           
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"])

                           
                            ,
                            Nome = rdr["Nome"].ToString()
                        };

                        
                        return funcionario;
                    }

                    
                    return null;
                }
            }
        }

        public void Cadastrar(FuncionarioDomain funcionario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Funcionarios(Nome) VALUES (@Nome)";

                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    
                    con.Open();

                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdFuncionario, Nome FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString()
                        };

                        funcionarios.Add(funcionario);
                    }

                }
            }

            return funcionarios;

        }
    }
}
