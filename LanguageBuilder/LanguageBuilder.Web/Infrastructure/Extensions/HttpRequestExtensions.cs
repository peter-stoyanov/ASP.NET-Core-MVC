﻿using Microsoft.AspNetCore.Http;
using System;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class HttpRequestExtensions
    {
        public static Uri UrlReferrer(this HttpRequest request)
        {
            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Value,
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            return builder.Uri;
        }

        public static string BaseUrl(this HttpRequest request, bool addTrailingSlash = true)
        {
            return $"{request.Scheme}://{request.Host}{request.PathBase}{(addTrailingSlash ? "/" : String.Empty)}";
        }
    }
}
