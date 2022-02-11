using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser_Test.UrlPArser
{
    public class Hotline
    {
        public string url { get; set; }

        public List<Phone> Phone { get; set; }

        public HtmlDocument htmlDoc { get; set; }

        public Hotline(string url)
        {
            Console.WriteLine("Start defult parse Hotline");
            Phone = new List<Phone>();
            this.url = url;

            htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(TextHtml(url).Result);


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
            Console.WriteLine("start parse hotline");

            var htmlmodel = htmlDoc.DocumentNode.SelectNodes("//p[@class='h4']")
                .Select(x => x.InnerText.Trim().ToUpper())
                .Where(x => x.Replace(" ", "").Contains("XIAOMI")).ToList();





            var deletepraice = new char[] { '&', 'n', 'b', 's', 'p', ';' };

            var htmlprice = htmlDoc.DocumentNode.SelectNodes("//span[@class='value']")
                .Select(x => x.InnerText.Replace("&nbsp;", ""))
                .Select(x => double.Parse(x)).ToList();





            var htmlUrl = htmlDoc.DocumentNode.SelectNodes("//p[@class='h4']//a")
                         .Select(x => "https://hotline.ua/" + $"{x.Attributes[0].Value.ToString()}").ToList();
            Console.WriteLine("stop PArse hotline");
            Console.WriteLine("Add model hotline");

            for (int i = 0; i < htmlprice.Count(); i++)
            {
                Phone.Add(new Phone()
                {
                    Name = htmlmodel[i],
                    Url = htmlUrl[i],
                    Praice = htmlprice[i]

                }); ;
            }
            Console.WriteLine("stop add model");
        }

    }
}
