using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : GameUnit
{
    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public Transform model;

    [Header("List Character")]
    private List<Character> characters = new();

    public Animator anim;
    private string currentAnim;
    [SerializeField] private float range;
    [SerializeField] private float size;

    public bool isAttack = false;
    public bool isMove = false;
    public bool isThrowing = false;

    [SerializeField] private Transform throwPoint;
    private Vector3 modelRotate;

    public int CharacterCount => characters.Count;

    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {

    }

    public virtual void Move() { }

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
        characters.Remove(this); // Cẩn thận nếu dùng trong list chung
        isThrowing = false;
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        Invoke(nameof(Throw), 0.4f);
    }

    public void Throw()
    {
        // Không tấn công nếu đang di chuyển hoặc không có mục tiêu
        if (isMove || characters.Count == 0)
        {
            isAttack = false;
            return;
        }
        // Lấy mục tiêu đầu tiên còn hợp lệ
        var target = characters.FirstOrDefault();
  
        if (target != null && isAttack && !isThrowing)
        {
            modelRotate = target.TF.position - TF.position;
            model.forward = modelRotate;

            isThrowing = true;

            // Lấy mũi tên từ pool và gắn vào throwPoint
            GameObject arrowObj = ObjectPoolManager.Instance.SpawnFromPool(PoolType.Bullet, throwPoint);

            // Đặt vị trí nếu cần (nếu prefab không auto gắn vị trí throwPoint)
            arrowObj.transform.position = throwPoint.position;

            // Khởi tạo lại mũi tên
            Arrow pooledArrow = arrowObj.GetComponent<Arrow>();

            pooledArrow.SetTargetFly(this, target, modelRotate); // Truyền Transform mục tiêu

            isAttack = false;
        }

    }

    public void RemoveTarget(Character c)
    {
        characters.Remove(c);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character c = other.GetComponent<Character>();
            characters.Add(c);
            Debug.Log(c.TF.position);
        }
        //Debug.Log(characters.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character exitingCharacter = other.GetComponent<Character>();
            characters.Remove(exitingCharacter); // Remove Character vừa đi ra
        }

        //Debug.Log(characters.Count);
    }

}
