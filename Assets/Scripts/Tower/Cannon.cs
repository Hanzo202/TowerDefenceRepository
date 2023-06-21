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


    private float countDown = 0.5f;
    private Transform target;  
    private Quaternion targetRot;

    private void Update()
    {
        target = zone.Target;    
        if(target!=null)
        {
            LookRotation();
            Shot();
        }
    }

    private void Shot()
    {
        if (countDown <= 0f)
        {
            Instantiate(projectile, shotPoint.transform.position, shotPoint.transform.rotation,transform);     
            countDown = atackSpeed;
        }

        countDown -= Time.deltaTime;
    }

    private void LookRotation()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        Vector3 rotation = lookRotation.eulerAngles;
        targetRot = Quaternion.Euler(_offsetRotX, rotation.y + _offsetRotY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
    }
}
