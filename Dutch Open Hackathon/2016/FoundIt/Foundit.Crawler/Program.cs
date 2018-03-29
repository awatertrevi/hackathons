using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Foundit.Crawler
{
    class Program
    {
        static string CreateHexStringHMAC(int applicationid, string key, string data)
        {
            data += DateTime.Now.ToString("yyyyMMdd");
            data += applicationid;

            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();

            HMACMD5 myHMACMD5 = new HMACMD5(encoding.GetBytes(key));
            byte[] byteArrayHMAC = myHMACMD5.ComputeHash(encoding.GetBytes(data));
            StringBuilder hexStringHMAC = new StringBuilder();
            foreach(byte bt in byteArrayHMAC)
            {
                hexStringHMAC.Append(bt.ToString("X").PadLeft(2, '0'));
            }
            return hexStringHMAC.ToString();
        }

        static void MockData()
        {
            var rnd = new Random();
            var context = new FoundIt.Webservice.LostItContext();
            var users = context.Users.ToArray();

            string[][] namedescriptions = new []
            {
                new []{ "Bankpas","ING bankpas () Gevonden in de / op de Stadhuisplein (Parkeergarage) te Almelo" ,null},
                new []{ "ABN AMRO bankpas","ABN AMRO bankpas (groen) Gevonden in de / op de Kasteel 1 (Nabij het gemeentehuis) te Coevorden. Op naam Lisa Nias",null },
                new []{ "Zorgpas","Zilveren Kruis zorgpas (blauw) met opschrift K.M. Dhubow. Gevonden in de / op de Arriva (Servicepunt) te Emmen.",null },
                new []{ "Portemonnee", "portemonnee (zwart) Gevonden te Tilburg.", Convert.ToBase64String(new System.Net.WebClient().DownloadData("https://www.verlorenofgevonden.nl/webviewer/images/gv0855-01/Foto/G0855-2016003702.jpg")) },
                new []{ "Hand-, dames- of schoudertas", "hand-, dames- of schoudertas (bruin) Gevonden in de / op de Kattendiep (Centrum) te Groningen.", Convert.ToBase64String(new System.Net.WebClient().DownloadData("https://www.verlorenofgevonden.nl/webviewer/images/gv0014-01/Foto/G0014-2016005060.jpg")) },
                new []{ "Laptoptas", "Dell laptoptas (zwart) Gevonden te Amstelveen.", Convert.ToBase64String(new System.Net.WebClient().DownloadData("https://www.verlorenofgevonden.nl/webviewer/images/gv0362-01/Foto/G0362-2016001523.jpg")) },
                new []{ "kaart houdertje", "de kaarthouder kwijt geraakt rondom het centrum",null }
            }.ToArray();

            var area = RectangleF.FromLTRB(4.443626f, 51.90297f, 4.524651f, 51.949767f);
            for (int i = 0; i < 10; i++)
            {
                var curRnd = namedescriptions[rnd.Next(namedescriptions.Length)];
                FoundIt.Models.LostItem lostItem;
                context.LostItems.Add(lostItem = new FoundIt.Models.LostItem()
                {
                    EstimatedLoseTime = DateTime.Now.AddMinutes(rnd.Next(-60 * 12, -60 * 6)),
                    Radius = new double[] { 1, 2, 5, 10 }[rnd.Next(4)],
                    Name = curRnd[0],
                    Description = curRnd[1],
                    Longitude = area.X + area.Width * rnd.NextDouble(),
                    Latitude = area.Y + area.Height * rnd.NextDouble(),
                    Reporter = users[rnd.Next(users.Length)],
                    LoseAddress = "Rotterdam"
                });

                context.SaveChanges();
                
                if (rnd.Next(3) == 0)
                {
                    var foundItem = new FoundIt.Models.FoundItem()
                    {
                        LostItem = lostItem,
                        Finder = users[rnd.Next(users.Length)],
                        FoundTime = DateTime.Now.AddMinutes(rnd.Next(-60 * 6, 0)),
                        Longitude = area.X + area.Width * rnd.NextDouble(),
                        Latitude = area.Y + area.Height * rnd.NextDouble(),
                        FindAddress = "Rotterdam",
                        Picture = curRnd[2]
                    };
                    context.FoundItems.Add(foundItem);

                    context.SaveChanges();
                }
            }
        }

        static void Main(string[] args)
        {
            MockData();
            return;
            System.Net.WebClient c2 = new System.Net.WebClient();
            c2.Headers.Add("Accept", "appplication/json");
            var hmac = CreateHexStringHMAC(57, "0659EA5CB027FFDB7F0F9CDB18EBB2D55F0314A193FFE0C0541213A32F531CF1", "12345678AB");
            var res = c2.DownloadString($"https://www.controleergoed.nl/DesktopModules/Stopheling/API/v1/WebAPI/GetSearchItems?searchterm=12345678AB&applicationid=57&hmac={hmac}&loc=52.3737;4.89917&ipaddress=192.168.1.12");

            return;
            int category = 2;

            for (int page = 1; page < 10; page++)
            {
                System.Net.WebClient client = new System.Net.WebClient();
                var html = client.DownloadString($"https://www.verlorenofgevonden.nl/gevonden-voorwerpen?category={category}&guiListing_VerlorenofgevondenSearch_page={page}&ajax=true");
                var matches = Regex.Matches(html, @"thumbnail.*?(src|href)=""(?<img>.*?)"".*?info.*?<strong>(?<title>.*?)</strong>.*?<p>(?<date>\d\d-\d\d-\d\d\d\d).*?<p>(?<description>.*?)</p>.*?<dt>Adres</dt><dd class=""left"">(?<location>.*?)</dd>", RegexOptions.Singleline);
            }

        }
    }
}
