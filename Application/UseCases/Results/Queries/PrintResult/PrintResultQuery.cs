using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Results.Queries.PrintResult;

public class PrintResultQuery : IRequest<PrintResultOutDto>
{
    public int ResultId { get; set; }
}

public class PrintResultHandler : IRequestHandler<PrintResultQuery, PrintResultOutDto>
{
    private readonly IUnitOfWork _unit;

    public PrintResultHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<PrintResultOutDto> Handle(PrintResultQuery request,
        CancellationToken cancellationToken)
    {

        var result = await _unit.ResultRepository.FirstToAsync<PrintResultOutDto>(x => x.Id == request.ResultId, include: source => source
        .Include(x => x.Application).ThenInclude(x => x.DistanceAge)
        .Include(x => x.Application).ThenInclude(x => x.User)
        .Include(x => x.Application).ThenInclude(x => x.Marathon)
        .Include(x => x.Application).ThenInclude(x => x.Distance)
        );
        //var html1 = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "staticfiles", "Result", "ResultHtml.html"));

        ////var html1 = "<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n    <style>\r\n\r\n        body {{\r\n            line-height: 30px;\r\n            margin: 0 auto;\r\n            padding: 0;\r\n            height: 100vh;\r\n            display: flex;\r\n            flex-direction: column;\r\n            font-family: 'Asap', sans-serif;\r\n            width: 80%;\r\n        }}\r\n\r\n        p {{\r\n            margin-top: 0;\r\n            margin-bottom: 0;\r\n        }}\r\n\r\n        .row {{\r\n            height: 31%;\r\n            display: flex;\r\n            flex-direction: column;\r\n            align-items: center;\r\n            justify-content: center;\r\n        }}\r\n\r\n        .secondRow {{\r\n                margin-top: 25px;\r\n                height: 38%;\r\n            }}\r\n\r\n        .participant {{\r\n            font-size: 24px;\r\n            font-weight: 800;\r\n            margin: 0;\r\n            text-transform: capitalize;\r\n        }}\r\n\r\n        p {{\r\n            color: #000;\r\n            font-size: 24px;\r\n            font-weight: 500;\r\n            margin: 0;\r\n        }}\r\n\r\n        span {{\r\n            font-weight: 600;\r\n            font-size: 24px;\r\n        }}\r\n\r\n        .distanceName {{\r\n            word-wrap: break-word;\r\n            text-align: center;\r\n        }}\r\n\r\n    </style>\r\n</head>\r\n\r\n<body>\r\n    <div class=\"row\">\r\n    </div>\r\n    <div class=\"row secondRow\">\r\n        <h3 class=\"participant\">{0} {1}</h3>\r\n        <p>successfully covered distance:</p>\r\n        <p class=\"distanceName\">{2}</p>\r\n         <p>in</p>\r\n        <p>Gun time: <span>{3}</span></p>\r\n        <p>Chip time: <span>{4}</span></p>\r\n        <p>and took place</p>\r\n        <p><span>{5}/{6}</span> overall</p>\r\n        <p><span>{7}/{8}</span> in <span>{9} {10}</span> category</p>\r\n        <p>BIB: <span>{11}</span></p>\r\n    </div>\r\n    <div class=\"row\">\r\n    </div>\r\n</body>\r\n\r\n</html>";
        //html1 = String.Format(html1, result.Name, result.Surname, result.Distance, result.GunTime, result.ChipTime, result.GeneralPlace, result.GeneralCount, result.CategoryPlace, result.CategoryCount, result.Gender, result.DistanceAge, result.Number);

        //var options = new LaunchOptions
        //{
        //    Headless = true,
        //    Args = new[] { "--no-sandbox" },
        //};
        //var browserFetcherOptions = new BrowserFetcherOptions { Path = Path.Combine(Directory.GetCurrentDirectory(), "staticfiles", "Result") };
        //var browserFetcher = new BrowserFetcher(browserFetcherOptions);
        //await browserFetcher.DownloadAsync();
        //using var browser = await Puppeteer.LaunchAsync(options);

        //using var page = await browser.NewPageAsync();
        //await page.SetContentAsync(html1);
        //var pdf = await page.PdfDataAsync();

        //return pdf;
        return result;
    }
}
