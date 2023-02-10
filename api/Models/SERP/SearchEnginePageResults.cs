namespace Api.Models.SERP
{
    public class SearchEnginePageResults
    {
        public Query Query { get; set; }
        public string FullUrl { get; set; }

        public DateTime Date { get; set; }
        public IEnumerable<SearchEnginePageResult> Results { get; set; }

        public SearchEnginePageResults(Query query, string fullUrl, DateTime date, IEnumerable<SearchEnginePageResult> results)
        {
            Query = query; 
            FullUrl = fullUrl;
            Date = date;
            Results = results;
        }
    }
}
