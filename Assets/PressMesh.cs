using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMesh : MonoBehaviour
{
	[Button]    
    public static void PressMeshAnimation() {
        
        var mesh = GameObject.Find("Bone2");
        var mainFlame = GameObject.Find("MainFlame");
        var flames = GameObject.Find("Flames");

        float time = 1f;
        float downAngle = 78f;
        float scaleMainFlameChange = mainFlame.transform.localScale.y * 1.7f;
        float scaleFlamesChange = flames.transform.localScale.y * 1.7f;
        LeanTween.rotateX(mesh, -downAngle, time).setLoopPingPong(1);
        LeanTween.scaleY(mainFlame, scaleMainFlameChange, time*3).setLoopPingPong(1);
        LeanTween.scaleY(flames, scaleFlamesChange, time*3).setLoopPingPong(1);
    }
}
