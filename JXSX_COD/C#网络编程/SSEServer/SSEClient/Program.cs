using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;

namespace SSEClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //string url = "http://localhost:8080/sse_endpoint"; // SSE endpoint URL
            string url = "http://localhost:8080"; // SSE endpoint URL
            sseclient(url);
            Console.ReadKey();
            
        }
        static async Task sseclient(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "text/event-stream");

                using (HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = await reader.ReadLineAsync();

                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                Console.WriteLine(line);
                                // Handle the received SSE event here
                            }
                        }
                    }
                }
            }
        }
    }
}
