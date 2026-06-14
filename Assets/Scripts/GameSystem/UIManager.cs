using TMPro;

// - Script that handles the UI of the game
// - Daniel Bruijn

public class UIManager
{
    // - Vairables
    private TMP_Text _ammoText;
    private TMP_Text _upgradeText;
    private TMP_Text _runInfoText;
    private TMP_Text _bestRunTimeText;

    public UIManager(TMP_Text ammo, TMP_Text upgrade, TMP_Text runInfo, TMP_Text bestRunTimeText)
    {
        _ammoText = ammo;
        _upgradeText = upgrade;
        _runInfoText = runInfo;
        _bestRunTimeText = bestRunTimeText;
    }
    
    public void UpdateAmmoUI(int current, int max)
    {
        _ammoText.text = $"{current} / {max}";
    }
    
    public void UpdateRunUI(string text, float bestTime)
    {
        _runInfoText.text = text;
        
        if (bestTime <= 0)
            _bestRunTimeText.text = "Best: --";
        else
            _bestRunTimeText.text = $"Best: {bestTime:F2}s";
    }

    public void ShowUpgrade()
    {
        _upgradeText.gameObject.SetActive(true);
    }

    public void HideUpgrade()
    {
        _upgradeText.gameObject.SetActive(false);
    }
}
