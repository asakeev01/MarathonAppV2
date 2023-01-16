using System;
using Domain.Common.Contracts;
using Domain.Entities.Documents;
using Domain.Entities.Documents.DocumentEnums;
using Domain.Entities.Statuses;
using Domain.Entities.Statuses.StatusEnums;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class SavedDocumentService : ISavedDocumentService
{

    private readonly IUnitOfWork _unit;

    public SavedDocumentService(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task UploadDocumentAsync(Status status, Document document, IFormFile file, DocumentsEnum documentType)
    {
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "staticfiles", documentType.ToString());
        string fileName = Guid.NewGuid().ToString() + "." + file.FileName;
        string filePath = Path.Combine(directoryPath, fileName);
        bool folderExists = Directory.Exists(directoryPath);
        if (!folderExists)
            Directory.CreateDirectory(directoryPath);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        string databasePath = Path.Combine("staticfiles", documentType.ToString(), fileName);

        if (documentType == DocumentsEnum.FrontPassport)
        {
            if (document.FrontPassportPath != null) {
                string oldDocument = Path.Combine(Directory.GetCurrentDirectory(), document.FrontPassportPath);
                if (File.Exists(oldDocument))
                    File.Delete(oldDocument);
            }
            document.FrontPassportPath = databasePath;
        }
        else if (documentType == DocumentsEnum.Insurance)
        {
            if (document.InsurancePath != null) {
                string oldDocument = Path.Combine(Directory.GetCurrentDirectory(), document.InsurancePath);
                if (File.Exists(oldDocument))
                    File.Delete(oldDocument);
            }
            document.InsurancePath = databasePath;
        }
        else
        {
            if (document.DisabilityPath != null) {
                string oldDocument = Path.Combine(Directory.GetCurrentDirectory(), document.DisabilityPath);
                if (File.Exists(oldDocument))
                    File.Delete(oldDocument);
            }
            document.DisabilityPath = databasePath;
        }
        status.CurrentStatus = StatusesEnum.Processing;
    }

    public void DeleteDocument(Status status, Document document, DocumentsEnum documentType)
    {
        if (documentType == DocumentsEnum.FrontPassport)
        {
            if (document.FrontPassportPath != null)
            {
                string oldDocument = Path.Combine(Directory.GetCurrentDirectory(), document.FrontPassportPath);
                if (File.Exists(oldDocument))
                    File.Delete(oldDocument);
            }
            document.FrontPassportPath = null;
        }
        else if (documentType == DocumentsEnum.Insurance)
        {
            if (document.InsurancePath != null)
            {
                string oldDocument = Path.Combine(Directory.GetCurrentDirectory(), document.InsurancePath);
                if (File.Exists(oldDocument))
                    File.Delete(oldDocument);
            }
            document.InsurancePath = null;
        }
        else
        {
            if (document.DisabilityPath != null)
            {
                string oldDocument = Path.Combine(Directory.GetCurrentDirectory(), document.DisabilityPath);
                if (File.Exists(oldDocument))
                    File.Delete(oldDocument);
            }
            document.DisabilityPath = null;
        }
        if (document.FrontPassportPath != null || document.DisabilityPath != null)
        {
            status.CurrentStatus = StatusesEnum.Processing;
        }
        else {
            status.CurrentStatus = StatusesEnum.Empty;
        }
    }
}

