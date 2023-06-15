using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private BuildTower buildTower;
    [SerializeField] private string towerName;
    public void OnPointerDown(PointerEventData eventData)
    {
        buildTower.ChooseTower(towerName);
    }

   
}
