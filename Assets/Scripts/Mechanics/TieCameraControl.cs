using CharacterControl;
using UnityEngine;

namespace Mechanics
{
    public class TieCameraControl : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private Transform Companion;

        private float _cameraPositionX;
        private float _cameraPositionY;
        private float _cameraPositionZ;

        private void LateUpdate()
        {
            _cameraPositionX = (Player.position.x + Companion.position.x) / 2;

            _cameraPositionY = (Player.position.y + Companion.position.y) / 4;

            _cameraPositionZ = (Player.position.z + Companion.position.z) / 2;

            transform.position = new Vector3(_cameraPositionX, _cameraPositionY, _cameraPositionZ);
        }
    }
}