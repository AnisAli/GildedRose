using Newtonsoft.Json;

namespace GildedRose.API.Middlewares.GlobalErrorHandling
{
    public class ErrorDetails
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
