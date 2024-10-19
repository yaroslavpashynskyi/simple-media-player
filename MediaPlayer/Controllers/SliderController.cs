using System.Windows.Controls;

namespace MediaPlayer.Controllers;

public class SliderController
{
    private Slider _progressSlider;
    private Slider _volumeSlider;

    public SliderController(Slider progressSlider, Slider volumeSlider)
    {
        _progressSlider = progressSlider;
        _volumeSlider = volumeSlider;
    }

    public void UpdateProgressSlider(double position, double maxDuration)
    {
        _progressSlider.Maximum = maxDuration;
        _progressSlider.Value = position;
    }

    public void ResetProgressSlider()
    {
        _progressSlider.Value = 0;
    }

    public bool IsProgressSliderBeingDragged()
    {
        return _progressSlider.IsMouseCaptureWithin;
    }

    public double GetProgressSliderValue()
    {
        return _progressSlider.Value;
    }

    public void UpdateVolumeSlider(double volume)
    {
        _volumeSlider.Value = volume;
    }

    public double GetVolumeSliderValue()
    {
        return _volumeSlider.Value;
    }
}