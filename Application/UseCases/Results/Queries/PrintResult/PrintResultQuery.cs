using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;

namespace Core.UseCases.Results.Queries.PrintResult;

public class PrintResultQuery : IRequest<byte[]>
{
    public int ResultId { get; set; }
}

public class PrintResultHandler : IRequestHandler<PrintResultQuery, byte[]>
{
    private readonly IUnitOfWork _unit;

    public PrintResultHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<byte[]> Handle(PrintResultQuery request,
        CancellationToken cancellationToken)
    {

        var result = await _unit.ResultRepository.FirstToAsync<PrintResultOutDto>(x => x.Id == request.ResultId, include: source => source
        .Include(x => x.Application).ThenInclude(x => x.DistanceAge)
        .Include(x => x.Application).ThenInclude(x => x.User)
        .Include(x => x.Application).ThenInclude(x => x.Marathon)
        .Include(x => x.Application).ThenInclude(x => x.Distance)
        );


        var html1 = File.ReadAllText(Path.Combine("Endpoints", "Results", "Dtos", "ResultHtml.html"));
        html1 = String.Format(html1, result.Name, result.Surname, result.Distance, result.GunTime, result.ChipTime, result.GeneralPlace, result.GeneralCount, result.CategoryPlace, result.CategoryCount, result.Gender, result.DistanceAge, result.Number);

        var options = new LaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox" }
        };

        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        using var browser = await Puppeteer.LaunchAsync(options);

        using var page = await browser.NewPageAsync();
        await page.SetContentAsync(html1);
        var pdf = await page.PdfDataAsync();

        return pdf;
    }
}
