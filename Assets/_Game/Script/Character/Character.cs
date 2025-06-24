using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Character : GameUnit
{
    public static event Action<Character> OnBotDeath;
    

    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform model;
    [SerializeField] private Transform character;

    [Header("List Character")]
    private List<Character> characters = new();


    [SerializeField] private Animator anim;
    private string currentAnim;
    private Vector3 modelRotate;

    [SerializeField] public int totalKill = 0;
    [SerializeField] private Transform throwPoint;

    [SerializeField] private TextMeshPro textKill;
    [SerializeField] private float size;
    [SerializeField] private float radius;

    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isThrowing = false;

    private GameObject weaponSpawnVS;
    private WeaponSO weaponcurrent;

    
    public int CharacterCount => characters.Count;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsThrowing { get => isThrowing; set => isThrowing = value; }
    public Transform Model { get => model; set => model = value; }
    public float Radius { get => radius; set => radius = value; }
    public Transform ThrowPoint { get => throwPoint; set => throwPoint = value; }
    public GameObject WeaponSpawnVS { get => weaponSpawnVS; set => weaponSpawnVS = value; }
    public WeaponSO WeaponCurrent => EquipmentManager.Instance.GetWeaponEquip();

    public override void OnInit() {}

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
        if (this is Player)
        {
            LevelManager.Instance.Lose();
        }
        if (this is Bot) {
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

        //if (this is Player)
        //{
        //    Debug.Log("Player");
        //}

        //if (this is Bot)
        //{
        //    Debug.Log("Bot");
        //}

        Invoke(nameof(Throw), 0.4f);
    }

    public void Throw()
    {
        // Không tấn công nếu đang di chuyển hoặc không có mục tiêu
        if (IsMoving || characters.Count == 0)
        {
            return;
        }
        // Lấy mục tiêu đầu tiên còn hợp lệ
        var target = characters.FirstOrDefault();

        if (target != null && !IsAttacking && !IsThrowing)
        {
            modelRotate = target.TF.position - TF.position;
            Model.forward = modelRotate;

            // Lấy mũi tên từ pool và gắn vào throwPoint

            //GameObject arrowObj = ObjectPoolManager.Instance.SpawnFromPool(PoolType.Bullet, ThrowPoint);
            BulletBase bullet = SimplePool.Spawn<BulletBase>(WeaponCurrent.PoolType, ThrowPoint.position, Quaternion.identity);

            bullet.SetTargetFly(this, target, modelRotate);

            IsThrowing = true;
            IsAttacking = true;
        }

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

    public void ChangeHat()
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
        }

    }

}
