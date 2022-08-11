using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.AzureDocumentFacade;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface
{
    public interface IAzureDocumentFacade
    {
        Task<string> UploadMultipleFile(string moduleName, int id, DateTime _createdUtc, List<string> filesBase64, string filesNameString, string beforeFilePaths);
        Task<DocumentFileResult> DownloadDocument(string documentPath);
        Task RemoveMultipleFile(string moduleName, string filesPath);
        Task<string> DownloadFile(string moduleName, string filePath);
        Task<List<string>> DownloadMultipleFiles(string moduleName, string filesPath);
    }
}
