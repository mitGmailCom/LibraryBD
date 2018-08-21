using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBD3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Library3Entities db = new Library3Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                //1
                Console.WriteLine("Выведите список должников");
                var tempRes1 = db.AuditLibrary.Include("Person").ToList();
                var res1 = tempRes1.Select(t => t.Person.FirstName + " " + t.Person.LastName).Distinct();
                foreach (var item in res1)
                {
                    Console.WriteLine(item);
                }

                //2
                Console.WriteLine();
                Console.WriteLine("Выведите список авторов книги №3(8) (по порядку из таблицы ‘Book’)");
                var book1 = db.Book.Include("RelationAuthorBook.Author").Where(b => b.Id == 8).FirstOrDefault();

                foreach (var item in book1.RelationAuthorBook)
                {
                    Console.WriteLine(item.Author.FirstName + " " + item.Author.LastName);
                }

                //3
                Console.WriteLine();
                Console.WriteLine("Выведите список книг, которые доступны в данный момент");
                var tempRes3 = db.AuditLibrary.Include("Book").ToList();
                List<Book> tempList = new List<Book>();
                var ListBooks = db.Book.ToList();
                foreach (var item in tempRes3)
                {
                    tempList.Add(item.Book);
                }
                var res3 = ListBooks.Except(tempList);
                foreach (var item in res3)
                {
                    Console.WriteLine(item.Title);
                }

                //4
                Console.WriteLine();
                Console.WriteLine("Вывести список книг, которые на руках у пользователя №2(3)");
                var tempRes4 = db.Person.Include("AuditLibrary.Book").Where(p => p.Id == 3).FirstOrDefault();
                foreach (var item in tempRes4.AuditLibrary)
                {
                    Console.WriteLine(item.Book.Title);
                }

                //5
                Console.WriteLine();
                Console.WriteLine("Обнулите задолженности всех должников");
                var trans = db.Database.BeginTransaction();
                Console.WriteLine($"Было записей: {db.AuditLibrary.Count()}");
                try
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE[AuditLibrary]");
                    Console.WriteLine($"Стало записей: {db.AuditLibrary.Count()}");
                    trans.Rollback();
                    Console.WriteLine("Отмена изменений");
                }
                catch
                {
                    trans.Rollback();
                }

                Console.ReadLine();
            }


        }
    }
}
