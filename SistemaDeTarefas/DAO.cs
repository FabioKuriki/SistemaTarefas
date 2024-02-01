using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;


namespace SistemaDeTarefas
{
    class DAO
    {
        public MySqlConnection bd;
        public string dadosInseridos;//Variável para guardar o dados a serem inclúídos
        public string insertUsuario;//Variável para o comando de execução de INSERT
        public string resultado;
        public DAO()
        {
            bd = new MySqlConnection("server = localhost; DataBase = tarefas; Uid = root; Password = ");//Localização do Banco de Dados
            try
            {
                bd.Open();//Para abrir a conexão com o BD
                Console.WriteLine("Conexão ao Banco de Dados realizado com sucesso");//Mensagem para verificar se conexão foi bem sucedida
            }
            catch(Exception erro)//Caso não funcione
            {
                Console.WriteLine("Banco de dados não conectado devido ao erro: " + erro);
                bd.Close();//Fechar a conexão
            }//Fim do try catch
        }//Fim do método

        public void Inserir(string entidade, string login, string senha, string nome, long telefone, string endereco)
        {
            try
            {
                dadosInseridos = "('" + login + "','" + senha + "','" + nome + "','" + telefone + "','" + endereco + "')";
                insertUsuario = "insert into " + entidade + "(login, senha, nome, telefone, endereco) values" + dadosInseridos;

                MySqlCommand insert = new MySqlCommand(insertUsuario, bd);//Prepara a execução no banco
                resultado = "" + insert.ExecuteNonQuery();//Ctrl + Enter
                Console.WriteLine(resultado + "Linha(s) afetada(s)");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro!! Algo deu errado\n\n\n" + erro);
            }

        }//Fim do método inserir
    }
}
