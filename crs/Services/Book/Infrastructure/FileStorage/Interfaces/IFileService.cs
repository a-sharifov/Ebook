namespace Infrastructure.FileStorage.Interfaces;

public interface IFileService
{
	void GetFile(IFormFile formFile);
	void UploadFile();
	void DeleteFile();
}