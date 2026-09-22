using System;
using PR5_ClientServer.Services;

namespace PR5_ClientServer
{
    internal class Program
    {
        private static int _currentPage = 1;
        private static int _pageSize = 3;
        private static string _filter = "";
        private static string _sortBy = "id";
        private static bool _ascending = true;

        private static readonly ProductServerService _service = new();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 5.1 ДВУХЗВЕННОЕ ПРИЛОЖЕНИЕ (SQLite) ===");
                Console.WriteLine($"[Параметры] ПоискЖ '${_filter}' | Сортировка: {_sortBy} ({(_ascending ? "А-Я / 1-9" : "Я-А / 9-1")})");
                Console.WriteLine(new string('-', 60));

                var (items, totalPages) = _service.GetProducts(_filter, _sortBy, _ascending, _currentPage, _pageSize);

                Console.WriteLine($"{"ID", -4} | {"Название", -15} | {"Цена (руб)", -12} | {"Категория", -20}");
                Console.WriteLine(new string('-', 60));

                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id,-4} | {item.Name,-15} | {item.Price,-12} | {item.CategoryName,-20}");
                }

                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Страница {_currentPage} из {totalPages}");
                Console.WriteLine(new string('-', 60));

                Console.WriteLine("1. Перейти на страницу");
                Console.WriteLine("2. Фильтрация (по названию)");
                Console.WriteLine("3. Сортировка (id, name, price)");
                Console.WriteLine("4. Детальный просмотр");
                Console.WriteLine("5. Добавить новую запись");
                Console.WriteLine("6. Редактировать запись");
                Console.WriteLine("7. Удалить запись");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите номер страницы: ");
                        if (int.TryParse(Console.ReadLine(), out int page) && page >= 1 && page <= totalPages)
                            _currentPage = page;
                        break;

                    case "2":
                        Console.WriteLine("Введите текст поиска: ");
                        _filter = Console.ReadLine() ?? "";
                        _currentPage = 1;
                        break;

                    case "3":
                        Console.WriteLine("Введите категорию для сортировки: ");
                        _sortBy = Console.ReadLine() ?? "id";
                        Console.WriteLine("По возрастанию? (y/n): ");
                        _ascending = Console.ReadLine()?.ToLower() == "y";
                        break;

                    case "4":
                        Console.WriteLine("Введите ID товара: ");
                        if(int.TryParse(Console.ReadLine(), out int detailId))
                        {
                            var details = _service.GetProductById(detailId);
                            if(details != null)
                            {
                                Console.WriteLine("\n--- ДЕТАЛЬНЫЙ ПРОСМОТР ---");
                                Console.WriteLine($"ID товара: {details.Id}");
                                Console.WriteLine($"Название: {details.Name}");
                                Console.WriteLine($"Цена: {details.Price}");
                                Console.WriteLine($"ID категории: {details.CategoryId}");
                                Console.WriteLine($"Название категории: {details.CategoryName}");
                            }
                            else
                            {
                                Console.WriteLine("Элемент с таким ID не найден");
                            }
                            Console.WriteLine("Нажмите любую клавишу для продолжения...");
                            Console.ReadLine();
                        }
                        break;

                    case "5":
                        Console.WriteLine("Название товара: ");
                        string addName = Console.ReadLine() ?? "";
                        Console.WriteLine("Стоимость: ");
                        decimal.TryParse(Console.ReadLine(), out decimal addPrice);
                        Console.WriteLine("ID категории: ");
                        int.TryParse(Console.ReadLine(), out int addCatId);

                        _service.AddProduct(addName, addPrice, addCatId);
                        break;

                    case "6":
                        Console.WriteLine("Введите ID товара для редактирвания: ");
                        if(int.TryParse(Console.ReadLine(), out int editId))
                        {
                            Console.WriteLine("Новое название: ");
                            string editName = Console.ReadLine() ?? "";
                            Console.WriteLine("Новая стоимость: ");
                            decimal.TryParse(Console.ReadLine(), out decimal editPrice);
                            Console.WriteLine("Новый ID категории: ");
                            int.TryParse(Console.ReadLine(), out int editCatId);

                            if(!_service.UpdateProduct(editId, editName, editPrice, editCatId))
                            {
                                Console.WriteLine("Запись для обновления не найдена!");
                                Console.ReadLine();
                            }                   
                        }
                        break;

                    case "7":
                        Console.WriteLine("Введите ID товара для удаления: ");
                        if(int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            if (!_service.DeleteProduct(deleteId))
                            {
                                Console.WriteLine("Товар с таким ID не найден!");
                                Console.ReadLine();
                            }
                        }
                        break;

                    case "0":
                        return;
                }
            }
        }
    }
}