using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject[] Hearts;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI LevelText;
    public GameObject KeyIcon;
    public RawImage Map;

    public static UI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealth(int health)
    {
        for (int x = 0; x <Hearts.Length; ++x)
        {
            Hearts[x].SetActive(x < health);
        }
    }

    public void UpdateCoinText(int coins)
    {
        CoinText.text = coins.ToString();
    }

    public void ToggleKeyIcon(bool toggle)
    {
        KeyIcon.SetActive(toggle);
    }

    public void UpdateLeveltext(int level)
    {
        LevelText.text = "Level " + level;
    }

}
