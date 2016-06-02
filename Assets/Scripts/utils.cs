using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils{

    public static class GlooConstants
    {
        public static KeyCode defaultLeft = KeyCode.Q;
        public static KeyCode defaultRight = KeyCode.D;
        public static KeyCode defaultJump = KeyCode.Space;
        public static KeyCode defaultDivide = KeyCode.E;
        public static KeyCode defaultSpecial = KeyCode.Return;
        // gloo absorbtion is consideres as his special capacity  so uses THE SAME key
        public static KeyCode defaultAbsorb = keySpecial;
        public static KeyCode defaultActivate = KeyCode.A;
        public static KeyCode defaultReset = KeyCode.R;


        public static KeyCode keyLeft = defaultLeft;
        public static KeyCode keyRight = defaultRight;
        public static KeyCode keyJump = defaultJump;
        public static KeyCode keyDivide = defaultDivide;
        public static KeyCode keySpecial = defaultSpecial;
        // gloo absorbtion is consideres as his special capacity  so uses THE SAME key
        public static KeyCode keyAbsorb = defaultAbsorb;
        public static KeyCode keyActivate = defaultActivate;
        public static KeyCode keyReset = defaultReset;

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

        public static void setDefaultKeys(Dictionary<string, KeyCode> newKeys)
        {
            KeyCode keyLeft = defaultLeft;
            KeyCode keyRight = defaultRight;
            KeyCode keyJump = defaultJump;
            KeyCode keyDivide = defaultDivide;
            KeyCode keySpecial = defaultSpecial;
            // gloo absorbtion is consideres as his special capacity  so uses THE SAME key
            KeyCode keyAbsorb = defaultAbsorb;
            KeyCode keyActivate = defaultActivate;
            KeyCode keyReset = defaultReset;
        }
    }
}
