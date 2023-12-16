using Application.Helpers.Interfaces;
using Application.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    public class ReportController : BaseApiController
    {
        private readonly IProductUOW _productUOW;
        private readonly IPdfHelper _pdfHelper;

        public ReportController(IProductUOW productUOW, IPdfHelper pdfHelper)
        {
            _productUOW = productUOW;
            _pdfHelper = pdfHelper;
        }

        [HttpGet]
        public IActionResult GeneratePdf([FromQuery] Domain.DTOs.Product.ProductReportRequest message)
        {
            var pdf = _productUOW.GetProductReportAsPdf(message);

            

            return File(pdf, "application/pdf");
        }
    }
}
