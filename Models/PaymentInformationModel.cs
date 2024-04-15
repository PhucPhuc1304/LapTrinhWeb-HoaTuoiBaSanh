namespace CF_HOATUOIBASANH.Models;

public class PaymentInformationModel
{
    public string OrderType { get; set; }
    public double Amount { get; set; }
    public string OrderDescription { get; set; }
    public string Name { get; set; }
    public string DeliveryMethod { get; set; }
    public string Note { get; set; }    
    public string ShipAddress { get; set; }

    public double ShipCost { get; set; }
}