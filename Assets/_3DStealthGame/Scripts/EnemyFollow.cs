using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;          
    public float moveSpeed = 3f;        
    public float followStartRadius = 5f;    
    public float followStopRadius = 10f;    
    public float rotationSpeed = 5f;      

    private Vector3 originalPosition;      
    private bool isFollowing = false;      

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= followStartRadius)
            {
                isFollowing = true;
            }

            if (distance > followStopRadius && isFollowing)
            {
                isFollowing = false;
            }

            if (isFollowing)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                transform.position += direction * moveSpeed * Time.deltaTime;

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
                {
                    transform.position = originalPosition; 
                }
            }
        }
    }
}
