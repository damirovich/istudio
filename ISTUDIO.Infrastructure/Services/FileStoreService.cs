using ISTUDIO.Application.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ISTUDIO.Infrastructure.Services;

public class FileStoreService : IFileStoreService
{
    private readonly string _storagePath;

    public FileStoreService(string storagePath)
    {
        _storagePath = storagePath;
    }

    public async Task<string> SaveImage(IFormFile photoFile)
    {
        // Проверяем наличие файла и его размер
        if (photoFile == null || photoFile.Length == 0)
            throw new BadRequestException("No file uploaded.");

        // Уникальное называние файла
        string photoFileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);

        // Полный путь к директории, где будут храниться фотографии
        string directoryPath = Path.Combine(_storagePath, "photos");

        // Полный путь к файлу
        string fullPath = Path.Combine(directoryPath, photoFileName);

        // Создаем директорию, если она не существует
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Сохраняем фотографию на сервере
        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await photoFile.CopyToAsync(fileStream);
        }

        // Возвращаем путь к сохраненной фотографии
        return Path.Combine("photos", photoFileName);
    }

    public async Task<byte[]> GetImage(string photoFilePath)
    {
        if (string.IsNullOrEmpty(photoFilePath))
        {
            throw new ArgumentException("Photo file path cannot be null or empty.", nameof(photoFilePath));
        }

        var filePath = Path.Combine(_storagePath, photoFilePath);

        if (File.Exists(filePath))
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                return buffer;
            }
        }
        else
        {
            throw new FileNotFoundException("File not found.", filePath);
        }
    }

    public void DeleteImage(string photoFilePath)
    {
        string filePath = Path.Combine(_storagePath, photoFilePath);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
