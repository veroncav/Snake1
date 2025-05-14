using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ===== Класс: FoodCreator =====
// Создаёт еду в случайной позиции
public class FoodCreator
{
    private int width;
    private int height;
    private Random rand;

    public FoodCreator(int w, int h)
    {
        width = w;
        height = h;
        rand = new Random();
    }

    public Food CreateFood()
    {
        int x = rand.Next(2, width - 2);
        int y = rand.Next(2, height - 2);
        return new Food(x, y);
    }
}
