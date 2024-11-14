using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManagement;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.ImageCloudinary;


public class ManageImageService : IManageImageService
{
    public CloudinarySettings _cloudinarySettings { get; }

    public ManageImageService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        _cloudinarySettings = cloudinarySettings.Value;
    }

    public async Task<ImagenResponse> UploadImage(ImageData imageStrem)
    {
        //validar cuenta y crear objeto de tipo cuenta
        var account = new Account(
            _cloudinarySettings.CloudName,
            _cloudinarySettings.ApiKey,
            _cloudinarySettings.ApiSecret
        );


        var cloudinary = new Cloudinary(account);

        var uploadImage = new ImageUploadParams()
        {
            File = new FileDescription(imageStrem.Nombre, imageStrem.ImageStream)
        };

        //Subida del archivo
        var uploadResult = await cloudinary.UploadAsync(uploadImage);

        //Validacion de subida
        if(uploadResult.StatusCode == HttpStatusCode.OK)
        {
            return new ImagenResponse{
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString()
            };
        }

        throw new Exception("No se pudo guardar la imagen");

    }
}