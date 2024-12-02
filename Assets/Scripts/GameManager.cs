using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    // SINGLETON 
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] private int puntuacion;
    // Coge el texto de la puntuación que es un TMP
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;

    public void puntuar(int puntos)
    {
        puntuacion += puntos;
        textoPuntuacion.text = puntuacion.ToString();
    }

    // game state
    private float _timer;
    
    
    // player reference
    [SerializeField] private PlayerControllerInputSystem _player;
    public PlayerControllerInputSystem Player
    {
        get => _player;
        set
        {
            _player = value;
            // _player.OnPlayerKilledEvent += OnPlayerKilled;
        }
    }
    
    
    
    public void OnPlayerKilled(){

    }

    // Update is called once per frame
    void Update()
    {

    }
}
