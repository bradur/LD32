using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour {

    int count;
    Animator animator;

    Text text;

    void Awake()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCount(int newCount)
    {
        this.count = newCount;
        UpdateDisplay();
    }

    public void Animate(int newCount)
    {
        if (newCount > 0)
        {
            count += 1;
            animator.SetTrigger("positive");
        }
        else if (newCount < 0)
        {
            count -= 1;
            animator.SetTrigger("negative");
        }
    }

    public void AnimateTitle(string title)
    {
        text.text = title;
        animator.SetTrigger("title");
    }

    public void UpdateDisplay()
    {
        text.text = count.ToString();
    }

}
