using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionClient
{
    public class Catalogue
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public int Price { get; set; }
    }

    public class StockCatalogue
    {
        [JsonProperty(PropertyName = "catalogue")]
        public List<Catalogue> Catalogues { get; set; }
    }
}
