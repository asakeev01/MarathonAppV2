using Core.Common.Bases;
using Domain.Entities.Marathons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.AddPartners
{
    public record AddPartnerCommandInDto : BaseDto<AddPartnerCommandInDto, Partner>
    {
        public ICollection<TrasnlationDto> Translations { get; set; }

        public class TrasnlationDto
        {
            public string Name { get; set; }
            public int LanguageId { get; set; }
        }
    }
}
