using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.StaticFiles;

namespace KhoaHocApi.Application.Handle.HandleFile
{
    public class HandleUploadFile
    {
        public static async Task<string> WirteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length-1];
                fileName = "MyFile_" + DateTime.Now.Ticks  +extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload","Files");
                if(!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var exactPath=Path.Combine(filePath, fileName);
                using(var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }catch (Exception ex)
            {
                throw;
            }
            return fileName;
        }
        static string cloudName = "dbfevlfy3";
        static string apiKey = "376462775548143";
        static string apiSecret = "CIUeRMJ8HTthU6fjB5wugpk8lo8";
        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);
        public static async Task<string> Upfile(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                throw new ArgumentNullException("Không có tập tin được chọn");
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = "xyz-abc" + "_" + DateTime.Now.Ticks + "image",
                    Transformation = new Transformation().Width(400).Height(400).Crop("fit")
                };
                var uploadResult = await HandleUploadFile._cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }
                string imageURL = uploadResult.SecureUrl.ToString();
                return imageURL;

            }
        }
    }
}
