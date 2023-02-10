using Api.Models.SERP;

namespace TestProject
{
    [TestClass]
    public class GoogleTests
    {
        [TestMethod]
        public void SearchEngineTest()
        {
            // Arrange
            var searchEngine = new GoogleSearchEngine();
            var query = new Query("efiling integration");

            // Act
            searchEngine.GetResults(query);

            // Assert
            Assert.IsNotNull(searchEngine.Query, "Query must be provided.");
            Assert.IsNotNull(searchEngine.Results, "Unable to get results.");
        }

        [TestMethod]
        public void RankTest()
        {
            // Arrange
            var searchEngine = new GoogleSearchEngine();
            var query = new Query("efiling integration");

            // Act
            searchEngine.GetResults(query);
            var positions = searchEngine.Rank("www.infotrack.com");

            // Assert
            Assert.IsTrue(positions.Count() > 0, "Unable to rank url.");
        }
    }
}