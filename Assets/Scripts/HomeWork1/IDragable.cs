using UnityEngine;

public interface IDragable
{
    public Vector3 Position { get; }

    public void Drag();
    public void Drop();

    public void Move(Vector3 position);
}
