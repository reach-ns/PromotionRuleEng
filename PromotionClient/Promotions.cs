using Newtonsoft.Json;
using System.Collections.Generic;

namespace PromotionClient
{
    

        public class Promotions
        {
            [JsonProperty(PropertyName = "id")]
            public int Id { get; set; }

            [JsonProperty(PropertyName = "catalogueId")]
            public int CatalogueId { get; set; }

            [JsonProperty(PropertyName = "units")]
            public int Units { get; set; }

            [JsonProperty(PropertyName = "price")]
            public int Price { get; set; }

            [JsonProperty(PropertyName = "secondaryId")]
            public int? SecondaryId { get; set; }
        }

        public class ActivePromotion
        {
            [JsonProperty(PropertyName = "promotions")]
            public List<Promotions> Promotions { get; set; }
        }
    
}