using System.IO;

try
{
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Kuud.txt"); //@"..\..\..\Kuud.txt"
    StreamWriter text = new StreamWriter(path, true); // true = добавляет в конец
    Console.WriteLine("Введите какой-нибудь текст: ");
    string lause = Console.ReadLine();
    text.WriteLine(lause);
    text.Close();
}
catch (Exception)
{
    Console.WriteLine("Какая-то ошибка с файлом");
}
StreamWriter sw = new StreamWriter(path);
sw.WriteLine("Что-то");
sw.Close(); 
