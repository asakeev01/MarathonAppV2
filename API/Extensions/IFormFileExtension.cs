

namespace API.Extensions;

internal static class IFormFileExtension
{
    internal static async Task<(Stream Source, string FileName)?> ToNullableFile(this IFormFile form)
    {
        if (form == null)
            return null;

        return await form.ToFile();
    }

    internal static async Task<(Stream Source, string FileName)> ToFile(this IFormFile form)
    {
        //if (form == null)
        //    throw new InnerException($"2509. Файл не привязан.", "File");

        var source = new MemoryStream();
        await form.CopyToAsync(source);

        return (source, form.FileName);
    }
}
