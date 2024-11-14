using AutoMapper;
using Ecommerce.Application.Features.Adresses.VMS;
using Ecommerce.Application.Features.Categories.VMS;
using Ecommerce.Application.Features.Countries.VMS;
using Ecommerce.Application.Features.Image.Queries.VMS;
using Ecommerce.Application.Features.Orders.VMS;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Features.Reviews.VMS;
using Ecommerce.Application.Features.ShoppingCarts.VMS;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductVM>()
        .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category!.Nombre))
        .ForMember(p => p.NumeroReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));

        //Mapear los objetos de tipo View Model
        CreateMap<Images, ImagenVM>();
        CreateMap<Review, ReviewVM>();
        CreateMap<Country, CountryVM>();
        CreateMap<Category, CategoryVM>();
        CreateMap<ShoppingCart, ShoppingCartVM>().ForMember(p => p.ShoppingCartId, x=> x.MapFrom(a=>a.ShoppingCartMasterId));
        CreateMap<ShoppingCartItem, ShoppingCartItemVM>();
        CreateMap<ShoppingCartItemVM, ShoppingCartItem>();
        CreateMap<Address, AddressVM>();
        CreateMap<Order, OrderVM>();
        CreateMap<OrderItem, OrderItemVM>();
        CreateMap<OrderAddress, AddressVM>();

        //Mapeo de Commads
        CreateMap<CreateProductCommand, Product>();
        CreateMap<CreateProductImageCommand, Images>();
        CreateMap<UpdateProducCommand, Product>();
    }
}