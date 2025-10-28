using UnityEngine;

public class Item : MonoBehaviour, IDragable
{
    private Rigidbody _rigidbody;

    public bool IsDragged;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 position) => transform.position = position;
    //public void Move(Vector3 position) => _rigidbody.MovePosition(position);

    public void Drop()
    {
        IsDragged = false;
        _rigidbody.isKinematic = false;
        
    }

    public void Drag()
    {
        transform.rotation = Quaternion.identity;
        IsDragged = true;
        _rigidbody.isKinematic = true;
    }

    public void PushTo(Vector3 direction, float forceValue)
    {
        _rigidbody.AddForce(direction * forceValue, ForceMode.Impulse);
    }
}
