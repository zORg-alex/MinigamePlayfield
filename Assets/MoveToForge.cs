using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToForge : MonoBehaviour
{
	public PositionCollection pc;
	public void OnClickMoveCameraToForge() {
		MoveCamera.MoveToPoint(pc.GetPoint("ForgeViewPoint", out var rotation), rotation);
	}
}
