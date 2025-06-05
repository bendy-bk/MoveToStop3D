using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraFollower : MonoBehaviour 
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
    }

}

