namespace DigitalCards.models
{
    public class Order
    {

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Province { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int ccNumber { get; set; }

        public string ccExpiryDate { get; set; } = string.Empty;

        public int ccv { get; set; }

        public string Products { get; set; } = string.Empty;

        public string ConfirmationNumber { get; set; } = string.Empty;


    }
}
