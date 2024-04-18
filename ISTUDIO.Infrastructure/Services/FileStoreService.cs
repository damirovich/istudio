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

        // Уникальное название файла
        string photoFileName = Guid.NewGuid().ToString() + ".png";

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
