using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    [Header("HP Properties")]
    public int value;

    [Header("DisplayProperties")]
    public Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = transform.GetComponentInChildren<Slider>();
        ReserHP();
    }

    public void ReserHP()
    {
        hpBar.value = 100;
        value = (int)hpBar.value;
    }

    public void TakeDamage(int damage)
    {
        hpBar.value -= damage;
        if(hpBar.value < 0)
        {
            hpBar.value = 0;
        }
        value = (int)hpBar.value;
    }

    public void HealDamage(int damage)
    {
        hpBar.value += damage;
        if (hpBar.value > 100)
        {
            hpBar.value = 100;
        }
        value = (int)hpBar.value;
    }

}
