﻿using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Entities.SavedFiles;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class SavedFileService : ISavedFileService
{

    private readonly IUnitOfWork _unit;

    public SavedFileService(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<SavedFile> UploadFile(IFormFile file, FileTypeEnum fileType)
    {
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "staticfiles", fileType.ToString());
        string fileName = Guid.NewGuid().ToString() + "." + file.FileName;
        string filePath = Path.Combine(directoryPath, fileName);
        bool folderExists = Directory.Exists(directoryPath);
        if (!folderExists)
            Directory.CreateDirectory(directoryPath);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        string databasePath = Path.Combine("staticfiles", fileType.ToString(), fileName);
        var dbFile = new SavedFile();
        dbFile.Path = databasePath;
        dbFile.Name = fileName;
        var db = await _unit.SavedFileRepository.CreateAsync(dbFile, save:true);
        return db;
    }

    public async Task DeleteFile(SavedFile file)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), file.Path);
        if (File.Exists(filePath))
            File.Delete(filePath);
        await _unit.SavedFileRepository.Delete(file);
        await _unit.SavedFileRepository.SaveAsync();
    }

    public async Task<SavedFile> EmptyFile()
    {
        var dbFile = new SavedFile();
        var db = await _unit.SavedFileRepository.CreateAsync(dbFile, save: true);
        return db;
    }
}
