namespace CF_HOATUOIBASANH.Models;

public class PaymentResponseModel
{
    public string DeliveryMethod { get; set; }
    public string OrderDescription { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentId { get; set; }
    public bool Success { get; set; }
    public string Token { get; set; }
    public string VnPayResponseCode { get; set; }
}