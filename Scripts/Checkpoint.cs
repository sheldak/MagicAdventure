using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Animator anim;

    public bool Checked;
    public bool setChecked;

    public float checkedTime;
    public bool CheckedChecked;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }


	void Update ()
    {
        if (Checked)
            setChecked = true;

        if (setChecked)
            CheckedChecked = true;

        anim.SetBool("Checked", setChecked);
        anim.SetBool("CheckedChecked", CheckedChecked);


    }
}
