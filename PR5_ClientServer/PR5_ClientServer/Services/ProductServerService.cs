using Microsoft.Data.Sqlite;
using PR5_ClientServer.Models;

namespace PR5_ClientServer.Services
{
    public class ProductServerService
    {
        private const string ConnectionString = "Data Source=db.db";

        public (List<Product> Items, int TotalPages) GetProducts(
            string? filterByName,
            string sortBy,
            bool ascending,
            int pageNumber,
            int pageSize)
        {
            var items = new List<Product>();
            int totalItems = 0;

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var countCmd = connection.CreateCommand();
            countCmd.CommandText = "SELECT COUNT(*) FROM Product WHERE Name LIKE $filter";
            countCmd.Parameters.AddWithValue("$filter", $"{filterByName ?? ""}%");
            totalItems = Convert.ToInt32(countCmd.ExecuteScalar());

            string sortColumn = sortBy.ToLower() switch
            {
                "price" => "p.Price",
                "name" => "p.Name",
                _ => "p.Id"
            };
            string sortDirection = ascending ? "ASC" : "DESC";

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = $@"
                SELECT p.Id, p.Name, p.Price, p.CategoryId, c.Name AS CategoryName
                FROM Product p
                INNER JOIN Category c ON p.CategoryId = c.Id
                WHERE p.Name LIKE $filter
                ORDER BY {sortColumn} {sortDirection}
                LIMIT $limit OFFSET $offset
            ";
            selectCmd.Parameters.AddWithValue("$filter", $"%{filterByName ?? ""}%");
            selectCmd.Parameters.AddWithValue("$limit", pageSize);
            selectCmd.Parameters.AddWithValue("$offset", (pageNumber - 1) * pageSize);

            using var reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    CategoryId = reader.GetInt32(3),
                    CategoryName = reader.GetString(4)
                });
            }
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            return (items, totalPages > 0 ? totalPages : 1);
        }

        public Product? GetProductById(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                SELECT p.Id, p.Name, p.Price, p.CategoryId, c.Name AS CategoryName
                FROM Product p
                INNER JOIN Category c ON p.CategoryId = c.Id
                WHERE p.ID = $id
            ";
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    CategoryId = reader.GetInt32(3),
                    CategoryName = reader.GetString(4)
                };
            }
            return null;
        }

        public void AddProduct(string name, decimal price, int categoryId)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Product (Name, Price, CategoryId) VALUES ($name, $price, $catId)";
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$price", price);
            cmd.Parameters.AddWithValue("$catId", categoryId);
            cmd.ExecuteNonQuery();
        }

        public bool UpdateProduct(int id, string newName, decimal newPrice, int newCategoryId)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Product SET Name = $name, Price = $price, CategoryId = $catId WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.Parameters.AddWithValue("$name", newName);
            cmd.Parameters.AddWithValue("$price", newPrice);
            cmd.Parameters.AddWithValue("$catId", newCategoryId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteProduct(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Product WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

    }
}
