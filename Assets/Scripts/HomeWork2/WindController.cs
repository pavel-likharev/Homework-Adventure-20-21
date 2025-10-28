using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] private float _windForce;
    [SerializeField] private float _updateTime;
    [SerializeField] private Transform _arrow;
    [SerializeField] private Vector3 _windDirection;

    private float _timer;

    public Vector3 WindDirection => _windDirection;
    public float WindForce => _windForce;

    private void Update()
    {
        UpdateWindDirection();
    }

    private void UpdateWindDirection()
    {
        if (_timer < 0)
        {
            float xCoordinat = Random.Range(-1.0f, 1.0f);
            float zCoordinat = Random.Range(-1.0f, 1.0f);

            _timer = _updateTime;
            _windDirection = new Vector3(xCoordinat, 0, zCoordinat);
            _arrow.rotation = Quaternion.LookRotation(WindDirection);

        }

        _timer -= Time.deltaTime;
    }
}
