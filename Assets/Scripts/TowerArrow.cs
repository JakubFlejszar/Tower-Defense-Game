using UnityEngine;

public class TowerArrow : MonoBehaviour
{
    [SerializeField] private float startingArrowSpeed = 2;
    private int damage;
    private Bandit target;
    private float arrowSpeed;
    private Vector3 lastTargetPosition;

    private void Start()
    {
        arrowSpeed = startingArrowSpeed;
    }

    private void Update()
    {
        if (target != null)
        {
            lastTargetPosition = target.transform.position;
        }
        if (Vector3.Distance(transform.position, lastTargetPosition) <= 1)
        {
            if (target != null)
            {
                target.ReceiveDamage(damage);
            }
            Destroy(gameObject);
            return;
        }
        Vector3 arrowDirection = lastTargetPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, arrowDirection);
        transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, maxDistanceDelta: arrowSpeed * Time.deltaTime);
    }

    public void Setup(int arrowDamage, Bandit arrowTarget)
    {
        damage = arrowDamage;
        target = arrowTarget;
        lastTargetPosition = target.transform.position;
    }
}