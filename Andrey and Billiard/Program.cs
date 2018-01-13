using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrey_and_Billiard
{
    class Program
    {
        class Customer
        {
            public string Name { get; set; }
            public Dictionary<string, int> ProductsAndQuantity { get; set; }
            public decimal Bill { get; set; }
        }

        static void Main(string[] args)
        {
                        var products = new Dictionary<string, decimal>();

            getProductsAndPrices(products);

            List<Customer> customers = new List<Customer>();

            string line = Console.ReadLine();
            while (line != "end of clients")
            {
                var info = line.Split(new char[] { '-', ',' });

                string name = info[0];
                string product = info[1];
                int quantity = int.Parse(info[2]);

                if (products.ContainsKey(product))
                {
                    Customer customer = new Customer();
                    customer.Name = name;
                    customer.ProductsAndQuantity = new Dictionary<string, int>();
                    customer.ProductsAndQuantity.Add(product, quantity);


                    if (customers.Any(x => x.Name == customer.Name))
                    {
                        var cust = customers.Where(x => x.Name == customer.Name).First();

                        if (cust.ProductsAndQuantity.ContainsKey(product))
                        {
                            cust.ProductsAndQuantity[product] += quantity;
                        }
                        else
                        {
                            cust.ProductsAndQuantity.Add(product, quantity);
                        }
                        //cust.Bill = customer.Bill + products[product] * quantit
                    }
                    else
                    {
                        customers.Add(customer);
                    }

                }
                line = Console.ReadLine();
            }
            foreach (var c in customers)
            {
                foreach (var item in c.ProductsAndQuantity)
                {
                    foreach (var p in products)
                    {
                        if (item.Key == p.Key)
                        {
                            c.Bill += item.Value * p.Value;
                        }
                    }
                }
            }

            decimal totalBill = 0m;
            foreach (var c in customers.OrderBy(s=>s.Name))
            {
                Console.WriteLine(c.Name);

                foreach (var ProductsAndQuantity in c.ProductsAndQuantity)
                {
                    Console.WriteLine("-- {0} - {1}",ProductsAndQuantity.Key, ProductsAndQuantity.Value);
                }
                Console.WriteLine("Bill: {0:F2}",c.Bill);
                totalBill += c.Bill;
            }
            Console.WriteLine("Total bill: {0:F2}", totalBill);
        }

        private static void getProductsAndPrices(Dictionary<string, decimal> products)
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine().Split('-');

                var product = line[0];
                var price = decimal.Parse(line[1]);

                if (!products.ContainsKey(product)) //ако във продуктите нямаме продукт от този тип
                {
                    products.Add(product, 0); // >> тогава ми постави продукта!
                }
                products[product] = price;//презаписваме с новата цена продукта
            }
        }
    }
}
