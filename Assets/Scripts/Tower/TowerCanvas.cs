using UnityEngine;
using UnityEngine.EventSystems;

public class TowerCanvas : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Tower tower;

    public void OnPointerDown(PointerEventData eventData) => tower.SellTower();

    private void Update()
    {
        if (Input.anyKeyDown && transform.gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }
     
}
