using Microsoft.AspNetCore.Http;

namespace ISTUDIO.Application.Helpers;

public static class HelperServices
{
    public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file), "File cannot be null.");

        // Создаем поток для считывания данных из IFormFile
        using (var stream = file.OpenReadStream())
        {
            // Создаем буфер для хранения байтов файла
            byte[] fileBytes = new byte[file.Length];

            // Считываем данные файла в буфер
            await stream.ReadAsync(fileBytes, 0, fileBytes.Length);

            // Возвращаем массив байтов
            return fileBytes;
        }
    }
}
