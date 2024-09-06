namespace CoreLayer.Utilities.Results
{
    public interface IDataResult<T>
    {
        string Message { get; }
        T Data { get; }
        bool Success { get; }
    }
}
