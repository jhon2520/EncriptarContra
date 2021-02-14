using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContraEncriptada.Models;

namespace ContraEncriptada
{
    class Program
    {
        static void Main(string[] args)
        {
            //agreagar usuario con contraseña encriptada

            //AddUser("Galilea","123456");

            //Validar usuario
            LoginUser("Galilea", "123456");


            Console.ReadKey();
        }

        private static void AddUser(string nombre, string contra)
        {
            using (EncriptarContraEntities db = new EncriptarContraEntities())
            {
                ContraEncriptadaTable table = new ContraEncriptadaTable();
                table.nombre = nombre;
                table.contra = Encriptador.Encriptar(contra);
                db.ContraEncriptadaTable.Add(table);
                db.SaveChanges();
            }
        }

        //Verificar usuario para hacer login
        
        private static void LoginUser(string userName, string contra)
        {
            using (EncriptarContraEntities db = new EncriptarContraEntities())
            {
                string contraEncriptada = Encriptador.Encriptar(contra);

                var objectUser = (from user in db.ContraEncriptadaTable
                                 where user.nombre == userName && user.contra == contraEncriptada select user).FirstOrDefault();

                if(objectUser != null){Console.WriteLine("Usuario validado correctamente");}
                else { Console.WriteLine("Usuario no encontrado"); }
            }
        }
    }
}
