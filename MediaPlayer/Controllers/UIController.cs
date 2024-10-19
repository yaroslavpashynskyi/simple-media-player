using System.Windows.Controls;

namespace MediaPlayer.Controllers;

public class UIController
{
    private TextBlock _volumeText;

    public UIController(TextBlock volumeText)
    {
        _volumeText = volumeText;
    }

    public void UpdateVolumeText(double volume)
    {
        if (_volumeText != null)
            _volumeText.Text = $"{(int)(volume * 100)}%";
    }
}