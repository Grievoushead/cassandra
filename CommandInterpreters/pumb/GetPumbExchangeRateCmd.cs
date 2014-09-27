using System.Text;
using System.Threading.Tasks;
using Contract;
using HtmlAgilityPack;

namespace pumb
{
    public class GetPumbExchangeRateCmd : ICommand
    {
        private const string C_URL = "http://pumb.ua/ru/";
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
            var rates = doc.DocumentNode.SelectSingleNode("//div[@class='currency all']");
            var title = rates.Element("span").InnerText;
            var result = new StringBuilder();
            result.AppendLine(title);
            result.AppendLine(C_URL);
            int col = 1;
            foreach (var td in rates.SelectNodes("//td"))
            {
                if(td.InnerHtml.Contains("span")) continue;

                if (td.InnerHtml.Contains("strong"))
                {
                    result.AppendFormat("{0} ", td.Element("strong").InnerText);
                }
                else
                {
                    result.AppendFormat("{0} ", td.InnerText);
                }
                
                if (col == 6) // last col 
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