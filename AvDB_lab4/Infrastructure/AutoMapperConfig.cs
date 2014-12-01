using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;

namespace AvDB_lab4.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<CreditApplicationViewModel, CreditApplication>()
                .ForMember(dest => dest.CreditCategoryId,
                    opts => opts.MapFrom(src => src.CreditCategoryViewModel.SelectedCreditCategoryId));

        }
    }
}