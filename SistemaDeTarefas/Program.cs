using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeTarefas
{
    class Program
    {
        static void Main(string[] args)
        {
            ControlUsuario control = new ControlUsuario();//Conectar a classe ControlUsuario
            control.MenuCompleto();
            Console.ReadLine();//Manter o programa aberto
        }
    }
}
