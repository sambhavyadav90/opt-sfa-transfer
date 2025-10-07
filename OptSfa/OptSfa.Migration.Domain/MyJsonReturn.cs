
namespace OptSfa.Migration.Domain
{
    public class MyJsonReturn<T>
    {
        public bool isSuccess { get; set; }
        public System.Net.HttpStatusCode status { get; set; }
        public string? message { get; set; }
        public List<string>? stackTrace { get; set; }
        public T? data { get; set; }
    }
}