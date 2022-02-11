using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser_Test.UrlPArser
{
    public class Foxtrot
    {
        public string url { get; set; }

        public List<Phone> Phone { get; set;}

        public HtmlDocument ?htmlDoc { get; set; }

        public Foxtrot(string url )
        {
            Console.WriteLine("Start default parse Foxtrot");
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
            Console.WriteLine("Start parse foxtrot");
            var delete = new char[] { '₴' };
            var htmlmodel = htmlDoc.DocumentNode.SelectNodes("//a[@class='card__title']")
                   .Select(x => x.InnerText.ToUpper()).ToList();

            var htmprice = htmlDoc.DocumentNode.SelectNodes("//div[@class='card-price']")
                          .Select(x => x.InnerText.Trim().Trim(delete).Replace(" ", ""))
                          .Select(x => double.Parse(x)).ToList();

            var htmllink = htmlDoc.DocumentNode.SelectNodes("//a[@class='card__title']")
                     .Select(x => x.Attributes[0].Value.ToString())
                     .Select(x => $"https://www.foxtrot.com.ua{x}").ToList();
            Console.WriteLine("Stop PArse Foxtrot");
            Console.WriteLine("Add model foxtrot");
            for (int i = 0; i < htmlmodel.Count(); i++)
            {
                Phone.Add(new Phone(){
                    Name = htmlmodel[i],
                    Url=htmllink[i],
                    Praice=htmprice[i]

                });;
            }
        }



    }
}
