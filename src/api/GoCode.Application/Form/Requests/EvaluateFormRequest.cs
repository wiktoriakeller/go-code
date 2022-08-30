using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Form.Requests
{
    public class EvaluateFormRequest
    {
        public int CourseId { get; init; }
        public IEnumerable<FormAnswearDto> FormAnswers { get; set; }
    }
}
