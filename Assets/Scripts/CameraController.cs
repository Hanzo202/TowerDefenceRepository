using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float swipeSpeed;
    [SerializeField] private BuildTower buildTower;
    [SerializeField] private float maxYScroll;
    [SerializeField] private float minYScroll;
    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;


    private Touch _touch1;
    private Touch _touch2;
    private Vector2 _touch1Direction;
    private Vector2 _touch2Direction;
    private float _dstBtwTouchesPositions;
    private float _dstBtwTouchesDirections;
    private float _zoom;


 

    private void Update()
    {
        if (buildTower.IsBuilding)
        {
            return;
        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Swipe();            
        }
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Scroll();           
        }

    }

    private void Scroll()
    {
        _touch1 = Input.GetTouch(0);
        _touch2 = Input.GetTouch(1);

        _touch1Direction = _touch1.position - _touch1.deltaPosition;
        _touch2Direction = _touch2.position - _touch2.deltaPosition;

        _dstBtwTouchesPositions = Vector2.Distance(_touch1.position, _touch2.position);
        _dstBtwTouchesDirections = Vector2.Distance(_touch1Direction, _touch2Direction);

        _zoom = _dstBtwTouchesPositions - _dstBtwTouchesDirections;


        float currentZoom = transform.position.y - _zoom * scrollSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minYScroll, maxYScroll);
        transform.position = new Vector3(transform.position.x, currentZoom, transform.position.z);
    }

    private void Swipe()
    {
        Vector3 touchDeltaPos = Input.GetTouch(0).deltaPosition;
        if (Mathf.Abs(touchDeltaPos.x) > Mathf.Abs(touchDeltaPos.y))
        {
            if (touchDeltaPos.x > 0)
            {
                transform.Translate(Vector3.right * swipeSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector3.left * swipeSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            if (touchDeltaPos.y > 0)
            {
                transform.Translate(Vector3.forward * swipeSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector3.back * swipeSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
