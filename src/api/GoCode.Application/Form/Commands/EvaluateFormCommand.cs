using GoCode.Application.Common;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Form.Responses;

namespace GoCode.Application.Form.Commands
{
    public class EvaluateFormCommand : IRequestWrapper<EvaluateFormResponse>
    {
        public int CourseId { get; init; }
        public IEnumerable<FormAnswearDto> FormAnswers { get; set; }
    }
}
