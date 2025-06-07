using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool finishCountDown = false;
    WaitForSeconds oneSec;
    public Text timer;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        oneSec = new WaitForSeconds(1);
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        timer.gameObject.SetActive(true);
        timer.text = "3";
        yield return oneSec;
        timer.text = "2";
        yield return oneSec;
        timer.text = "1";
        yield return oneSec;
        timer.text = "Fight";
        yield return oneSec;
        finishCountDown = true;
        timer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
