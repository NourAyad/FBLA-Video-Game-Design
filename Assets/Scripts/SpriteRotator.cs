using UnityEngine;
using System.Collections;

public class SpriteRotator : MonoBehaviour {

    private int stage;
    public int speed;
    private int zones;
    private SpriteRenderer renderer;
	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
        stage = 1;
        zones = speed / 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Rotator(zones);
	}

    void Rotator(int zone)
    {
        int zone2 = zone * 2;
        int zone3 = zone * 3;

        if (stage < zone)
        {
            renderer.flipX = false;
            renderer.flipY = false;
        }
        else if (zone <= stage && stage < zone2)
        {
            renderer.flipX = true;
            renderer.flipY = false;

        }
        else if (zone2 <= stage && stage < zone3)
        {
            renderer.flipX = true;
            renderer.flipY = true;

        }
        else if (zone3 <= stage && stage < speed)
        {
            renderer.flipX = false;
            renderer.flipY = true;

        }

        stage += 1;
        if (stage == speed)
        {
            stage = 1;
        }
    }
}
