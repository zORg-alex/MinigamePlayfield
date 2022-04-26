using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSkillsPanel : MonoBehaviour
{
    public void TogglePanel() {
        gameObject.SetActive(!gameObject.activeSelf);
	}
}
