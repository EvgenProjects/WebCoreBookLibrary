using AutoMapper;
using BookLibrary.Models;
using BookLibrary.ViewModels;
using BookLibrary.ViewModels.Book;
using BookLibrary.ViewModels.User;

namespace BookLibrary.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Book -> ShowBookViewModel
            CreateMap<Book, ShowBookViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BookTypeName, opt => opt.MapFrom(src => src.BookType.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            // CreateBookViewModel -> Book
            CreateMap<CreateBookViewModel, Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BookTypeId, opt => opt.MapFrom(src => src.BookTypeId))
                .ForAllOtherMembers(opt => opt.Ignore());

            // Book -> EditBookViewModel
            CreateMap<Book, EditBookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BookTypeId, opt => opt.MapFrom(src => src.BookTypeId))
                .ForAllOtherMembers(opt => opt.Ignore());

            // EditBookViewModel -> Book
            CreateMap<EditBookViewModel, Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BookTypeId, opt => opt.MapFrom(src => src.BookTypeId))
                .ForAllOtherMembers(opt => opt.Ignore());

            // UserRegisterViewModel -> User
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.LoginEmail, opt => opt.MapFrom(src => src.LoginEmail))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}
