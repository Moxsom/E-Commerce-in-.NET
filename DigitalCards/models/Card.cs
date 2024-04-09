namespace DigitalCards.models
{
    public class Card
    {
        public int CardID { get; set; }

        public string CardName { get; set; } = string.Empty;

        public string CardType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public string ImageName { get; set; } = string.Empty;

        public bool isFeatured { get; set; } = false;
    }
}
