using System.Net.Http;

class HttpGetExample
{
    static async Task Main()
    {
        using var client = new HttpClient();



        try
        {
            HttpResponseMessage response = await client.GetAsync("https://httpbin.org/get");
            Console.WriteLine($"Status Code:{(int)response.StatusCode} {response.StatusCode}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("RESPONSE HEADERS: ");

            foreach (var header in response.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            foreach (var header in response.Content.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("RESPONSE BODY: ");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}