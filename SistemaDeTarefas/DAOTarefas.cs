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
        public string[] usuarioLogado;
        public string msg;
        //Método para conectar no BD
        public DAOTarefas()
        {
            bd = new MySqlConnection("server = localhost; DataBase = tarefas; Uid = root; Password = ");//Localização do Banco de Dados
            bd.Open();
        }//Fim do método

        //Método para inserir dados na entidade Tarefas
        public void InserirTarefas(string nome, string descricao, string dataHora, string usuarioLogado)
        {
            try
            {
                //Olhar a parte da chave estrangeira
                string dadosInseridos = "('','" + nome + "','" + descricao + "','" + dataHora + "', '" + usuarioLogado + "')";
                string insertUsuario = "insert into tarefasDados(codigoTarefas, nome, descricao, dataHora, login) values" + dadosInseridos;

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
            usuarioLogado = new string[100];

            for (i = 0; i < 100; i++)
            {
                idTarefas[i] = 0;
                nomeTarefa[i] = "";
                descricao[i] = "";
                dataHora[i] = DateTime.Now;
                usuarioLogado[i] = "";
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
                usuarioLogado[i] = "" + select["login"];
                i++;
                contador++;

            }//Preenchendo o vetor com os dados do banco

            select.Close();//Encerrar o acesso ao Banco de Dados
        }//Fim do método PreencherTarefas

        public void Consultar(string loginAtual)
        {
            PreencherTarefas();
            for(i = 0; i < contador; i++)
            {
                if(loginAtual == usuarioLogado[i])
                {
                    Console.WriteLine("\nCódigo da tarefa: " + idTarefas[i] +
                                      "\nNome: " + nomeTarefa[i] +
                                      "\nDescrição: " + descricao[i] +
                                      "\nCriado em " + dataHora[i]);
                }
            }
        }//Fim do método Consultar

        public void ConsultarTarefasDeUsuario(string loginUsuario)
        {
            PreencherTarefas();
            for (i = 0; i < contador; i++)
            {
                if (loginUsuario == usuarioLogado[i])
                {
                    Console.WriteLine("\nCódigo da tarefa: " + idTarefas[i] +
                                      "\nNome: " + nomeTarefa[i] +
                                      "\nDescrição: " + descricao[i] +
                                      "\nCriado em " + dataHora[i]);
                }
            }
        }//Fim do método Consultar
        public string ConsultarParaAtualizar(int cod, string loginAtual)
        {
            msg = "";
            PreencherTarefas();
            for (i = 0; i < contador; i++)
            {
                if (idTarefas[i] == cod && loginAtual == usuarioLogado[i])
                {
                    msg += "\nCódigo da tarefa: " + idTarefas[i] +
                                      "\nNome: " + nomeTarefa[i] +
                                      "\nDescrição: " + descricao[i] +
                                      "\nCriado em " + dataHora[i];
                    return msg;
                }
            }
            msg = "O código informado é inválida ou não existe";
            return msg;
        }//Fim do método Consultar

        public Boolean ConsultarParaAtualizarVerificacao(int cod, string loginAtual)
        {
            PreencherTarefas();
            for (i = 0; i < contador; i++)
            {
                if (idTarefas[i] == cod && loginAtual == usuarioLogado[i])
                {
                    return true;
                }
            }
            return false;
        }//Fim do método Consultar
        public void AtualizarTarefas(int opcao, string dados, int cod)
        {
            string coluna = "";
            if(opcao == 1)
            {
                coluna = "nome";
            }else
            {
                coluna = "descricao";
            }
            string atualizar = "update tarefasDados set " + coluna + " = '" +  dados + "' where codigoTarefas" + " = " + cod;
            MySqlCommand update = new MySqlCommand(atualizar, bd);
            update.ExecuteNonQuery();
        }

        public void AtualizarDateTime(int cod)
        {
            DateTime dataHora = DateTime.Now;
            string dataHoraFormatado = dataHora.ToString("yyyy-MM-ss H:mm:ss");
            string atualizarDataHora = "update tarefasDados set dataHora = '" + dataHoraFormatado + "' where codigoTarefas" + " = " + cod;
            MySqlCommand updateDataHora = new MySqlCommand(atualizarDataHora, bd);
            updateDataHora.ExecuteNonQuery();
        }

        public void DeletarTarefas(int cod)
        {
            string deletar = "delete from tarefasDados where codigoTarefas = '" + cod + "'";
            MySqlCommand delete = new MySqlCommand(deletar, bd);
            delete.ExecuteNonQuery();
        }//Fim do método DeletarTarefas

        public void DeletarTodasTarefasUsuario(string login)
        {
            string deletar = "delete from tarefasDados where login = '" + login + "'";
            MySqlCommand delete = new MySqlCommand(deletar, bd);
            delete.ExecuteNonQuery();
        }//Fim do método DeletarTarefas
    }
}
