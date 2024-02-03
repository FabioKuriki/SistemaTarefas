using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaDeTarefas
{
    class DAOTarefas
    {
        public int[] idTarefas;
        public string[] nomeTarefa;
        public string[] descricao;
        public DateTime[] dataHora;
        public MySqlConnection bd;
        public int i;
        public int contador;

        //Método para conectar no BD
        public DAOTarefas()
        {
            bd = new MySqlConnection("server = localhost; DataBase = tarefas; Uid = root; Password = ");//Localização do Banco de Dados
            bd.Open();
        }//Fim do método

        //Método para inserir dados na entidade Tarefas
        public void InserirTarefas(string nome, string descricao, string dataHora)
        {
            try
            {
                //Olhar a parte da chave estrangeira
                string dadosInseridos = "('','" + nome + "','" + descricao + "','" + dataHora + "')";
                string insertUsuario = "insert into tarefasDados(codigoTarefas, nome, descricao, dataHora) values" + dadosInseridos;

                MySqlCommand insert = new MySqlCommand(insertUsuario, bd);//Prepara a execução no banco
                string resultado = "" + insert.ExecuteNonQuery();//Ctrl + Enter
                Console.WriteLine("Tarefa criada com sucesso");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro!! Algo deu errado\n\n\n" + erro);
            }

        }//Fim do método Inserir

        //Método para pegar os dados do BD e armazenar em variáveis para uso
        public void PreencherTarefas()
        {

            string consultar = "select * from tarefasDados";

            idTarefas = new int[100];
            nomeTarefa = new string[100];
            descricao = new string[100];
            dataHora = new DateTime[100];

            for (i = 0; i < 100; i++)
            {
                idTarefas[i] = 0;
                nomeTarefa[i] = "";
                descricao[i] = "";
                dataHora[i] = DateTime.Now;
            }//Fim do For

            MySqlCommand leitura = new MySqlCommand(consultar, bd);
            MySqlDataReader select = leitura.ExecuteReader();
            i = 0;
            contador = 0;

            while (select.Read())
            {
                idTarefas[i] = Convert.ToInt32(select["codigoTarefas"]);
                nomeTarefa[i] = "" + select["nome"];
                descricao[i] = "" + select["descricao"];
                dataHora[i] = Convert.ToDateTime(select["dataHora"]);
                i++;
                contador++;
            }//Preenchendo o vetor com os dados do banco

            select.Close();//Encerrar o acesso ao Banco de Dados
        }//Fim do método

        public void Consultar()
        {
            PreencherTarefas();
            for(i = 0; i < contador; i++)
            {
                Console.WriteLine("\nCódigo da tarefa: " + idTarefas[i] +
                                  "\nNome: " + nomeTarefa[i] +
                                  "\nDescrição: " + descricao[i] +
                                  "\nCriado em " + dataHora[i]);
            }
        }//Fim do método Consultar

        public void Consultar(int cod)
        {
            PreencherTarefas();
            for (i = 0; i < contador; i++)
            {
                if (idTarefas[i] == cod)
                {
                    Console.WriteLine("\nCódigo da tarefa: " + idTarefas[i] +
                                      "\nNome: " + nomeTarefa[i] +
                                      "\nDescrição: " + descricao[i] +
                                      "\nCriado em " + dataHora[i]);
                }
            }
        }//Fim do método Consultar

        public void AtualizarTarefas(string coluna)
        {
            string atualizar = "update tarefasDados set " + coluna;
        }
    }
}
