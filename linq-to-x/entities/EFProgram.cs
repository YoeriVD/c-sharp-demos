using System;
using System.Linq;
using linq_app;

namespace linq_to_x.entities
{
    internal static class EfProgram
    {
        public static void RunEfProgram()
        {
            using var context = new PeopleDbContext();
            context.Database.EnsureCreated();
            if (!context.People.Any())
            {
                context.People.AddRange(
                    Generator.GetPeople().Take(100).Select(p => new Person()
                    {
                        Name = p.FullName,
                        DateOfBirth = p.DateOfBirth,
                        Reviews = Generator.GetComments().Take(100).Select(comment => new Review()
                        {
                            Content = comment
                        }).ToHashSet()
                    })
                );
                context.SaveChanges();
            }
            //
            // foreach (var person in context.People
            //     .Take(5)
            //     .ToList()
            // )
            // {
            //     Console.WriteLine(person.Name);
            // }


// // n + 1 !!
//             foreach (var person in context.People.Take(5).ToList())
//             {
//                 foreach (var review in person.Reviews.Take(5))
//                 {
//                     Console.WriteLine(review.Content);
//                 }
//             }

            
            
            // // projection solve a lot
            // var list = context.People
            //         //.Where(p => p.DateOfBirth.Year > 1989)
            //         .Take(10)
            //         .Select(p => new
            //         {
            //             p.Name, 
            //             Reviews = p.Reviews.Select(r => r.Content).Take(5)
            //         })
            //         //.ToList()
            //     ;
            // foreach (var person in list)
            // {
            //     Console.Write(person.Name);
            //     foreach (var review in person.Reviews)
            //     {
            //         Console.WriteLine(review);
            //     }
            // }
        }
    }
}