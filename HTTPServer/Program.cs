using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

class HttpServer
{
    static async Task Main()
    {
        var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();
        Console.WriteLine("Server started at http://localhost:8080/");

        while (true)
        {
            HttpListenerContext context = null;
            try
            {
                context = await listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;

                if (request.Url.AbsolutePath == "/info")
                {
                    string netFramework = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
                    string userHost = request.UserHostName; 
                    string clientIp = request.RemoteEndPoint.Address.ToString(); 
                    string browser = request.Headers["User-Agent"] ?? "Unknown";
                    
                    string html = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; padding: 20px;'>
                        <h1>Web server on c###!!!!!</h1>
                        <hr>
                        <p><b>Name of the host:</b> {userHost}</p>
                        <p><b>Ip adress:</b> {clientIp}</p>
                        <p><b>Browser of client:</b> {browser}</p>
                        <hr>
                        <p><b>version .NET server:</b> {netFramework}</p>
                    </body>
                    </html>";

                    byte[] buffer = Encoding.UTF8.GetBytes(html);
                    response.StatusCode = 200;
                    response.ContentType = "text/html; charset=utf-8";
                    response.ContentLength64 = buffer.Length;
                    
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                } 
                else if (request.Url.AbsolutePath == "/" && request.HttpMethod == "GET")
                {
                    DateTime time = DateTime.Now;
                    string responseText = $"<h1>{time}</h1>";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                    response.StatusCode = 200;
                    response.ContentType = "text/html; charset=utf-8";
                    response.ContentLength64 = buffer.Length;
                    
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                }
                else
                {
                    response.StatusCode = 404;
                    string errorText = "<h1>404 Not Found - Page was not found</h1>";
                    byte[] buffer = Encoding.UTF8.GetBytes(errorText);
                    
                    response.ContentType = "text/html; charset=utf-8";
                    response.ContentLength64 = buffer.Length;
                    
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                
                context?.Response?.Abort(); 
            }
        }
    }
}