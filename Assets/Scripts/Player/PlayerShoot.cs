using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float bulletSpeed = 25f;

    [SerializeField] private Transform shootingPoint;
    
    [SerializeField] private float shootingWindup = 0.2f;
    [SerializeField] private float shootingDelay = 0.2f;
    // private float flippedPositionOfShootingPointX;
    //private float initialPositionOfShootingPointX;
    
    public bool shooting = false;
    [SerializeField] private InputAction shoot;

    [Header("Options Menu Controller Script")]
    [SerializeField] private OptionsMenuManager optionsMenuController;

    void Start()
    {
        shoot = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        // if(optionsMenuController.isOptionMenuOpen)
        //     return;

        if(shoot.WasPressedThisFrame() && !shooting) //left click & F-key -- DB
        {
            shooting = true;
            // Creates a brief pause between clicking shoot and actually shooting -- DB
            Invoke(nameof(Shoot), shootingWindup);
            // The delay until the next shot can be taken -- DB
            Invoke(nameof(AllowShooting), shootingDelay);
        }
    }

    void Awake()
    {
        // initialPositionOfShootingPointX = shootingPoint.localPosition.x;
        // flippedPositionOfShootingPointX = initialPositionOfShootingPointX * -1;
    }

    void Shoot()
    {
        // Get mouse position
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Direction from us to mouse
        // Vector3 shootDirection = (mousePosition - transform.position + new Vector3(0, 0.25f, 0)).normalized;
        // Direction to match player scale X -- Darren B.
        Vector3 shootDirection = transform.localScale;
        shooting = true;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        // Adding velocity on the X axis only.
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shootDirection.x, 0) * bulletSpeed;
        Destroy(bullet, 2f);

    }

    // Will do what it says on the can once invoked -- Darren B.
    private void AllowShooting()
    {
        shooting = false;
    }

    /// <summary>
    /// Flips the Shooting point's local position on the X depending on the if the player is moving LEFT or RIGHT
    /// </summary>
    /// <param name="inputDirection"></param>
    // public void FlipShootingPointPosition(float inputDirection)
    // {
    //     if(inputDirection > 0.1 && shootingPoint.localPosition.x != flippedPositionOfShootingPointX)
    //     {
    //         shootingPoint.localPosition = new Vector3(shootingPoint.localPosition.x * -1f, shootingPoint.localPosition.y, shootingPoint.localPosition.z);
    //     }
    //     else if(inputDirection < 0 && shootingPoint.localPosition.x != initialPositionOfShootingPointX)
    //     {
    //         shootingPoint.localPosition = new Vector3(shootingPoint.localPosition.x * -1f, shootingPoint.localPosition.y, shootingPoint.localPosition.z);
    //     }
        
    // }
    
}
