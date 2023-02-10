using System.Net;
using System.Text.RegularExpressions;

namespace Api.Models.SERP
{
    public class GoogleSearchEngine : SearchEngine
    {

        private readonly Regex _searchResultsStartRegex = new Regex(@"id=[""']search[""']", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private readonly Regex _searchResultsEndRegex = new Regex(@"<footer", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private readonly Regex _linkRegex = new Regex(@"<a\s+(?:[^>]*?\s+)?href=([""'])/url\?q=(.*?)\1", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public override string FullUrl {
            get => $"https://www.google.com/search?num={this.Query?.MaxResults}&q={WebUtility.UrlEncode(this.Query?.SearchText)}";
        }

        public override SearchEnginePageResults GetResults()
        {
            if (this.Query == null) throw new InvalidOperationException("Query cannot be null.");

            if(this.Results != null) return this.Results;

            var content = this.GetPageContentAsync().Result;

            var searchResultsMatch = _searchResultsStartRegex.Match(content);
            var startIndex = searchResultsMatch.Index + searchResultsMatch.Length;
            var endIndex = _searchResultsEndRegex.Match(content).Index;

            MatchCollection matches = _linkRegex.Matches(content, startIndex);

            return this.Results = new SearchEnginePageResults(this.Query, this.FullUrl, DateTime.Now, matches.Where(m => m.Index < endIndex).Select((match, index) => new SearchEnginePageResult()
            {
                Id = index,
                Url = WebUtility.HtmlDecode(match.Groups[2].Value).ToLowerInvariant(),
            }));

        }
    }
}
