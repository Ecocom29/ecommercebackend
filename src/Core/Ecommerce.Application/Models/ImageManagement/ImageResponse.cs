using AutoMapper.Configuration.Conventions;

namespace Ecommerce.Application.Models.ImageManagement;


public class ImagenResponse
{
    public string? Url {get;set;}
    public string? PublicId { get; set; }
}