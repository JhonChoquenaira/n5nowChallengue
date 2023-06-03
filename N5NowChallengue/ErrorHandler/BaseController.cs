using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace N5NowChallengue.ErrorHandler
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected ObjectResult Result<T>(Func<T> func)
        {
            try
            {
                var res = func();
                var result = Ok(new BaseResult<T>
                {
                    Data = res,
                });
                return result;
            }
            catch (BaseException e)
            {
                e.AppError.Message = e.MessageCode;
                e.AppError.Severity = e.MessageCode.Substring(e.MessageCode.Length - 1);
                return Ok(new BaseResult<T>
                {
                    Data = default,
                    Error = e.AppError
                });
            }
            catch (Exception e)
            {
                //add dataservice exceptions
                if (e.GetType() == typeof(InvalidOperationException) || e is SqlException)
                {
                    AppError appError = new AppError
                    {
                        ErrorId = Guid.NewGuid(),
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = "Error in platform. Contact the administrator.",
                        DateTime = DateTime.Now,
                    };
                    return Ok(new BaseResult<T>
                    {
                        Data = default,
                        Error = appError
                    });
                }

                if (e.GetType() == typeof(BaseException))
                {
                    BaseException baseException = (BaseException)e;
                    AppError appError = baseException.AppError;
                    return Ok(new BaseResult<T>
                    {
                        Data = default,
                        Error = appError
                    });
                }
                else
                {
                    var message = new StringBuilder();
                    message.Append(e.Message);
                    while (e.InnerException != null)
                    {
                        message.Append("\n");
                        message.Append(e.InnerException.Message);
                        e = e.InnerException;
                    }

                    AppError appError = new AppError
                    {
                        ErrorId = Guid.NewGuid(),
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = message.ToString(),
                        DateTime = DateTime.Now
                    };
                    return Ok(new BaseResult<T>
                    {
                        Data = default,
                        Error = appError
                    });
                }
            }
        }
    }
}