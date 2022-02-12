using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSkills : MonoBehaviour
{
    public GameObject panel;
    InputActions input;
    private void OnEnable() {
        input = new InputActions();
        input.Enable();
        input.UI.Cancel.performed += Cancel_performed; ;
    }

    private void Cancel_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        panel.SetActive(false);
    }

    public void ToggleSkillsPanel() {
        if (panel != null) panel.SetActive(!panel.activeSelf);
	}
}
