using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAnvil : MonoBehaviour
{
	public PositionCollection pc;
	public void OnClickMoveCameraToAnvil() {
		MoveCamera.MoveToPoint(pc.GetPoint("AnvilViewPoint", out var rotation), rotation);
	}
}
