using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface
{
    public interface IAzureImageFacade
    {
        Task<string> DownloadImage(string moduleName, string imagePath);
        Task<string> UploadImage(string moduleName, long id, DateTime _createdUtc, string imageBase64);
        Task<List<string>> DownloadMultipleImages(string moduleName, string imagesPath);
        Task<string> UploadMultipleImage(string moduleName, int id, DateTime _createdUtc, List<string> imagesBase64, string beforeImagePaths);
        Task RemoveMultipleImage(string moduleName, string imagesPath);
    }
}
