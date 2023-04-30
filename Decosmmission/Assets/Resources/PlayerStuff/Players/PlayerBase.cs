using System.Collections;
using System.Collections.Generic;
using System;
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
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;

    public AbstractUpgrade[] baseUpgrades = new AbstractUpgrade[] {new BasicHpUpgrade()};
    public List<(AbstractUpgrade, AbstractUpgrade)> availableUpgrades = new List<(AbstractUpgrade, AbstractUpgrade)>();

    //Item[] cargo;
    float MaxCargo;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CombatCameraScript.instance.target = Player.player.transform;
        foreach (var upgrade in baseUpgrades)
        {
            if (upgrade.unlocked && upgrade.enabled)
            upgrade.OnStartBase(this);
        }
        rb.mass = BaseMass;// + cargo.getsummasses()
        if (CurrentHP == 0)
            CurrentHP = MaxHP;
        if (CombatUiManager.instance != null)
        {
            CombatUiManager.UpdateMaxHP(MaxHP);
            CombatUiManager.UpdateHP(_CurrentHP);
        }
    }

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerBase.player = this;
        rb.mass = BaseMass;
    }

    protected override void SetDefaults()
    {
        acceleration = 10;
        JumpVelocity = 35;
        MaxSpeedX = 20;
        MaxSpeedY = 200;
        BaseMass = 1;
        MaxHPBase = 100;
        MaxHPmultiplyer = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        rb.angularVelocity = -(Mathf.Pow(Mathf.Abs(rb.rotation), 1.5f) * Mathf.Sign(rb.rotation));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeedX, MaxSpeedX), Mathf.Clamp(rb.velocity.y, -MaxSpeedY, MaxSpeedY));
        anim.SetFloat("VerticalSpeed", rb.velocity.y / MaxSpeedY);
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.x / MaxSpeedX));
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
            //rb.rotation = -rb.velocity.x / MaxSpeedX * 20f;
            grounded = false;
            anim.ResetTrigger("Jump");
        }
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            rb.AddForce(new Vector2(-acceleration, 0) * Time.deltaTime*500);
        }
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            rb.AddForce(new Vector2(acceleration, 0) * Time.deltaTime*500);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ApproachedObject != null)
                ApproachedObject.Interact();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
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
            if (CombatUiManager.instance != null)
            {
                CombatUiManager.UpdateMaxHP(MaxHP);
                CombatUiManager.UpdateHP(_CurrentHP);
            }
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
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}
