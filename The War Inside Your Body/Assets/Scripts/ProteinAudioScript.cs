using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinAudioScript : MonoBehaviour
{
    public AudioSource baseNote;
    public float transpose = 0;  // transpose in semitones


    public void CallbackTest(GameObject thiis, GameObject that) { Debug.Log("protein1: " + thiis.name + ". proetin2: " + that.name) ; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayPing()
    {
        //taken from aldonaletto's code snippet on unity answers
        //https://answers.unity.com/questions/141771/whats-a-good-way-to-do-dynamically-generated-music.html
        /* 
         * One more answer to help musical noobies: the note sequence is A-A#-B-C-C#-D-D#-E-F-F#-G-G#.
         * Notice that there are no semitones between B-C and E-F, but every other pair has semitones in between (the #notes). 
         * These sandwiched semitones correspond to the piano's small black keys, and the full notes are the white ones.
         * So, if you have an E and wants a G, you must change the pitch to 1.05946^3, since there are three semitones in between.
         * Finally, the scale is endless: after G# comes another A, but with exactly 2 times the previous A pitch, and so on.
        */

        // use pentatonic scale for pleasant intervals, chord for confirmation?
        // 0=c4, 2=d4, 4=e4, 7=g4, 9=a4, 12=c5, 14=d5

        //(it's offset by 9 semitones so it's easier to count from C4
        baseNote.pitch = Mathf.Pow(2, (transpose - 9) / 12.0f);
        baseNote.Play();

    }
}
