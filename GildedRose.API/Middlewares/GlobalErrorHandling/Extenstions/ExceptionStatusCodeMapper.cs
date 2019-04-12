using System;
using System.Net;


namespace GildedRose.API.Middlewares.GlobalErrorHandling
{
    public static class ExceptionStatusCodeMapper
    {

        public static int GetStatusCode(this Exception ex)
        {

            if (ex is UnauthorizedAccessException)
            {
                return (int)HttpStatusCode.Unauthorized;
            }
            else if (ex is InvalidOperationException || ex is BadRequestException)
            {
                return (int)HttpStatusCode.BadRequest;
            }
            else if (ex is OutOfStockException)
            {
                return (int) HttpStatusCode.Gone;
            }
            else if (ex is NotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }

        }
    }
}
