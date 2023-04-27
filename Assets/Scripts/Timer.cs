using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    private float currentTime;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        slider = GetComponent<Slider>();
        slider.maxValue = maxTime;
        slider.value = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        if (currentTime > maxTime)
        {
            currentTime = 0f;
            Debug.Log("EndTime");
        }
        slider.value = currentTime;
    }
}
