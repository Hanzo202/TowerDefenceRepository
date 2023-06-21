using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPos : MonoBehaviour
{
    private Transform enemy;
    [SerializeField]private float offsetY;
    [SerializeField]private float offsetZ;

    private void Start() => enemy = transform.parent; 

    void Update()
    {
        transform.rotation = Quaternion.Euler(-45, -enemy.rotation.y, 0);
        transform.position = new Vector3(enemy.position.x, enemy.position.y + offsetY, enemy.position.z + offsetZ);
    }
}
