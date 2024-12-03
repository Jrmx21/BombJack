using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [SerializeField] private ControlHUD controlHUD;
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

    private const float DELTA_HUD = 0.5f;
    private float timerHUD;
    private int lives;
    [SerializeField] private PlayerControllerInputSystem player;

    [SerializeField] private int puntuacion;
    // Coge el texto de la puntuación que es un TMP

    void Start()
    {
        controlHUD.setPuntuacionTxt(0);
        controlHUD.setVidasTxt(0);
        controlHUD.setTiempoTxt(0);
    }
    public void puntuar(int puntos)
    {
        puntuacion += puntos;
        controlHUD.setPuntuacionTxt(puntos);
    }
    public void PlayerKilled(){
        Debug.Log("Player killed");
        controlHUD.setVidasTxt(--lives);
    }

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





    // Update is called once per frame
    void Update()
    {
        timerHUD += Time.deltaTime;
        if (timerHUD >= DELTA_HUD)
        {

            // controlHUD.setTiempoTxt();
            timerHUD = 0;
        }
    }
}
