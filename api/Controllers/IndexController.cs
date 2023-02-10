using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Graph;
using Api.Models;
using System.Text.RegularExpressions;
using System.Net;
using Api.Models.SERP;

namespace Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class IndexController : ControllerBase
    {
        private readonly GraphServiceClient _graphServiceClient;

        private readonly ILogger<IndexController> _logger;

        
        public IndexController(ILogger<IndexController> logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;
        }


        [HttpPost(Name = "SEORank")]
        public string SEORank(string searchText, string url, int maxResults = 100)
        {
            //var user = await _graphServiceClient.Me.Request().GetAsync();
            var searchEngine = new GoogleSearchEngine();
            var lowerSearchText = url.ToLowerInvariant();
            var positions = searchEngine.GetResults(new Query(searchText, maxResults)).Results.Aggregate(new List<int>(), (positions, result) =>
            {
                if(result.Url.Contains(lowerSearchText))
                {
                    positions.Add(result.Id);
                }

                return positions;
            });

            return String.Join(", ", positions);
        }
    }
}