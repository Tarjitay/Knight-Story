using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool _isEnemyInRange;

    [SerializeField] private float damage = 20f;

    [SerializeField] private Vector2 size;
    [SerializeField] private float angle;
    [SerializeField] private float offsetX;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioSource enemyHitSound;

    private Vector2 _origin;
    private Vector2 _direction;

    private float _currentHitDistance;

    public void EnemyInRange()
    {
        _origin = transform.position + Vector3.right * offsetX;
        RaycastHit2D[] hits = new RaycastHit2D[10];

        hits = Physics2D.BoxCastAll(_origin, size, angle, _direction, maxDistance, enemyLayer);

        if (hits.Length > 0)
        {
            _currentHitDistance = hits[0].distance;
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyHealth enemyHealth = hits[i].transform.GetComponent<EnemyHealth>();
                enemyHealth.ReduceHealth(damage);
                enemyHitSound.Play();
            }
        }
        else
        {
            _currentHitDistance = maxDistance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_origin + _direction * _currentHitDistance, size);
    }
}
