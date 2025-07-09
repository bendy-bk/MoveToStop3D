using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : GameUnit
{
    /// <summary>
    /// Event for bot die
    /// </summary>
    public static event Action<Character> RemoveBotDeaded;

    [Header("Tranform")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform character;
    [SerializeField] private Transform throwPoint;

    [Header("Target")]
    private List<Character> characters = new();
    private Vector3 fixedTarget;

    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshPro textKill;
    [SerializeField] private int totalKill = 0;
    [SerializeField] private float timedelay = 1f;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] private bool isMoving = false;
    
    private Weapon weaponCurrent;
    private string currentAnim;
    protected Coroutine attackCoroutine;
    private Character targetCharacter;


    public int CharacterCount => Characters.Count;
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Transform Model { get => model; set => model = value; }
    public Transform ThrowPoint { get => throwPoint; set => throwPoint = value; }
    public Weapon WeaponEquip { get => weaponCurrent; set => weaponCurrent = value; }
    public Vector3 FixedTarget { get => fixedTarget; set => fixedTarget = value; }
    public int TotalKill { get => totalKill; set => totalKill = value; }
    public List<Character> Characters { get => characters; set => characters = value; }
    public Character TargetCharacter { get => targetCharacter; set => targetCharacter = value; }

    protected void HandleTargetDeath(Character deadChar)
    {
        if (Characters.Contains(deadChar))
        {
            Characters.Remove(deadChar);
            Debug.Log($"{name} removed dead character {deadChar.name}");
        }
    }

    public override void OnInit() {}

    public virtual void Move() {}

    public override void OnDespawn() {}

    public void OnHit()
    {
        TotalKill++;
        UpSize();
        LevelManager.Instance.CheckWin(TotalKill);
    }

    public void UpSize()
    {
        character.localScale += Vector3.one * 0.01f;

        if (textKill != null)
        {
            textKill.text = $"{TotalKill}";
        }
    }

    public void Attack()
    {
        //model.LookAt(targetCharacter.TF);
        ChangeAnim(Constants.ANIM_ATTACK);
    }

    private IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(1f); // delay ngắn để giữ anim Attack thêm chút
        //Debug.Log("idle");
        ChangeAnim(Constants.ANIM_IDLE); // chuyển về idle sau khi đánh xong
        yield return new WaitForSeconds(timedelay); // delay tiếp theo
    }

    public void Throw()
    {  
        if (Characters.Count > 0 && !IsMoving)
        {
            if (this is Bot)
            {
                Debug.Log("bot shoot");
            }
            TargetCharacter = Characters.Count > 0 ? Characters[0] : null;
            FixedTarget = TargetCharacter.TF.position;
            weaponCurrent.SetCharacterowner(this, TargetCharacter, FixedTarget);
            WeaponEquip.Shoot();

            StartCoroutine(WaitForNextAttack());
        }
    }

    public void RemoveTarget(Character c)
    {
        Characters.Remove(c);
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

    public virtual void ResetCharacter() {}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            Character c = other.GetComponent<Character>();
            Characters.Add(c);
        }

        if (Characters.Count > 0 && TargetCharacter is Bot botTarget && botTarget.CircleTarget != null)
        {
            //Debug.Log("active Circle target");
            botTarget.CircleTarget.SetActive(true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            Character exitingCharacter = other.GetComponent<Character>();
            Characters.Remove(exitingCharacter); // Remove Character vừa đi ra
            if (exitingCharacter is Bot bot && bot.CircleTarget != null)
            {
                //Debug.Log("inactive Circle target");
                bot.CircleTarget.SetActive(false);
            }
            //Debug.Log("Taget remove: " + exitingCharacter.TF.position);
        }
    }


}
