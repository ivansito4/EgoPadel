using EgoPadel.Infrastructura;
using Microsoft.Extensions.Hosting.Internal;
using System;

namespace EgoPadel.Servicios
{
    public class UnitOfWork : IUnitOfWork
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public UnitOfWork(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public async void UploadImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string filename = file.FileName.Trim('"');
            filename = EnsurefileName(filename);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = System.IO.File.Create(GetpathAndFileName(filename)))
            {
                using (Stream input = file.OpenReadStream()) 
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);

                        totalBytes += readBytes;
                    }
                }
            }
        }

        private string GetpathAndFileName(string filename)
        {
            string path = _environment.WebRootPath + "\\fotosUsuarios\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path+filename;
        }

        private string EnsurefileName(string filename)
        {
            if (filename.Contains("\\")) filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            return filename;
        }
    }
}
