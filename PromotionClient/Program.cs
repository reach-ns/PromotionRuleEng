using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace PromotionClient
{
    class Program
    {
        static string PromotionFilePath = "PromotionData/Promotions.json";
        static string CatalogueFilePath = "PromotionData/Catalogue.json";
              
        static void Main(string[] args)
        {
          
            var priceData = GetPriceCatalogues();
            foreach (var v in priceData)
            {
                Console.WriteLine(v.Id + "\t" + v.Name + "\t" + v.Price);
            }

            var promo_list = GetPromotions();                  
            double total = GetTotalAmount(priceData, promo_list);

            Console.WriteLine("Total Amount=" + total);


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

        public static List<Promotions> GetPromotions()
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


        public static List<OrderViewModel> CalculatePromotion(List<Catalogue> priceData, List<Promotions> promotion, List<OrderViewModel> OrderList)
        {

            if (promotion.Any() && priceData.Any() && OrderList.Any())
            {
                foreach (var v in priceData)
                {
                    var priceForA = OrderList.Where(x => x.Id == v.Id).Select(x => x.Quantity).FirstOrDefault();
                    if (priceForA != 0)
                    {
                        var promotions = promotion.FirstOrDefault(x => x.CatalogueId == v.Id && x.SecondaryId == null);
                        if (promotions != null && promotions.Units <= priceForA)
                        {
                            int temp = priceForA % promotions.Units;
                            int stdPrice = v.Price * temp;
                            int itemsToApply = (priceForA - temp) / promotions.Units;
                            int offerPrice = itemsToApply * promotions.Price;
                            int Total = offerPrice + stdPrice;

                            foreach (var o in OrderList.Where(x => x.Id == v.Id))
                            {
                                o.FinalPrice = Total;
                            }
                        }
                        else
                        {
                            int total = priceForA * v.Price;
                            foreach (var o in OrderList.Where(x => x.Id == v.Id))
                            {
                                o.FinalPrice = total;
                            }
                        }
                    }
                }


                var combo = promotion.Where(x => x.SecondaryId != null);

                foreach (var cp in combo)
                {
                    var comboItemIds = new List<int>();
                    comboItemIds.Add(cp.Id);
                    comboItemIds.Add(cp.SecondaryId.Value);



                    if (OrderList.Any(x => x.Id == cp.Id && x.Quantity > 0) && OrderList.Any(y => y.Id == cp.SecondaryId.Value && y.Quantity > 0))
                    {
                        var orderLocalPrimary = OrderList.FirstOrDefault(x => x.Id == cp.Id); // Get the primary ITem

                        // Get the Count which doesnt fall under combo
                        int tempC = orderLocalPrimary.Quantity > cp.Units ? orderLocalPrimary.Quantity - cp.Units : cp.Units - orderLocalPrimary.Quantity;
                        int tempCprice = tempC * (promotion.FirstOrDefault(x => x.Id == orderLocalPrimary.Id).Price);

                        // Now calculate for the remaining by attaching the combo

                        int comboC = cp.Price;

                        int primaryItemsPiceTag = tempCprice + comboC;

                        // Now check for the Secondary Items

                        var orderLocalSecondary = OrderList.FirstOrDefault(x => x.Id == cp.SecondaryId); // Get the primary ITem

                        int secondaryCount = orderLocalSecondary.Quantity > cp.Units ? orderLocalSecondary.Quantity - cp.Units : cp.Units - orderLocalSecondary.Quantity;

                        // Apply standard price for the Secondary

                        int tempDprice = secondaryCount * (promotion.FirstOrDefault(x => x.SecondaryId == orderLocalSecondary.Id).Price);
                        int tempDTotal = tempDprice;

                        // Update Prices for primary
                        foreach (var v in OrderList.Where(x => x.Id == orderLocalPrimary.Id))
                        {
                            v.FinalPrice = primaryItemsPiceTag;
                        }

                        //update prices for secondary
                        foreach (var v in OrderList.Where(x => x.Id == orderLocalSecondary.Id))
                        {
                            v.FinalPrice = tempDTotal;
                        }
                    }

                }
            }

            return OrderList;
        }

        public static double GetTotalAmount(List<Catalogue> priceData, List<Promotions> promotion)
        {
            double totalValue = 0;
            List<OrderViewModel> OrderList = new List<OrderViewModel>();

            foreach (var v in priceData)
            {
                Console.WriteLine(v.Id + "\t" + v.Name);
                int.TryParse(Console.ReadLine(), out int Qty);
                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    Id = v.Id,
                    Quantity = Qty
                };
                OrderList.Add(orderViewModel);
              
            }

            if (OrderList.Any())
            {
                OrderList = CalculatePromotion(priceData, promotion, OrderList);

                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var v in OrderList)
                {
                    Console.WriteLine(v.Id + "\t" + v.Quantity + "\t" + v.FinalPrice);
                }

                Console.ResetColor();
                totalValue = OrderList.Sum(item => item.FinalPrice);
              


            }


            return totalValue;
               
        }

    }
}
