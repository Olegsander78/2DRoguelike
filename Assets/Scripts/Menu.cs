using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField SeedInput;

    private void Start()
    {
        SeedInput.text = PlayerPrefs.GetInt("Seed").ToString();
    }

    public void OnUpdateSeed()
    {
        PlayerPrefs.SetInt("Seed", int.Parse(SeedInput.text));
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

}
