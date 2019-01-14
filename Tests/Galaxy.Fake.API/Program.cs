using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Galaxy.Fake.API
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = FluentMockServer.Start(1453);
            Console.Out.WriteLine("Fake Server started on port " + "1453");

            server
              .Given(Request.Create().WithPath("/*"))
              .RespondWith(Response.Create()
                  .WithStatusCode(200)
                  .WithHeader("Content-Type", "application/json")
                  .WithBody(@"{ ""result"": ""Hello From WireMock""}")
                  //.WithDelay(TimeSpan.FromSeconds(1))
             );

            Console.ReadLine();
        }
    }
}
