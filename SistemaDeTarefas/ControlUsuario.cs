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
        private int codigo;
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
            Console.WriteLine("\nPara o cadastro, informe as seguintes informações: ");
            do
            {
                Console.WriteLine("\nLogin: ");
                login = Console.ReadLine();

                if(bd.LoginUnico(login) == true)
                {
                    Console.WriteLine("Login existente, tente outro");
                }
            } while (bd.LoginUnico(login) == true);
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
                  "\n2. Consultar tarefa" +
                  "\n3. Atualizar tarefa" +
                  "\n4. Deletar tarefa" +
                  "\n5. Sair");
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
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 2:
                        bdTarefas.Consultar();
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 3:
                        MenuAtualizarCerteza();
                        break;
                    case 4:
                        //Deletar
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Deslogado com sucesso");
                        break;
                    default:
                        Console.WriteLine("Escolha uma das opções válidas");
                        break;
                }
            } while(Opcao != 3);
        }//Fim do método
        
        //Método para criar uma tarefa
        public void CriarTarefa()
        {
            Console.WriteLine("\nNome da tarefa: ");
            string nomeTarefa = Console.ReadLine();
            Console.WriteLine("Descrição: ");
            string descricao = Console.ReadLine();
            DateTime dataHora = DateTime.Now;
            string dt = dataHora.ToString("yyyy-MM-dd H:mm:ss");

            bdTarefas.InserirTarefas(nomeTarefa, descricao, dt);
        }//Fim do método
        
        //Método para atualizar a tarefa
        public void MenuAtualizarTarefa()
        {
            Console.WriteLine("\nInforme o código da tarefa a ser atualizada: ");
            codigo = Convert.ToInt32(Console.ReadLine());
            bdTarefas.Consultar(codigo);
        }//Fim do método

        public void AtualizarCerteza()
        {
            MenuAtualizarTarefa();
            Console.WriteLine("\nTem certeza?" +
                              "\n1. Sim" +
                              "\n2. Não");
            Opcao = Convert.ToInt32(Console.ReadLine());
        }//fim do método

        public void MenuAtualizarCerteza()
        {
            AtualizarCerteza();
            switch (Opcao)
            {
                case 1:
                    break;
                case 2:
                    MenuTarefas(bd.Verificar(login, senha));
                    break;
                default:
                    Console.WriteLine("Use uma opção válida");
                    break;
            }
        }
    }
}
