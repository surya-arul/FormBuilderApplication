﻿using FormBuilderDTO.DTOs.Base;

namespace FormBuilderDTO.DTOs.Survey
{
    public class CreateSurveyRequest
    {
        public SurveysDto Survey { get; set; } = new();
    }
}