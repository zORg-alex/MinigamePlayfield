using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAnvil : MonoBehaviour
{
    public void OnClickMoveCameraToAnvil() {
		Transform viewPoint = GameObject.Find("AnvilViewPoint").transform;
		MoveCamera.MoveToPoint(viewPoint);
	}
}
