using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public GameObject bullet;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    [SerializeField] private GameObject shootingPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("MagicBullet", startDelay, spawnInterval);
    }


    void MagicBullet()
    {
        Instantiate(bullet, new Vector3(shootingPoint.transform.position.x + 0.5f, shootingPoint.transform.position.y, 0), bullet.transform.rotation);
    }
}
