using UnityEngine;
using UnityEngine.UI;
using AK.Core;

namespace AK.UI
{
    public class UpdateTutorial : MonoBehaviour
    {
        [TextArea(1, 3)] [SerializeField] string rightControlText = null;
        [TextArea(1, 3)] [SerializeField] string leftControlText = null;
        [SerializeField] Text tutorialText = null;
        [SerializeField] ControlSettingsSO controlSettings = null;

        private void Awake()
        {
            tutorialText.text = controlSettings.GetSettings ? leftControlText : rightControlText;
        }
    }
}