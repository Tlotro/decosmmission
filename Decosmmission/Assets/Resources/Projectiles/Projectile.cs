using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D cd;
    public GameObject shooter;
    private Vector3 shotposition;
    public int Damage;
    public int TargetLayer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetDefaults();
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    protected virtual void SetDefaults()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if((transform.position - shotposition).magnitude > 300)
            Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 0)
            Destroy(this.gameObject);
        if ((TargetLayer & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<BaseEntity>().TakeDamage(this.gameObject, this.Damage);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basePrefab"> the prefab that has to be instantiated, this must be a projectile prefab</param>
    /// <param name="shooter"> the GameObject that instantiated the prefab</param>
    /// <param name="position"> the position from which the projectile starts its way</param>
    /// <param name="direction">the direction in which the projectile starts its way, local position</param>
    /// <param name="speed"> the fixed parameter of speed, at which the projectile travels</param>
    /// <param name="Damage"> the damage the projectile does</param>
    /// <param name="LayerMask"> the LayerMask that the projectile targets</param>
    /// <returns></returns>
    public static GameObject Create(GameObject basePrefab, GameObject shooter, Vector3 position, Vector3 direction, int speed, int Damage, int LayerMask)
    {
        GameObject item = Instantiate(basePrefab, position, Quaternion.FromToRotation(Vector3.right, direction));
        Projectile projectile = item.GetComponent<Projectile>();
        projectile.shooter = shooter;
        projectile.rb.velocity = direction.normalized * speed;
        projectile.Damage = Damage;
        projectile.TargetLayer = LayerMask;
        projectile.shotposition = shooter.transform.position;
        return item;
    }
}
