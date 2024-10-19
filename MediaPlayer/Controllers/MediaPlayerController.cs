using System.Windows.Controls;
using System.Windows.Threading;

namespace MediaPlayer.Controllers;

public class MediaPlayerController
{
    private MediaElement _mediaElement;
    private DispatcherTimer _timer;
    private SliderController _sliderController;

    public MediaPlayerController(MediaElement mediaElement, SliderController sliderController)
    {
        _mediaElement = mediaElement;
        _sliderController = sliderController;

        // Таймер для оновлення положення слайдера
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(10);
        _timer.Tick += Timer_Tick;
    }

    public void PlayMedia()
    {
        _mediaElement.Play();
        _timer.Start();
    }

    public void PauseMedia()
    {
        _mediaElement.Pause();
    }

    public void StopMedia()
    {
        _mediaElement.Stop();
        _timer.Stop();
        _sliderController.ResetProgressSlider();
    }

    public void SetSpeed(double speed)
    {
        _mediaElement.SpeedRatio = speed;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (_mediaElement.NaturalDuration.HasTimeSpan && !_sliderController.IsProgressSliderBeingDragged())
        {
            _sliderController.UpdateProgressSlider(_mediaElement.Position.TotalSeconds, _mediaElement.NaturalDuration.TimeSpan.TotalSeconds);
        }
    }

    public void SetMediaPosition(double seconds)
    {
        _mediaElement.Position = TimeSpan.FromSeconds(seconds);
    }

    public void LoadMedia(string filePath)
    {
        _mediaElement.Source = new Uri(filePath);
        PlayMedia();
    }
}