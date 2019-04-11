using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace GildedRose.API.Middlewares.GlobalErrorHandling
{
    public static class ExceptionStatusCodeMapper
    {

        public const int Status401UnauthorizedError         = 401;
        public const int Status401UnauthorizedAccessError   = 403;
        public const int Status500InternalError             = 500;
        public const int Status400BadRequestError           = 400;
        public const int Status404NotFoundError             = 404;

        public static int GetStatusCode(this Exception ex)
        {

            if (ex is UnauthorizedAccessException)
            {
                return Status401UnauthorizedAccessError;
            }
            else if (ex is InvalidOperationException || ex is BadRequestException)
            {
                return Status400BadRequestError;
            }
            else if (ex is InvalidUserException)
            {
                return Status401UnauthorizedError;
            }
            else if (ex is NotFoundException)
            {
                return Status404NotFoundError;
            }
            else
            {
                return Status500InternalError;
            }

        }
    }
}
