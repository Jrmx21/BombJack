using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHUD : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TMPro.TextMeshProUGUI textoVidas;
    [SerializeField] private TMPro.TextMeshProUGUI textoTiempo;
    [SerializeField] private TMPro.TextMeshProUGUI textoGameOver;

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
        textoVidas.text = "LIVES X" + vidas.ToString();
    }
    public void setGameOver()
    {
        textoGameOver.gameObject.SetActive(true);
    }
}
