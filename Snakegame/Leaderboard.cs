using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ===== Класс: Leaderboard =====
// Чтение и запись результатов игры
public class Leaderboard
{
    private static string filePath = "scores.txt";

    public static void SaveScore(string name, int score, TimeSpan time)
    {
        string line = $"{name}:{score}:{time.Minutes:D2}:{time.Seconds:D2}";
        File.AppendAllLines(filePath, new[] { line });
    }

    public static List<string> GetTopScores(int topN = 5)
    {
        if (!File.Exists(filePath)) return new List<string>();
        return File.ReadAllLines(filePath)
            .OrderByDescending(line =>
            {
                var parts = line.Split(':');
                return int.TryParse(parts[1], out int score) ? score : 0;
            })
            .Take(topN)
            .ToList();
    }
}
