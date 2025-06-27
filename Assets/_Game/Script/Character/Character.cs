using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Character : GameUnit
{
    public static event Action<Character> OnBotDeath;

    [Header("Tranform")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform character;
    [SerializeField] private Transform throwPoint;

    [Header("List")]
    private List<Character> characters = new();

    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshPro textKill;
    [SerializeField] public int totalKill = 0;
    [SerializeField] public float timedelay = 0.6f;
    [SerializeField] private float size;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isThrowing = false;

    private GameObject weaponSpawnVS;
    private float angleY;
    private string currentAnim;
    private Vector3 dirAttack;
    protected Coroutine throwCoroutine;

    public int CharacterCount => characters.Count;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsThrowing { get => isThrowing; set => isThrowing = value; }
    public Transform Model { get => model; set => model = value; }
    public Transform ThrowPoint { get => throwPoint; set => throwPoint = value; }
    public GameObject WeaponSpawnVS { get => weaponSpawnVS; set => weaponSpawnVS = value; }

    public WeaponSO WeaponCurrent => EquipmentManager.Instance.GetWeaponEquip();

    public override void OnInit() { }

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
        if (this is Player)
        {
            LevelManager.Instance.Lose();
        }
        if (this is Bot)
        {
            OnBotDeath?.Invoke(this); // Gửi sự kiện
            characters.Remove(this); // Cẩn thận nếu dùng trong list chung
            SimplePool.Despawn(this);
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

    //public void GetDistanceToCircle()
    //{
    //    // Lấy điểm gần nhất trên collider so với vị trí player
    //    Vector3 closestPoint = circleCollider.ClosestPoint(TF.position);

    //    // Khoảng cách từ player (tâm) đến mép vòng tròn (bán kính thực tế)
    //    radius = Vector3.Distance(TF.position, closestPoint);

    //}

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
        if (this is Player)
        {
            Debug.Log("player Throw()");
        }

        if (this is Bot)
        {
            Debug.Log("bot Throw()");
        }

        // Không tấn công nếu đang di chuyển hoặc không có mục tiêu
        if (IsMoving || characters.Count == 0) return;

        // Lấy mục tiêu đầu tiên còn hợp lệ
        var target = characters.FirstOrDefault();

        if (target != null && !IsAttacking && !IsThrowing)
        {

            dirAttack = target.TF.position - TF.position;
            Model.forward = dirAttack; 
            Debug.Log("Throw point" + throwPoint.position);
            // Lấy mũi tên từ pool và gắn vào throwPoint  "Bug /// spawn khong dung position va rotation"
            BulletBase bullet = SimplePool.Spawn<BulletBase>(WeaponCurrent.PoolType, Vector3.zero, Quaternion.identity);
            // Xac dinh goc bay bullet
            AngelFly(dirAttack);
            // Set angel bullet and position
            bullet.transform.position = throwPoint.position;
            bullet.transform.rotation = Quaternion.Euler(90, angleY, 0);

            Debug.Log(target.TF.position);
            Debug.Log(dirAttack);

            // set target cho bullet
            bullet.SetTargetFly(this, target, dirAttack);

            IsThrowing = true;
            IsAttacking = true;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character exitingCharacter = other.GetComponent<Character>();
            characters.Remove(exitingCharacter); // Remove Character vừa đi ra
            Debug.Log("Taget remove: " + exitingCharacter.TF.position);
        }
    }

}
