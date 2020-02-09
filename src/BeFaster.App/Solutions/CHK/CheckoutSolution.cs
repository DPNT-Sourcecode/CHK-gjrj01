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
            int total = 0;

            foreach (var item in items)
            {
                switch (item.Key)
                {
                    case "A":
                        total += PriceWithOffer(item.Value, 3, 130, 50);
                        break;

                    case "B":
                        total += PriceWithOffer(item.Value, 2, 45, 30);
                        break;

                    case "C":
                        total += item.Value * 20;
                        break;

                    case "D":
                        total += item.Value * 15;
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
            foreach (var item in qtyPriceDiscount)
            {

            }



            return (qty / qtyOffer) * priceWithOffer + (qty % qtyOffer * pricePerSingle);
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
    }
}

