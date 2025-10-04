namespace ServicesLayer.Helper
{
    public class PaginatedResultDto<T>
    {

        public PaginatedResultDto(int pageIndex, int pageSize, int totalProducts, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalProducts = totalProducts;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
