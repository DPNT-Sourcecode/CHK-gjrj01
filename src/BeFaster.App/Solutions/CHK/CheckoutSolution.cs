using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using BeFaster.Runner.Exceptions;
using CsvHelper;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            var basket = GetBasket(skus);


            int total = 0;

            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (var reader = new StreamReader($"{dir}\\Solutions\\CHK\\Prices.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var pricings = csv.GetRecords<Item>().ToList();

                    basket.ApplyPromotions(pricings);

                    total = GetTotal(pricings, basket);
                }
            }

            return total;
        }

        private static int GetTotal(List<Item> pricings, IDictionary<string, int> basket)
        {
            int total = 0;
            foreach (var item in basket)
            {
                if (pricings.Any(x => x.Sku == item.Key))
                {
                    var pricing = (pricings.Single(x => x.Sku == item.Key));

                    if (string.IsNullOrEmpty(pricing.Offer))
                        total += item.Value * pricing.Price;
                    else
                    {
                        var dict = new Dictionary<int, int>();
                        foreach (var offer in pricing.Offer.Split(' '))
                        {
                            if (!string.IsNullOrEmpty(offer))
                            {
                                var qty = int.Parse(offer.Split('-')[0]);
                                var price = int.Parse(offer.Split('-')[1]);
                                dict.Add(qty, price);
                            }

                        }

                        total += PriceWithOffer(item.Value, pricing.Price, dict);
                    }
                }
                else
                    return -1;
            }
            return total;
        }



        private static int PriceWithOffer(int qty, int pricePerSingle, Dictionary<int, int> qtyPriceDiscount)
        {
            var otherItemsPrice = 0;

            foreach (var item in qtyPriceDiscount.OrderByDescending(x => x.Key))
            {
                var group = qty / item.Key;
                if (group > 0)
                {
                    qty -= group * item.Key;
                    otherItemsPrice += group * item.Value;
                }
            }

            otherItemsPrice += qty * pricePerSingle;

            return otherItemsPrice;
        }

        private static IDictionary<string, int> GetBasket(string skus)
        {
            // there are no requirements about how the list of skus is provided.
            // I would ask for the PO about the format of the string SKUs
            // i.e. what separator? is it case sensitive a so on...
            // I'm assuming the PO replied saying the string is case sensitive and separator is , (comma)

            var dict = new Dictionary<string, int>();

            foreach (var x in skus)
            {
                var key = x.ToString();
                var contains = dict.ContainsKey(key);

                if (contains)
                    dict[key] = dict[key] + 1;
                else
                    dict.Add(key, 1);
            }

            return dict;
        }
        private static void ApplyPromotions(this IDictionary<string, int> basket, IList<Item> pricings)
        {
            foreach (var pricing in pricings)
            {
                if (!string.IsNullOrEmpty(pricing.Combined))
                {
                    foreach (var comb in pricing.Combined.Split(' '))
                    {
                        var values = comb.Split('-');
                        basket.ApplyPromotion(pricing.Sku, int.Parse(values[0]), values[1], int.Parse(values[2]));
                    }
                }
            }
        }

        private static void ApplyPromotion(this IDictionary<string, int> basket, string inOffer, int qty, string freeItem, int qtyFreeItem)
        {
            if (basket.ContainsKey(inOffer) && basket.ContainsKey(freeItem))
            {
                basket[freeItem] = basket[freeItem] - (qtyFreeItem * basket[inOffer] / qty);

                if (basket[freeItem] < 0)
                    basket[freeItem] = 0;
            }
        }
    }
}
