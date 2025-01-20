using UnityEngine;

public class Take : MonoBehaviour
{
    [SerializeField] private Transform _pos;

    private float _distance = 3;
    private Rigidbody _rb;
    private bool _isPick = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, _distance)&& !_isPick)
        {
            _rb.isKinematic = true;
            _rb.MovePosition(_pos.position);
            _isPick = true;
        }
        else if (_isPick)
        {
            _rb.useGravity = true;
            _rb.isKinematic = false;
            _isPick=false;
        }
    }

    private void FixedUpdate()
    {
        if (_rb.isKinematic == true && _isPick)
        {
            this.gameObject.transform.position = _pos.position;
            
        }
    }
}
