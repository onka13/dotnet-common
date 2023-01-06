using System;
using DotNetCommon.Data.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DotNetCommon.Infrastructure.Exceptions
{
    public static class ExceptionExtenstionHelpers
    {
        public static LogLevel GetLogLevel(this Exception ex)
        {
            if (ex is not AppException)
            {
                return LogLevel.Error;
            }

            return (ex as AppException).LogLevel;
        }
    }
}
