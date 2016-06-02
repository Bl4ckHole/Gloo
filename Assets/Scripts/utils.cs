using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils{
    
    public static class GlooConstants
    {
        public static KeyCode keyLeft = KeyCode.Q;
        public static KeyCode keyRight = KeyCode.D;
        public static KeyCode keyJump = KeyCode.Space;
        public static KeyCode keyDivide = KeyCode.E;
        public static KeyCode keySpecial = KeyCode.Return;
        // gloo absorbtion is consideres as his special capacity  so uses THE SAME key
        public static KeyCode keyAbsorb = keySpecial;
        public static KeyCode keyActivate = KeyCode.A;
        public static KeyCode keyReset = KeyCode.R;

        public static Dictionary<string, KeyCode> getKeys()
        {
            Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

            keys.Add("Left", keyLeft);
            keys.Add("Right", keyRight);
            keys.Add("Jump", keyJump);
            keys.Add("Divide", keyDivide);
            keys.Add("Special", keySpecial);
            keys.Add("Activate", keyActivate);
            keys.Add("Reset", keyDivide);

            return keys;
        }

        public static void setKeys(Dictionary<string, KeyCode> newKeys)
        {
            KeyCode keyLeft = newKeys["Left"];
            KeyCode keyRight = newKeys["Right"];
            KeyCode keyJump = newKeys["Jump"];
            KeyCode keyDivide = newKeys["Divide"];
            KeyCode keySpecial = newKeys["Special"];
            // gloo absorbtion is consideres as his special capacity  so uses THE SAME key
            KeyCode keyAbsorb = keySpecial;
            KeyCode keyActivate = newKeys["Activate"];
            KeyCode keyReset = newKeys["Reset"];
        }
    }
}