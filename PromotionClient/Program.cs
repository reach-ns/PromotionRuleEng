using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace PromotionClient
{
    class Program
    {
        static string PromotionFilePath = "PromotionData/Promotions.json";
        static string CatalogueFilePath = "PromotionData/Catalogue.json";
              
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var priceData = GetPriceCatalogues();
            foreach (var v in priceData)
            {
                Console.WriteLine(v.Id + "\t" + v.Name + "\t" + v.Price);
            }
        }


        /// <summary>
        /// Prce Data from JSon
        /// </summary>
        /// <returns></returns>
        public static List<Catalogue> GetPriceCatalogues()
        {
            string CatalogueData = string.Empty;

            List<Catalogue> stockConfigurations = new List<Catalogue>();
            using (StreamReader fi = File.OpenText(CatalogueFilePath))
            {
                CatalogueData = fi.ReadToEnd();
                if (!string.IsNullOrEmpty(CatalogueData))
                {
                    stockConfigurations = JsonConvert.DeserializeObject<StockCatalogue>(CatalogueData).Catalogues;
                }
            }

            return stockConfigurations;
        }

        /// <summary>
        /// Promotion data retrievel from data store -json
        /// </summary>
        /// <returns></returns>

        public static List<Promotions> GetAllPromotions()
        {

            string PromotionData = string.Empty;

            List<Promotions> promoConfigurations = new List<Promotions>();
            using (StreamReader fi = File.OpenText(PromotionFilePath))
            {
                PromotionData = fi.ReadToEnd();
                if (!string.IsNullOrEmpty(PromotionData))
                {
                    promoConfigurations = JsonConvert.DeserializeObject<ActivePromotion>(PromotionData).Promotions;
                }
            }

            return promoConfigurations;
        }


     


    }
}
