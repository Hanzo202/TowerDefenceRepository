using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int coinsForTheEnemy;
    [SerializeField] private GameObject coin;
    [SerializeField] private Slider hpBar;
    [SerializeField] private ParticleSystem dieParticle;
    [SerializeField] private float speed;
    [SerializeField] private float speedRot;
    [SerializeField] private float hp;
    [SerializeField] private int damage;


    public bool isAlive = true;

    private Transform[] _path;
    private int _indexPath = 0;
    private Castle _castle;
    private Animator _animator;
    private BoxCollider _boxCollider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _path = WayPoints.points;
        _castle = FindObjectOfType<Castle>();
        hpBar.maxValue = hp;
        hpBar.value = hp;
        transform.LookAt(_path[_indexPath].position);
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (isAlive)
        {
            if (_indexPath < _path.Length)
            {
                EnemyMove();
                transform.position = Vector3.MoveTowards(transform.position, _path[_indexPath].position, Time.deltaTime * speed);              
            }
        }       
    }

    private void EnemyMove()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_path[_indexPath].position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRot * Time.deltaTime);
        if (Vector3.Distance(transform.position, _path[_indexPath].position) < 0.5)
        {
            _indexPath++;
        }
    }

    public void HitEnemy(float damage)
    {
        hp -= damage;
        hpBar.value = hp;
        if (hp <= 0 && isAlive)
        {
            StartCoroutine(EnemyDeathCoroutine());          
        }
    }

    IEnumerator EnemyDeathCoroutine()
    {
        EnemyDeathProp("Death");
        yield return new WaitForSeconds(3);
        for (int i = 0; i < coinsForTheEnemy; i++)
        {
            Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }
        EnemyDeathEffect();
    }


    IEnumerator EnemyReachedCastleCoroutine()
    {
        EnemyDeathProp("Attack");
        _castle.EnemyDamage(damage);
        AudioManager.Instance.PlaySfx("castleHit");
        yield return new WaitForSeconds(2);
        _animator.SetBool("Death", true);
        yield return new WaitForSeconds(3);
        EnemyDeathEffect();
    }

    public void Idle()
    {
        speed = 0;
        _animator.SetBool("Idle", true);
    }

    public void EnemyDeathProp(string anim)
    {
        AudioManager.Instance.PlaySfx("enemyDeath");
        isAlive = false;
        _animator.SetBool(anim, true);
        _boxCollider.enabled = false;
        hpBar.gameObject.SetActive(false);
    }

    public void EnemyDeathEffect()
    {
        Instantiate(dieParticle, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        GameManager.Instance.EnemyWasDestroy();
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            StartCoroutine(EnemyReachedCastleCoroutine());
        }
    }
}
