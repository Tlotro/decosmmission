using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class StaticUnit : BaseEntity
{
    [HideInInspector]
    protected Collider2D cd; 
    [HideInInspector]
    public Vector3 savedposition; 
    [HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public int detectionRange;

    protected override void Awake()
    {
        base.Awake();
        cd = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        savedposition = transform.position;
        InvokeRepeating("DetectTargets", 0.0f, 0.5f);
        if (CurrentHP == 0)
            CurrentHP = MaxHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Target != null)
            savedposition = Target.transform.position;
        TargetRetentionCheck();
        AI();
    }

    protected virtual void FixedUpdate()
    {
        FixedAI();
    }

    /// <summary>
    /// </summary>
    protected virtual void AI()
    {

    }

    /// <summary>
    /// override this when you need physics
    /// </summary>
    protected virtual void FixedAI()
    {

    }


    /// <summary>
    /// Used for target detection by almost every unit. Override this only if you know what you are doing
    /// Sets the "Target" variable of the unit
    /// Please, use if(Target==null) check.
    /// </summary>
    protected virtual void DetectTargets()
    {
        if (Target == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange,LayerMask.GetMask("Player"));
            foreach (Collider2D collider in colliders)
            {
                if (!Physics2D.LinecastAll(transform.position, collider.gameObject.transform.position).Any<RaycastHit2D>(x => x.collider.gameObject.layer == 0))
                {
                    Target = collider.gameObject;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Used for target retention by every unit. Override this only if you know what you are doing
    /// checks if the "Target" of the unit is still available (by default in range and not obstructed) and sets it to null if not
    /// Please, use if(Target!=null) check.
    /// </summary>
    protected virtual void TargetRetentionCheck()
    {
        if (Target != null)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) > detectionRange || Physics2D.LinecastAll(transform.position, Target.transform.position).Any<RaycastHit2D>(x => x.collider.gameObject.layer == 0))
            {
                Target = null;
            }
        }
    }
}
