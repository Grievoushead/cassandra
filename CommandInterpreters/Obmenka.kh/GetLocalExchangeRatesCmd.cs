using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Contract;
using HtmlAgilityPack;

namespace Obmenka.kh
{
    /// <summary>
    /// Get Kharkiv exchange rates from http://obmenka.kharkov.ua/
    /// </summary>
    public class GetLocalExchangeRatesCmd : ICommand
    {
        private const string C_URL = "http://obmenka.kharkov.ua/";
        private const string C_USER_AGENT = "Cassandra";

        public bool IsAsync { get { return true; } }
        public string Run()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> RunAsync()
        {
            var web = new HtmlWeb();
            var doc = web.Load(C_URL);
            var rates = doc.DocumentNode.SelectSingleNode("//div[@class='rates-cont']");
            var title = rates.Element("h2").InnerText;
            var result = new StringBuilder();
            result.AppendLine(title);
            result.AppendLine(C_URL);
            int col = 1;
            foreach (var td in rates.SelectNodes("//td"))
            {
                result.AppendFormat("{0} ", td.InnerText);
                if (col == 5) // last col 
                {
                    col = 1;
                    result.AppendLine();
                    continue;
                }
                col++;
            }
            return result.ToString();
        }
    }
}