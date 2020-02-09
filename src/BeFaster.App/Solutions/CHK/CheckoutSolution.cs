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
                        total += 50;
                        break;

                    case "B":
                        total += 30;
                        break;

                    case "C":
                        total += 20;
                        break;

                    case "D":
                        total += 15;
                        break;

                    default:
                        return -1;
                }
            }

            return total;
        }

        private static IDictionary<string, int> GetListFromBasket(string skus)
        {
            // there are no requirements about how the list of skus is provided.
            // I would ask for the PO about the format of the string SKUs
            // i.e. what separator? is it case sensitive a so on...
            // I'm assuming the PO replied saying the string is case sensitive and separator is , (comma)

            var dict = new Dictionary<string, int>();

            foreach (var x in skus.Split(','))
            {
                var key = x.Trim().ToUpper();
                var value = dict.ContainsKey(key) ? dict[key] + 1 : 1;

                dict.Add(key, value);
            }

            return dict;
        }
    }
}



