using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace AminWeb.Controllers
{
    public class UploadImageController : Controller
    {
        [HttpPost]
        public ActionResult UploadImages(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            try
            {
                string vImagePath = String.Empty;
                string vMessage = String.Empty;
                string vFilePath = String.Empty;
                string vOutput = String.Empty;
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                        Path.GetExtension(upload.FileName).ToLower();
                        var vFolderPath = Server.MapPath("/Resources/UploadEditor/");
                        if (!Directory.Exists(vFolderPath))
                        {
                            Directory.CreateDirectory(vFolderPath);
                        }
                        vFilePath = Path.Combine(vFolderPath, vFileName);
                        upload.SaveAs(vFilePath);
                        vImagePath = Url.Content("/Resources/UploadEditor/" + vFileName);
                        vMessage = "عکس مورد نظر آپلود شد";
                    }
                }
                catch
                {
                    vMessage = "There was an issue uploading";
                }
                vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
                return Content(vOutput);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

    }
}