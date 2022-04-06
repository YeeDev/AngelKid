using UnityEngine;

namespace AK.Core
{
    [CreateAssetMenu(fileName = "New Control Settings", menuName = "Input Settings")]
    public class ControlSettingsSO : ScriptableObject
    {
        [SerializeField] bool useLeftSettings = false;
        [SerializeField] string leftySuffix = "(L)";
        [SerializeField] string horizontalInput = "Horizontal";
        [SerializeField] string verticalInput = "Vertical";
        [SerializeField] string fireInput = "Fire";
        [SerializeField] string jumpInput = "Jump";

        //Set In UI
        public bool SetSettings { set => useLeftSettings = value; }

        //Used in UpdateTutorial and Locally
        public bool GetSettings { get => useLeftSettings; }

        //Used in Controller
        public string GetHorizontal { get => useLeftSettings ? horizontalInput + leftySuffix : horizontalInput; }
        public string GetVertical { get => useLeftSettings ? verticalInput + leftySuffix : verticalInput; }
        public string GetFire { get => useLeftSettings ? fireInput + leftySuffix : fireInput; }
        public string GetJump { get => jumpInput; }
    }
}