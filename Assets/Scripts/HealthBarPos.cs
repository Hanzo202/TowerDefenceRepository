using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPos : MonoBehaviour
{
     private Transform _enemy;

    [SerializeField]private float offsetY;
    [SerializeField]private float offsetZ;

    private void Start()
    {
        _enemy = transform.parent;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(-45, -_enemy.rotation.y, 0);
        transform.position = new Vector3(_enemy.position.x, _enemy.position.y + offsetY, _enemy.position.z + offsetZ);
    }
}
