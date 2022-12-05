using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : BaseEntity
{
    static public Player player;
    protected Rigidbody2D rb;
    protected Collider2D cd;
    
    int JumpVelocity;
    int MaxSpeedX;
    int MaxSpeedY;
    float BaseMass;
    float acceleration;
    bool grounded;
    float attackDelay;
    int attackmode;
    GameObject bullet;
    [SerializeField]
    TMPro.TMP_Text text;

    //Item[] cargo;
    float MaxCargo;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb.mass = BaseMass;// + cargo.getsummasses()
        HPBar.instance.UpdateMaxHP(_maxHP);
        HPBar.instance.UpdateHP(_CurrentHP);
        text.text = "Weapon: melee";
    }

    protected override void Awake()
    {
        bullet = Resources.Load<GameObject>("Projectiles/BaseBullet");
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        Player.player = this;
        CombatCameraScript.instance.target = Player.player.transform;
}

    protected override void SetDefaults()
    {
        acceleration = 10;
        JumpVelocity = 35;
        MaxSpeedX = 100;
        MaxSpeedY = 1000;
        BaseMass = 1;
        MaxHP = 100;
        CurrentHP = 100;
    }

    // Update is called once per frame
    protected override void Update()
    {
        rb.angularVelocity = -(Mathf.Pow(Mathf.Abs(rb.rotation), 1.5f) * Mathf.Sign(rb.rotation));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x,-MaxSpeedX,MaxSpeedX),Mathf.Clamp(rb.velocity.y,-MaxSpeedY,MaxSpeedY));
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
            rb.rotation = -rb.velocity.x/MaxSpeedX*20f;
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackDelay <= 0)
            {
                if (attackmode == 0)
                {
                    foreach (var i in Physics2D.CircleCastAll(transform.position, 3, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 0.1f, LayerMask.GetMask("Unit")))
                        if (i.collider.gameObject.tag != "Player" && Vector2.Dot((i.transform.position - transform.position).normalized, ((Vector2)(CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized) > 0.75)
                            i.collider.GetComponent<BaseEntity>().TakeDamage(this.gameObject, 10);
                    attackDelay = 0.75f;
                }
                if (attackmode == 1)
                {
                    Projectile.Create(bullet, this.gameObject, transform.position, CombatCameraScript.instance.Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position, 50, 7, LayerMask.GetMask("Unit"));
                    attackDelay = 0.5f;
                }
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-acceleration,0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(acceleration, 0));
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            attackmode = (attackmode + 1) % 2;
            switch (attackmode)
            {
                case 0:
                    text.text = "Weapon: melee";
                    break;
                    case 1:
                    text.text = "Weapon: ranged";
                    break;
            }
        }
        base.Update();
        attackDelay -= Time.deltaTime;
    }

    public override void TakeDamage(GameObject inflictor,int damage)
    {
        if (!Iframelist.ContainsKey(inflictor))
        {
            HPBar.instance.UpdateMaxHP(_maxHP);
            HPBar.instance.UpdateHP(_CurrentHP);
        }
        base.TakeDamage(inflictor, damage);
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!grounded && Physics2D.CircleCast(transform.position, 0.45f, transform.rotation * Vector2.down, 0.5f, LayerMask.GetMask("Default")).collider != null)
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    protected void OnDestroy()
    {
        SceneLoader.instance.LoadScene("SampleScene");
    }
}
