using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser_Test.UrlPArser
{
    public class Comfy
    {
        public string url { get; set; }

        public List<Phone> Phone { get; set; }

        public HtmlDocument htmlDoc { get; set; }

        public Comfy(string url)
        {
            Console.WriteLine("Start default PArse Comfy");

            Phone = new List<Phone>();
            this.url = url;

            htmlDoc = new HtmlDocument();

            this.htmlDoc.LoadHtml(TextHtml(url).Result);


            Initilizator();

        }




        public async Task<string> TextHtml(string url)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public void Initilizator()
        {
            Console.WriteLine("PArse comfy start");
            var htmlmodel = htmlDoc.DocumentNode.SelectNodes("//a[@class='products-list-item__name']")
                  .Select(x => x.InnerText.ToUpper()).ToList();
            var delete = new char[] { '₴' };

            var htmlprice = htmlDoc.DocumentNode.SelectNodes("//div[@class='products-list-item__actions-price-current']")
                              .Select(x => x.InnerText.Trim().Trim(delete).Replace(" ", ""))
                              .Select(x => double.Parse(x)).ToList();



            var htmlUrl = htmlDoc.DocumentNode.SelectNodes("//a[@class='products-list-item__name']")
                         .Select(x => x.Attributes[0].Value.ToString()).ToList();
            Console.WriteLine("Parse Comfy stor");



            Console.WriteLine("Add comfy model");
            for (int i = 0; i < htmlprice.Count(); i++)
            {
                Phone.Add(new Phone()
                {
                    Name = htmlmodel[i],
                    Url = htmlUrl[i],
                    Praice =htmlprice[i]

                });
            }

        }
    }
}
