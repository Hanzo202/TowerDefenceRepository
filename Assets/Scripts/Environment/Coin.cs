using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]private float speed;
    [SerializeField]private float speedMove;

    private Vector3 coinImagePos;
    private Vector3 pointMove;
    private Vector3 dir;
    private bool isMove = false;
    private Camera mainCam;

    private const float OffsetZ = 15f;
    private const float VanishingPoint = 2f;

    void Start()
    {
        mainCam = Camera.main;
        coinImagePos = GameObject.FindGameObjectWithTag("CoinImage").GetComponent<RectTransform>().position;
        pointMove = mainCam.ScreenToWorldPoint(new Vector3(coinImagePos.x, coinImagePos.y, OffsetZ));
        dir = pointMove - transform.position;
    }
    void Update ()
    {       
        if (!isMove)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
            return;
            
        }

        transform.rotation = Quaternion.identity;
        transform.Translate(dir * speedMove * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointMove) < VanishingPoint)
        {
            isMove = false;
            GameManager.Instance.GetCoins(1);
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        isMove = true;
        AudioManager.Instance.PlaySfx("getCoin");
    }
}
