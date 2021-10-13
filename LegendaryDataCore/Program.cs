using LegendaryDataCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryDataCore
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new LegendaryContext())
            {
                // Note: This sample requires the database to be created before running.
                Console.WriteLine($"Database path: {db.DbPath}.");


                // Read
                Console.WriteLine("Querying for a set");
                var legendarySet = db.Sets.FirstOrDefault();
                if (legendarySet == null)
                {
                    // Create
                    Console.WriteLine("Inserting a new Set");
                    db.Add(new LegendarySetModel { Set = "Core Set" });
                    db.SaveChanges();
                }

                //// Update
                //Console.WriteLine("Updating the blog and adding a Set");
                //legendarySet.Set = "Base Set";
                //legendarySet.Masterminds = new List<MastermindModel> {
                //    new MastermindModel { MastermindName = "Red Skull",Villain="Hydra" } };
                //db.SaveChanges();

                // Delete
                // Console.WriteLine("Delete the blog");
                // db.Remove(blog);
                //  db.SaveChanges();

                Console.ReadLine();
            }
        }
    }
}
