using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Slider slider;
    private bool running = false;
    private float timer = 0;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
            return;
        timer += Time.deltaTime;
        slider.value = timer;
        if (timer > slider.maxValue)
        {
            running = false;
            timer = 0;
        }
    }

    public void Run()
    {
        running = true;
    }

    public void Pause()
    {
        running = false;
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }
}