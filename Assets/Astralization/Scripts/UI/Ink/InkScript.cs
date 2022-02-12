using Ink.Runtime;
using UnityEngine;

public class InkScript : MonoBehaviour
{
    [SerializeField]
    private TextAsset _dialogueJson;
    private Story _story;

    // Start is called before the first frame update
    void Start()
    {
        _story = new Story(_dialogueJson.text);
        Debug.Log(_story.Continue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
