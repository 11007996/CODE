using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class HolidayChecker
    {
        public static async Task<List<string>> GetHolidaysAsync(int year)
        {
            string url = $"https://timor.tech/api/holiday/year/{year}/";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            JObject json = JObject.Parse(response);
            var holidays = new List<string>();

            foreach (var holiday in json["holiday"])
            {
                holidays.Add($"{holiday.First["date"]}: {holiday.First["name"]}");
            }
            return holidays;
        }

        public static async Task Main(string[] args)
        {
            int year = 2025;
            var holidays = await GetHolidaysAsync(year);

            Console.WriteLine($"全年节假日 ({year}):");
            foreach (var holiday in holidays)
            {
                Console.WriteLine(holiday);
            }
            Console.ReadKey();
        }
    }
}
