using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeTarefas
{
    class ControlUsuario
    {
        private string login;
        private string senha;
        private int opcao;
        //Método Construtor
        public ControlUsuario()
        {
            opcao = 0;
        }
        //Fim do método construtor

        //Método Modificador
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

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
            Login = Console.ReadLine();
            Console.WriteLine("\nSenha: ");
            Senha = Console.ReadLine();
        }//Fim do método MenuLogin

        public void MenuCadastro()
        {
            Console.WriteLine("\nPara o cadastro, informe as seguintes informações: " +
                              "\n\nNome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("\nTelefone: ");
            string telefone = Console.ReadLine();
            Console.WriteLine("\nCPF: ");
            float cpf = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEndereco: ");
            string endereco = Console.ReadLine();
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
    }
}
