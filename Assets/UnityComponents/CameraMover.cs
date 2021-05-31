using UnityEngine;

namespace UnityComponents
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _sensitivity = 1;
        [SerializeField] private float _boost;
        private float _rotX;
        private float _rotY;

        private void Start()
        {
            _rotX = transform.rotation.x;
            _rotY = transform.rotation.y;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) _speed += _boost;
            if (Input.GetKeyUp(KeyCode.LeftShift)) _speed -= _boost;
            float moveX = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
            _rotY += Input.GetAxis("Mouse X") * _sensitivity;
            _rotX -= Input.GetAxis("Mouse Y") * _sensitivity;
        
            transform.rotation = Quaternion.Euler(_rotX, _rotY, 0);

            transform.position += transform.TransformDirection(new Vector3(moveX, 0, moveZ));

        }
    }
}
