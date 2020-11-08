using AutoMapper;
using Witter.Data.Models;
using Witter.Services.Mapping;

namespace Witter.Web.ViewModels.Tags
{
    public class TagViewModel : IMapFrom<Tag>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WeetTag, TagViewModel>()
                .ForMember(x => x.Id, opt =>
                        opt.MapFrom(y => y.TagId));

            configuration.CreateMap<WeetTag, TagViewModel>()
                .ForMember(x => x.Name, opt =>
                        opt.MapFrom(y => y.Tag.Name));
        }
    }
}
