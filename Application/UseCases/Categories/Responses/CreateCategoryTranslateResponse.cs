﻿namespace Application.UseCases.Categories.Responses
{
    public class CreateCategoryTranslateResponse
    {
        public string TranslateText { get; set; }

        public string ColumnName { get; set; }

        public Guid? LangaugeId { get; set; }
    }
}