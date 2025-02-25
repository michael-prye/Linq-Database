﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count

            var test = _context.Users.ToList().Count;
            Console.WriteLine(test);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.


            var products = _context.Products.Where(p => p.Price > 150);
            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.Name} Price: ${product.Price}");
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

            var products = _context.Products.Where(p => p.Name.Contains("s"));
            foreach(var product in products)
            {
                Console.WriteLine($"Name: {product.Name}");
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var users = _context.Users.Where(u => u.RegistrationDate.Value.Year < 2016);
            foreach(var user in users)
            {
                Console.WriteLine($"Email: {user.Email} Date: {user.RegistrationDate}");
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.

            var users = _context.Users.Where(u => u.RegistrationDate.Value.Year == 2017);
            foreach (var user in users)
            {
                Console.WriteLine($"Email: {user.Email} Date: {user.RegistrationDate}");
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var aftonsCart = _context.ShoppingCarts.Include(sh => sh.Product).Include(sh => sh.User).Where(sh => sh.User.Email == "afton@gmail.com");
            foreach (var product in aftonsCart)
            {
                Console.WriteLine($"Name: {product.Product.Name}\tPrice: {product.Product.Price}\tQuantity: {product.Quantity}");
            }

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var odasCartSum = _context.ShoppingCarts.Include(sh => sh.User).Include(sh => sh.Product).Where(sh => sh.User.Email == "oda@gmail.com").Select(sh => sh.Product.Price).Sum();
            Console.WriteLine($"Sum of products in Oda's Shopping Cart: ${odasCartSum}");

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var employeeEmails = _context.UserRoles.Include(ur => ur.User).Include(ur => ur.Role).Where(ur => ur.Role.RoleName == "employee").Select(ur => ur.User.Email).ToList();
            var employeeShoppingCarts = _context.ShoppingCarts.Include(sh => sh.User).Include(sh => sh.Product).Where(sh => employeeEmails.Contains(sh.User.Email));

            foreach (var employeeShoppingCartItem in employeeShoppingCarts)
            {
                Console.WriteLine($"Email: {employeeShoppingCartItem.User.Email}\nProduct: {employeeShoppingCartItem.Product.Name}\nPrice: {employeeShoppingCartItem.Product.Price}\nQuantity: {employeeShoppingCartItem.Quantity}\n\n");
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product product = new Product()
            {
                Name = "Barack Obama NFT Collection",
                Description = "Barack Obama in non-fungible form babyyyyyy",
                Price = 100000000000.00m
            };
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.

            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Name == "Barack Obama NFT Collection").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 10
            };

            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Barack Obama NFT Collection").SingleOrDefault();
            product.Price = 5000;
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(u => u.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var odaUser = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(odaUser);
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private User BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".

            Console.Write("Please enter your email: ");
            string emailInput = Console.ReadLine();
            Console.Write("Please enter your password: ");
            string passwordInput = Console.ReadLine();

            var validUser = _context.Users.Where(u => u.Email == emailInput && u.Password == passwordInput).SingleOrDefault();

            if (validUser == null)
            {
                Console.WriteLine("\nInvalid Email or Password\n");
                return null;
            } else
            {
                Console.WriteLine("\nSigned In!\n");
                return validUser;
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.

            var users = _context.Users.ToList();

            decimal totalSum = 0m;
            foreach (var user in users)
            {
                var shoppingCartSum = _context.ShoppingCarts.Include(sc => sc.Product).Where(sc=>sc.UserId == user.Id).Select(sc => sc.Product.Price).Sum();
                totalSum += shoppingCartSum;
                Console.WriteLine($"User: {user.Email}\t\t\tShopping Cart Total: ${shoppingCartSum}");
            }
            Console.WriteLine($"\nTotal Sum of Shopping Carts: ${totalSum}");

        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sign in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

            Console.WriteLine("Welcome to the Console Store. Please sign in...");
            User user = null;
            while (user == null)
            {
                user = BonusOne();
            }
            Console.WriteLine($"Welcome {user.Email}");

            bool signedOut = false;
            while (!signedOut)
            {
                Console.WriteLine("Please select from the following options:" +
                    "\n\n\t(1) View shopping cart" +
                    "\n\t(2) View products" +
                    "\n\t(3) Add Product To Cart" +
                    "\n\t(4) Remove Product From Cart" +
                    "\n\t(5) Sign Out\n\n");
                string optionChoice = Console.ReadLine();
                switch (optionChoice)
                {
                    case "1":
                        Console.WriteLine($"\n\n{user.Email}'s Shopping Cart");
                        var userCart = _context.ShoppingCarts.Include(sh => sh.Product).Where(sh => sh.UserId == user.Id);
                        foreach (var product in userCart)
                        {
                            Console.WriteLine($"Name: {product.Product.Name}\nPrice: {product.Product.Price}\nQuantity: {product.Quantity}\n\n");
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("PRODUCTS");
                        var products = _context.Products;
                        foreach (var product in products)
                        {
                            Console.WriteLine($"Name: {product.Name}\nPrice: ${product.Price}\n\n");
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("Please enter the name of the product you'd like to add to your cart.");
                        string inputProductName = Console.ReadLine();

                        var chosenProduct = _context.Products.Where(p => p.Name.ToLower() == inputProductName.ToLower()).SingleOrDefault();
                        if (chosenProduct != null)
                        {

                            var shoppingCartItem = _context.ShoppingCarts.Include(sh => sh.Product).Where(p => p.Product.Name.ToLower() == inputProductName.ToLower()).SingleOrDefault();
                            if (shoppingCartItem != null)
                            {
                                shoppingCartItem.Quantity += 1;
                                _context.ShoppingCarts.Update(shoppingCartItem);
                                _context.SaveChanges();
                            }
                            else
                            {
                                ShoppingCart newCartItem = new ShoppingCart()
                                {
                                    UserId = user.Id,
                                    ProductId = chosenProduct.Id,
                                    Quantity = 1
                                };
                                _context.ShoppingCarts.Add(newCartItem);
                                _context.SaveChanges();
                            }

                           
                        } else
                        {
                            Console.WriteLine($"We dont sell a {inputProductName} here. Better try Amazon...\n");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Please enter the name of the product you would like to delete");
                        string inputProduct = Console.ReadLine();
                        var deleteShoppingCartItem = _context.ShoppingCarts.Include(sh => sh.Product).Where(p => p.Product.Name.ToLower() == inputProduct.ToLower()).SingleOrDefault();
                        if (deleteShoppingCartItem != null)
                        {
                            _context.ShoppingCarts.Remove(deleteShoppingCartItem);
                            _context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine($"That item is not in your shopping cart...\n");
                        }


                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        signedOut = true;
                        break;
                    default:
                        Console.WriteLine("That's not a valid choice idiot");
                        break;
                }
            }



        }

    }
}
