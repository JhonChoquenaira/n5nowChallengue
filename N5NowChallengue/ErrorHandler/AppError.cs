using System;

namespace N5NowChallengue.ErrorHandler
{
    public class AppError
    {
        public Guid ErrorId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public int Code { get; set; }
    }
}