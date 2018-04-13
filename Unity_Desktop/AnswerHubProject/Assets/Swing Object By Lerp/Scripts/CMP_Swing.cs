using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwingObjectByLerp
{
    public class CMP_Swing : MonoBehaviour
    {
        [Tooltip("How fast you want the swing to swing")]
        public float SpeedOfSwing = .001f;
        [Tooltip("The direction that we want to swing in")]
        public Vector3 DirectionOfSwing;
        [Tooltip("The string representation of the key command")]
        public string ActionString = "Fire1";
        
        //The original rotation of the object
        private Quaternion OriginQuat;
        private Quaternion ForwardQuat;
        private Quaternion BackQuat;
        //We set alpha to .5 so that we're at a rest position when we start the script
        private float alpha = .5f;
        private float direction = -1;
        private bool previousKeyState = false;

        void Awake()
        {
            //Get our current rotation in case we want to reset the rotation at any point.
            OriginQuat = transform.rotation;

            //Get our quarternions based on our current transform. We get the positive and the negative so that we can lerp between them.
            ForwardQuat = Quaternion.FromToRotation(transform.forward, DirectionOfSwing);
            BackQuat = Quaternion.FromToRotation(transform.forward, -DirectionOfSwing);
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //Get an up/down key
            var keystate = Input.GetButton(ActionString);

            if (keystate) //If we've pressed our action button
            {
                if(!previousKeyState)
                {
                    //Flip the direction
                    direction *= -1;
                }
                //Set the objects rotation to the lerp of the forward and back quartenions
                transform.rotation = Quaternion.Lerp(ForwardQuat, BackQuat, alpha);
                //Add to our alpha so we can continue moving and multiply it by direction so that we can determine which direction it rotates
                alpha += Time.deltaTime * SpeedOfSwing * direction;
                //Clamp the alpha between 0 and 1 to ensure that if we hold a key down when alpha is 1 or 0, alpha will never go beyond those values
                alpha = Mathf.Clamp01(alpha);
            }
            previousKeyState = keystate;
        }
    }
}