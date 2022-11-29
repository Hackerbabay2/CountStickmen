using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private UnitsManager unitsManager;

    private EnemyManager _enemyManager;
    private EnemyMovement _enemyMovement;

    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyManager = transform.GetComponentInParent<EnemyManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out AIUnit aIUnit))
        {
            unitsManager.RemoveUnit(aIUnit);
            _enemyManager.RemoveUnit(_enemyMovement);
        }
    }
}
