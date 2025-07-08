using UnityEngine;
public class AnimationEventShoot : MonoBehaviour
{
    [SerializeField] private Character Character;

    public void CallThrow()
    {
        //Debug.Log("Shooted");
        Character.Throw();
    }
}

