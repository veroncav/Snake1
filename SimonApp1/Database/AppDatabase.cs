using SQLite;
using SimonApp1.Models;

namespace SimonApp1.Database
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public AppDatabase(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<ScoreRecord>().Wait();
        }

        public Task<List<ScoreRecord>> GetScoresAsync() => _db.Table<ScoreRecord>().ToListAsync();

        public Task<int> SaveScoreAsync(ScoreRecord score) => _db.InsertAsync(score);
    }
}
