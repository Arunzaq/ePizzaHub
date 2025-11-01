namespace ePizza.UI.Models.ApiResponses
{
    public class ApiResponseModel<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string message { get; set; }

        public ApiResponseModel(bool success, T data, string Message)
        {
            Success = success;
            Data = data;
            message = Message;



        }
    }
}
