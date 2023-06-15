using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]private float atackSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float rotSpeed;
    [SerializeField] private ZoneTrigger zone;
    [SerializeField] private GameObject shotPoint;
    [SerializeField] private float _offsetRotY;
    [SerializeField] private float _offsetRotX;


    private float _countDown = 0.5f;
    private Transform _target;  
    private Quaternion _targetRot;

    private void Update()
    {
        _target = zone.target;    
        if(_target!=null)
        {
            LookRotation();
            Shot();
        }
    }

    private void Shot()
    {
        if (_countDown <= 0f)
        {
            Instantiate(projectile, shotPoint.transform.position, shotPoint.transform.rotation,transform);     
            _countDown = atackSpeed;
        }
        _countDown -= Time.deltaTime;
    }

    private void LookRotation()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_target.position - transform.position, Vector3.up);
        Vector3 rotation = lookRotation.eulerAngles;
        _targetRot = Quaternion.Euler(_offsetRotX, rotation.y + _offsetRotY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRot, rotSpeed * Time.deltaTime);
    }
}
