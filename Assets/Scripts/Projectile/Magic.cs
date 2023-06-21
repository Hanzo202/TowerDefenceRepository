using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private LineRenderer laser;
    private bool wasMagicSound = false;
    private Transform target;

    [SerializeField]private ParticleSystem hitEffect;
    [SerializeField] private float damage;
    [SerializeField] private float offsetHitEffect;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetX;
    [SerializeField] private ZoneTrigger zone;


    private void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position);
    }

    private void Update()
    {
        target = zone.Target;
        TargetAttack();

    }

    private void TargetAttack()
    {

        if (target != null)
        {
            SetLaser();
            return;
        }

        laser.SetPosition(1, transform.position);

        if (hitEffect.isPlaying)
        {
            hitEffect.Stop();
        }
    }

    private void SetLaser()
    {
        Vector3 changeTargetPos = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z);
        laser.SetPosition(1, changeTargetPos);
        if (!hitEffect.isPlaying)
        {
            hitEffect.Play();
        }
        if (!wasMagicSound)
        {
            StartCoroutine(MagicSoundCoroutine());
        }
        Vector3 dir = transform.position - changeTargetPos;
        hitEffect.transform.position = changeTargetPos - dir.normalized * offsetHitEffect;
        target.GetComponent<Enemy.EnemyMain>().HitEnemy(damage * Time.deltaTime);
    }

    IEnumerator  MagicSoundCoroutine()
    {
        AudioManager.Instance.PlaySfx("magicTowerAttack");
        wasMagicSound = true;
        yield return new WaitForSeconds(10);

        wasMagicSound = false;
    }
}
