using Java.Sql;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SimonApp1.Database;
using SimonApp1.Models;
using SimonApp1.Services;

namespace SimonApp1.Views;

public partial class MainPage : ContentPage
{
    private readonly SettingsService settings;
    private readonly ThemeService themeService;
    private readonly AppDatabase db;

    private List<Button> colorButtons;
    private List<int> sequence = new();
    private List<int> userInput = new();
    private bool isUserTurn = false;
    private Random rnd = new();
    private int score = 0;

    // Цвета (можно привязывать в MVVM)
    public Color GreenColor => Color.FromArgb("#2ecc71");
    public Color RedColor => Color.FromArgb("#e74c3c");
    public Color YellowColor => Color.FromArgb("#f1c40f");
    public Color BlueColor => Color.FromArgb("#3498db");

    public MainPage(SettingsService settings, ThemeService themeService, AppDatabase db)
    {
        InitializeComponent();
        this.settings = settings;
        this.themeService = themeService;
        this.db = db;

        colorButtons = new List<Button> { GreenButton, RedButton, YellowButton, BlueButton };

        // bind entry
        NameEntry.Text = settings.PlayerName;

        // привяжем цвета через BindingContext (упрощённо)
        BindingContext = this;
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage(settings, themeService));
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        // сохранить имя
        settings.PlayerName = NameEntry.Text?.Trim() ?? "Player";

        sequence.Clear();
        userInput.Clear();
        score = 0;
        isUserTurn = false;
        StartButton.IsEnabled = false;
        await Task.Delay(300);

        await NextRoundAsync();
    }

    private async Task NextRoundAsync()
    {
        if (sequence.Count >= settings.MaxRounds)
        {
            // победа
            await ShowResultAsync(true);
            return;
        }

        // добавляем шаг
        sequence.Add(rnd.Next(0, colorButtons.Count));
        await PlaySequenceAsync();
        isUserTurn = true;
        userInput.Clear();
    }

    private async Task PlaySequenceAsync()
    {
        isUserTurn = false;
        foreach (var idx in sequence)
        {
            var btn = colorButtons[idx];
            await HighlightButtonAsync(btn);
            await Task.Delay(300);
        }
    }

    private async Task HighlightButtonAsync(Button btn)
    {
        var original = btn.Background;
        // подсветка: увеличим и временно сменим цвет на белый
        await btn.ScaleTo(1.08, 110);
        var prev = btn.BackgroundColor;
        btn.BackgroundColor = Colors.White;
        if (settings.SoundOn)
        {
            // проиграть звук, если у тебя есть AudioHelper
            try { await Helpers.AudioHelper.PlaySoundAsync("good.wav"); } catch { }
        }
        await Task.Delay(220);
        btn.BackgroundColor = prev;
        await btn.ScaleTo(1, 120);
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        if (!isUserTurn) return;

        var btn = (Button)sender;
        int idx = colorButtons.IndexOf(btn);

        // местная подсветка и звук
        await HighlightButtonAsync(btn);

        userInput.Add(idx);

        int i = userInput.Count - 1;
        if (userInput[i] != sequence[i])
        {
            // проигрыш
            await ShowResultAsync(false);
            return;
        }

        if (userInput.Count == sequence.Count)
        {
            // успешное повторение
            score = sequence.Count;
            isUserTurn = false;
            await Task.Delay(400);
            await NextRoundAsync();
        }
    }

    private async Task ShowResultAsync(bool won)
    {
        isUserTurn = false;
        Overlay.IsVisible = true;
        ResultTitle.Text = won ? "Победа!" : "Игра окончена";
        ResultText.Text = won ? $"Вы прошли все уровни! Очки: {score}" : $"Ошибка! Очки: {score}";
        StartButton.IsEnabled = true;
        // Pause sounds if needed
    }

    private async void OnRestartClicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        sequence.Clear();
        userInput.Clear();
        score = 0;
        await Task.Delay(200);
    }

    private async void OnSaveScoreClicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        // Сохранить рекорд в БД
        var rec = new ScoreRecord
        {
            PlayerName = settings.PlayerName,
            Score = score,
            Date = DateTime.Now
        };
        await db.SaveScoreAsync(rec);
        await DisplayAlert("Сохранено", "Рекорд сохранён", "OK");
    }
}

