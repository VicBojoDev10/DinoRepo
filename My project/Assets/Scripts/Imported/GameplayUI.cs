using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Vic.Code;

public class GameplayUI : UIWindow
{
    [SerializeField] private PauseUI pauseUI;
    
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slashButton;
    [SerializeField] private Button pauseButton;
    
    [SerializeField] private Button highjumpButton;
    [SerializeField] private Image highjumpCooldownFill;
    [SerializeField] private Button resetSlashButton;
    [SerializeField] private Image slashCooldownFill;

    [SerializeField] private float highJumpDuration = 10f;
    [SerializeField] private float highJumpCooldown = 20f;
    [SerializeField] private float resetSlashCooldown = 15f;

    private bool _isHighJumpActive = false;

    public override void Initialize()
    {
        base.Initialize();
        
        jumpButton.onClick.AddListener(OnJumpClicked);
        slashButton.onClick.AddListener(OnSlashClicked);
        pauseButton.onClick.AddListener(OnPauseClicked);
        
        highjumpButton.onClick.AddListener(OnHighJumpPowerUpClicked);
        resetSlashButton.onClick.AddListener(OnResetSlashPowerUpClicked);
        
        highjumpCooldownFill.fillAmount = 0;
        slashCooldownFill.fillAmount = 0;
    }

    private void OnHighJumpPowerUpClicked()
    {
        if (!_isHighJumpActive)
        {
            StartCoroutine(HighJumpRoutine());
        }
    }

    private void OnResetSlashPowerUpClicked()
    {
        StartCoroutine(ResetSlashCooldownRoutine());
    }

    private IEnumerator ResetSlashCooldownRoutine()
    {
        resetSlashButton.interactable = false;
        
        float elapsed = 0;
        while (elapsed < resetSlashCooldown)
        {
            elapsed += Time.deltaTime;
            slashCooldownFill.fillAmount = 1 -  (elapsed/resetSlashCooldown);
            yield return null;
        }
        
        slashCooldownFill.fillAmount = 0;
        resetSlashButton.interactable = true;
    }

    public void OnSlashClicked()
    {
        PlayerManager.Instance.Attack();
    }

    public void OnJumpClicked()
    {
        PlayerManager.Instance.Jump();
    }

    public void OnPauseClicked()
    {
        if(pauseUI != null) pauseUI.Show();
    }
    private IEnumerator HighJumpRoutine()
    {
        _isHighJumpActive = true;
        highjumpButton.interactable = false;

        float elapsed = 0;
        while (elapsed < highJumpDuration)
        {
            elapsed += Time.deltaTime;
            highjumpCooldownFill.fillAmount = 1 - (elapsed/highJumpDuration);
            yield return null;
        }
        
        elapsed = 0;
        while (elapsed < highJumpCooldown)
        {
            elapsed += Time.deltaTime;
            highjumpCooldownFill.fillAmount = elapsed /  highJumpCooldown;
            yield return null;
        }
        
        highjumpCooldownFill.fillAmount = 0;
        highjumpButton.interactable = true;
        _isHighJumpActive = false;
        
    }
}
