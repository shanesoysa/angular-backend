﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FinalTest.Controllers
{
    public class SaveTestController : ApiController
    {
        public async Task<bool> Post()
        {
            try
            {

                string Savepath = ConfigurationManager.AppSettings["TestPath"].ToString();
                var fileuploadPath = HttpContext.Current.Server.MapPath("~/Photos/");



                var provider = new MultipartFormDataStreamProvider(fileuploadPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }


                await content.ReadAsMultipartAsync(provider);

                string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[0].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));

                if (File.Exists(originalFileName))
                {
                    File.Delete(originalFileName);
                }

                File.Move(uploadingFileName, originalFileName);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
