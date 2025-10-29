using Microsoft.Maui.Controls;
using SimonApp1.Helpers;
using System.Collections.Generic;

namespace SimonApp1.Views
{
    public partial class MainPage : ContentPage
    {
        private List<Button> colorButtons;
        private List<int> sequence = new();
        private List<int> userInput = new();
        private bool isUserTurn = false;
        private Random random = new();

        public MainPage()
        {
            InitializeComponent();

            colorButtons = new List<Button> { GreenButton, RedButton, YellowButton, BlueButton };
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            sequence.Clear();
            userInput.Clear();
            isUserTurn = false;

            StatusLabel.Text = "Смотри внимательно!";
            await DisplaySequence();
        }

        private async Task DisplaySequence()
        {
            // Добавляем новый шаг
            sequence.Add(random.Next(0, colorButtons.Count));

            foreach (var index in sequence)
            {
                var btn = colorButtons[index];

                var originalColor = btn.BackgroundColor;
                btn.BackgroundColor = Colors.White;

                await AudioHelper.PlaySoundAsync("good.wav");

                await Task.Delay(400);

                btn.BackgroundColor = originalColor;
                await Task.Delay(200);
            }

            isUserTurn = true;
            userInput.Clear();
            StatusLabel.Text = "Теперь твоя очередь!";
        }

        private async void OnColorClicked(object sender, EventArgs e)
        {
            if (!isUserTurn) return;

            var clickedButton = (Button)sender;
            int index = colorButtons.IndexOf(clickedButton);

            // Проигрываем звук и подсветку
            var originalColor = clickedButton.BackgroundColor;
            clickedButton.BackgroundColor = Colors.White;
            await AudioHelper.PlaySoundAsync("bad.wav");
            await Task.Delay(200);
            clickedButton.BackgroundColor = originalColor;

            userInput.Add(index);

            // Проверяем ввод игрока
            if (userInput[userInput.Count - 1] != sequence[userInput.Count - 1])
            {
                isUserTurn = false;
                StatusLabel.Text = $"Ошибка! Очки: {sequence.Count - 1}";
                await AudioHelper.PlaySoundAsync("bad.wav");
                return;
            }

            // Если игрок ввёл всё правильно
            if (userInput.Count == sequence.Count)
            {
                isUserTurn = false;
                StatusLabel.Text = "Правильно! Следующий раунд...";
                await Task.Delay(800);
                await DisplaySequence();
            }
        }
    }
}

