using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]private float speed;
    [SerializeField] private float speedMove;

    private bool _isMove = false;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }
    void Update ()
    {       
        if (_isMove)
        {
            Vector3 point = _cam.ScreenToWorldPoint(new Vector3(Screen.width - 200, Screen.height - 50, 20));
            Vector3 dir = point - transform.position;
            transform.rotation = Quaternion.identity;
            transform.Translate(dir * speedMove * Time.deltaTime);

            if (Vector3.Distance(transform.position, point) < 0.5f) 
            {
                _isMove = false;
                GameManager.Instance.GetCoins(1);
                Destroy(gameObject);
            }
            return;
        }
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        _isMove = true;
        AudioManager.Instance.PlaySfx("getCoin");
    }
}
