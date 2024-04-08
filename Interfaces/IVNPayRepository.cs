using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{
    public interface IVNPayRepository
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
