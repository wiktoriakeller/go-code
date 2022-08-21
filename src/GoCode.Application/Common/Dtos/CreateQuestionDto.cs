﻿namespace GoCode.Application.Common.Dtos
{
    public record CreateQuestionDto
    {
        public string Content { get; init; }
        public int XP { get; init; }
        public IEnumerable<CreateAnswearDto> Answers { get; init; }
    }
}
