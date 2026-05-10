using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Vic.Code;

public class GameplayUI : UIWindow
{
    [SerializeField] private PauseUI pauseUI;
    
    [Header("Botones Principales")]
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slashButton;
    [SerializeField] private Button pauseButton;
    
    [Header("Power-Ups")]
    [SerializeField] private Button activateHighJumpButton; 
    [SerializeField] private Image highjumpCooldownFill;
    [SerializeField] private Button resetSlashButton;
    [SerializeField] private Image slashCooldownFill;

    [SerializeField] private float highJumpDuration = 10f;
    [SerializeField] private float highJumpCooldown = 20f;
    [SerializeField] private float resetSlashCooldown = 15f;

    private bool _isHighJumpPowerUpActive = false;

    public override void Initialize()
    {
        base.Initialize();
        
        jumpButton.onClick.AddListener(OnJumpClicked);
        slashButton.onClick.AddListener(OnSlashClicked);
        pauseButton.onClick.AddListener(OnPauseClicked);
        
        activateHighJumpButton.onClick.AddListener(OnHighJumpPowerUpClicked);
        resetSlashButton.onClick.AddListener(OnResetSlashPowerUpClicked);
        
        highjumpCooldownFill.fillAmount = 0;
        slashCooldownFill.fillAmount = 0;
    }
    
    public void OnJumpClicked()
    {
        if (_isHighJumpPowerUpActive)
        {
            Debug.Log("Boosted Jump");
        }
        
        PlayerManager.Instance.GetComponent<PlayerController>().TriggerJump();
    }

    public void OnSlashClicked()
    {
        PlayerManager.Instance.GetComponent<PlayerController>().TriggerSlash();
    }
    
    private void OnHighJumpPowerUpClicked()
    {
        if (!_isHighJumpPowerUpActive)
        {
            StartCoroutine(HighJumpRoutine());
        }
    }

    private IEnumerator HighJumpRoutine()
    {
        _isHighJumpPowerUpActive = true;
        activateHighJumpButton.interactable = false;

        float elapsed = 0;
        while (elapsed < highJumpDuration)
        {
            elapsed += Time.deltaTime;
            highjumpCooldownFill.fillAmount = 1 - (elapsed / highJumpDuration);
            yield return null;
        }
        
        _isHighJumpPowerUpActive = false; 
        
        elapsed = 0;
        while (elapsed < highJumpCooldown)
        {
            elapsed += Time.deltaTime;
            highjumpCooldownFill.fillAmount = elapsed / highJumpCooldown;
            yield return null;
        }
        
        highjumpCooldownFill.fillAmount = 0;
        activateHighJumpButton.interactable = true;
    }

    private void OnResetSlashPowerUpClicked()
    {
        StartCoroutine(ResetSlashCooldownRoutine());
    }

    private IEnumerator ResetSlashCooldownRoutine()
    {
        resetSlashButton.interactable = false;
       
        yield return new WaitForSeconds(resetSlashCooldown);
        resetSlashButton.interactable = true;
    }

    public void OnPauseClicked()
    {
        if(pauseUI != null) pauseUI.Show();
    }
}
