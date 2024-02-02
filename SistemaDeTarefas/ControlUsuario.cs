using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace SistemaDeTarefas
{
    class ControlUsuario
    {

        CultureInfo culture;
        private int opcao;
        DAO bd;
        private string login;
        private string senha;
        DAOTarefas bdTarefas;
        //Método Construtor
        public ControlUsuario()
        {
            culture = new CultureInfo("en-US");
            opcao = 0;
            bd = new DAO();
            bdTarefas = new DAOTarefas();
        }
        //Fim do método construtor

        //Método Modificador

        public int Opcao
        {
            get { return opcao; }
            set { opcao = value; }
        }
        //Fim do método modificador

        public void Menu()
        {
            Console.WriteLine("\nSelecione uma das opções abaixo: " +
                              "\n1. Logar" +
                              "\n2. Cadastrar");
            Opcao = Convert.ToInt32(Console.ReadLine());
        }//Fim do método Menu
        public void MenuLogin()
        {
            Console.WriteLine("\nLogin: ");
            login = Console.ReadLine();
            Console.WriteLine("\nSenha: ");
            senha = Console.ReadLine();

            Console.WriteLine(bd.UsuarioAtual(login, senha));
            
        }//Fim do método MenuLogin

        public void MenuCadastro()
        {
            Console.WriteLine("\nPara o cadastro, informe as seguintes informações: " +
                              "\nLogin: ");
            string login = Console.ReadLine();
            Console.WriteLine("\nSenha: ");
            string senha = Console.ReadLine();
            Console.WriteLine("\nNome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("\nTelefone: ");
            long telefone = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEndereco: ");
            string endereco = Console.ReadLine();

            bd.Inserir(login, senha, nome, telefone, endereco);
        }

        public void MenuCompleto()
        {
            do
            {
                Menu();
                Console.Clear();
                switch (Opcao)
                {
                    case 1:
                        MenuLogin();
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 2:
                        MenuCadastro();
                        break;
                    default:
                        Console.WriteLine("Informe uma opção válida");
                        break;
                }
            } while (Opcao != 1);
        }//Fim do método MenuCompleto

        public void MenuUsuario(Boolean teste)
        {
            if(teste == true)
            {
                Console.WriteLine("\nO que você gostaria de fazer? " +
                  "\n1. Criar tarefa" +
                  "\n2. Consultar tarefa");
                Opcao = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                MenuCompleto();
            }
        }

        public void MenuTarefas(Boolean teste)
        {
            MenuUsuario(teste);
            do
            {
                switch (Opcao)
                {
                    case 1:
                        CriarTarefa();
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Escolha uma das opções válidas");
                        break;
                }
            } while(Opcao != 1 || Opcao != 2);
        }

        public void CriarTarefa()
        {
            Console.WriteLine("\nNome da tarefa: ");
            string nomeTarefa = Console.ReadLine();
            Console.WriteLine("Descrição: ");
            string descricao = Console.ReadLine();
            DateTime dataHora = DateTime.Now;
            string dt = dataHora.ToString("yyyy-MM-dd H:mm:ss");
            

            Console.WriteLine(dt);

            bdTarefas.InserirTarefas(nomeTarefa, descricao, dt);
        }
    }
}
