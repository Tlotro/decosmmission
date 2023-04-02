using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : BaseEntity
{
    static public PlayerBase player;
    protected Rigidbody2D rb;
    protected Collider2D cd;

    [HideInInspector]
    public int JumpVelocity;
    [HideInInspector]
    public int MaxSpeedX;
    [HideInInspector]
    public int MaxSpeedY;
    [HideInInspector]
    public float BaseMass;
    [HideInInspector]
    public float acceleration;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public Interactable ApproachedObject;

    //Item[] cargo;
    float MaxCargo;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        HPBar.UpdateMaxHP(_maxHP);
        HPBar.UpdateHP(_CurrentHP);
        CombatCameraScript.instance.target = Player.player.transform;
        rb.mass = BaseMass;// + cargo.getsummasses()
    }

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        PlayerBase.player = this;
        rb.mass = BaseMass;
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
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeedX, MaxSpeedX), Mathf.Clamp(rb.velocity.y, -MaxSpeedY, MaxSpeedY));
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
            rb.rotation = -rb.velocity.x / MaxSpeedX * 20f;
            grounded = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-acceleration, 0) * Time.deltaTime*500);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(acceleration, 0) * Time.deltaTime*500);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ApproachedObject != null)
                ApproachedObject.Interact();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(LayerMask.NameToLayer("Player"));
            Debug.Log(LayerMask.NameToLayer("Platforms"));
            Physics2D.IgnoreLayerCollision(7,3,true);
        }
        if (Input.GetKeyUp(KeyCode.S))
            Physics2D.IgnoreLayerCollision(7, 3, false);
        base.Update();
    }

    public override void TakeDamage(GameObject inflictor, int damage)
    {
        if (!Iframelist.ContainsKey(inflictor))
        {
            base.TakeDamage(inflictor, damage);
            HPBar.UpdateMaxHP(_maxHP);
            HPBar.UpdateHP(_CurrentHP);
        }
    }

    public override void Death()
    {
        base.Death();
        SceneLoader.instance.LoadScene("Main Menu");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!grounded && Physics2D.CircleCast(transform.position, 0.4f, transform.rotation * Vector2.down, 0.5f, LayerMask.GetMask("Default", "Platforms")).collider != null)
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}
