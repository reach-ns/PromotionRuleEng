using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionClient;
using System.Collections.Generic;
using System.Linq;

namespace PromotionRuleUnitTest
{
    [TestClass]
    public class PromotionTest
    {
       
       

        [TestMethod]
        public void Scenario1()
        {
            var priceData = Program.GetPriceCatalogues();
            var promotions = Program.GetPromotions();

            List<OrderViewModel> orders = new List<OrderViewModel>();

            OrderViewModel order1 = new OrderViewModel() // A
            {
                Id = 1,
                Quantity = 1
            };
            orders.Add(order1);

            OrderViewModel order2 = new OrderViewModel() // B
            {
                Id = 2,
                Quantity = 1
            };
            orders.Add(order2);

            OrderViewModel order3 = new OrderViewModel() // C
            {
                Id = 3,
                Quantity = 1
            };
            orders.Add(order3);

            OrderViewModel order4 = new OrderViewModel() // D
            {
                Id = 4,
                Quantity = 0
            };

            orders.Add(order4);

            var orderlist = Program.CalculatePromotion(priceData, promotions, orders);
            double totalValue = orders.Sum(item => item.FinalPrice);

            Assert.AreEqual(totalValue, 100);
        }


        [TestMethod]
        public void Scenario2()
        {
            var priceData = Program.GetPriceCatalogues();
            var promotions = Program.GetPromotions();

            List<OrderViewModel> orders = new List<OrderViewModel>();

            OrderViewModel order1 = new OrderViewModel() // A
            {
                Id = 1,
                Quantity = 5
            };
            orders.Add(order1);

            OrderViewModel order2 = new OrderViewModel() // B
            {
                Id = 2,
                Quantity = 5
            };
            orders.Add(order2);

            OrderViewModel order3 = new OrderViewModel() // C
            {
                Id = 3,
                Quantity = 1
            };
            orders.Add(order3);

            OrderViewModel order4 = new OrderViewModel() // D
            {
                Id = 4,
                Quantity = 0
            };

            orders.Add(order4);

            var orderlist = Program.CalculatePromotion(priceData, promotions, orders);
            double totalValue = orders.Sum(item => item.FinalPrice);

            Assert.AreEqual(totalValue, 370);
        }


      

        [TestMethod]
        public void Scenario3()
        {
            var priceData = Program.GetPriceCatalogues();
            var promotions = Program.GetPromotions();

            List<OrderViewModel> orders = new List<OrderViewModel>();

            OrderViewModel order1 = new OrderViewModel() // A
            {
                Id = 1,
                Quantity = 3
            };
            orders.Add(order1);

            OrderViewModel order2 = new OrderViewModel() // B
            {
                Id = 2,
                Quantity = 5
            };
            orders.Add(order2);

            OrderViewModel order3 = new OrderViewModel() // C
            {
                Id = 3,
                Quantity = 1
            };
            orders.Add(order3);

            OrderViewModel order4 = new OrderViewModel() // D
            {
                Id = 4,
                Quantity = 1
            };

            orders.Add(order4);

            var orderlist = Program.CalculatePromotion(priceData, promotions, orders);
            double totalValue = orders.Sum(item => item.FinalPrice);

            Assert.AreEqual(totalValue, 280);
        }


    }
}
