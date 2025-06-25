using System;
using System.Linq;

namespace Product_sell
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly DatabaseFacade _dbFacade = new DatabaseFacade();
        public bool Register(string login, string password, string surname, string name, string patronymic, string phoneNumber, string mail)
        {
            //проверка входных данных
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                // Нельзя зарегистрироваться с пустым логином или паролем.
                return false;
            }

            //Проверка, не занят ли логин 

            // Получаем всех пользователей из БД.
            var allCustomers = _dbFacade.GetAllEntities<Customer>();

            // Проверка на совпадение логинов
            if (allCustomers.Any(c => c.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
            {
                //Если пользователь с таким логином уже есть, регистрация невозможна.
                return false;
            }

            //Создание и сохранение нового покупателя
            try
            {
                var newCustomer = new Customer
                {
                    Login = login,
                    Password = password,
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    PhoneNumber = phoneNumber,
                    Mail = mail,
                    IsBlocked = false
                };

                _dbFacade.AddEntity(newCustomer);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Customer Login(string login, string password)
        {
            //Валидация входных данных
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            //Поиск пользователя по логину
            var customer = _dbFacade.GetAllEntities<Customer>()
                .FirstOrDefault(c => c.Login.Equals(login, StringComparison.OrdinalIgnoreCase));

            // Проверяем, был ли найден пользователь и совпадает ли пароль
            if (customer == null || customer.Password != password)
            {
                return null;
            }

            // Проверка на блок
            if (customer.IsBlocked)
            {
                // Если заблокирован, то не сможешь войти
                return null;
            }
            // Если все проверки пройдены, возвращаем найденный объект покупателя
            return customer;
        }
       
        // Получает список всех доступных товаров для отображения в каталоге.
   
        public List<Product> GetAllProducts()
        {
            return _dbFacade.GetAllEntities<Product>();
        }


        // Создает новый заказ для покупателя, обновляет остатки на складе и создает уведомление для администратора.
        public Order CreateOrder(Customer customer, List<(Product product, int quantity)> productsInCart)
        {
            if (customer == null || productsInCart == null || !productsInCart.Any())
            {
                return null;
            }

            try
            {
                // Шаг 1: Создаем сам заказ
                var order = new Order
                {
                    CustomerId = customer.Id,
                    OrderNumber = GenerateOrderNumber(),
                    Date = DateTime.UtcNow,
                };
                _dbFacade.AddEntity(order); // Сразу сохраняем, чтобы получить его Id для связей

                // Шаг 2: Добавляем товары в заказ и уменьшаем остатки на складе
                foreach (var (product, quantity) in productsInCart)
                {
                    // Добавляем запись в связующую таблицу ProductInOrder
                    var productInOrder = new ProductInOrder
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quality = quantity,
                        Price = product.Price // Фиксируем цену на момент покупки
                    };
                    _dbFacade.AddEntity(productInOrder);

                    // Уменьшаем количество товара на складе
                    var productInDb = _dbFacade.GetEntityById<Product>(product.Id);
                    if (productInDb != null)
                    {
                        productInDb.Quality -= quantity;
                        _dbFacade.UpdateEntity(productInDb);
                    }
                }

                // Шаг 3: Создаем уведомление для администратора
                var admin = _dbFacade.GetAllEntities<Admin>().FirstOrDefault(a => a.Email == "fransus36@gmail.com");
                if (admin != null)
                {
                    var notification = new Notification
                    {
                        Type = "Новый заказ",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true,
                        OrderId = order.Id,
                        AdminId = admin.Id
                    };
                    _dbFacade.AddEntity(notification);
                }

                return order;
            }
            catch (Exception)
            {
                // В реальном приложении здесь было бы логирование ошибки
                return null;
            }
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            var allOrders = _dbFacade.GetAllEntities<Order>();
            return allOrders.Where(o => o.CustomerId == customerId).ToList();
        }
        private int GenerateOrderNumber()
        {
            var allOrders = _dbFacade.GetAllEntities<Order>();
            if (!allOrders.Any())
            {
                return 1001; // Начнем с красивого номера
            }
            return allOrders.Max(o => o.OrderNumber) + 1;
        }
        public string GetProductName(int productId)
        {
            return _dbFacade.GetEntityById<Product>(productId).Name;
        }
    }
}