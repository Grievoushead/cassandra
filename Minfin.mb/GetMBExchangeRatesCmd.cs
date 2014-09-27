using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Contract;
using Newtonsoft.Json;

namespace Minfin.mb
{
    /// <summary>
    /// Get MezhBank exchange rates from http://minfin.com.ua/currency/mb/
    /// </summary>
    public class GetMBExchangeRatesCmd : ICommand
    {
        private const string C_API_URL = "http://minfin.com.ua/api/mb/1847543313/";
        private const string C_USER_AGENT = "Cassandra";

        public bool IsAsync { get{return true;} }

        public string Run()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> RunAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create(C_API_URL + GetDateParamIfNeeded());
            request.UserAgent = C_USER_AGENT;
            var response = (HttpWebResponse) await request.GetResponseAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = response.GetResponseStream())
                {
                    if (receiveStream != null)
                    {
                        using (var streamReader = new StreamReader(receiveStream))
                        {
                            var jsonString = await streamReader.ReadToEndAsync();
                            streamReader.Close();
                            receiveStream.Close();
                            var currencies = JsonConvert.DeserializeObject<List<CurrencyApiResponse>>(jsonString);
                            return FormatResponse(currencies);
                        }
                    }
                    else
                    {
                        return "receiveStream is null";
                    }
                }
            }

            return "API response status code is :" + response.StatusCode;
        }

        private static string FormatResponse(List<CurrencyApiResponse> currencies)
        {
            var res = new StringBuilder();
            foreach (var cur in currencies)
            {
                res.AppendFormat("Date: {0}, point date:{1}, currency: {2}, ask: {3}, bid: {4}, status: {5}",
                    cur.Date.ToString("G"), cur.PointDate.ToString("G"), cur.Currency, cur.Ask, cur.Bid, cur.Status);
                res.AppendLine();
            }

            return res.ToString();
        }

        private static string GetDateParamIfNeeded()
        {
            switch (DateTime.UtcNow.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
                case DayOfWeek.Sunday:
                    return DateTime.UtcNow.AddDays(-2).ToString("yyyy-MM-dd");
                default:
                    return string.Empty;
            }
        }
    }
}