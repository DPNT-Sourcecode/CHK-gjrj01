using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            var items = GetListFromBasket(skus);

            // 2E get 1 B free
            items.ApplyPromotion("E", 2, "B", 1);

            items.ApplyPromotion("N", 3, "M", 1);
            items.ApplyPromotion("R", 3, "Q", 1);

            int total = 0;

            foreach (var item in items)
            {
                switch (item.Key)
                {
                    case "A":
                        total += PriceWithOffer(item.Value, 50, new Dictionary<int, int> { { 5, 200 }, { 3, 130 } });
                        break;

                    case "B":
                        total += PriceWithOffer(item.Value, 30, new Dictionary<int, int> { { 2, 45 } });
                        break;

                    case "C":
                        total += item.Value * 20;
                        break;

                    case "D":
                        total += item.Value * 15;
                        break;

                    case "E":
                        total += item.Value * 40;
                        break;

                    case "F":
                        total += PriceWithOffer(item.Value, 10, new Dictionary<int, int> { { 3, 20 } });
                        break;

                    case "G":
                        total += item.Value * 20;
                        break;

                    case "H":
                        total += PriceWithOffer(item.Value, 10, new Dictionary<int, int> { { 5, 45 }, { 10, 80 } });
                        break;

                    case "I":
                        total += item.Value * 35;
                        break;

                    case "J":
                        total += item.Value * 60;
                        break;

                    case "K":
                        total += PriceWithOffer(item.Value, 80, new Dictionary<int, int> { { 2, 150 } });
                        break;

                    case "L":
                        total += item.Value * 90;
                        break;

                    case "M":
                        total += item.Value * 15;
                        break;

                    case "N":
                        total += item.Value * 40;
                        break;

                    case "O":
                        total += item.Value * 10;
                        break;

                    case "P":
                        total += PriceWithOffer(item.Value, 50, new Dictionary<int, int> { { 5, 200 } });
                        break;

                    case "Q":
                        total += PriceWithOffer(item.Value, 30, new Dictionary<int, int> { { 3, 80 } });
                        break;

                    case "R":
                        total += item.Value * 50;
                        break;

                    default:
                        return -1;
                }
            }

            return total;
        }

        private static int PriceWithOffer(int qty, int pricePerSingle, Dictionary<int, int> qtyPriceDiscount)
        {
            var combinedPrice = 0;

            foreach (var item in qtyPriceDiscount.OrderByDescending(x => x.Key))
            {
                var group = qty / item.Key;
                if (group > 0)
                {
                    qty -= group * item.Key;
                    combinedPrice += group * item.Value;
                }
            }

            combinedPrice += qty * pricePerSingle;

            return combinedPrice;
        }

        private static IDictionary<string, int> GetListFromBasket(string skus)
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
