using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galaxy.Gateway.API.Aggregator
{
    public class GalaxyGatewayAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamResponse> responses)
        {
            var aggregatedResponseContentAsString = string.Empty;

            aggregatedResponseContentAsString += "{";
            
            var contentList = responses.Select(async e => await e.Content.ReadAsStringAsync());

            for (int i = 0; i < responses.Count; i++)
            {
                if (i != 0)
                {
                    aggregatedResponseContentAsString += ",";
                }

                aggregatedResponseContentAsString += $"\"DownStreamResponse_{(i + 1).ToString()}\" : {await contentList.ElementAtOrDefault(i)}";
            }

            //aggregatedResponseContentAsString += string.Join(",", contentList
            //        .Select(c => $" \"DownStreamResponse\" : {c.ConfigureAwait(false).GetAwaiter().GetResult()}"));

            aggregatedResponseContentAsString += "}";

            var aggregatedResponse = new DownstreamResponse(new StringContent(aggregatedResponseContentAsString, System.Text.Encoding.UTF8, "application/json")
                , (responses.Where(s => s.StatusCode != HttpStatusCode.OK).Any() ? HttpStatusCode.InternalServerError : HttpStatusCode.OK )
                , responses.FirstOrDefault().Headers, responses.FirstOrDefault().ReasonPhrase);

            return aggregatedResponse;

        }
    }
}
