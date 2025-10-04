

namespace ServicesLayer.HandleResponses.CommanResponse
{
    public class CommonResponse<T>
    {
        public bool IsSuccess { get; set; }
        public Error Errors { get; set; }
        public T Data { get; set; }
    }
}
