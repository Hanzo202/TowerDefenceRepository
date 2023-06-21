using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private Button sellButton;
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private string towerName;

    public string TowerName => towerName;   
    public int Cost => cost;
    public void SellTower()
    {
        AudioManager.Instance.PlaySfx("destroyTower");
        GameManager.Instance.GetCoinsForBuilding(Cost);
        Instantiate(destroyEffect,transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        if (!sellButton.isActiveAndEnabled)
            sellButton.gameObject.SetActive(true);
    }
}
