using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSupplies : MonoBehaviour
{
	public void OnClickMoveCameraToSupplies()
	{
		Transform viewPoint = GameObject.Find("SuppliesViewPoint").transform;
		MoveCamera.MoveToPoint(viewPoint);
	}
}
