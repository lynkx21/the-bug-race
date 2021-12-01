using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private Slider _slider;

    private void Awake() {
        _slider = gameObject.GetComponent<Slider>();
    }

    public void TakeDamage(int damage) {
        _slider.value -= damage;
    }

    public int getHealth() {
        return (int)_slider.value;
    }

    public void setHealth(int health) {
        _slider.value = health;
    }

    public void setMaxHealth(int maxHealth) {
        _slider.maxValue = maxHealth;
    }
}