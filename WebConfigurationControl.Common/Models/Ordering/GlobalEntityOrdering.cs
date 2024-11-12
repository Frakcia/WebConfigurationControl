using WebConfigurationControl.Common.Enums;

namespace WebConfigurationControl.Common.Models.Ordering
{
    public class GlobalEntityOrdering
    {
        public string FieldName { get; set; }
        public FilterDirection Direction { get; set; }
        public int Order { get; set; }
    }
}
