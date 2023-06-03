using System;
using System.Runtime.Serialization;

namespace N5NowChallengue.ErrorHandler
{
    [Serializable]
    public class BaseException : Exception
    {
        public AppError AppError { get; set; }
        public int Code { get; }
        public string MessageCode { get; set; }
        public string[] Args { get; set; }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public BaseException(string messageCode, int code = 400)
        {
            MessageCode = messageCode;
            AppError = new AppError
                { ErrorId = Guid.NewGuid(), Code = code, Message = messageCode, DateTime = DateTime.Now };
        }

        public BaseException(string messageCode, params string[] args)
        {
            Args = args;
            MessageCode = messageCode;
            AppError = new AppError
                { ErrorId = Guid.NewGuid(), Code = 400, Message = messageCode, DateTime = DateTime.Now };
        }
    }
}