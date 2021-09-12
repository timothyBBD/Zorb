using UnityEngine;

public class GunPart : MonoBehaviour
{
    public int id = 0;
    string playerTag = "Player";
    private float minimum = -0.1f;
    private float maximum = 0.3f;

    private float yPos;
    private float bounceSpeed = 1.5f;

    void Awake() 
    {
        if(GameState.PartsCollected[id]){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float sinValue = Mathf.Sin(Time.time * bounceSpeed);

        yPos = Mathf.Lerp(maximum, minimum, Mathf.Abs(sinValue));
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string collisionTag = other.gameObject.tag;
        if (collisionTag == playerTag)
        {
            Destroy(gameObject);
        }
    }
}
