namespace MyWebApp.ApplicationLayer.Common
{
    public static class OperationResult
    {
        public static OperationResult<T> Ok<T>(T data) => OperationResult<T>.Ok(data);
        public static OperationResult<T> Fail<T>(string error) => OperationResult<T>.Fail(error);
        public static OperationResult<T> Fail<T>(IEnumerable<string> errors) => OperationResult<T>.Fail(errors);
    }

    public class OperationResult<T>
    {
        public bool Success { get; private set; }
        public string? ErrorMessage { get; private set; }
        public T? Data { get; private set; }

        private OperationResult(bool success, T? data, string? error)
        {
            Success = success;
            Data = data;
            ErrorMessage = error;
        }

        public static OperationResult<T> Ok(T data) =>
            new OperationResult<T>(true, data, null);

        public static OperationResult<T> Fail(string errorMessage) =>
            new OperationResult<T>(false, default, errorMessage);

        public static OperationResult<T> Fail(IEnumerable<string> errors) =>
            new OperationResult<T>(false, default, string.Join("; ", errors));
    }
}