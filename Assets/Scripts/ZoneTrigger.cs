using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    private Queue<Transform> _targets;

    public Transform target;


    private void Start()
    {
        _targets = new Queue<Transform>();
    }

    private void Update()
    {
        CheckingTargets();       
    }

    private void CheckingTargets()
    {
        if (_targets.Count > 0)
        {
            if (_targets.Peek() != null)
            {
                if (_targets.Peek().GetComponent<Enemy>().isAlive == true)
                {
                    target = _targets.Peek().transform;

                }
                else
                {
                    _targets.Dequeue();
                    target = null;
                }
            }
            else
            {
                _targets.Dequeue();
                target = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Enemy>() != null)
        {
            _targets.Enqueue(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Enemy>() != null) 
        {
            _targets.Dequeue();
            target = null;
        }
    }
}
