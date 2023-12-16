namespace Application.Helpers.Interfaces
{
    public interface IPdfHelper
    {
        public byte[] ConvertHtmlToPdf(string html);
        string GenerateHtmlTable<T>(List<T> dataSource, string title);
    }
}