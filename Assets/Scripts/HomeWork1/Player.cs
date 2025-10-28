using UnityEngine;

public class Player : MonoBehaviour
{
    private DragAndDropController _dragAndDropController;
    private ExplosionEffect _explosionEffect;

    private void Awake()
    {
        _dragAndDropController = GetComponent<DragAndDropController>();
        _explosionEffect = GetComponent<ExplosionEffect>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _dragAndDropController.TrySetDrag(ray);
        }

        if (Input.GetMouseButton(0) && _dragAndDropController.Dragable != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _dragAndDropController.MoveItem(ray);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _dragAndDropController.SetDrop();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _explosionEffect.Shoot(ray);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(mousePos.origin, mousePos.direction * 100);
    }
}
