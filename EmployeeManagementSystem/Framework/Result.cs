using System.Collections;

namespace Framework
{
    public class Result<T> 
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Result(bool hasError, T data, string message)

        {
            HasError = hasError;
            Data = data;
            Message = message;
        }

        public static Result<T> Success(T data) => new Result<T>(false, data, "Action completed successfully.");
        public static Result<T> Failure(string errorMessage) => new Result<T>(true, default(T),
            errorMessage);
        
    }
}
