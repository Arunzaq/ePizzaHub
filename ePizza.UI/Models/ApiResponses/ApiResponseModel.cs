namespace ePizza.UI.Models.ApiResponses
{
    public class ApiResponseModel<T>
    {
        public bool success { get; set; }
        public T Data { get; set; }
        public string message { get; set; }

        public ApiResponseModel(bool Success, T data, string Message)
        {
            success = success;
            Data = data;
            message = Message;



        }
    }
}
