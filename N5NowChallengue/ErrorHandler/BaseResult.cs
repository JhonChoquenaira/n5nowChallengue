namespace N5NowChallengue.ErrorHandler
{
    public class BaseResult<T>
    {
        public T Data { get; set; }
        public AppError Error { get; set; }

    }
}