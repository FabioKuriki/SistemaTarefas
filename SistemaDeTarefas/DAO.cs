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
        public string[] login;
        public string[] senha;
        public string[] nome;
        public long[] telefone;
        public string[] endereco;
        public int i;
        public int contador;
        
        //Método para se conectar ao BD
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

        //Método Inserir
        public void Inserir(string login, string senha, string nome, long telefone, string endereco)
        {
            try
            {
                dadosInseridos = "('" + login + "','" + senha + "','" + nome + "','" + telefone + "','" + endereco + "')";
                insertUsuario = "insert into usuario(login, senha, nome, telefone, endereco) values" + dadosInseridos;

                MySqlCommand insert = new MySqlCommand(insertUsuario, bd);//Prepara a execução no banco
                insert.ExecuteNonQuery();//Ctrl + Enter
                Console.WriteLine("Cadastro realizado com sucesso");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro!! Algo deu errado\n\n\n" + erro);
            }

        }//Fim do método Inserir

        //Método para pegar os dados do BD e armazenar em variáveis para uso
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

        public void ConsultarUsuario(string loginAtual)
        {
            PreencherVetor();
            for(i = 0; i < contador; i++)
            {
                if(loginAtual == login[i])
                {
                    Console.WriteLine("\nLogin: " + login[i] +
                                      "\nSenha: " + senha[i] +
                                      "\nNome: " + nome[i] +
                                      "\nTelefone: " + telefone[i] +
                                      "\nEndereço: " + endereco[i]);
                }
            }
        }

        //Método para verificar com base no login informado, se o mesmo está no banco de dados
        public string UsuarioAtual(string loginAtual, string senhaAtual)
        {
            PreencherVetor();
            for(i = 0;i < contador; i++)
            {
                if (loginAtual == login[i] && senhaAtual == senha[i])
                {
                    Console.Clear();
                    return "\nBem-vindo " + nome[i];
                }
            }
            Console.Clear();
            return "\nUsuário não encontrado em sistema ou senha inválida";
        }//Fim do método Verificar

        //Método para validar o login do usuário e seguir para o próximo menu
        public Boolean Verificar(string loginAtual, string senhaAtual)
        {
            PreencherVetor();
            for (i = 0; i < contador; i++)
            {
                if (loginAtual == login[i] && senhaAtual == senha[i])
                {
                    return true;
                }
            }
            return false;
        }//Fim do método Verificar

        //Método para verificar se o login utilizado no cadastro se encontra disponível ou não
        public Boolean LoginUnico(string login2)
        {
            PreencherVetor();
            for(i = 0; i < contador; i++)
            {
                if(login2 == login[i])
                {
                    return true;
                }
            }
            return false;
        }//Fim do método
    }
}
