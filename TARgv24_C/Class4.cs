// Пример, демонстрирующий основы ООП на C#
using System;

// 1. Класс
public class Person
{
    public string Name;
    public int Age;

    public void Introduce()
    {
        Console.WriteLine($"Привет! Меня зовут {Name}, мне {Age} лет.");
    }
}

// 2. Наследование
public class Employee : Person
{
    public string Position;

    public void Work()
    {
        Console.WriteLine($"{Name} работает как {Position}.");
    }
}

// 3. Абстракция
public abstract class Animal
{
    public string Name;
    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Гав-гав!");
    }
}

// 4. Инкапсуляция
public class BankAccount
{
    private double balance;

    public double Balance
    {
        get { return balance; }
        set
        {
            if (value >= 0)
                balance = value;
        }
    }
}

// 5. Интерфейс
public interface ISoundMaker
{
    void MakeSound();
}

public class Cat : ISoundMaker
{
    public void MakeSound()
    {
        Console.WriteLine("Мяу!");
    }
}

// Главный класс
class Program
{
    static void Main()
    {
        Person p = new Person { Name = "Анна", Age = 25 };
        p.Introduce();

        Employee e = new Employee { Name = "Иван", Age = 30, Position = "Программист" };
        e.Introduce();
        e.Work();

        Dog d = new Dog { Name = "Шарик" };
        d.MakeSound();

        BankAccount acc = new BankAccount();
        acc.Balance = 100;
        Console.WriteLine($"Баланс: {acc.Balance}");

        Cat c = new Cat();
        c.MakeSound();
    }
}
