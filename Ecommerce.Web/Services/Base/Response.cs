namespace EcommerceWeb.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public string ValidatonError { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        
        public IList<T> DataEnum { get; set; }


    }
}
