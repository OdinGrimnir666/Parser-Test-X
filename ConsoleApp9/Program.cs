

using HtmlAgilityPack;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using Parser_Test;
using Parser_Test.UrlPArser;
using System.Text;

string foxtrot = "https://www.foxtrot.com.ua/uk/shop/mobilnye_telefony_xiaomi.html";
string Hotline = "https://hotline.ua/mobile/mobilnye-telefony-i-smartfony/294391/";
string Comfy = "https://comfy.ua/ua/smartfon/brand__xiaomi/";
var x = new Comfy(Comfy);
var x1 = new Foxtrot(foxtrot);
var x3 = new Hotline(Hotline);







CreateXls(x.Phone, "Comfy.xlsx");
Console.WriteLine("file Comfy.Xlsx create");
CreateXls(x1.Phone, "Foxtrot.xlsx");
Console.WriteLine("file Foxtrot.Xlsx create");
CreateXls(x3.Phone, "Hotline.xlsx");
Console.WriteLine("file Hotline.Xlsx create");




var list = ReadExel("Comfy.xlsx");
var list1 = ReadExel("Foxtrot.xlsx");
var list2 = ReadExel("Hotline.xlsx");


var Glavlist = list.Intersect(list1).OrderBy(x=>x.Praice).ToList();





var xlsp = new List<Phone>();
xlsp.Add(new Phone()
{
    Name=Glavlist[0].Name,
    Praice=Glavlist[0].Praice,
    Url=Glavlist[0].Url
});




CreateXls( xlsp, "Минимальная цена.xlsx");
Console.WriteLine("фаил минимальная цена создан");









var message = new MimeMessage();
message.From.Add(new MailboxAddress("testparser38", "testparser38.gmail.com"));
message.To.Add(new MailboxAddress("Nikita", "nlingyt@gmail.com"));
var emailBody = new BodyBuilder();

var k =emailBody.LinkedResources.Add(@"Минимальная цена.xlsx");
k.ContentId = MimeUtils.GenerateMessageId();

emailBody.HtmlBody = string.Format("@", k.ContentId);

message.Body = emailBody.ToMessageBody();

using (var client = new SmtpClient())
{
    client.Connect("smtp.gmail.com", 587, false);

    // Note: only needed if the SMTP server requires authentication
    client.Authenticate("testparser38", "Qwerty12345()()");

    client.Send(message);
    client.Disconnect(true);
}
List<Phone> ReadExel(string namefile)
{

try
{
    using ExcelHelper helper = new ExcelHelper();
        if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory, namefile)))
        {
            var x = helper.GetPhone();
            return x;


        }
        return new List<Phone>();
}
catch (Exception ex) { Console.WriteLine(ex.Message);return new List<Phone>(); }
}

void CreateXls(List<Phone> phone,string NameFail)
{

try
{
    using (ExcelHelper helper = new ExcelHelper())
    {
        if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory, NameFail)))
        {
            helper.SetPhome(phone);

            helper.Save();
        }
    }

    
}
catch (Exception ex) { Console.WriteLine(ex.Message); }
}