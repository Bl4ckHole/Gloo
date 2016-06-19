using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils{

    public static class GlooConstants
    {
        private static KeyCode defaultLeft = KeyCode.Q;
        private static KeyCode defaultRight = KeyCode.D;
        private static KeyCode defaultJump = KeyCode.Space;
        private static KeyCode defaultDivide = KeyCode.E;
        private static KeyCode defaultSpecial = KeyCode.S;
        // gloo absorbtion is consideres as his special capacity  so uses THE SAME key
        private static KeyCode defaultAbsorb = defaultSpecial;
        private static KeyCode defaultActivate = KeyCode.A;
        private static KeyCode defaultReset = KeyCode.R;



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

            keys.Add("DEPLACEMENT GAUCHE", keyLeft);
            keys.Add("DEPLACEMENT DROITE", keyRight);
            keys.Add("SAUT", keyJump);
            keys.Add("SE DIVISER", keyDivide);
            keys.Add("CAPACITE SPECIALE", keySpecial);
            keys.Add("ACTIONNER OBJET", keyActivate);
            keys.Add("RETOUR AU DERNIER POINT DE CONTROLE", keyReset);

            return keys;
        }

        public static void setKeys(Dictionary<string, KeyCode> newKeys)
        {
            keyLeft = newKeys["DEPLACEMENT GAUCHE"];
            keyRight = newKeys["DEPLACEMENT DROITE"];
            keyJump = newKeys["SAUT"];
            keyDivide = newKeys["SE DIVISER"];
            keySpecial = newKeys["CAPACITE SPECIALE"];
            // gloo absorbtion is considered as his special capacity  so uses THE SAME key
            keyAbsorb = keySpecial;
            keyActivate = newKeys["ACTIONNER OBJET"];
            keyReset = newKeys["RETOUR AU DERNIER POINT DE CONTROLE"];
        }

        public static void resetToDefaultConfig()
        {
            keyLeft = defaultLeft;
            keyRight = defaultRight;
            keyJump = defaultJump;
            keyDivide = defaultDivide;
            keySpecial = defaultSpecial;
            // gloo absorbtion is considered as his special capacity  so uses THE SAME key
            keyAbsorb = defaultAbsorb;
            keyActivate = defaultActivate;
            keyReset = defaultReset;
        }
    }
}
