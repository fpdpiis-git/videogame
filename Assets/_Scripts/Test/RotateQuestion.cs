using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateQuestion : MonoBehaviour {

    [SerializeField]
    private GameObject m_objToRotate = null;
	// Use this for initialization
	void Start () {
        RotateObj(m_objToRotate);

    }
	
    private void RotateObj ( GameObject objToRotate)
    {
        LeanTween.rotateZ(objToRotate, 90, 5.0f);
    }
	
}
