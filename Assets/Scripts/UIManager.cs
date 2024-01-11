using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider; 
    public Gradient gradient; // Optional
    public Image fill;

    private void Start()
    {
        slider.value = FPCamera.MaxHealth;
    }
    private void Update()
    {
        slider.maxValue = FPCamera.MaxHealth;


        // Optional
        fill.color = gradient.Evaluate(1f);

        slider.value = FPCamera.CurrentHealth;

        // Optional
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

