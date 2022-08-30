using AutoMapper;
using GoCode.Application.Form.Commands;
using GoCode.Application.Form.Requests;

namespace GoCode.Application.Form
{
    public class FormMappingProfile : Profile
    {
        public FormMappingProfile()
        {
            CreateMap<EvaluateFormRequest, EvaluateFormCommand>();
        }
    }
}
