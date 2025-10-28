using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private float _targetHeight;
    [SerializeField] private float _targetDistance;
    [SerializeField] private CameraContoller _cameraContoller;
    
    private IDragable _currentDraggable;

    private Plane _xPlane;
    private Plane _yPlane;
    private Plane _currentPlane;

    private bool _hasCheckOrientation;

    public IDragable Dragable => _currentDraggable;

    private void Awake()
    {
        _xPlane = new Plane(Vector3.up, new Vector3(0, _targetHeight, 0));
        _yPlane = new Plane(Vector3.forward, new Vector3(0, _targetHeight, 0));
    }

    public void TrySetDrag(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDragable dragable))
            {
                SetDrag(dragable);
            }
        }
    }

    public void MoveItem(Ray ray)
    {
        if (_hasCheckOrientation == false)
            CheckPlane(ray);

        VisualizePlane(_currentPlane, new Vector3(0, _targetHeight, 0), 3f);


        if (_currentPlane.Raycast(ray, out float distance))
        {
            Vector3 cursorWorldPos = ray.GetPoint(distance);
            Vector3 targetPosition = cursorWorldPos;

            _currentDraggable.Move(targetPosition);
        }
    }

    private void CheckPlane(Ray ray)
    {
        if (_cameraContoller.IsVerticalCamera)
            _currentPlane = _xPlane;
        else
            _currentPlane = _yPlane;

        _hasCheckOrientation = true;
    }

    private void SetDrag(IDragable dragable)
    {
        _currentDraggable = dragable;
        _currentDraggable.Drag();
    }

    public void SetDrop()
    {
        if (_currentDraggable != null)
        {
            _currentDraggable.Drop();
            _currentDraggable = null;
        }

        _hasCheckOrientation = false;
    }

    void VisualizePlane(Plane plane, Vector3 center, float size)
    {
        Vector3 normal = plane.normal;

        // Находим два перпендикулярных вектора в плоскости
        Vector3 v1 = Vector3.Cross(normal, Vector3.up).normalized;
        if (v1.magnitude < 0.1f)
            v1 = Vector3.Cross(normal, Vector3.forward).normalized;

        Vector3 v2 = Vector3.Cross(normal, v1).normalized;

        // Углы плоскости
        Vector3[] corners = new Vector3[4]
        {
        center + v1 * size + v2 * size,
        center + v1 * size - v2 * size,
        center - v1 * size - v2 * size,
        center - v1 * size + v2 * size
        };

        // Рисуем прямоугольник
        for (int i = 0; i < 4; i++)
        {
            Debug.DrawLine(corners[i], corners[(i + 1) % 4], Color.cyan, 0.1f);
        }

        // Рисуем нормаль
        Debug.DrawLine(center, center + normal * 2f, Color.magenta, 0.1f);

        // Подписываем тип плоскости
        Debug.Log($"Plane: normal={normal}, center={center}");
    }
}
