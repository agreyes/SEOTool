namespace Api.Models.SERP
{
    public class Query
    {
        public string SearchText { get; set; }

        public int MaxResults { get; set; }

        public Query(string searchText, int maxResults = 100)
        {
            SearchText = searchText;
            MaxResults = maxResults;
        }
    }
}
