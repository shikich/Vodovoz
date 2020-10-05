namespace Vodovoz.Validators.Orders {
    public class OrderValidateParameters {
        public bool CreatedFromUndeliveryOrder { get; set; }
        public bool AcceptedOrder { get; set; }
        public bool WaitingForPayment { get; set; }
        public bool ClosingOrder { get; set; }
    }
}