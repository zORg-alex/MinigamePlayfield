using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSupplies : MonoBehaviour
{
	public PositionCollection pc;
	public void OnClickMoveCameraToSupplies()
	{
		MoveCamera.MoveToPoint(pc.GetPoint("SuppliesViewPoint", out var rotation), rotation);
	}
}
