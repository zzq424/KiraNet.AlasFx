﻿using Demo.Core.Common;
using Demo.DataAccess;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

using Rye.Language;
using Rye.Web.Filter;

using System.Linq;

namespace Demo.WebApi.Attribute
{
    public class ValidationAttribute : ModelValidationAttribute
    {
        protected override string GetErrorMessage(HttpContext httpContext, ModelErrorCollection errors)
        {
            var langService = httpContext.RequestServices.GetRequiredService<ILangService>();
            string lang = null;
            if (httpContext.Request.Query.TryGetValue("lang", out var val))
                lang = val.ToString();

            if (string.IsNullOrEmpty(lang))
                lang = LangCode.ZHCN;

            var dicKey = errors.First().ErrorMessage;
            return langService.Get(lang, dicKey, dicKey);
        }
    }
}
