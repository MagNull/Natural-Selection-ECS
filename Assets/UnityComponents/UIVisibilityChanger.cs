using System;
using UnityEngine;

namespace UnityComponents
{
    [RequireComponent(typeof(CameraMover))]
    public class UIVisibilityChanger : MonoBehaviour
    {
        [SerializeField] private Canvas _ui;
        private CameraMover _cameraMover;

        private void Awake()
        {
            _cameraMover = GetComponent<CameraMover>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeUIVisibility();
            }
        }

        public void ChangeUIVisibility()
        {
            bool state = !_ui.gameObject.activeSelf;
            _ui.gameObject.SetActive(state);
            Cursor.visible = state;
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            _cameraMover.enabled = !state;
        }
    }
}
