using Ecommerce.Application.Models.ImageManagement;

namespace Ecommerce.Application.Contracts.Infrastructure;

public interface IManageImageService
{
    Task<ImagenResponse> UploadImage(ImageData imageStrem);
}