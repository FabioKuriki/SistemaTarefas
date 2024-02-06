using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SistemaDeTarefas
{
    class DAOAdmin
    {
        MySqlConnection bd;
        public string[] login;
        public string[] senha;
        public string[] nome;
        public long[] telefone;
        public string[] endereco;
        public int i;
        public int contador;
        //Método Construtor
        public DAOAdmin()
        {
            bd = new MySqlConnection("server = localhost; DataBase = tarefas; Uid = root; Password = ");
            bd.Open();
        }//Fim do método Construtor

        public void PreencherVetor()
        {
            string consultar = "select * from usuario";

            //Vetores que serão utilizados para pegar os dados no banco de dados e mostrar em tela
            login = new string[100];
            senha = new string[100];
            nome = new string[100];
            telefone = new long[100];
            endereco = new string[100];

            //Preencher com valores genéricos quer serão substituídos de acordo com novos dados adicionados
            for (i = 0; i < 100; i++)
            {
                login[i] = "";
                senha[i] = "";
                nome[i] = "";
                telefone[i] = 0;
                endereco[i] = "";
            }//Fim do For

            //Preparando o comando para o banco
            MySqlCommand leitura = new MySqlCommand(consultar, bd);
            //Leitura do banco
            MySqlDataReader select = leitura.ExecuteReader();
            i = 0;
            contador = 0;//Variavél para contar quantos dados ja foram inseridos

            while (select.Read())
            {
                login[i] = "" + select["login"];
                senha[i] = "" + select["senha"];
                nome[i] = "" + select["nome"];
                telefone[i] = Convert.ToInt64(select["telefone"]);
                endereco[i] = "" + select["endereco"];
                i++;
                contador++;
            }//Preenchendo o vetor com os dados do banco

            select.Close();//Encerrar o acesso ao Banco de Dados
        }//Fim do preencher
    }
}
