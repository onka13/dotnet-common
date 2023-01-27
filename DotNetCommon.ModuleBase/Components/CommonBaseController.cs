using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetCommon.Data.Domain.Business;
using DotNetCommon.Data.Domain.Models;
using DotNetCommon.ModuleBase.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace DotNetCommon.ModuleBase.Components;

[TypeFilter(typeof(ModelStateFilter))]
public abstract class CommonBaseController : Controller
{
    public IHostingEnvironment HostingEnvironment { get; set; }

    public IHttpContextAccessor HttpContextAccessor { get; set; }

    protected string GetIpAddress()
    {
        return (HttpContextAccessor?.HttpContext ?? HttpContext).Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

    protected string GetFromHeader(string headerName)
    {
        StringValues values;
        if (Request.Headers.TryGetValue(headerName, out values))
        {
            return values.ToArray().ToList().FirstOrDefault();
        }

        return null;
    }

    protected IActionResult SuccessResponse()
    {
        var response = ServiceResult<string>.Instance.SuccessResult();
        return Json(response);
    }

    protected ServiceResult<T> ServiceResponse<T>(T resultValue = default, int resultCode = 0)
    {
        return ServiceResult<T>.Instance.SuccessResult(resultValue, resultCode);
    }

    protected IActionResult SuccessResponse<T>(T resultValue = default, int resultCode = 0)
    {
        var response = ServiceResult<T>.Instance.SuccessResult(resultValue, resultCode);
        return Json(response);
    }

    protected IActionResult SuccessListResponse(List<object> resultValue, long total)
    {
        var response = ServiceListResult<object>.Instance.SuccessResult(resultValue, total);
        return Json(response);
    }

    protected IActionResult ErrorResponse(int responseCode = 2, string message = "")
    {
        var response = ServiceResult<object>.Instance.ErrorResult(responseCode, message);
        return Json(response);
    }

    protected ServiceResult<T> ServiceErrorResponse<T>(int responseCode = ServiceResultCode.Error, string message = "")
    {
        return ServiceResult<T>.Instance.ErrorResult(responseCode, message);
    }

    protected IActionResult ResponseNextPage<T>(ServiceResult<List<T>> result, int take)
    {
        var response = ServiceListMoreResult<T>.Instance.SuccessResult(result.Value, take);
        return Json(response);
    }

    protected IActionResult ResponseNextPage<T>(List<T> result, int take)
    {
        var response = ServiceListMoreResult<T>.Instance.SuccessResult(result, take);
        return Json(response);
    }

    /// <summary>
    /// Returns invalid models as ServiceResult error.
    /// </summary>
    /// <returns></returns>
    protected ActionResult InvalidModelResult()
    {
        var response = ServiceResult<List<string>>.Instance.ErrorResult(ServiceResultCode.InvalidModel);
        response.Value = new List<string>();
        foreach (var item in ModelState)
        {
            if (item.Value.Errors.Count > 0)
            {
                response.Value.Add(item.Key);
            }
        }

        return Json(response);
    }

    protected FormFile ToFormFile(IFormFile file)
    {
        if (file == null)
        {
            return null;
        }

        var model = new FormFile
        {
            Extension = Path.GetExtension(file.FileName).Replace(".", string.Empty),
            Name = file.FileName,
            ContentType = file.ContentType,
            Stream = file.OpenReadStream(),
        };
        if (file.ContentType.Contains("image"))
        {
            model.FileType = FileType.Image;
        }
        else if (file.ContentType.Contains("json"))
        {
            model.FileType = FileType.Json;
        }
        else if (file.ContentType.Contains("audio"))
        {
            model.FileType = FileType.Audio;
        }
        else if (file.ContentType.Contains("video"))
        {
            model.FileType = FileType.Video;
        }
        else
        {
            model.FileType = FileType.Other;
        }

        return model;
    }
}
