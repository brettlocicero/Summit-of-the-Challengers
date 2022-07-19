using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseHoverInfo : MonoBehaviour
{
    public static MouseHoverInfo instance;

    [SerializeField] TextMeshPro infoText;
    [SerializeField] Color targetColor;

    void Awake () => instance = this;

    void FixedUpdate () 
    {
        infoText.color = Color.Lerp(targetColor, infoText.color, 0.8f);
    }

    public void ShowHoverText (string text, Vector3 pos)
    {
        infoText.text = text;
        infoText.transform.position = pos;
        targetColor = new Color(1f, 1f, 1f, 1f);
    }

    public void HideHoverText () 
    {
        targetColor = new Color(1f, 1f, 1f, 0f);
    }
}