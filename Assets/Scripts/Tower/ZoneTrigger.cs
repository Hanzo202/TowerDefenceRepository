using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class ZoneTrigger : MonoBehaviour
{
    private Queue<Transform> targets;

    private Transform target;
    public Transform Target => target;


    private void Start()
    {
        targets = new Queue<Transform>();
    }

    private void Update() => CheckingTargets();       


    private void CheckingTargets()
    {
        if (targets.Count <=  0)
        {
            return;
        }

        if (targets.Peek() == null)
        {
            targets.Dequeue();
            target = null;
            return;
        }

        if (targets.Peek().GetComponent<EnemyMain>().IsAlive == true)
        {
            target = targets.Peek().transform;

        }
        else
        {
            targets.Dequeue();
            target = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<EnemyMain>() != null)
        {
            targets.Enqueue(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<EnemyMain>() != null) 
        {
            targets.Dequeue();
            target = null;
        }
    }
}
