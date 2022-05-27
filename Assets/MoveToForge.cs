using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToForge : MonoBehaviour
{
	public void OnClickMoveCameraToForge() {
		Transform viewPoint = GameObject.Find("ForgeViewPoint").transform;
		MoveCamera.MoveToPoint(viewPoint);
	}
}
