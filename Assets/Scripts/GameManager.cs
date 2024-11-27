using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int puntuacion;
    // Coge el texto de la puntuación que es un TMP
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntuacion;

    public void puntuar(int puntos)
    {
        puntuacion += puntos;
        textoPuntuacion.text = puntuacion.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
