namespace clinicAPI.Helpers
{
    public interface IFileStorageService
    {
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(string containerName, IFormFile file);
        Task<String> EditFile(string containerName, IFormFile file, string fileRoute);
    }
}
