using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private int castleHealth;
    [SerializeField]private Slider healthBar;
    [SerializeField] private ParticleSystem TheFire;

    public void EnemyDamage(int damage)
    {
        castleHealth -= damage;
        healthBar.value = castleHealth;
        if (castleHealth <= 0)
        {
            StartCoroutine(CastleFell());            
        }
    }

    private IEnumerator CastleFell()
    {
        yield return new WaitForSeconds(1);

        GameManager.Instance.ChangeState(State.LoseGame);
        TheFire.gameObject.SetActive(true);
    }
}
