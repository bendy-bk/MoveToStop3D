using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : GameUnit
{
    //Event
    public static event Action<Character> OnBotDeath;

    [Header("Tranform")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform character;
    [SerializeField] private Transform throwPoint;

    [Header("Target")]
    private List<Character> characters = new();
    private Vector3 fixedTarget ;

    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshPro textKill;
    [SerializeField] private int totalKill = 0;
    [SerializeField] private float timedelay;
    [SerializeField] private float size;

    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private GameObject CircleTarget;

    private Weapon weaponEquip;
    private string currentAnim;
    protected Coroutine throwCoroutine;

    public int CharacterCount => Characters.Count;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Transform Model { get => model; set => model = value; }
    public Transform ThrowPoint { get => throwPoint; set => throwPoint = value; }

    public Character TargetCharacter => Characters.Count > 0 ? Characters[0] : null;

    public Weapon WeaponEquip { get => weaponEquip; set => weaponEquip = value; }
    public Vector3 FixedTarget { get => fixedTarget; set => fixedTarget = value; }
    public int TotalKill { get => totalKill; set => totalKill = value; }
    public List<Character> Characters { get => characters; set => characters = value; }

    public override void OnInit() {}

    public virtual void Move() {}

    public override void OnDespawn() {}

    public void OnHit()
    {
        TotalKill++;
        UpSize();
        LevelManager.Instance.CheckWin(TotalKill);
    }

    public virtual void OnDeath()
    {
        if (this is Player)
        {
            GameManager.Instance.ChangeState(GameState.Pause); // 1. Pause trước
        }

        ChangeAnim(Constants.ANIM_DEAD); // 2. Rồi mới chơi anim
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f); // 3. Đợi anim chết chơi xong

        if (this is Player)
        {
            transform.localScale = Vector3.one * 1.3f;
            LevelManager.Instance.Lose(); // 4. Gọi Lose UI
        }
        else if (this is Bot)
        {
            OnBotDeath?.Invoke(this);
            if (CircleTarget != null)
                CircleTarget.SetActive(false);

            BotManger.Instance.Bots.Remove(this as Bot);
            SimplePool.Despawn(this);
        }

    }

    public void UpSize()
    {
        character.localScale += Vector3.one * 0.05f;

        if (textKill != null)
        {
            textKill.text = $"{TotalKill}";
        }
    }

    public void Attack()
    {      
        ChangeAnim(Constants.ANIM_ATTACK);
        // Nếu chưa có coroutine thì mới bắt đầu
        if (throwCoroutine == null)
        {
            throwCoroutine = StartCoroutine(DelayThrow());
        }
        
    }

    public void Throw()
    {
        if (Characters.Count > 0 && !IsMoving) {           
            FixedTarget = TargetCharacter.TF.position;
            weaponEquip.SetCharacterowner(this, TargetCharacter, FixedTarget);
            WeaponEquip.Shoot();
            isAttacking = true;
        } else
        {
            isAttacking = false;
            return;
        }
        
    }

    private IEnumerator DelayThrow()
    {
        yield return new WaitForSeconds(timedelay);
        Throw();
        throwCoroutine = null; // reset lại

        ChangeAnim(Constants.ANIM_IDLE);
        yield return null;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            Character c = other.GetComponent<Character>();
            Characters.Add(c);          
        }

        if (Characters.Count > 0 && TargetCharacter is Bot)
        {
            TargetCharacter.CircleTarget.SetActive(true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            Character exitingCharacter = other.GetComponent<Character>();
            Characters.Remove(exitingCharacter); // Remove Character vừa đi ra
            exitingCharacter.CircleTarget.SetActive(false);
            //Debug.Log("Taget remove: " + exitingCharacter.TF.position);
        }
    }


}
