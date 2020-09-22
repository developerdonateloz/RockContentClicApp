using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Project_ClicLikes.Models
{
    public class RequestRateLimitAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public int Seconds { get; set; }
        private static MemoryCache Cache { get; } = new MemoryCache( new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //base.OnActionExecuting(context);
            var ipaddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            var memoryCacheKey = $"{Name}-{ipaddress}";

            if(!Cache.TryGetValue(memoryCacheKey,out bool entry))
            {
                var cacheoptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(memoryCacheKey, true, cacheoptions);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = $"The Requests are limited to 1, every {Seconds} seconds",
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            }
        }
    }
}
