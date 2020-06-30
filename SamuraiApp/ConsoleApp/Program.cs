using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
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
            //InsertMultipleSamurais();
            //AddSamurai();
            //GetSamurais("After Add:");
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //InsertNewSamuraiWithAQuote();
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSomeSamuraiWithQuotes();
            //ExplicitLoadQuotes();
            //ModifyingRelatedDataWhenTracked();
            //GetSamuraisWithBattles();
            ReplaceAHorse();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void ReplaceAHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Id == 7);
            samurai.Horse = new Horse { Name = "Rocinante" };
            _context.SaveChanges();
        }

        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jian Ujichika" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void GetSamuraisWithBattles()
        {
            var samuraiWithBattles = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 1);

            var samuraiWithBattlesCleaner = _context.Samurais.Where(samurai => samurai.Id == 1)
                .Select(s => new
                {
                    samurai = s,
                    battles = s.SamuraiBattles.Select(sb => sb.Battle)
                })
                .FirstOrDefault();
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);

            var quote = samurai.Quotes[0];
            quote.Text += "Quote Eva";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }

        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Manuel"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void ProjectSomeSamuraiWithQuotes()
        {
            var samuraiWithHappyQuotes = _context.Samurais
                .Select(s => new
                {
                    Samurai = s,
                    HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
                })
                .ToList();
        }

        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).ToList();
        }

        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Manue Shimada",
                Quotes = new List<Quote>
                {
                   new Quote { Text = "I've come to save you"}
                }
            };

            _context.Samurais.Add(samurai);
            _context.SaveChanges();
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
            _context.Samurais.AddRange(samurai, samurai2, samurai3, samurai3);
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
