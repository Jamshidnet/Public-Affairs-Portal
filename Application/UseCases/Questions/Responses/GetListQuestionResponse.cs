﻿namespace Application.UseCases.Questions.Responses
{
    public class GetListQuestionResponse
    {
        public Guid Id { get; set; }

        public string QuestionText { get; set; }

        public string Category { get; set; }

        public string CreatorUser { get; set; }

        public string QuestionType { get; set; }
    }
}
