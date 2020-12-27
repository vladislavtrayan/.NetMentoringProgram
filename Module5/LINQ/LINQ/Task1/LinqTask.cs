using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select(c => (c, suppliers.Where(s => s.Country == c.Country && s.City == c.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.GroupJoin(suppliers,
                customer => new {customer.City, customer.Country},
                supplier => new {supplier.City, supplier.Country},
                (customer, suppliers) => (customer, suppliers));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Any(o => o.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(c => c.Orders.Length != 0).Select(c => (c, c.Orders.Min(o => o.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(c => c.Orders.Length != 0).Select(c => (c, c.Orders.Min(o => o.OrderDate)))
                .OrderBy(cd => cd.Item2.Year)
                .ThenBy(cd => cd.Item2.Month)
                .ThenByDescending(cd => cd.c.Orders.Sum(o => o.Total))
                .ThenBy(cd => cd.c.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.Where(c =>
                Regex.IsMatch(c.PostalCode, @"[A-Za-z]+") || !Regex.IsMatch(c.Phone, @"^\(.*?\)") ||
                c.Region == String.Empty || c.Region == null);
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */
            return products.GroupBy(p => p.Category).Select(g => new Linq7CategoryGroup()
            {
                Category = g.Key,
                UnitsInStockGroup = g.Select(p => new Linq7UnitsInStockGroup()
                {
                  UnitsInStock  = p.UnitsInStock,
                  Prices = g.Select(p => p.UnitPrice).OrderBy(price => price).ToArray()
                })
            });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            // stupid task ?!?Ы
            
            return products.GroupBy(p => new
            {
                isCheap = p.UnitPrice <= cheap,
                isMiddle = p.UnitPrice <= middle & p.UnitPrice > cheap,
                isExpensive = p.UnitPrice <= expensive & p.UnitPrice > middle
            }).Select(g =>
            {
                if(g.Key.isExpensive)
                    return (expensive, g.AsEnumerable());
                return g.Key.isMiddle ? (middle, g.AsEnumerable()) : (cheap, g.AsEnumerable());
            });
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers.GroupBy(c => c.City).Select(g => (g.Key, (int)Math.Round(g.Average(c => c.Orders.Sum(o => o.Total))), (int)Math.Round(g.Average(c => c.Orders.Length))));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            return string.Join("", suppliers.Select(s => s.Country).Distinct().OrderBy(c => c.Length).ThenBy(c => c[0]));
        }
    }
}