using GoCode.Application.Common.Dtos;

namespace GoCode.Application.Form.Responses
{
    public class EvaluateFormResponse
    {
        public int GainedXP { get; set; }
        public bool Passed { get; set; }
        public bool LevelUp { get; set; }
        public IEnumerable<FormAnswearResponseDto> ResponsesEvaluation { get; set; }
    }
}
