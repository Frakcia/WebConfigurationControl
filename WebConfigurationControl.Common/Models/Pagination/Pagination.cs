namespace WebConfigurationControl.Common.Models.Pagination
{
    public class Pagination
    {
        public int PageCount { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int Skip => (PageNumber - 1) * PageCount;
    }
}
