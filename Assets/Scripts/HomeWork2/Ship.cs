using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private WindController _windController;
    [SerializeField] private GameObject _sail;
    [SerializeField] private Transform _shipBody;
    [SerializeField] private float _rotateSailStep = 2;
    [SerializeField] private float MaxSailTilt = 90;
    [SerializeField] private float _rotationSpeedShip = 20;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            _sail.transform.localRotation = Quaternion.RotateTowards(_sail.transform.localRotation, Quaternion.Euler(0, -1 * MaxSailTilt, 0), _rotateSailStep * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            _sail.transform.localRotation = Quaternion.RotateTowards(_sail.transform.localRotation, Quaternion.Euler(0, 1 * MaxSailTilt, 0), _rotateSailStep * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            _shipBody.Rotate(0, -1 * _rotationSpeedShip * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.D))
            _shipBody.Rotate(0, 1 * _rotationSpeedShip * Time.deltaTime, 0);

        // Визуализация сил
        Debug.DrawRay(transform.position, _sail.transform.forward * 9f, Color.yellow);                   // Парус
        Debug.DrawRay(transform.position, Vector3.Cross(_sail.transform.right, Vector3.up) * 7f, Color.green); // Сила

        // Показываем эффективность
        float efficiency = Mathf.Max(0, Vector3.Dot(_sail.transform.forward, _windController.WindDirection.normalized));
        Debug.Log($"Эффективность паруса: {efficiency:F2}");
    }

    private void FixedUpdate()
    {
        Vector3 sailDirection = Vector3.Cross(_sail.transform.right, Vector3.up);
        float sailPowerFromWind = Mathf.Max(0, Vector3.Dot(_sail.transform.forward, _windController.WindDirection));
        Vector3 forwardForce = sailDirection * _windController.WindForce * sailPowerFromWind;

        _rigidbody.MovePosition(_rigidbody.position + forwardForce * Time.fixedDeltaTime);
    }
}
