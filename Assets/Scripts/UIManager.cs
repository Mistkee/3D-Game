using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider; 
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        slider.value = FPCamera.MaxHealth;
    }
    private void Update()
    {
        slider.maxValue = FPCamera.MaxHealth;


        fill.color = gradient.Evaluate(1f);

        slider.value = FPCamera.CurrentHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

