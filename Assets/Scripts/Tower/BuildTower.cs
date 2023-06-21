using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using GameUI;

public class BuildTower : MonoBehaviour
{
    private bool isBuilding;
    private Vector3 touchPosition;
    private Node currentNode;
    private Tower currentTower;


    private const int BuildOffsetY = 1;
    private const int BuildingEffectOffestY = 2;

    [SerializeField] private Tower[] towerPrefabs;
    [SerializeField] private Mesh buildingPreviewMesh;
    [SerializeField] private Mesh ZoneMesh;
    [SerializeField] private Material buildingPreviewMatBlue;
    [SerializeField] private Material buildingPreviewMatRed;
    [SerializeField] private Material buildingPreviewMatZone;
    [SerializeField] private GameObject buildingParticlePrefab;
    [SerializeField] private GUIController _gUIController;

    public bool IsBuilding => isBuilding;


    public void ChooseTower(Tower tower)
    {
        if (towerPrefabs.Contains(tower))
        {
            currentTower = Array.Find(towerPrefabs, element => element == tower);
            isBuilding = GameManager.Instance.CanBuildTower(currentTower);
        }     
    }

    private void Update()
    {
        if (!isBuilding)
        {
            return;
        }
        
        if (Input.touchCount <= 0)
        {
            if (currentNode != null)
            {
                StartCoroutine(BuildTowerCoroutine(currentNode));
                currentNode = null;
            }
            else
            {
                _gUIController.AnnouncementText("You can't build here");
                isBuilding = false;
            }
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit info))
        {
            touchPosition = new Vector3(info.point.x, info.point.y, info.point.z);
            if (info.transform.GetComponent<Node>() != null)
            {
                currentNode = info.transform.GetComponent<Node>();
                Graphics.DrawMesh(buildingPreviewMesh, touchPosition, Quaternion.identity, buildingPreviewMatBlue, 0);
                Graphics.DrawMesh(ZoneMesh, touchPosition, Quaternion.identity, buildingPreviewMatZone, 0);
            }
            else
            {
                Graphics.DrawMesh(buildingPreviewMesh, touchPosition, Quaternion.identity, buildingPreviewMatRed, 0);
                currentNode = null;
            }
        }
      
    }

    private IEnumerator  BuildTowerCoroutine(Node node)
    {
        StartToBuildTower();

        Vector3 buildingEffectPos = new Vector3(node.transform.position.x, node.transform.position.y + BuildingEffectOffestY, node.transform.position.z);
        GameObject _buildingParticle = Instantiate(buildingParticlePrefab, buildingEffectPos, Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(_buildingParticle);

        Vector3 buildTowerPos = new Vector3(node.transform.position.x, node.transform.position.y + BuildOffsetY, node.transform.position.z);
        Instantiate(currentTower, buildTowerPos, Quaternion.identity);

    }

    private void StartToBuildTower()
    {
        isBuilding = false;
        GameManager.Instance.BuyingTower(currentTower.GetComponent<Tower>());
        AudioManager.Instance.PlaySfx("buildTower");
    }


 
}
