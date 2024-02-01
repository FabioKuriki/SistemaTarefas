using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeTarefas
{
    class ControlUsuario
    {
        private int opcao;
        DAO bd;
        //Método Construtor
        public ControlUsuario()
        {
            opcao = 0;
            bd = new DAO();
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
            string login = Console.ReadLine();
            Console.WriteLine("\nSenha: ");
            string senha = Console.ReadLine();
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

            bd.Inserir("Usuário",login, senha, nome, telefone, endereco);
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
