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
    [SerializeField] public int totalKill = 0;
    [SerializeField] public float timedelay = 0.6f;
    [SerializeField] private float size;

    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private GameObject CircleTarget;

    private Weapon weaponEquip;
    private float angleY;
    private string currentAnim;
    private Vector3 dirAttack;
    protected Coroutine throwCoroutine;

    public int CharacterCount => characters.Count;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Transform Model { get => model; set => model = value; }
    public Transform ThrowPoint { get => throwPoint; set => throwPoint = value; }

    public Character TargetCharacter => characters.Count > 0 ? characters[0] : null;

    public Weapon WeaponEquip { get => weaponEquip; set => weaponEquip = value; }
    public Vector3 FixedTarget { get => fixedTarget; set => fixedTarget = value; }

    public override void OnInit() {
        
    }

    public virtual void Move() { }

    public override void OnDespawn()
    {
    }

    public void OnHit()
    {
        totalKill++;
        UpSize();
        LevelManager.Instance.CheckWin(totalKill);
    }

    public virtual void OnDeath()
    {
        ChangeAnim(Constants.ANIM_DEAD);
        StartCoroutine(HandleDeath());

    }
    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1f);

        if (this is Player)
        {
            LevelManager.Instance.Lose();
        }
        else if (this is Bot)
        {
            OnBotDeath?.Invoke(this);
            characters.Remove(this);
            //SimplePool.Despawn(this);
            gameObject.SetActive(false);
            BotManger.Instance.Bots.Remove(this as Bot);

            if (CircleTarget != null)
                CircleTarget.SetActive(false);
        }
    }

    public void UpSize()
    {
        character.localScale += Vector3.one * 0.05f;

        if (textKill != null)
        {
            textKill.text = $"{totalKill}";
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
        if (characters.Count > 0 && !IsAttacking && !IsMoving) {
            isAttacking = true;
            FixedTarget = TargetCharacter.TF.position;
            weaponEquip.SetCharacterowner(this, TargetCharacter, FixedTarget);
            WeaponEquip.Shoot();
        } else
        {
            isAttacking = false;
            return;
        }

    }

    public void AngelFly(Vector3 dirAt)
    {
        angleY = Mathf.Atan2(dirAt.x, dirAt.z) * Mathf.Rad2Deg;
    }

    private IEnumerator DelayThrow()
    {
        yield return new WaitForSeconds(timedelay);
        Throw();
        throwCoroutine = null; // reset lại
    }

    public void RemoveTarget(Character c)
    {
        characters.Remove(c);
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
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();
            characters.Add(c);          
        }

        if (characters.Count > 0 && TargetCharacter is Bot)
        {
            TargetCharacter.CircleTarget.SetActive(true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character exitingCharacter = other.GetComponent<Character>();
            characters.Remove(exitingCharacter); // Remove Character vừa đi ra
            exitingCharacter.CircleTarget.SetActive(false);
            Debug.Log("Taget remove: " + exitingCharacter.TF.position);
        }
    }


}
