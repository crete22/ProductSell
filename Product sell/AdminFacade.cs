using System;
using System.Linq;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;


namespace Product_sell
{
    public class AdminFacade : IAdminFacade
    {
        private readonly DatabaseFacade _dbFacade = new DatabaseFacade();

 
        public Admin Login(string login, string password)
        {
            // Логика аналогична входу покупателя, но проверяется таблица Admins.
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var admin = _dbFacade.GetAllEntities<Admin>()
                .FirstOrDefault(a => a.Login.Equals(login));

            if (admin == null || admin.Password != password)
            {
                return null;
            }

            return admin;
        }


        public bool AddProduct(string name, decimal price, int quality, string description)
        {
            // Валидация данных, как вы и хотели.
            // Проверяем, что название не пустое, а цена и количество - положительные.
            if (string.IsNullOrWhiteSpace(name) || price <= 0 || quality < 0)
            {
                return false;
            }

            var product = new Product
            {
                Name = name,
                Price = price,
                Quality = quality,
                Description = description
            };

            try
            {
                _dbFacade.AddEntity(product);
                return true;
            }
            catch (Exception)
            {
                // Если при сохранении возникла ошибка
                return false;
            }
        }

       
        public bool UpdateProduct(Product productToUpdate)
        {
            // Такая же валидация, как и при добавлении.
            if (productToUpdate == null || string.IsNullOrWhiteSpace(productToUpdate.Name) || productToUpdate.Price <= 0 || productToUpdate.Quality < 0)
            {
                return false;
            }

            try
            {
                _dbFacade.UpdateEntity(productToUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        public bool DeleteProduct(int productId)
        {
            var product = _dbFacade.GetEntityById<Product>(productId);
            if (product == null)
            {
                return false;
            }

            try
            {
                _dbFacade.DeleteEntity(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
      
        public bool ToggleUserBlock(int customerId, bool shouldBeBlocked)
        {
            // Находим покупателя в базе данных по его ID.
            var customer = _dbFacade.GetEntityById<Customer>(customerId);

            if (customer == null)
            {
                // Если пользователь с таким ID не найден, ничего не делаем.
                return false;
            }

            // Устанавливаем новое значение для поля IsBlocked.
            customer.IsBlocked = shouldBeBlocked;

            try
            {
                // Обновляем данные пользователя в базе.
                _dbFacade.UpdateEntity(customer);
                return true;
            }
            catch (Exception)
            {
                // В случае ошибки при сохранении.
                return false;
            }
        }
        public bool IssueOrder(int orderId, string adminEmail)
        {
            // 1. Найти заказ
            var order = _dbFacade.GetEntityById<Order>(orderId);
            if (order == null)
                return false;

            // 2. Создать уведомление
            var notification = new Notification
            {
                OrderId = order.Id,
                Type = "Выдача заказа",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                AdminId = 1 // если у вас один админ, можно захардкодить или передавать как параметр
            };
            _dbFacade.AddEntity(notification);

            //email
            //try
            //{
            //    var message = new MimeMessage();
            //    message.From.Add(new MailboxAddress("Product Sell App", "fransus36@gmail.com")); //email
            //    message.To.Add(new MailboxAddress("Администратор", adminEmail));
            //    message.Subject = "Выдача заказа";
             
                
            //    message.Body = new TextPart("plain")
            //    {
            //        Text = $"Заказ №{order.OrderNumber} был выдан покупателю с айд {order.CustomerId}."
            //    };

            //    using (var client = new SmtpClient())
            //    {
            //        // Подключение к SMTP серверу (пример для Gmail)
            //        client.Connect("smtp.gmail.com", 587, false);
            //        client.Authenticate("fransus36@gmail.com", "your_email_password"); // Вставить данные
            //        client.Send(message);
            //        client.Disconnect(true);
            //    }
            //    return true;
            //}
            //catch (Exception)
            //{
            //    // Если не удалось отправить письмо, но уведомление в базе уже создано
            //    return false;
            //}
            return true;
        }
        // Получает список всех заказов в системе.
        public List<Order> GetAllOrders()
        {
            return _dbFacade.GetAllEntities<Order>();
        }

        // Получает список всех активных уведомлений.
        public List<Notification> GetActiveNotifications()
        {
            var allNotifications = _dbFacade.GetAllEntities<Notification>();
            // Фильтруем, чтобы вернуть только те, у которых IsActive = true
            return allNotifications.Where(n => n.IsActive).ToList();
        }

        // Получает список всех зарегистрированных покупателей
        public List<Customer> GetAllCustomers()
        {
            return _dbFacade.GetAllEntities<Customer>();
        }
        public List<Product> GetAllProducts()
        {
            return _dbFacade.GetAllEntities<Product>();
        }
        public List<ProductInOrder> GetProductsInOrderByOrderId(int id)
        {
            return _dbFacade.GetAllEntities<ProductInOrder>().Where(p => p.OrderId ==  id).ToList();
        }
    }

}