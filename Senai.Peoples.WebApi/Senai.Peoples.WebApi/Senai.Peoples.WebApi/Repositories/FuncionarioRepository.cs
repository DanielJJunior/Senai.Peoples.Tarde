using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_peoples_webApi.Repositories
{
  
    public class FuncionarioRepository : IFuncionarioRepository
    {
      
        private string stringConexao = "Data Source=DANIEL-PC\\SQLEXPRESS;initial catalog=T_Peoples;user Id=sa; pwd=1Senhasegur@";
         
        public void Atualizar(int id, FuncionarioDomain funcionarioAtualizado)
        {
            
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
               
                string queryUpdate = "UPDATE Funcionarios " +
                                     "SET Nome = @Nome, Sobrenome = @Sobrenome, DataNascimento = @DataNascimento " +
                                     "WHERE IdFuncionario = @ID";

                
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                   
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", funcionarioAtualizado.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarioAtualizado.Sobrenome);

                    con.Open();

                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        
        public FuncionarioDomain BuscarPorID(int id)
        {
            
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string querySelectById = "SELECT * FROM Funcionarios;" +
                                        " WHERE IdFuncionario = @ID";

                
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

                            idFuncionario = Convert.ToInt32(rdr["IdFuncionario"])
                            ,Nome = rdr["Nome"].ToString()
                            ,Sobrenome = rdr["Sobrenome"].ToString()

                          
                        };

                        
                        return funcionario;
                    }

                    
                    return null;
                }
            }
        }

        
       

      
        public void Cadastrar(FuncionarioDomain novoFuncionario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string queryInsert = "INSERT INTO Funcionarios(Nome, Sobrenome) " +
                                     "VALUES (@Nome, @Sobrenome)";

                
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                    cmd.Parameters.AddWithValue("@Nome", novoFuncionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", novoFuncionario.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
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

    
        public List<FuncionarioDomain> ListarTodos()
        {
            
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

          
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT * FROM Funcionarios;";

                
                con.Open();

                
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {

                            idFuncionario = Convert.ToInt32(rdr["IdFuncionario"])
                            ,Nome = rdr["Nome"].ToString()
                            ,Sobrenome = rdr["Sobrenome"].ToString()


                        };

                     
                        funcionarios.Add(funcionario);
                    }
                }
            }

           
            return funcionarios;
        }

        
        
    }
}