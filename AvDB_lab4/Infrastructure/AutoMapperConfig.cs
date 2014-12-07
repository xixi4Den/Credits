using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Credits.Tasks;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;
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
            AutoMapper.Mapper.CreateMap<CreditApplication, ApplicationDetailsViewModel>();
            AutoMapper.Mapper.CreateMap<BaseTask, TaskViewModel>()
                .ForMember(dest => dest.CreditCategoryName,
                    opts => opts.MapFrom(src => src.CreditApplication.CreditCategory.Name))
                .ForMember(dest => dest.ClientId,
                    opts => opts.MapFrom(src => src.CreditApplication.ClientId));
            AutoMapper.Mapper.CreateMap<ApprovalTask, ApprovalTaskViewModel>();
        }
    }
}