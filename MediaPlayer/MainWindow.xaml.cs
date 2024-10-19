using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            // Створюємо таймер для оновлення положення слайдера
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files|*.mp4;*.mp3;*.avi;*.mkv;*.wmv;*.wav|All files|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaElement.Source = new Uri(openFileDialog.FileName);
                mediaElement.Play();
                timer.Start(); // Запускаємо таймер для оновлення слайдера
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
           
            if (Math.Abs(progressSlider.Value - progressSlider.Maximum) < 0.01)
            {
                Stop_Click(sender, e);
            }
            timer.Start(); 
            mediaElement.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            timer.Stop(); // Зупиняємо таймер
            progressSlider.Value = 0;
        }

        private void SlowSpeed_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.SpeedRatio = 0.5; // Швидкість відтворення 0.5x
        }
        private void NormalSpeed_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.SpeedRatio = 1; // Швидкість відтворення 1x
        }

        private void FastSpeed_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.SpeedRatio = 2.0; // Швидкість відтворення 2x
        }

        // Оновлюємо слайдер під час відтворення
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan && !progressSlider.IsMouseCaptureWithin)
            {
                progressSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                progressSlider.Value = mediaElement.Position.TotalSeconds;
            }
        }

        // Перемотка при зміні слайдера
        private void Slider_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(progressSlider.Value);
        }
        
        private void Slider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;
            if (slider != null)
            {
                // Calculate the new value based on the mouse position
                var point = e.GetPosition(slider);
                double newValue = slider.Minimum + (point.X / slider.ActualWidth) * (slider.Maximum - slider.Minimum);
                slider.Value = newValue;
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = volumeSlider.Value;
            if (volumeText != null)
                volumeText.Text = $"{(int)(volumeSlider.Value * 100)}%"; // Оновлюємо ToolTip для гучності
        }
    }
}
