﻿namespace Api.Models.SERP
{
    public class SearchEnginePageResult
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Title { get; set; }

    }
}
