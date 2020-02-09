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
                        if (IsCombinedOffer(pricing.Offer))
                        {
                            var splits = pricing.Offer.Replace("(", "").Replace(")", "").Split('-');
                            var qty = int.Parse(splits[0]);
                            var inOffer = splits[1].Split('|')
                                .Select(x => new { sku = x, price = pricings.Single(p => p.Sku == x).Price })
                                .OrderByDescending(x => x.price).ToList();

                            var price = int.Parse(splits[2]);

                            if (basket.Where(x => inOffer.Select(s => s.sku).Contains(x.Key)).Select(x => x.Value).Sum() >= qty)
                            {
                                var index = 0;
                                for (int i = 0; i < qty; i++)
                                {
                                    if (basket.ContainsKey(inOffer[index]))
                                }
                            }
                        }

                        else
                        {
                            var dict = pricing.Offer.Split(' ').Select(x =>
                            new
                            {
                                qty = int.Parse(x.Split('-')[0]),
                                price = int.Parse(x.Split('-')[1])
                            }).ToDictionary(k => k.qty, v => v.price);

                            total += PriceWithOffer(item.Value, pricing.Price, dict);
                        }
                    }
                }
                else
                    return -1;
            }
            return total;
        }

        private static bool IsCombinedOffer(string offer)
        {
            return offer.Contains('(') && offer.Contains(')');
        }

        private static int PriceWithOffer(int qty, int pricePerSingle, Dictionary<int, int> qtyPriceDiscount)
        {
            var price = 0;

            foreach (var item in qtyPriceDiscount.OrderByDescending(x => x.Key))
            {
                var group = qty / item.Key;
                if (group > 0)
                {
                    qty -= group * item.Key;
                    price += group * item.Value;
                }
            }

            price += qty * pricePerSingle;

            return price;
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
                if (!string.IsNullOrEmpty(pricing.OtherItemsOffer))
                {
                    foreach (var comb in pricing.OtherItemsOffer.Split(' '))
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
