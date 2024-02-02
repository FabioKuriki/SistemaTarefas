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
        public DAOTarefas()
        {
            bd = new MySqlConnection("server = localhost; DataBase = tarefas; Uid = root; Password = ");//Localização do Banco de Dados
            bd.Open();
        }

        public void InserirTarefas(string nome, string descricao, string dataHora)
        {
            try
            {
                string dadosInseridos = "('','" + nome + "','" + descricao + "','" + dataHora + "')";
                string insertUsuario = "insert into Tarefas(idTarefas, nome, descricao, dataHora) values" + dadosInseridos;

                MySqlCommand insert = new MySqlCommand(insertUsuario, bd);//Prepara a execução no banco
                string resultado = "" + insert.ExecuteNonQuery();//Ctrl + Enter
                Console.WriteLine(resultado + "Linha(s) afetada(s)");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro!! Algo deu errado\n\n\n" + erro);
            }

        }//Fim do método Inserir

        public void PreencherTarefas()
        {

            string consultar = "select * from Tarefas";

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
                idTarefas[i] = Convert.ToInt32(select["idTarefas"]);
                nomeTarefa[i] = "" + select["nome"];
                descricao[i] = "" + select["descricao"];
                dataHora[i] = Convert.ToDateTime(select["dataHora"]);
                i++;
                contador++;
            }//Preenchendo o vetor com os dados do banco

            select.Close();//Encerrar o acesso ao Banco de Dados
        }
    }
}
