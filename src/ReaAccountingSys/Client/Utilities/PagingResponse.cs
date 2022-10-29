using ReaAccountingSys.Shared.ReadModels;

namespace ReaAccountingSys.Client.Utilities
{
    public class PagingResponse<T> where T : class
    {
        public List<T>? Items { get; set; }
        public MetaData? MetaData { get; set; }
    }
}