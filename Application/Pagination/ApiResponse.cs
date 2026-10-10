
using Application.Entities;

namespace Application.Pagination
{
    public class ApiResponse<T>
    {
        public int current_page { get; set; }
        public List<T> data { get; set; } = new List<T>(); 
        public long totalRecords { get; set; }
        public string first_page_url { get; set; } = string.Empty;
        public int from { get; set; }
        public int last_page { get; set; }
        public string last_page_url { get; set; } = string.Empty;
        public IEnumerable<Link> links { get; set; } = Enumerable.Empty<Link>();
        public string next_page_url { get; set; } = string.Empty;
        public string path { get; set; } = string.Empty;
        public int per_page { get; set; }
        public string prev_page_url { get; set; } = string.Empty;
        public int to { get; set; }
        public int total { get; set; }
        public ResultCount resultCount_ { get; set; }

    }

    public class Link
    {
        public string url { get; set; } = string.Empty;
        public string label { get; set; } = string.Empty;
        public bool active { get; set; }
    }
}
