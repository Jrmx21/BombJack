using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHUD : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TMPro.TextMeshProUGUI textoVidas;
    [SerializeField] private TMPro.TextMeshProUGUI textoTiempo;
    public void setTiempoTxt(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60;
        textoTiempo.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }
    public void setPuntuacionTxt(int puntuacion)
    {
        textoPuntuacion.text = puntuacion.ToString();
    }
      public void setVidasTxt(int vidas)
    {
        textoPuntuacion.text = vidas.ToString();
    }
}
