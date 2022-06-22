using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Level;
    public int BaseSeed;

    private int _prevRoomPlayerHealth;
    private int _prevRoomPlayerCoins;

    private Player _player;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Level = 1;
        BaseSeed = PlayerPrefs.GetInt("Seed");
        Random.InitState(BaseSeed);
        Generation.Instance.Generate();
        UI.Instance.UpdateLeveltext(Level);

        _player = FindObjectOfType<Player>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void GoToNextLevel()
    {
        _prevRoomPlayerHealth = _player.CurHP;
        _prevRoomPlayerCoins = _player.Coins;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Game")
        {
            Destroy(gameObject);
            return;
        }

        _player = FindObjectOfType<Player>();
        Level++;
        BaseSeed++;

        Generation.Instance.Generate();

        _player.CurHP = _prevRoomPlayerHealth;
        _player.Coins = _prevRoomPlayerCoins;

        UI.Instance.UpdateHealth(_prevRoomPlayerHealth);
        UI.Instance.UpdateCoinText(_prevRoomPlayerCoins);
        UI.Instance.UpdateLeveltext(Level);
    }
}
