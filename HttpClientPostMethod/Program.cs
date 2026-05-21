using System.Net.Http;

class HttpPostExample
{
    static async Task Main()
    {
        using var client = new HttpClient();
        
        var payload = new { name = "Mukeria", course = "Python basics" };
        var json = System.Text.Json.JsonSerializer.Serialize(payload);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("https://httpbin.org/post", content);
            Console.WriteLine($"Status Code:{(int)response.StatusCode} {response.StatusCode}");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Body: ");
            Console.WriteLine(body);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
//Відповіді на питання:

// 1. Що сервер httpbin.org повернув у полі json відповіді? Чи збіглося з тим, що ви надіслали?
  //  У полі "json" відповіді сервер повертає точно такий самий JSON-об'єкт, який ми йому надіслали: {"name": "Mukeria", "course": "Python basics"}. 

// 2. Навіщо потрібен заголовок Content-Type? Що буде без нього?
 //   Заголовок Content-Type повідомляє серверу, у якому саме форматі закодовані дані в тілі запиту (наприклад: JSON
}