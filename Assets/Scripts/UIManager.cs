using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider; 
    public Gradient gradient;
    public Image fill;
    public static int score;
    public Text scoreText;

    private void Start()
    {

        slider.value = FPCamera.MaxHealth;
    }
    private void Update()
    {
        slider.maxValue = FPCamera.MaxHealth;
        scoreText.text = score+"$";

        fill.color = gradient.Evaluate(1f);

        slider.value = FPCamera.CurrentHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

