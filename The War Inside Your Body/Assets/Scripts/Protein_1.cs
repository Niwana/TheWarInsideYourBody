using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protein_1 : MonoBehaviour
{
    private Vector3 lastPos;
    private bool dockingAnimation = true;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Play when the protein should fly in
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space");
            anim.SetTrigger("playFlyIn");
        }

        if (!IsMoving() && dockingAnimation)
        {
            anim.SetTrigger("playDocking");
            anim.SetTrigger("playFlyAway");
            anim.SetTrigger("playActivated");


            dockingAnimation = false;
        }
    }

    private bool IsMoving()
    {
        Vector3 currentPos = gameObject.transform.position;

        if (currentPos == lastPos)
            return false;
    
        lastPos = currentPos;

        return true;
        
    }
}
