using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildTower : MonoBehaviour
{
    public bool isBuilding;

    private Vector3 _touchPosition;
    private const int _buildOffsetY = 1;
    private const int _buildingEffectOffestY = 2;
    private Node _currentNode = null;
    private Tower _currentTower;

    [SerializeField] private Tower[] towerPrefabs;
    [SerializeField] private Mesh buildingPreviewMesh;
    [SerializeField] private Mesh ZoneMesh;
    [SerializeField] private Material buildingPreviewMatBlue;
    [SerializeField] private Material buildingPreviewMatRed;
    [SerializeField] private Material buildingPreviewMatZone;
    [SerializeField] private GameObject buildingEffect;
    [SerializeField] private CanvasController canvas;


    public void ChooseTower(string towerName)
    {
        foreach (Tower tower in towerPrefabs)
        {
            if (tower.TowerName == towerName)
            {
                isBuilding = GameManager.Instance.CanBuildTower(tower);
                _currentTower = tower;
            }
        }     
    }

    private void Update()
    {
        if (isBuilding)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit info))
                    {
                        _touchPosition = new Vector3(info.point.x, info.point.y, info.point.z);
                        if (info.transform.GetComponent<Node>() != null)
                        {
                            _currentNode = info.transform.GetComponent<Node>();
                            Graphics.DrawMesh(buildingPreviewMesh, _touchPosition, Quaternion.identity, buildingPreviewMatBlue, 0);
                            Graphics.DrawMesh(ZoneMesh, _touchPosition, Quaternion.identity, buildingPreviewMatZone, 0);
                        }
                        else
                        {
                            Graphics.DrawMesh(buildingPreviewMesh, _touchPosition, Quaternion.identity, buildingPreviewMatRed, 0);
                            _currentNode = null;
                        }
                    }
                }               
            }
            else
            {
                if (_currentNode != null)
                {
                    StartCoroutine(BuildTowerCoroutine(_currentNode));
                }
                else
                {
                    canvas.AnnouncementText("You can't build here");
                    isBuilding = false;
                }
            }
        }
    }

    IEnumerator  BuildTowerCoroutine(Node node)
    {
        GameManager.Instance.BuyingTower(_currentTower.GetComponent<Tower>());
        AudioManager.Instance.PlaySfx("buildTower");
        Vector3 buildingEffectPos = new Vector3(node.transform.position.x, node.transform.position.y + _buildingEffectOffestY, node.transform.position.z);
        GameObject effect =  Instantiate(buildingEffect, buildingEffectPos, Quaternion.identity);
        isBuilding = false;
        yield return new WaitForSeconds(2);
        Destroy(effect);
        Vector3 buildTowerPos = new Vector3(node.transform.position.x, node.transform.position.y + _buildOffsetY, node.transform.position.z);
        Instantiate(_currentTower, buildTowerPos, Quaternion.identity);
        this._currentNode = null;
    }
}
