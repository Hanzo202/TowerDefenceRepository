using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private LineRenderer _laser;
    private bool _wasMagicSound = false;
    private Transform _target;

    [SerializeField]private ParticleSystem hitEffect;
    [SerializeField] private float damage;
    [SerializeField] private float offsetHitEffect;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetX;
    [SerializeField] private ZoneTrigger zone;


    private void Start()
    {
        _laser = GetComponent<LineRenderer>();
        _laser.SetPosition(0, transform.position);
        _laser.SetPosition(1, transform.position);
    }

    private void Update()
    {
        _target = zone.target;
        TargetAttack();

    }

    private void TargetAttack()
    {

        if (_target != null)
        {
            SetLaser();            
        }
        else
        {
            _laser.SetPosition(1, transform.position);
            if (hitEffect.isPlaying)
            {
                hitEffect.Stop();
            }
        }
    }

    private void SetLaser()
    {
        Vector3 changeTargetPos = new Vector3(_target.position.x + offsetX, _target.position.y + offsetY, _target.position.z);
        _laser.SetPosition(1, changeTargetPos);
        if (!hitEffect.isPlaying)
        {
            hitEffect.Play();
        }
        if (!_wasMagicSound)
        {
            StartCoroutine(MagicSoundCoroutine());
        }
        Vector3 dir = transform.position - changeTargetPos;
        hitEffect.transform.position = changeTargetPos - dir.normalized * offsetHitEffect;
        _target.GetComponent<Enemy>().HitEnemy(damage * Time.deltaTime);
    }

    IEnumerator  MagicSoundCoroutine()
    {
        AudioManager.Instance.PlaySfx("magicTowerAttack");
        _wasMagicSound = true;
        yield return new WaitForSeconds(10);
        _wasMagicSound = false;
    }
}
