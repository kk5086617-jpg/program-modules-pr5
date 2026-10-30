using ClassLibrary1;
using OWO_аунтификация.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWO_аунтификация
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Создание новой учетной записи пользователя ===\n");

            // Считываем то, что пользователь вводит с клавиатуры
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();


            
            // Передаем чистый пароль в метод вашей библиотеки классов.
            // .ToUpper() переводит все буквы хеша в верхний регистр
            string hashedPassword = PasswordHasher.HashPassword(password).ToUpper();

            Console.WriteLine($"\n[Система] Пароль захеширован: {hashedPassword}\n");

            // Конструкция using автоматически закроет соединение с базой после выполнения кода.
            using (var db = new OWOEntities())
            {
                // Создаем новый объект (строку таблицы). 
                var newUser = new users
                {
                    login = login,
                    hash = hashedPassword 
                };

                
                db.users.Add(newUser);

                db.SaveChanges();
            }

            Console.WriteLine("[Система] Учетная запись успешно добавлена в базу данных!");
            Console.ReadLine();
        }
    }
}
