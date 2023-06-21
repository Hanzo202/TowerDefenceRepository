using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private BuildTower buildTower;
    [SerializeField] private Tower tower;
    public void OnPointerDown(PointerEventData eventData) =>  buildTower.ChooseTower(tower);


   
}
