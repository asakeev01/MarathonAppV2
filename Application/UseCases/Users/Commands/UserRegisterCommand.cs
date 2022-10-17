using System.ComponentModel.DataAnnotations;
using System.Net;
using Core.Common.Contracts;
using Domain.Common.Contracts;
using Domain.Entities.Users.UserEnums;
using MediatR;

namespace Core.UseCases.Users.Commands;

public class UpdateProfileCommand : IRequest<HttpStatusCode>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }

    public GenderEnum? Gender { get; set; }

    public TshirtEnum? Tshirt { get; set; }

    public CountriesEnum? Country { get; set; }

    public string PhoneNumber { get; set; }

    public string? ExtraPhoneNumber { get; set; }
}

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public UpdateProfileCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(UpdateProfileCommand cmd, CancellationToken cancellationToken)
    {
        return HttpStatusCode.OK;
    }
}