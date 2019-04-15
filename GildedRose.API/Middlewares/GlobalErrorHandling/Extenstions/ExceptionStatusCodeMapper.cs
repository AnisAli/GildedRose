using System;
using System.Net;


namespace GildedRose.API.Middlewares.GlobalErrorHandling
{
    public static class ExceptionStatusCodeMapper
    {

        public static int GetStatusCode(this Exception ex)
        {
            switch (ex)
            {
                case UnauthorizedAccessException _:
                    return (int)HttpStatusCode.Unauthorized;
                case InvalidOperationException _:
                case BadRequestException _:
                    return (int)HttpStatusCode.BadRequest;
                case OutOfStockException _:
                    return (int) HttpStatusCode.Gone;
                case NotFoundException _:
                    return (int)HttpStatusCode.NotFound;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
