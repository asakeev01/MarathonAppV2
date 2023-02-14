using System;
using System.Text;

namespace Domain.Common.Helpers;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding { get { return Encoding.UTF8; } }
}

