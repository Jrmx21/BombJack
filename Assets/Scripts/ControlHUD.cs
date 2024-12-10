using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHUD : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TMPro.TextMeshProUGUI textoVidas;
    [SerializeField] private TMPro.TextMeshProUGUI textoTiempo;
    [SerializeField] private TMPro.TextMeshProUGUI textoGameOver;
    [SerializeField] private Image imagenVida1;
    [SerializeField] private Image imagenVida2;
    [SerializeField] private Image imagenVida3;

    public void setTiempoTxt(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60;
        textoTiempo.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }
    private void Start()
    {
        textoGameOver.gameObject.SetActive(false);
    }
    [SerializeField] private int puntos;
    public void setPuntuacionTxt(int puntuacion)
    {
        puntos += puntuacion;
        textoPuntuacion.text = puntos.ToString();
    }
    public void setVidasTxt(int vidas)
    {
        switch(vidas)
        {
            case 2:
                imagenVida3.gameObject.SetActive(false);
                break;
            case 1:
                imagenVida2.gameObject.SetActive(false);
                imagenVida3.gameObject.SetActive(false);
                break;
            case 0:
                imagenVida1.gameObject.SetActive(false);
                imagenVida2.gameObject.SetActive(false);
                imagenVida3.gameObject.SetActive(false);
                break;
        }

    }
    public void setGameOver(bool win)

    {
        textoGameOver.gameObject.SetActive(true);
        if (win)
        {
            textoGameOver.text = "YOU WIN!!!";
        }
        else
        {
            textoGameOver.text = "GAME OVER";
        }

    }
}
