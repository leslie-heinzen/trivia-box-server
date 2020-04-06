using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TriviaBoxServer.Models.Response
{
    public class Result<T>
    {
        public T Item { get; set; }
        public Error Error { get; set; }

        internal void Deconstruct(out T item, out Error error)
        {
            item = Item;
            error = Error;
        }
    }

    public class Error
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }

    public static class Result
    {
        public static Result<T> FromItem<T>(T item)
        {
            return new Result<T>()
            {
                Item = item
            };
        }

        public static Result<T> FromError<T>(HttpStatusCode code, string message = null)
        {
            return new Result<T>()
            {
                Error = new Error
                {
                    Code = code,
                    Message = message
                },
            };
        }
    }
}
