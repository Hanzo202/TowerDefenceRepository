using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    [SerializeField]private float blinkFadeInTime;
    [SerializeField] private float blinkStayTime;
    [SerializeField] private float blinkFadeOutTime;

    private float timer = 0;
    private float alphaColor = 1f;
    private Color color;
    private TextMeshProUGUI text;

    private const float AlphaConst = 1f;


    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        color = text.color;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= blinkFadeInTime)
        {
            alphaColor -= AlphaConst * Time.deltaTime; ;
            text.color = new Color(color.r, color.g, color.b,  alphaColor);
            return;
        }
        else if (timer <= blinkFadeInTime + blinkFadeOutTime)
        {
            alphaColor += AlphaConst * Time.deltaTime;
            text.color = new Color(color.r, color.g, color.b, alphaColor);
            return;           
        }
        else if (timer <= blinkFadeInTime + blinkStayTime + blinkFadeOutTime)
        {
            text.color = new Color(color.r, color.g, color.b, 1);
            return;
        }
        timer = 0;
    }
}
