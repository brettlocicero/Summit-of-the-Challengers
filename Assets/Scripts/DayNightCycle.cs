using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    [Header("Runtime")]
    [SerializeField] float currentTime;
    float counter;

    [Header("Settings")]
    [SerializeField] float daySpeed = 100f;
    [SerializeField] Gradient skyGradient;
    [SerializeField] Gradient lightGradient;
    [SerializeField] Light2D sun;
    [SerializeField] SpriteRenderer sky;
    [SerializeField] TextMeshProUGUI timeText;

    void FixedUpdate ()
    {
        Tick();
    }

    void Tick () 
    {
        if (counter > daySpeed) counter = 0f;

        counter += Time.deltaTime;
        currentTime = counter / daySpeed;

        sun.color = lightGradient.Evaluate(currentTime);
        sky.material.SetColor("_Top_Color", skyGradient.Evaluate(currentTime));
        timeText.text = "Time: " + currentTime.ToString("F4");
    }
}
