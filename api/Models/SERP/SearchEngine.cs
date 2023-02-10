using System.Drawing.Printing;

namespace Api.Models.SERP
{
    public abstract class SearchEngine
    {
        public Query? Query { get; set; }

        public SearchEnginePageResults? Results { get; set; }

        public abstract string FullUrl { get; }

        public SearchEngine() { }

        public SearchEngine(string searchText)
        {
            Query = new Query(searchText);
        }

        public SearchEngine(Query query)
        {
            Query = query;
        }

        protected async Task<string> GetPageContentAsync()
        {
            if(this.Query == null) throw new InvalidOperationException("Query cannot be null.");
            if (this.Query.SearchText == null) throw new InvalidOperationException("SearchText cannot be null.");

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(this.FullUrl);
            return response;
        }

        public abstract SearchEnginePageResults GetResults();

        public SearchEnginePageResults GetResults(string searchText)
        {
            this.Query = new Query(searchText);
            return this.GetResults();
        }

        public SearchEnginePageResults GetResults(Query query)
        {
            this.Query = query;
            return this.GetResults();
        }

        public IEnumerable<int> Rank(string url)
        {
            var lowerSearchText = url.ToLowerInvariant();
            return this.GetResults().Results.Aggregate(new List<int>(), (positions, result) =>
            {
                if (result.Url.Contains(lowerSearchText))
                {
                    positions.Add(result.Id);
                }

                return positions;
            });
        }

        

    }
}
