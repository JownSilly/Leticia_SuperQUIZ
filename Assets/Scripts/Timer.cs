using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    // Delegate
    // Delegates são um tipo de dados que representam referências a um ou mais métodos 
    public delegate void OnParadaTimer();
    public OnParadaTimer onParadaTimer;

    [SerializeField] private float maxTime;
    [SerializeField] private Slider slider;
    private float currentTime;
    private bool isCount;

    void Start()
    {
        currentTime = 0f;
        slider.maxValue = maxTime;
        slider.value = currentTime;
        isCount = true;
    }
    void Update()
    {
        if (isCount)
        {
            currentTime += 1 * Time.deltaTime;
            slider.value = currentTime;

            if (currentTime > maxTime && onParadaTimer != null)
            {
                Parar();
            }
        }
    }
    // Registro do método para o delegate
    public void RegistrarParada(OnParadaTimer metodo)
    {
        onParadaTimer += metodo;
    }

    public void Parar()
    {
        isCount = false;

        if (onParadaTimer != null)
        {
            onParadaTimer();
        }
    }

    public void Zerar()
    {
        currentTime = 0f;
        slider.value = currentTime;
        isCount = true;
    }
}
