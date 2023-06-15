using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private Canvas canvas;
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private string towerName;

    public string TowerName
    {
        get { return towerName; }
    }


    public int Cost
    {
        get { return cost; }
    }
    public void SellTower()
    {
        AudioManager.Instance.PlaySfx("destroyTower");
        GameManager.Instance.GetCoinsForBuilding(Cost);
        Instantiate(destroyEffect,transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        if (!canvas.isActiveAndEnabled)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
