using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            //context.Database.EnsureCreated();
            //GetSamurais("Before Add:");
            InsertMultipleSamurais();
            //AddSamurai();
            //GetSamurais("After Add:");
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            RetrieveAndUpdateMultipleSamurais();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void QueryFilters()
        {
            //var name = "Manuel";
            //var samurai = _context.Samurais.Where(s => s.Name == name).ToList();
            var samurai = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "M%")).ToList();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Manuel" };
            var samurai2 = new Samurai { Name = "Carlos" };
            var samurai3 = new Samurai { Name = "Evangelina" };
            var samurai4 = new Samurai { Name = "Fernando" };
            _context.Samurais.AddRange(samurai, samurai2);
            _context.SaveChanges();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Manuel" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}
