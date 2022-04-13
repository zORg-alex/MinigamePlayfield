using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayOnStartUp : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        yield return null;
        yield return null;
        GetComponent<VisualEffect>().Play();
    }

 
}
