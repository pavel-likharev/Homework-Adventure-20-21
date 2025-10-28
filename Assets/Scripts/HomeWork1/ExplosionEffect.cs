using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float _forceValue;
    [SerializeField] private ParticleSystem explosionEffect;

    private float _radius = 5;

    public void Shoot(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            explosionEffect.transform.position = hitInfo.point;
            explosionEffect.Play();

            Execute(hitInfo.point);
        }
    }

    public void Execute(Vector3 point)
    {
        Collider[] targets = Physics.OverlapSphere(point, _radius);

        foreach (Collider target in targets)
        {
            if (target.TryGetComponent<Item>(out Item item))
            {
                Vector3 direction = item.transform.position - point;
                item.PushTo(direction, _forceValue);
            }
        }

    }
}
