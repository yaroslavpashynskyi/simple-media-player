using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MediaPlayer.Controllers;
using Microsoft.Win32;

namespace SimpleMediaPlayer
{
    public partial class MainWindow : Window
    {
        private MediaPlayerController _mediaPlayerController;
        private SliderController _sliderController;
        private UIController _uiController;

        public MainWindow()
        {
            InitializeComponent();

            _sliderController = new SliderController(progressSlider, volumeSlider);
            _uiController = new UIController(volumeText);
            _mediaPlayerController = new MediaPlayerController(mediaElement, _sliderController);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files|*.mp4;*.mp3;*.avi;*.mkv;*.wmv;*.wav|All files|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                _mediaPlayerController.LoadMedia(openFileDialog.FileName);
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (Math.Abs(progressSlider.Value - progressSlider.Maximum) < 0.01)
            {
                Stop_Click(sender, e);
            }

            _mediaPlayerController.PlayMedia();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerController.PauseMedia();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerController.StopMedia();
        }

        private void SlowSpeed_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerController.SetSpeed(0.5);
        }

        private void NormalSpeed_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerController.SetSpeed(1.0);
        }

        private void FastSpeed_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerController.SetSpeed(2.0);
        }

        private void Slider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _mediaPlayerController.SetMediaPosition(_sliderController.GetProgressSliderValue());
        }

        private void Slider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;
            if (slider != null)
            {
                var point = e.GetPosition(slider);
                double newValue = slider.Minimum + (point.X / slider.ActualWidth) * (slider.Maximum - slider.Minimum);
                slider.Value = newValue;
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_sliderController != null)
            {
                mediaElement.Volume = _sliderController.GetVolumeSliderValue();
                _uiController.UpdateVolumeText(_sliderController.GetVolumeSliderValue());
            }
        }
    }
}