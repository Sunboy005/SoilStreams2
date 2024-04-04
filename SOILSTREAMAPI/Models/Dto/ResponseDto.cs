namespace SOILSTREAMAPI.Models.Dto
{

    public class ResponseDto<T>
    {
        public string StatusMessage { get; set; }
        public string StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
    }
}