namespace PromotionClient
{
    public class OrderViewModel
    {
        /// <summary>
        /// Item Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// After All the Discount
        /// </summary>
        public int FinalPrice { get; set; } = 0;
    }
}