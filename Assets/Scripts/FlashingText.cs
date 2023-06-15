using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    [SerializeField]private float blinkFadeInTime;
    [SerializeField] private float blinkStayTime;
    [SerializeField] private float blinkFadeOutTime;

    private float _timer = 0;
    private float alphaColor = 1f;
    private Color _color;
    private TextMeshProUGUI _text;

    private const float aConst = 1f;


    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _color = _text.color;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer <= blinkFadeInTime)
        {
            alphaColor -= aConst * Time.deltaTime; ;
            _text.color = new Color(_color.r, _color.g, _color.b,  alphaColor);
            return;
        }
        else if (_timer <= blinkFadeInTime + blinkFadeOutTime)
        {
            alphaColor += aConst * Time.deltaTime;
            _text.color = new Color(_color.r, _color.g, _color.b, alphaColor);
            return;           
        }
        else if (_timer <= blinkFadeInTime + blinkStayTime + blinkFadeOutTime)
        {
            _text.color = new Color(_color.r, _color.g, _color.b, 1);
            return;
        }
        _timer = 0;
    }
}
