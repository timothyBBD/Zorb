using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public Rigidbody2D bullet;
    private float bulletSpeed;

    void Start()
    {
        bulletSpeed = bullet.gameObject.GetComponent<EnemyBulletProjectile>().bulletSpeed;   
    }

    Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public void SingleFire(Vector2 direction)
    {
        Rigidbody2D bulletClone = (Rigidbody2D) Instantiate(bullet, transform.position, Quaternion.identity);
        bulletClone.velocity = direction * bulletSpeed;
    }

    public void ConeFire(Vector2 direction)
    {
        for (int i = -1; i <= 1; i++)
        {
            Vector2 bulletDirection = Rotate(direction, i * 15f);
            Rigidbody2D bulletClone = (Rigidbody2D) Instantiate(bullet, transform.position, Quaternion.identity);
            bulletClone.velocity = bulletDirection * bulletSpeed;
        }
    }

    public void RadialFire(int bulletCount)
    {
        for(int i = 0; i < bulletCount; i++)
        {
            Vector2 bulletDirection = Rotate(Vector2.one, i * (360f / bulletCount));
            Rigidbody2D bulletClone = (Rigidbody2D) Instantiate(bullet, transform.position, Quaternion.identity);
            bulletClone.velocity = bulletDirection * bulletSpeed;
        }
    }

}
