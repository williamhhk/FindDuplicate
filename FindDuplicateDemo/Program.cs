using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindDuplicateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var magazines = new List<Subscription>()
            {
                new Subscription () { Id = 1, Magazines = new List<Magazine> ()
                                                {
                                                    new Magazine()  {Name = "NationalGeo", Expiration = new DateTime(2021,1,1)},
                                                    new Magazine()  {Name = "NationalGeo", Expiration = new DateTime(2025,1,1)}
                                                } },
                new Subscription () { Id = 2, Magazines = new List<Magazine> ()
                                                {
                                                    new Magazine()  {Name = "People", Expiration = new DateTime(2020,1,1)},
                                                    new Magazine()  {Name = "Wired", Expiration = new DateTime(2020,12,1) },

                                                } },
                new Subscription () { Id = 3, Magazines = new List<Magazine> ()
                                                { new Magazine()  {Name = "Linux", Expiration = new DateTime(2021,1,1)} } },
                new Subscription () { Id = 4, Magazines = new List<Magazine> ()
                                                { new Magazine()  {Name = "Investment", Expiration = new DateTime(2022,1,1)} } },
                new Subscription () { Id = 5, Magazines = new List<Magazine> ()
                                                { new Magazine()  {Name = "Windows", Expiration = new DateTime(2023,1,1)} } }
                
            };

            PrintList(magazines);

            var newList = new List<Subscription>();
            foreach (var magazine in magazines)
            {

                var test = magazine.Magazines.GroupBy(x => x.Name)
                    .Select(gr => new { gr = gr, date = gr.Max(d => d.Expiration) })
                     .SelectMany(g1 => g1.gr, (g2, g3) => new { g2, g3 = g3 })
                .Where(ti => ti.g3.Expiration == ti.g2.date)
                .Select(st => st.g3);

                magazine.Magazines = qr.ToList() ;
                newList.Add(magazine);

            }

            PrintList(newList);
        }

        private static void PrintList(List<Subscription> deviceList)
        {
           var json = JsonConvert.SerializeObject(deviceList, Formatting.Indented);
            Console.WriteLine(json);
        }

    }

    public class Subscription
    {
        public int Id;
        public List<Magazine> Magazines;

    }
    public class Magazine
    {
        public string Name { get; set; }
        public DateTime  Expiration { get; set; }
    }



}
