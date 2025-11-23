using System;
using System.Collections.Generic;

// абстракт
abstract class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public void Register() => Console.WriteLine($"{Name} зарегистрирован");
    public void Login() => Console.WriteLine($"{Name} вошёл в систему");
    public void UpdateData() => Console.WriteLine($"{Name} обновил данные");
}

class Client : User
{
    public List<Order> Orders { get; set; } = new();
    public int LoyaltyPoints { get; set; }
}

class Admin : User
{
    public void Log(string action) => Console.WriteLine($"ADMIN LOG: {action}");
}

class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Category Category { get; set; }

    public void Create() => Console.WriteLine($"Товар {Name} создан");
    public void Update() => Console.WriteLine($"Товар {Name} обновлён");
    public void Delete() => Console.WriteLine($"Товар {Name} удалён");
}


class PromoCode
{
    public string Code { get; set; }
    public decimal DiscountPercent { get; set; }
}


class Cart
{
    public List<Product> Items { get; set; } = new();
    public PromoCode Promo { get; set; }

    public decimal GetTotal()
    {
        decimal sum = 0;
        foreach (var p in Items) sum += p.Price;
        if (Promo != null) sum -= sum * (Promo.DiscountPercent / 100);
        return sum;
    }
}


class Order
{
    public int Id { get; set; }
    public Client Client { get; set; }
    public List<Product> Items { get; set; } = new();
    public string Status { get; set; }
    public decimal Total { get; set; }

    public void Place()
    {
        Status = "Оформлен";
        Console.WriteLine($"Заказ {Id} оформлен. Сумма: {Total}");
    }

    public void Cancel()
    {
        Status = "Отменён";
        Console.WriteLine($"Заказ {Id} отменён");
    }
}


class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }

    public void Process()
    {
        Status = "Оплачен";
        Console.WriteLine($"Оплата успешна: {Amount}");
    }

    public void Refund()
    {
        Status = "Возврат выполнен";
        Console.WriteLine("Деньги возвращены");
    }
}


class Delivery
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Status { get; set; }

    public void Send()
    {
        Status = "В доставке";
        Console.WriteLine("Заказ отправлен");
    }

    public void Complete()
    {
        Status = "Доставлено";
        Console.WriteLine("Заказ доставлен");
    }
}


class Review
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public Product Product { get; set; }
}


// мэйн
class Program
{
    static void Main()
    {
        
        var client = new Client { Id = 1, Name = "Джорш", Email = "djorzh@mail.com", Role = "Client" };

        
        var phones = new Category { Id = 1, Name = "Телефоны" };

        
        var p1 = new Product { Id = 1, Name = "iPhone", Price = 450000, Stock = 5, Category = phones };
        var p2 = new Product { Id = 2, Name = "Чехол", Price = 5000, Stock = 20, Category = phones };

        
        var cart = new Cart();
        cart.Items.Add(p1);
        cart.Items.Add(p2);
        cart.Promo = new PromoCode { Code = "SALE10", DiscountPercent = 10 };

        decimal finalPrice = cart.GetTotal();

        // заказ
        var order = new Order
        {
            Id = 101,
            Client = client,
            Items = cart.Items,
            Total = finalPrice
        };
        order.Place();

        
        var payment = new Payment { Id = 1, Amount = order.Total };
        payment.Process();

       
        var delivery = new Delivery { Id = 500, Address = "Алматы, Абая 10" };
        delivery.Send();
        delivery.Complete();

        
        var review = new Review { Rating = 5, Comment = "Отличный товар", Product = p1 };
        Console.WriteLine($"Отзыв: {review.Rating}★ - {review.Comment}");
    }
}



// Вывод

// фасад упрощает работу делая незаметным использование нескольких систем

// логика бронирования

// снижает связность между частями 