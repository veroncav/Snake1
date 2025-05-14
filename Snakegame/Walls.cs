using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ===== Класс: Walls =====
// Стены по границе карты
public class Walls
{
    private List<Point> wallPoints;

    public Walls(int width, int height)
    {
        wallPoints = new List<Point>();

        for (int x = 0; x < width; x++)
        {
            wallPoints.Add(new Point(x, 0, '#'));
            wallPoints.Add(new Point(x, height - 1, '#'));
        }

        for (int y = 0; y < height; y++)
        {
            wallPoints.Add(new Point(0, y, '#'));
            wallPoints.Add(new Point(width - 1, y, '#'));
        }
    }

    public void Draw()
    {
        foreach (var p in wallPoints)
        {
            p.Draw();
        }
    }

    public bool IsHit(Snake snake)
    {
        foreach (var wall in wallPoints)
        {
            foreach (var part in snake.GetBody())
            {
                if (wall.IsHit(part))
                    return true;
            }
        }
        return false;
    }
}
