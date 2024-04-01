using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int totalPages = GetTotalPages(team, year);

        using (var httpClient = new HttpClient())
        {
            for (int page = 1; page <= totalPages; page++)
            {
                var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}"; 
                //Utilizei o postman para verificar o retorno da API, notei que ele entrega resultados por páginas, sendo assim, adaptei o código para entregar todos os resultados como esperado.
                var response = httpClient.GetAsync(url).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var data = JObject.Parse(content);
                var matches = data["data"].ToObject<JArray>();

                foreach (var match in matches)
                {
                    totalGoals += match["team1goals"].ToObject<int>();
                }

                url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";
                response = httpClient.GetAsync(url).Result;
                content = response.Content.ReadAsStringAsync().Result;
                data = JObject.Parse(content);
                matches = data["data"].ToObject<JArray>();

                foreach (var match in matches)
                {
                    totalGoals += match["team2goals"].ToObject<int>();
                }
            }
        }

        return totalGoals;
    }

    public static int GetTotalPages(string team, int year)
    {
        using (var httpClient = new HttpClient())
        {
            var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page=1";
            var response = httpClient.GetAsync(url).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var data = JObject.Parse(content);
            return data["total_pages"].ToObject<int>();
        }
    }
}