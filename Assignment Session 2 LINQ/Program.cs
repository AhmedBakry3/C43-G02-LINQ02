using Assignment_Session_2_LINQ.Data;
using System.IO;
using System;
using System.Xml;
using static Assignment_Session_2_LINQ.Classes.ListGenerators;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;

namespace Assignment_Session_2_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Aggregate Operators

            #region Question 1 :  Get the total units in stock for each product category.

            //Fluent Syntax
            
            var Result = productsList.GroupBy(P => P.Category)
                                     .Select(Category => new
                                     {
                                         CategoryName = Category.Key,
                                         TotalUnitInStock = Category.Sum(P => P.UnitsInStock)
                                     });


            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         select new
                         {
                             CategoryName = Category.Key,
                             TotalUnitInStock = Category.Sum(P => P.UnitsInStock)
                         };


            foreach (var item in Result)
                Console.WriteLine(item);

            //Output:

            //{ CategoryName = Beverages, TotalUnitInStock = 620 }
            //{ CategoryName = Condiments, TotalUnitInStock = 507 }
            //{ CategoryName = Produce, TotalUnitInStock = 100 }
            //{ CategoryName = Meat / Poultry, TotalUnitInStock = 165 }
            //{ CategoryName = Seafood, TotalUnitInStock = 701 }
            //{ CategoryName = Dairy Products, TotalUnitInStock = 393 }
            //{ CategoryName = Confections, TotalUnitInStock = 386 }
            //{ CategoryName = Grains / Cereals, TotalUnitInStock = 308 }


            #endregion

            #region Question 2 : Get the cheapest price among each category's products

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                     .Select(Category => new
                                     {
                                         CatgeoryName = Category.Key,
                                         CheapestPrice = Category.Min(P => P.UnitPrice)
                                     });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         select new
                         {
                             CatgeoryName = Category.Key,
                             CheapestPrice = Category.Min(P => P.UnitPrice)
                         };

            foreach (var item in Result)
                Console.WriteLine(item);

            //Output :

            //{ CatgeoryName = Beverages, CheapestPrice = 4.5000 }
            //{ CatgeoryName = Condiments, CheapestPrice = 10.0000 }
            //{ CatgeoryName = Produce, CheapestPrice = 10.0000 }
            //{ CatgeoryName = Meat / Poultry, CheapestPrice = 7.4500 }
            //{ CatgeoryName = Seafood, CheapestPrice = 6.0000 }
            //{ CatgeoryName = Dairy Products, CheapestPrice = 2.5000 }
            //{ CatgeoryName = Confections, CheapestPrice = 9.2000 }
            //{ CatgeoryName = Grains / Cereals, CheapestPrice = 7.0000 }

            #endregion

            #region Question 3 : Get the products with the cheapest price in each category (Use Let)

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                      .Select(Category => new
                                      {
                                          CategoryName = Category.Key,
                                          CheapestPrice = Category.Min(P => P.UnitPrice),
                                          Products = Category.Where(P => P.UnitPrice == Category.Min(P => P.UnitPrice))
                                      })
                                      .SelectMany(Category => Category.Products, (Category, product) => new
                                      {
                                          Category.CategoryName,
                                          product.ProductName,
                                          Category.CheapestPrice
                                      });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         let CheapestPrice = Category.Min(P => P.UnitPrice)
                         from P in productsList
                         where P.UnitPrice == CheapestPrice
                         select new
                         {
                             CategoryName = Category.Key,
                             P.ProductName,
                             CheapestPrice
                         };


            foreach (var item in Result)
                Console.WriteLine(item);

            //Output :
            //{ CategoryName = Beverages, ProductName = Guaraná Fantástica, CheapestPrice = 4.5000 }
            //{ CategoryName = Condiments, ProductName = Aniseed Syrup, CheapestPrice = 10.0000 }
            //{ CategoryName = Produce, ProductName = Longlife Tofu, CheapestPrice = 10.0000 }
            //{ CategoryName = Meat / Poultry, ProductName = Tourtière, CheapestPrice = 7.4500 }
            //{ CategoryName = Seafood, ProductName = Konbu, CheapestPrice = 6.0000 }
            //{ CategoryName = Dairy Products, ProductName = Geitost, CheapestPrice = 2.5000 }
            //{ CategoryName = Confections, ProductName = Teatime Chocolate Biscuits, CheapestPrice = 9.2000 }
            //{ CategoryName = Grains / Cereals, ProductName = Filo Mix, CheapestPrice = 7.0000 }


            #endregion

            #region Question 4 : Get the most expensive price among each category's products.

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                     .Select(Category => new
                                     {
                                         CatgeoryName = Category.Key,
                                         MostExpensivePrice = Category.Max(P => P.UnitPrice)
                                     });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         select new
                         {
                             CatgeoryName = Category.Key,
                             MostExpensivePrice = Category.Max(P => P.UnitPrice)
                         };

            foreach (var item in Result)
                Console.WriteLine(item);

            //Output:

            //{ CatgeoryName = Beverages, MostExpensivePrice = 263.5000 }
            //{ CatgeoryName = Condiments, MostExpensivePrice = 43.9000 }
            //{ CatgeoryName = Produce, MostExpensivePrice = 53.0000 }
            //{ CatgeoryName = Meat / Poultry, MostExpensivePrice = 123.7900 }
            //{ CatgeoryName = Seafood, MostExpensivePrice = 62.5000 }
            //{ CatgeoryName = Dairy Products, MostExpensivePrice = 55.0000 }
            //{ CatgeoryName = Confections, MostExpensivePrice = 81.0000 }
            //{ CatgeoryName = Grains / Cereals, MostExpensivePrice = 38.0000 }

            #endregion

            #region Question 5 : Get the products with the most expensive price in each category.

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                     .Select(Category => new
                                     {
                                         CategoryName = Category.Key,
                                         MostExpensivePrice = Category.Max(P => P.UnitPrice),
                                         products = Category.Where(P => P.UnitPrice == Category.Max(P => P.UnitPrice))
                                     })
                                     .SelectMany(Categroy => Categroy.products, (Category, Products) => new
                                     {
                                         Category.CategoryName,
                                         Products.ProductName,
                                         Category.MostExpensivePrice
                                     });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         from P in productsList
                         where P.UnitPrice == Category.Max(P => P.UnitPrice)
                         select new
                         {
                             CategoryName = Category.Key,
                             P.ProductName,
                             MostExpensivePrice = Category.Max(P => P.UnitPrice)
                         };


            foreach (var item in Result)
                Console.WriteLine(item);

            //Output:

            //{ CategoryName = Beverages, ProductName = Côte de Blaye, MostExpensivePrice = 263.5000 }
            //{ CategoryName = Condiments, ProductName = Vegie - spread, MostExpensivePrice = 43.9000 }
            //{ CategoryName = Produce, ProductName = Manjimup Dried Apples, MostExpensivePrice = 53.0000 }
            //{ CategoryName = Meat / Poultry, ProductName = Thüringer Rostbratwurst, MostExpensivePrice = 123.7900 }
            //{ CategoryName = Seafood, ProductName = Carnarvon Tigers, MostExpensivePrice = 62.5000 }
            //{ CategoryName = Dairy Products, ProductName = Raclette Courdavault, MostExpensivePrice = 55.0000 }
            //{CategoryName = Confections, ProductName = Sir Rodney's Marmalade, MostExpensivePrice = 81.0000 }
            //{ CategoryName = Grains / Cereals, ProductName = Gnocchi di nonna Alice, MostExpensivePrice = 38.0000 }

            #endregion

            #region Question 6 : Get the average price of each category's products.

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                     .Select(Category => new
                                     {
                                         CatgeoryName = Category.Key,
                                         AveragePrice = Category.Average(P => P.UnitPrice)
                                     });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         select new
                         {
                             CatgeoryName = Category.Key,
                             AveragePrice = Category.Average(P => P.UnitPrice)
                         };

            foreach (var item in Result)
                Console.WriteLine(item);

            //Output :

            //{ CatgeoryName = Beverages, AveragePrice = 37.979166666666666666666666667 }
            //{ CatgeoryName = Condiments, AveragePrice = 23.0625 }
            //{ CatgeoryName = Produce, AveragePrice = 32.3700 }
            //{ CatgeoryName = Meat / Poultry, AveragePrice = 54.006666666666666666666666667 }
            //{ CatgeoryName = Seafood, AveragePrice = 20.6825 }
            //{ CatgeoryName = Dairy Products, AveragePrice = 28.7300 }
            //{ CatgeoryName = Confections, AveragePrice = 25.1600 }
            //{ CatgeoryName = Grains / Cereals, AveragePrice = 20.2500 }

            #endregion
            #endregion

            #region LINQ - Set Operators

            #region Question 1 : Find the unique Category names from Product List

            //Fluent Syntax

            var Result = productsList.Select(P => P.Category).Distinct();


            //Set Operators only works with Fluent Syntax , but it can be used with Query Syntax Throught Fluent Syntax [Hybrid Syntax]
            //Set Operators can be used as Query Syntax when it applied to filtering, projecting
            // Hybrid Syntax = Query Syntax + Fluent Syntax
            // Hybrid Syntax = (Query Syntax).Fluent Syntax

            //Hybrid Syntax

            var Result = (from P in productsList
                          select P.Category).Distinct();


            foreach (var item in Result)
                Console.WriteLine(item);

            //Output:

            //Beverages
            //Condiments
            //Produce
            //Meat / Poultry
            //Seafood
            //Dairy Products
            //Confections
            //Grains / Cereals



            #endregion
            #region Question 2 : Produce a Sequence containing the unique first letter from both product and customer names.

            //Fluent Syntax

            var Result = productsList.Select(P => P.ProductName[0])
                                     .Union(CustomersList.Select(C => C.CustomerName[0]))
                                     .Distinct();


            //Hybrid Syntax

            var Result = (from P in productsList
                          select P.ProductName[0])
                         .Union(CustomersList.Select(C => C.CustomerName[0]))
                         .Distinct();


            foreach (var item in Result)
                Console.Write($"{item} ");


            //Output :

            //C A G U N M I Q K T P S R B J Z V F E W L O D H

            #endregion

            #region Question 3 : Create one sequence that contains the common first letter from both product and customer names.

            //Fluent Syntax

            var Result = productsList.Select(P => P.ProductName[0])
                                     .Intersect(CustomersList.Select(C => C.CustomerName[0]));



            //Hybrid Syntax

            var Result = (from P in productsList
                          select P.ProductName[0])
                         .Intersect(CustomersList.Select(C => C.CustomerName[0]));


            foreach (var item in Result)
                Console.Write($"{item} ");

            //Output :

            //C A G N M I Q K T P S R B V F E W L O



            #endregion

            #region Question 4 :  Create one sequence that contains the first letters of product names that are not also first letters of customer names.


            //Fluent Syntax

            var Result = productsList.Select(P => P.ProductName[0])
                                     .Except(CustomersList.Select(C => C.CustomerName[0]));



            //Hybrid Syntax

            var Result = (from P in productsList
                          select P.ProductName[0])
                         .Except(CustomersList.Select(C => C.CustomerName[0]));


            foreach (var item in Result)
                Console.Write($"{item} ");

            //Output :

            //U J Z
            #endregion

            #region Question 5 : Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates

            //Fluent Syntax

            var Result = productsList.Select(P => new string(P.ProductName.TakeLast(3).ToArray()))
                          .Concat(CustomersList.Select(C => new string(C.CustomerName.TakeLast(3).ToArray())));


            //Hybrid Syntax

            var Result = (from P in productsList
                          select new string(P.ProductName.TakeLast(3).ToArray()))
                          .Concat(from C in CustomersList
                                  select new string(C.CustomerName.TakeLast(3).ToArray()));


            foreach (var item in Result)
                Console.Write($"{item} ");




            #endregion
            #endregion

            #region LINQ - Partitioning Operators

            #region Question 1 : Get the first 3 orders from customers in Washington

            //Fluent Syntax

            var Result = CustomersList.Where(C => C.Region == "WA")
                                      .SelectMany(C => C.Orders)
                                      .Take(3);


            //Partitioning Operators only works with Fluent Syntax , but it can be used with Query Syntax Throught Fluent Syntax [Hybrid Syntax]
            //Partitioning Operators can be used as Query Syntax when it applied to filtering, projecting
            // Hybrid Syntax = Query Syntax + Fluent Syntax
            // Hybrid Syntax = (Query Syntax).Fluent Syntax

            //Hybrid Syntax

            var Result = (from C in CustomersList
                          where C.Region == "WA"
                          from O in C.Orders
                          select O)
                          .Take(3);


            foreach (var item in Result)
                Console.WriteLine(item);

            //Output : 

            //Order Id: 10482, Date: 3/21/1997, Total: 147.00
            //Order Id: 10545, Date: 5/22/1997, Total: 210.00
            //Order Id: 10574, Date: 6/19/1997, Total: 764.30
            #endregion

            #region Question 2 : Get all but the first 2 orders from customers in Washington.

            //Fluent Syntax

            var Result = CustomersList.Where(C => C.Region == "WA")
                                      .SelectMany(C => C.Orders)
                                      .Skip(2);

            //Hybrid Syntax

            var Result = (from C in CustomersList
                          where C.Region == "WA"
                          from O in C.Orders
                          select O)
                          .Skip(2);


            foreach (var item in Result)
                Console.WriteLine(item);

            //Output :

            //Order Id: 10574, Date: 6 / 19 / 1997, Total: 764.30
            //Order Id: 10577, Date: 6 / 23 / 1997, Total: 569.00
            //Order Id: 10822, Date: 1 / 8 / 1998, Total: 237.90
            //Order Id: 10269, Date: 7 / 31 / 1996, Total: 642.20
            //Order Id: 10344, Date: 11 / 1 / 1996, Total: 2296.00
            //Order Id: 10469, Date: 3 / 10 / 1997, Total: 956.68
            //Order Id: 10483, Date: 3 / 24 / 1997, Total: 668.80
            //Order Id: 10504, Date: 4 / 11 / 1997, Total: 1388.50
            //Order Id: 10596, Date: 7 / 11 / 1997, Total: 1180.88
            //Order Id: 10693, Date: 10 / 6 / 1997, Total: 2071.20
            //Order Id: 10696, Date: 10 / 8 / 1997, Total: 996.00
            //Order Id: 10723, Date: 10 / 30 / 1997, Total: 468.45
            //Order Id: 10740, Date: 11 / 13 / 1997, Total: 1416.00
            //Order Id: 10861, Date: 1 / 30 / 1998, Total: 3523.40
            //Order Id: 10904, Date: 2 / 24 / 1998, Total: 1924.25
            //Order Id: 11032, Date: 4 / 17 / 1998, Total: 8902.50
            //Order Id: 11066, Date: 5 / 1 / 1998, Total: 928.75
            #endregion

            #region Question 3 : Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.

            int[] Numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //Fluent Syntax

            var Result = Numbers.TakeWhile((Num, I) => Num > I);


            //Hybrid Syntax

            var Result = (from N in Numbers
                          select N).TakeWhile((Num, I) => Num > I);


            foreach (var item in Result)
                Console.Write($"{item} ");

            //Output :

            //5 4


            #endregion

            #region Question 4 : Get the elements of the array starting from the first element divisible by 3.

            int[] Numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //Fluent Syntax

            var Result = Numbers.SkipWhile(N => N % 3 != 0);

            //Hybrid Syntax

            var Result = (from N in Numbers
                          select N).SkipWhile(N => N % 3 != 0);


            foreach (var item in Result)
                Console.Write($"{item} ");


            //Output : 
            //3 9 8 6 7 2 0

            #endregion

            #region Question 5 : Get the elements of the array starting from the first element less than its position.

            int[] Numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //Fluent Syntax

            var Result = Numbers.TakeWhile((Num, I) => Num > I);


            //Hybrid Syntax

            var Result = (from N in Numbers
                          select N).TakeWhile((Num, I) => Num > I);


            foreach (var item in Result)
                Console.Write($"{item} ");

            //Output:

            //5 4
            #endregion
            #endregion

            #region LINQ - Quantifiers Operators

            #region Question 1 : Determine if any of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First) contain the substring 'ei'.

            //Read dictionary_english.txt into Array of String First

            string FilePath = "dictionary_english.txt";
            string[] EnglishDictionary = File.ReadAllLines(FilePath);

            ////Fluent Syntax
            
            var Result = EnglishDictionary.Any(W => W.Contains("ei"));

            //Quantifiers Operators only works with Fluent Syntax , but it can be used with Query Syntax Throught Fluent Syntax [Hybrid Syntax]
            //Quantifiers Operators can be used as Query Syntax when it applied to filtering, projecting
            // Hybrid Syntax = Query Syntax + Fluent Syntax
            // Hybrid Syntax = (Query Syntax).Fluent Syntax

            //Hybrid syntax

            var Result = (from E in EnglishDictionary
                          select E)
                         .Any(W => W.Contains("ei"));

            if (Result)
            {
                Console.WriteLine("At least one word contains 'ei'");
            }
            else
            {
                Console.WriteLine("No words contain 'ei'");
            }


            //Output : 
            // At least one word contains 'ei'

            #endregion

            #region Question 2 : Return a grouped a list of products only for categories that have at least one product that is out of stock.


            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                     .Where(Category => Category.Any(P => P.UnitsInStock == 0))
                                     .Select(Category => new
                                     {
                                         CategoryName = Category.Key,
                                         Products = Category.Where(P => P.UnitsInStock == 0)
                                     })
                                     .SelectMany(Category => Category.Products, (Category, Products) => new
                                     {
                                         Category.CategoryName,
                                         Products.ProductName,
                                     });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         where Category.Any(P => P.UnitsInStock == 0)
                         from P in Category
                         where P.UnitsInStock == 0
                         select new
                         {
                             CategoryName = Category.Key,
                             P.ProductName
                         };


            foreach (var item in Result)
                Console.WriteLine(item);


            //Output :

            //{CategoryName = Condiments, ProductName = Chef Anton's Gumbo Mix }
            //{ CategoryName = Meat / Poultry, ProductName = Alice Mutton }
            //{ CategoryName = Meat / Poultry, ProductName = Thüringer Rostbratwurst }
            //{ CategoryName = Meat / Poultry, ProductName = Perth Pasties }
            //{ CategoryName = Dairy Products, ProductName = Gorgonzola Telino }
            #endregion


            #region Question 3 :  Return a grouped a list of products only for categories that have all of their products in stock.

            //Fluent Syntax

            var Result = productsList.GroupBy(P => P.Category)
                                     .Where(Category => Category.All(P => P.UnitsInStock > 0))
                                     .Select(Category => new
                                     {
                                         CategoryName = Category.Key,
                                         Products = Category.Where(P => P.UnitsInStock > 0)
                                     })
                                     .SelectMany(Category => Category.Products, (Category, Products) => new
                                     {
                                         Category.CategoryName,
                                         Products.ProductName,
                                     });

            //Query Syntax

            var Result = from P in productsList
                         group P by P.Category
                         into Category
                         where Category.All(P => P.UnitsInStock > 0)
                         from P in Category
                         where P.UnitsInStock > 0
                         select new
                         {
                             CategoryName = Category.Key,
                             P.ProductName
                         };


            foreach (var item in Result)
                Console.WriteLine(item);

            #endregion
            #endregion

            #region LINQ – Grouping Operators

            #region Question 1 : Use group by to partition a list of numbers by their remainder when divided by 5

            List<int> Numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            //Fluent Syntax

            var Result = Numbers.GroupBy(Num => Num % 5);


            //Query Syntax

            var Result = from N in Numbers
                         group N by N % 5;

            foreach (var numbers in Result)
            {
                Console.WriteLine($"Numbers with a reminder {numbers.Key} when divdided by 5 ");
                foreach (var item in numbers)
                    Console.WriteLine(item);
            }

            //Output : 

            //Numbers with a reminder 0 when divdided by 5
            //0
            //5
            //10
            //15
            //Numbers with a reminder 1 when divdided by 5
            //1
            //6
            //11
            //Numbers with a reminder 2 when divdided by 5
            //2
            //7
            //12
            //Numbers with a reminder 3 when divdided by 5
            //3
            //8
            //13
            //Numbers with a reminder 4 when divdided by 5
            //4
            //9
            //14
            #endregion

            #region Question 2 : Uses group by to partition a list of words by their first letter.
            //Use dictionary_english.txt for Input

            //Use dictionary_english.txt for Input

            string FilePath = "dictionary_english.txt";
            string[] EnglishDictionary = File.ReadAllLines(FilePath);

            //Fluent Syntax

            var Resullt = EnglishDictionary.GroupBy(W => W[0]);


            //Query Syntax

            var Result = from E in EnglishDictionary
                         group E by E[0];

            foreach (var item in Result)
                Console.Write($"{item.Key} ");

            //Output :
            //w d a b c e f g h i y j k l m n o p q r s t u v x z
            #endregion

            #region Question 3 : Consider this Array as an Input String [] Arr = {"from", "salt", "earn", " last", "near", "form"}; Use Group By with a custom comparer that matches words that are consists of the same Characters Together

            string[] Arr = { "from", "salt", "earn", "last", "near", "form" };

            //Fluent Syntax

            var Result = Arr.GroupBy(W => W, new StringEqualityComparer<string>());


            //Query Syntax doesn't Support IEqualityComparer but we can implement in  another way

            //Query Syntax

            var Result = from A in Arr
                         group A by new string(A.OrderBy(c => c).ToArray()) into Word
                         select Word;


            bool Grouped = true;

            foreach (var Word in Result)
            {
                if(!Grouped)
                {
                    Console.WriteLine("....");
                }
                foreach (var item in Word)
                {
                    Console.WriteLine(item);
                }
                Grouped = false;
            }

            //Output : 
            //from
            //form
            //....
            //salt
            //last
            //....
            //earn
            //near
            #endregion
            #endregion

        }
    }
}
