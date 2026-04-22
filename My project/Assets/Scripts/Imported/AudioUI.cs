using UnityEngine;
using UnityEngine.UI;
using Vic.Code;

public class AudioUI : UIWindow
{
    [Header("SettingsUI")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    public override void Initialize()
    {
        base.Initialize();
        musicSlider.onValueChanged.AddListener(SettingManager.Instance.SetMusicValue);
        sfxSlider.onValueChanged.AddListener(SettingManager.Instance.SetSFXValue);
    }


    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
