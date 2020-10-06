namespace Vodovoz.Validators.Orders {
    public class OrderValidateParameters {
        public bool CreatedFromUndeliveryOrder { get; set; }
        public OrderValidateAction OrderAction { get; set; }
    }

    public enum OrderValidateAction{
        None,
        Accept,
        Close
    }
}