using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    private int bombsCatched;
    private int bombsCounter;
    [SerializeField] private ControlHUD controlHUD;
    [SerializeField] private int seconds;
    private const float DELTA_HUD = 1f;
    private float timerHUD;
    [SerializeField] private int lives = 3;
    [SerializeField] private PlayerControllerInputSystem player;

    [SerializeField] private int puntuacion;
    // Coge el texto de la puntuación que es un TMP

    // flag for game paused
    private bool isPaused = false;
    // getter and setter for isPaused
    public bool IsPaused
    {
        get => isPaused;
        set => isPaused = value;
    }

    void Start()
    {
        controlHUD = FindObjectOfType<ControlHUD>();
        controlHUD.setPuntuacionTxt(0);
        controlHUD.setVidasTxt(lives);
        controlHUD.setTiempoTxt(0);
        player.OnPlayerKilledEvent += PlayerKilled;

        bombsCounter = FindObjectsOfType<BombController>().Length;
    }
    public void puntuar(int puntos)
    {
        puntuacion = puntuacion + puntos;
        controlHUD.setPuntuacionTxt(puntos);
        bombsCatched++;
        if (bombsCatched == bombsCounter)
        {
            Debug.Log("Level completed");
            controlHUD.setGameOver(true);
            // pausa
            isPaused = true;
            // Freeze the physics of the object physics
            Physics2D.simulationMode = SimulationMode2D.Script;
            Time.timeScale = 0;
        }

    }

    public void PlayerKilled()
    {
        Debug.Log("Player killed");
        controlHUD.setVidasTxt(--lives);
        if (lives <= 0)
        {
            controlHUD.setGameOver(false);
            // manera para salir del paso de pausa
            // Time.timeScale = 0;
            // forma menos mala pausando el juego
            isPaused = true;
            // Freeze the physics of the object physics
            Physics2D.simulationMode = SimulationMode2D.Script;
            Time.timeScale = 0;
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

    void Update()
    {
        if (isPaused)
        {
            return;
        }
        timerHUD += Time.deltaTime;
        if (timerHUD >= DELTA_HUD)
        {
            seconds++;
            controlHUD.setTiempoTxt(seconds);
            timerHUD = 0;
        }
    }
}
