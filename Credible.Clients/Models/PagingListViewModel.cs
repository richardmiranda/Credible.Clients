using System.Collections.Generic;

namespace Credible.Clients.Models
{
    public class PagingListViewModel<T>
    {
        public PagingListViewModel()
        {
            PagerButtonCount = 5;
            MatchedCount = -1;
        }

        public IEnumerable<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int MatchedCount { get; set; }
        public int PagerButtonCount { get; set; }
    }

}
