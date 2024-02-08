using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SistemaDeTarefas
{
    class ControlUsuario
    {
        private int opcao;
        private int opcao2;
        private int opcao3;
        private int opcao4;
        DAO bd;
        private string login;
        private string senha;
        DAOTarefas bdTarefas;
        private int codigo;
        private string dadosParaAlterar;
        //Método Construtor
        public ControlUsuario()
        {
            opcao = 0;
            opcao2 = 0;
            opcao3 = 0;
            opcao4 = 0;
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

        //--------------------------------------------------------------Parte para Identificação do Usuário--------------------------------------------------------------
        public void Menu()
        {
            Console.WriteLine("\nSelecione uma das opções abaixo: " +
                              "\n1. Logar" +
                              "\n2. Cadastrar" +
                              "\n3. Sair");
            Opcao = Convert.ToInt32(Console.ReadLine());
        }//Fim do método Menu
        public void MenuLogin()
        {
            Console.Write("\nLogin: ");
            login = Console.ReadLine();
            Console.Write("\nSenha: ");
            senha = Console.ReadLine();

            Console.WriteLine(bd.UsuarioAtual(login, senha));
            
        }//Fim do método MenuLogin

        public void MenuCadastro()
        {
            Console.WriteLine("\nPara o cadastro, informe as seguintes informações: ");
            do
            {
                Console.Write("\nÉ importante lembrar que após a criação do login, o mesmo não poderá ser alterado" + 
                                  "\nLogin: ");
                login = Console.ReadLine();

                if(bd.LoginUnico(login) == true)
                {
                    Console.WriteLine("Login existente, tente outro");
                }
            } while (bd.LoginUnico(login) == true);
            Console.Write("\nSenha: ");
            string senha = Console.ReadLine();
            Console.Write("\nNome: ");
            string nome = Console.ReadLine();
            Console.Write("\nTelefone: ");
            long telefone = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEndereco: ");
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
                        AdminOuCliente();
                        break;
                    case 2:
                        MenuCadastro();
                        break;
                    case 3:
                        Console.WriteLine("\nTenha um ótimo dia");
                        break;
                    default:
                        Console.WriteLine("Informe uma opção válida");
                        break;
                }
            } while (Opcao != 3);
        }//Fim do método MenuCompleto
        //-----------------------------------------------------------------------------Parte do Usuário---------------------------------------------------------------------------
 
        public void MenuUsuario(Boolean verificacaoLogin)
        {
            if(verificacaoLogin == true)
            {
                Console.WriteLine("\nO que você gostaria de fazer? " +
                  "\n1. Criar tarefa" +
                  "\n2. Consultar tarefa" +
                  "\n3. Atualizar tarefa" +
                  "\n4. Deletar tarefa" +
                  "\n5. Configurações do usuário" +
                  "\n6. Deslogar");
                opcao2 = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                MenuCompleto();
            }
        }//Fim do método MenuUsuario
       

        public void MenuTarefas(Boolean verificacaoLogin)
        {
            MenuUsuario(verificacaoLogin);
            do
            {
                switch (opcao2)
                {
                    case 1:
                        Console.Clear();
                        CriarTarefa();
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 2:
                        Console.Clear();
                        bdTarefas.Consultar(login);
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 3:
                        Console.Clear();
                        MenuAtualizarCompleto();
                        break;
                    case 4:
                        Console.Clear();
                        MenuDeletarCompleto();
                        break;
                    case 5:
                        Console.Clear();
                        MenuConfiguracao();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("\nDeslogado com sucesso");
                        break;
                    default:
                        Console.WriteLine("Escolha uma das opções válidas");
                        break;
                }
            } while(opcao2 != 6);
        }//Fim do método

        public void Configuracao()
        {
            Console.WriteLine("\nAbrindo configurações do usuário: " +
                              "\n1. Consultar dados" + 
                              "\n2. Alterar dados" +
                              "\n3. Deletar conta" +
                              "\n4. Voltar");
            opcao4 = Convert.ToInt32(Console.ReadLine());   
        }

        public void MenuConfiguracao()
        {
            Console.Clear();
            do
            {
                Configuracao();
                switch (opcao4)
                {
                    case 1:
                        Console.Clear();
                        bd.ConsultarUsuario(login);
                        break;
                    case 2:
                        Console.Clear();
                        bd.ConsultarUsuario(login);
                        AlterarDadosCompleto();
                        break;
                    case 3:
                        MenuCertezaDeletarDadosCompleto();
                        //Deletar dados
                        break;
                    case 4:
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    default:
                        Console.WriteLine("Selecione uma opção válida");
                        break;
                }
            }while(opcao4 != 4);
            
        }

        public void MenuCertezaDeletarDados()
        {
            Console.WriteLine("\nTem certeza?" +
                              "\n1. Sim" +
                              "\n2. Não");
            opcao3 = Convert.ToInt32(Console.ReadLine());
        }

        public void MenuCertezaDeletarDadosCompleto()
        {
            do
            {
                MenuCertezaDeletarDados();
                switch (opcao3)
                {
                    case 1:
                        bdTarefas.DeletarTodasTarefasUsuario(login);
                        bd.DeletarUsuario(login);
                        Console.Clear();
                        Console.WriteLine("Conta deletada com sucesso!!!");
                        MenuCompleto();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }while(opcao3 < 1 || opcao3 > 2);
        }
        public void AlterarDados()
        {
            Console.WriteLine("\nQual opção gostaria de alterar? " +
                              "\n1. Senha" +
                              "\n2. Nome" +
                              "\n3. Telefone" +
                              "\n4. Endereço" +
                              "\n5. Voltar");
            opcao4 = Convert.ToInt32(Console.ReadLine());
        }

        public void AlterarDadosCompleto()
        {
            do
            {
                AlterarDados();
                switch (opcao4)
                {
                    case 1:
                        Console.Write("Nova senha: ");
                        string senha = Console.ReadLine();
                        bd.AtualizarUsuario(opcao4, senha, login);
                        MenuConfiguracao();
                        break;
                    case 2:
                        Console.Write("Novo nome: ");
                        string nome = Console.ReadLine();
                        bd.AtualizarUsuario(opcao4, nome, login);
                        MenuConfiguracao();
                        break;
                    case 3:
                        Console.Write("Novo telefone: ");
                        long telefone = Convert.ToInt64(Console.ReadLine());
                        string telefone2 = telefone.ToString();
                        bd.AtualizarUsuario(opcao4, telefone2, login);
                        MenuConfiguracao();
                        break;
                    case 4:
                        Console.Write("Novo endereço: ");
                        string endereco = Console.ReadLine();
                        bd.AtualizarUsuario(opcao4, endereco, login);
                        MenuConfiguracao();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Selecione uma opção válida");
                        break;
                }
            }while(opcao4 != 5);
        }

        //--------------------------------------------------------------------Parte das tarefas--------------------------------------------------------------------------------------
        
        //Método para criar uma tarefa
        public void CriarTarefa()
        {
            Console.Write("\nNome da tarefa: ");
            string nomeTarefa = Console.ReadLine();
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();
            DateTime dataHora = DateTime.Now;
            string dt = dataHora.ToString("yyyy-MM-dd H:mm:ss");

            bdTarefas.InserirTarefas(nomeTarefa, descricao, dt, login);
        }//Fim do método


        //Método para atualizar a tarefa
        public void MenuAtualizarTarefa()
        {
            Console.Write("\nInforme o código da tarefa: ");
            codigo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(bdTarefas.ConsultarParaAtualizar(codigo, login));
        }//Fim do método

        public void Atualizar()
        {
            do
            {
                Console.WriteLine("\nQual das opções vc gostaria de alterar?" +
                              "\n1. Nome" +
                              "\n2. Descrição");
                opcao3 = Convert.ToInt32(Console.ReadLine());
                switch (opcao3)
                {
                    case 1:
                        Console.Write("\nNovo nome: ");
                        dadosParaAlterar = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("\nNova Descrição: ");
                        dadosParaAlterar = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Selecione uma opção válida");
                        break;
                }

            } while (opcao3 < 1 || opcao3 > 2);
            bdTarefas.AtualizarTarefas(opcao3, dadosParaAlterar, codigo);
            Console.Clear();
            Console.WriteLine("\nAlteração realizada com sucesso!!");
        }//fim do método

        public void MenuAtualizarCompleto()
        {
            MenuAtualizarTarefa();
            if (bdTarefas.ConsultarParaAtualizarVerificacao(codigo, login))
            {
                MenuAtualizarCerteza();
            }
            else
            {
                MenuTarefas(bd.Verificar(login, senha));
            }
            
        }

        public void MenuAtualizarCerteza()
        {
            
            Console.WriteLine("\nTem certeza?" +
                  "\n1. Sim" +
                  "\n2. Não");
            opcao3 = Convert.ToInt32(Console.ReadLine());
            do
            {
                switch (opcao3)
                {
                    case 1:
                        Atualizar();
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 2:
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    default:
                        Console.WriteLine("Use uma opção válida");
                        break;
                }
            } while (opcao3 < 1 || opcao3 > 2);
        }//Fim do método

        public void MenuDeletarCompleto()
        {
            MenuAtualizarTarefa();
            if (bdTarefas.ConsultarParaAtualizarVerificacao(codigo, login))
            {
                MenuDeletarCerteza();
            }
            else
            {
                MenuTarefas(bd.Verificar(login, senha));
            }

        }

        public void MenuDeletarCerteza()
        {

            Console.WriteLine("\nTem certeza?" +
                  "\n1. Sim" +
                  "\n2. Não");
            opcao3 = Convert.ToInt32(Console.ReadLine());
            do
            {
                switch (opcao3)
                {
                    case 1:
                        bdTarefas.DeletarTarefas(codigo);
                        Console.WriteLine("Tarefa deletada com sucesso!");
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    case 2:
                        MenuTarefas(bd.Verificar(login, senha));
                        break;
                    default:
                        Console.WriteLine("Use uma opção válida");
                        break;
                }
            } while (opcao3 < 1 || opcao3 > 2);
        }//Fim do métdod MenuDeletarCerteza

        //---------------------------------------------------------------Parte do Admin--------------------------------------------------------------------

        //ver lista de usuarios
        //quantidades de tarefas registradas

        private int opcao6;
        public void MenuAdmin()
        {
            Console.WriteLine("\nO que você gostaria de fazer? " +
                  "\n1. Consultar todos os usuários" +
                  "\n2. Consultar tarefas de usuário" +
                  "\n3. Deslogar");
            opcao6 = Convert.ToInt32(Console.ReadLine());
        }

        public void MenuAdminCompleto()
        {
            do
            {
                MenuAdmin();
                switch (opcao6)
                {
                    case 1:
                        bd.ConsultarTodosUsuarios();
                        break;
                    case 2:
                        Console.Write("Informe o login do usuário a ser verificado: ");
                        string loginUsuario = Console.ReadLine();
                        bdTarefas.ConsultarTarefasDeUsuario(loginUsuario);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Tenha um bom dia!!");
                        break;
                    default:
                        Console.WriteLine("Informe uma opção válida");
                        break;
                }
            } while (opcao6 != 3);
        }//Fim do método

        public void AdminOuCliente()
        {
            if (bd.VerificarAdmin(login, senha) == true)
            {
                MenuAdminCompleto();
            }
            else
            {
                MenuTarefas(bd.Verificar(login, senha));
            }
        }//Fim do método
    }
}
