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
    [SerializeField] private ControlHUD controlHUD;
    [SerializeField] private int seconds;
    private const float DELTA_HUD = 1f;
    private float timerHUD;
    [SerializeField] private int lives = 3;
    [SerializeField] private PlayerControllerInputSystem player;

    [SerializeField] private int puntuacion;
    // Coge el texto de la puntuación que es un TMP

    void Start()
    {
        controlHUD = FindObjectOfType<ControlHUD>();
        controlHUD.setPuntuacionTxt(0);
        controlHUD.setVidasTxt(lives);
        controlHUD.setTiempoTxt(0);
        player.OnPlayerKilledEvent += PlayerKilled;
    }
    public void puntuar(int puntos)
    {
        puntuacion = puntuacion + puntos;
        controlHUD.setPuntuacionTxt(puntos);
    }
    public void PlayerKilled()
    {
        Debug.Log("Player killed");
        controlHUD.setVidasTxt(--lives);
        if (lives <= 0)
        {
            controlHUD.setGameOver();
        }
        
    }

    // player reference

    public PlayerControllerInputSystem Player
    {
        get => player;
        set
        {
            player = value;
            player.OnPlayerKilledEvent += PlayerKilled;
        }
    }





    // Update is called once per frame
    void Update()
    {
        timerHUD += Time.deltaTime;
        if (timerHUD >= DELTA_HUD)
        {
            seconds++;
            controlHUD.setTiempoTxt(seconds);
            timerHUD = 0;
        }
    }
}
