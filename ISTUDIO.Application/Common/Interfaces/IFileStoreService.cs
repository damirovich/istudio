using Microsoft.AspNetCore.Http;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IFileStoreService
{
    // Сохраняет изображение.
    Task<string> SaveImage(IFormFile photoFile);
    // Получает изображение.
    Task<byte[]> GetImage(string photoFilePath);
    // Удаляет изображение.
    void DeleteImage(string photoFilePath);
}
