using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class MySqlRepository
    {

        public List<Product> GetProducts()
        {


            List<Product> products = new List<Product>();
            string connectionString = "server=localhost;port=3306;database=sia2finalsmysql;username=root;charset=utf8;";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT 
                p.ProductId, 
                p.ProductName, 
                p.SupplierId, 
                s.SupplierName,  
                p.CategoryId, 
                c.CategoryName,  
                p.QuantityPerUnit, 
                p.UnitPrice, 
                p.UnitsInStock, 
                p.UnitsInOrder, 
                p.ReorderLevel, 
                p.Discontinued
            FROM 
                Products p
            JOIN 
                Suppliers s ON p.SupplierId = s.SupplierId
            JOIN 
                Categories c ON p.CategoryId = c.CategoryId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.ProductId = reader.GetInt32("ProductId");
                            product.ProductName = reader.GetString("ProductName");
                            product.SupplierId = reader.GetInt32("SupplierId");
                            product.SupplierName = reader.GetString("SupplierName");
                            product.CategoryId = reader.GetInt32("CategoryId");
                            product.CategoryName = reader.GetString("CategoryName");
                            product.QuantityPerUnit = reader.GetInt32("QuantityPerUnit");
                            product.UnitPrice = reader.GetInt32("UnitPrice");
                            product.UnitsInStock = reader.GetInt32("UnitsInStock");
                            product.UnitsInOrder = reader.GetInt32("UnitsInOrder");
                            product.ReorderLevel = reader.GetInt32("ReorderLevel");
                            product.Discontinued = reader.GetBoolean("Discontinued");

                            products.Add(product);
                        }
                    }
                }
            }

            return products;


        }


        public void AddProduct(Product product)
        {
            string connectionString = "server=localhost;port=3306;database=sia2finalsmysql;username=root;password='';charset=utf8;";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Resolve SupplierId and CategoryId based on SupplierName and CategoryName
                int supplierId = GetSupplierIdByName(product.SupplierName, connection);
                int categoryId = GetCategoryIdByName(product.CategoryName, connection);

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO products(ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsInOrder, ReorderLevel, Discontinued) " +
                    $"VALUES('{product.ProductName}', '{supplierId}', '{categoryId}', '{product.QuantityPerUnit}', '{product.UnitPrice}', '{product.UnitsInStock}', " +
                    $"'{product.UnitsInOrder}', '{product.ReorderLevel}', '{product.Discontinued}')", connection);

                cmd.ExecuteNonQuery();
            }




            //List<Product> products = new List<Product>();
            //string connectionstring = "server=localhost;port=3306;database=its406;username=root;password='';";
            //MySqlConnection connection = new MySqlConnection(connectionstring);
            //connection.Open();
            //MySqlCommand cmd = new MySqlCommand("insert into products(ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsInOrder, ReorderLevel, Discontinued) values('" + product.ProductName
            //    + "','" + product.SupplierId
            //    + "','" + product.CategoryId
            //    + "','" + product.QuantityPerUnit
            //    + "','" + product.UnitPrice
            //    + "','" + product.UnitsInStock
            //    + "','" + product.UnitsInOrder
            //    + "','" + product.ReorderLevel
            //    + "','" + product.Discontinued + "')", connection);
            //var reader = cmd.ExecuteScalar();



        }

        public Product GetProductById(int productId)
        {
            Product product = null;
            string connectionString = "server=localhost;port=3306;database=sia2finalsmysql;username=root;password='';charset=utf8;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                SELECT 
                    p.ProductId, 
                    p.ProductName, 
                    p.SupplierId, 
                    s.SupplierName,  
                    p.CategoryId, 
                    c.CategoryName,  
                    p.QuantityPerUnit, 
                    p.UnitPrice, 
                    p.UnitsInStock, 
                    p.UnitsInOrder, 
                    p.ReorderLevel, 
                    p.Discontinued
                FROM 
                    Products p
                JOIN 
                    Suppliers s ON p.SupplierId = s.SupplierId
                JOIN 
                    Categories c ON p.CategoryId = c.CategoryId
                WHERE
                    p.ProductId = @ProductId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product();
                            product.ProductId = reader.GetInt32("ProductId");
                            product.ProductName = reader.GetString("ProductName");
                            product.SupplierId = reader.GetInt32("SupplierId");
                            product.SupplierName = reader.GetString("SupplierName");
                            product.CategoryId = reader.GetInt32("CategoryId");
                            product.CategoryName = reader.GetString("CategoryName");
                            product.QuantityPerUnit = reader.GetInt32("QuantityPerUnit");
                            product.UnitPrice = reader.GetInt32("UnitPrice");
                            product.UnitsInStock = reader.GetInt32("UnitsInStock");
                            product.UnitsInOrder = reader.GetInt32("UnitsInOrder");
                            product.ReorderLevel = reader.GetInt32("ReorderLevel");
                            product.Discontinued = reader.GetBoolean("Discontinued");
                        }
                    }
                }
            }

            return product;
        }


        public void UpdateProduct(Product product)
        {
            string connectionString = "server=localhost;port=3306;database=sia2finalsmysql;username=root;password='';charset=utf8;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Resolve SupplierId and CategoryId based on SupplierName and CategoryName
                int supplierId = GetSupplierIdByName(product.SupplierName, connection);
                int categoryId = GetCategoryIdByName(product.CategoryName, connection);

                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE products SET " +
                    "ProductName = @ProductName, " +
                    "SupplierId = @SupplierId, " +
                    "CategoryId = @CategoryId, " +
                    "QuantityPerUnit = @QuantityPerUnit, " +
                    "UnitPrice = @UnitPrice, " +
                    "UnitsInStock = @UnitsInStock, " +
                    "UnitsInOrder = @UnitsInOrder, " +
                    "ReorderLevel = @ReorderLevel, " +
                    "Discontinued = @Discontinued " +
                    "WHERE ProductId = @ProductId", connection);

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
                cmd.Parameters.AddWithValue("@UnitsInOrder", product.UnitsInOrder);
                cmd.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
                cmd.Parameters.AddWithValue("@Discontinued", product.Discontinued);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            string connectionString = "server=localhost;port=3306;database=sia2finalsmysql;username=root;password='';charset=utf8;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM products WHERE ProductId = @ProductId", connection);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                cmd.ExecuteNonQuery();
            }
        }


        private int GetSupplierIdByName(string supplierName, MySqlConnection connection)
        {
            int supplierId = 0; // Default value if not found

            using (MySqlCommand cmd = new MySqlCommand($"SELECT SupplierId FROM suppliers WHERE SupplierName = '{supplierName}'", connection))
            {
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    supplierId = Convert.ToInt32(result);
                }
            }

            return supplierId;
        }



        private int GetCategoryIdByName(string categoryName, MySqlConnection connection)
        {
            int categoryId = 0; // Default value if not found

            using (MySqlCommand cmd = new MySqlCommand($"SELECT CategoryId FROM categories WHERE CategoryName = '{categoryName}'", connection))
            {
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    categoryId = Convert.ToInt32(result);
                }
            }

            return categoryId;
        }
    }
}