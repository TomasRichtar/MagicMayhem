namespace TastyCore.Utils
{
    public class Result<T>
    {
        public static Result<T> Success(T result)
        {
            return new Result<T>(true, "", result);
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(false, errorMessage, default);
        }
    
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Value { get; private set; }
    
        private Result(bool success, string error, T value)
        {
            IsSuccess = success;
            ErrorMessage = error;
            Value = value;
        }
        
    }
}
