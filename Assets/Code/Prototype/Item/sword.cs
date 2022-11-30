using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    //Outlets
    Rigidbody2D _rb;

    //State Tracking
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float acceleration = 20f;
        float maxSpeed = 4f;

        ChooseNearestTarget();

        if (target != null)
        {
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            _rb.MoveRotation(angle);
        }

        _rb.AddForce(transform.right * acceleration);

        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);

    }

    void ChooseNearestTarget()
    {
        float closestDistance = 9999f;
        Target[] enemies = FindObjectsOfType<Target>();
        for (int i = 0; i < enemies.Length; i++)
        {
            Target enemy = enemies[i];

            if (enemy.transform.position.x > transform.position.x)
            {
                Vector2 directionToTarget = enemy.transform.position - transform.position;

                if (directionToTarget.sqrMagnitude < closestDistance)
                {
                    closestDistance = directionToTarget.sqrMagnitude;

                    target = enemy.transform;
                }
            }
        }
    }

}
