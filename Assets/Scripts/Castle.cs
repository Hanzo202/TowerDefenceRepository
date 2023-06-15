using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    private int _castleHealth;

    [SerializeField]private Slider healthBar;
    [SerializeField] private ParticleSystem TheFire;

    public void EnemyDamage(int damage)
    {
        _castleHealth -= damage;
        healthBar.value = _castleHealth;
        if (_castleHealth <= 0)
        {
            StartCoroutine(CastleFell());            
        }
    }

    IEnumerator CastleFell()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.ChangeState(State.LoseGame);
        TheFire.gameObject.SetActive(true);
    }
}
