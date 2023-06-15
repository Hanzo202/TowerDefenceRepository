using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCanvas : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown &&  transform.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
