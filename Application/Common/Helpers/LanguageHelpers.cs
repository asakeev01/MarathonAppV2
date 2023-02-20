using Core.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Helpers;

public static class LanguageHelpers
{
    public static string CheckLanguageCode(string code)
    {
        if (!ApplicationConstants.SupportedLanguages.Contains(code))
        {
            code = ApplicationConstants.SupportedLanguages[0];
        }
        return code;
    }
}
