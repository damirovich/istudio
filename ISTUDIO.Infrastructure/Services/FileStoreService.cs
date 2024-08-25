using ISTUDIO.Application.Common.Exceptions;
namespace ISTUDIO.Infrastructure.Services;

public class FileStoreService : IFileStoreService
{
    private readonly string _storagePath;

    public FileStoreService(string storagePath)
    {
        _storagePath = storagePath;
    }

    public async Task<string> SaveImage(byte[] photoBytes)
    {
        // Проверяем наличие данных и их размер
        if (photoBytes == null || photoBytes.Length == 0)
            throw new BadRequestException("No file data provided.");

        var fileInfo = GetFileInfoFromBytes(photoBytes);
        // Уникальное название файла
        string photoFileName = Guid.NewGuid().ToString() + fileInfo.Extension;

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
        await File.WriteAllBytesAsync(fullPath, photoBytes);

        // Возвращаем путь к сохраненной фотографии
        return Path.Combine("photos", photoFileName);
    }
    private string GetContentTypeFromBytes(byte[] fileData)
    {
        // Пример использования заголовков файла для определения типа содержимого
        if (fileData.Length > 4)
        {
            // Пример обработки нескольких известных форматов
            if (fileData[0] == 0x89 && fileData[1] == 0x50 && fileData[2] == 0x4E && fileData[3] == 0x47)
                return "image/png";
            if (fileData[0] == 0xFF && fileData[1] == 0xD8)
                return "image/jpeg";
            if (fileData[0] == 0x49 && fileData[1] == 0x49 && fileData[2] == 0x2A && fileData[3] == 0x00)
                return "image/tiff";
            if (fileData[0] == 0x47 && fileData[1] == 0x49 && fileData[2] == 0x46)
                return "image/gif";
            if (fileData[0] == 0x42 && fileData[1] == 0x4D)
                return "image/bmp";
            if (fileData[8] == 0x57 && fileData[9] == 0x45 && fileData[10] == 0x42 && fileData[11] == 0x50)
                return "image/webp";

            if (fileData[0] == 0x3C && fileData[1] == 0x3F && fileData[2] == 0x78 && fileData[3] == 0x6D && fileData[4] == 0x6C)
                return "image/svg+xml";
        }

        return "application/octet-stream"; // Возвращает общий тип содержимого, если формат неизвестен
    }
    private (string Extension, string ContentType) GetFileInfoFromBytes(byte[] fileData)
    {
        // Пример извлечения информации о типе и формате файла
        var contentType = GetContentTypeFromBytes(fileData);
        string extension;

        switch (contentType)
        {
            case "image/png":
                extension = ".png";
                break;
            case "image/jpeg":
                extension = ".jpg";
                break;
            case "image/tiff":
                extension = ".tiff";
                break;
            case "image/gif":
                extension = ".gif";
                break;
            case "image/bmp":
                extension = ".bmp";
                break;
            case "image/webp":
                extension = ".webp";
                break;
            case "image/svg+xml":
                extension = ".svg";
                break;
            default:
                extension = string.Empty; // Использовать пустое значение или обработать ошибку
                break;
        }

        return (extension, contentType);
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
