#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EssentialJS2WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;

namespace EssentialJS2WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public FileManagerProviderBase operation;
        public string basePath;
        string root = "wwwroot\\files";

        public IActionResult Index()
        {
            return View();
        }

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            this.basePath = hostingEnvironment.ContentRootPath;
            this.operation = new FileManagerProviderBase(this.basePath + "\\" + this.root);
        }
        public object FileOperations([FromBody] FEParams args)
        {
            if (args.action == "remove" || args.action == "rename")
            {
                if ((args.targetPath == null) && (args.path == ""))
                {
                    FileManagerResponse response = new FileManagerResponse();
                    ErrorProperty er = new ErrorProperty
                    {
                        Code = "403",
                        Message = "Restricted to modify the root folder."
                    };
                    response.Error = er;
                    return this.operation.ToCamelCase(response);
                }
            }
            switch (args.action)
            {
                case "read":
                    return this.operation.ToCamelCase(this.operation.GetFiles(args.path, args.showHiddenItems));
                case "remove":
                    return this.operation.ToCamelCase(this.operation.Remove(args.path, args.itemNames));
                case "getdetails":
                    return this.operation.ToCamelCase(this.operation.GetDetails(args.path, args.itemNames));
                case "createfolder":
                    return this.operation.ToCamelCase(this.operation.CreateFolder(args.path, args.name));
                case "search":
                    return this.operation.ToCamelCase(this.operation.Search(args.path, args.searchString, args.showHiddenItems, args.caseSensitive));
                case "rename":
                    return this.operation.ToCamelCase(this.operation.Rename(args.path, args.name, args.itemNewName));
            }
            return null;
        }
        public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
        {
            FileManagerResponse uploadResponse;
            uploadResponse = operation.Upload(path, uploadFiles, action, null);
            if (uploadResponse.Error != null)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
            }
            return Content("");
        }
        public IActionResult Download(string downloadInput)
        {
            FEParams args = JsonConvert.DeserializeObject<FEParams>(downloadInput);
            args.path = (args.path);
            return operation.Download(args.path, args.itemNames);
        }
        public IActionResult GetImage(FEParams args)
        {
            return this.operation.GetImage(args.path, true);
        }

    }

    public class FEParams
    {
        public string action { get; set; }

        public string path { get; set; }

        public string targetPath { get; set; }

        public bool showHiddenItems { get; set; }

        public string[] itemNames { get; set; }

        public string name { get; set; }

        public bool caseSensitive { get; set; }
        public string[] CommonFiles { get; set; }

        public string searchString { get; set; }

        public string itemNewName { get; set; }

        public IList<IFormFile> UploadFiles { get; set; }

        public object[] data { get; set; }
    }
}
