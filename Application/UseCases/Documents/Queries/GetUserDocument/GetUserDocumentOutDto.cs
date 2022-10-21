using System;
using Core.Common.Bases;
using Domain.Entities.Documents;

namespace Core.UseCases.Documents.Queries.GetUserDocument
{
    public record GetUserDocumentOutDto : BaseDto<Document, GetUserDocumentOutDto>
    {
        public string? FrontPassportPath { get; set; }
        public string? BackPassportPath { get; set; }
        public string? InsurancePath { get; set; }
        public string? DisabilityPath { get; set; }
    }
}

