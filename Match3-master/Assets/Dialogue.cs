using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Match3
{
    public class Dialogue : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        public string[] lines;
        public float textSpeed;

        private int index;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void StartDialogue()
        {
            index = 0;
        }
        IEnumerator TypeLine()
        {

        }
    }
}
