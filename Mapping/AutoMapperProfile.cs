using AutoMapper;
using TinyFeetBackend.DTOs.Auth;
using TinyFeetBackend.DTOs.Orders;
using TinyFeetBackend.DTOs.Paymnt;
using TinyFeetBackend.DTOs.Products;
using TinyFeetBackend.Entities;

namespace TinyFeetBackend.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ======================
            // User
            // ======================
            CreateMap<UserRegistrationDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserLoginDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            // ======================
            // Product
            // ======================
            CreateMap<Product, ProductDto>()
               .ForMember(dest => dest.CategoryName,
                          opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

            CreateMap<ProductCreateDto, Product>();

            // ======================
            // Category
            // ======================
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));

            // ======================
            // Order
            // ======================
            CreateMap<Order, OrderResponseDto>()
     .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.OrderDate))
     .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>
         src.Items.Sum(i => i.Price * i.Quantity)))
     .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            // OrderItem → OrderItemResponseDto
            CreateMap<OrderItem, OrderItemResponseDto>();


            // DTO → Entities
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();

            // ======================
            // Payment
            // ======================
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<Payment, PaymentResponseDto>();
        }
    }
}
