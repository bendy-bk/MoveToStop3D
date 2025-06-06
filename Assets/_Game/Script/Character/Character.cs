using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected Transform model;

    [Header("List Character")]
    private List<Character> characters = new();

    public Animator anim;
    private string currentAnim;
    [SerializeField] private float range;
    [SerializeField] private float size;
    private bool isAttack;
    private bool isMove;
    


    public override void OnInit()
    {
        
    }
    public override void OnDespawn()
    {
        
    }

    public virtual void Move(){}

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }

        return TF.position;

    }

    public void ChangeAnim(string newAnim)
    {
        if (currentAnim != newAnim)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangeWeapon()
    {

    }
    public void ChangeHat()
    {

    }
    public void OnHit()
    {

    }
    public void OnDeath()
    {

    }

    public void Attack()
    {
        Invoke(nameof(Throw), 0.4f);
    }

    public void Throw()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();
            Debug.Log(c.TF.position);
            characters.Add(c);
        }

        Debug.Log(characters.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();
            characters.Remove(c);
        }

        Debug.Log(characters.Count);

    }


}
