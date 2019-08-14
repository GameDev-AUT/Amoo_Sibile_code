using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState 
{

        private string animationName="speed";
        private float speed =0;
        private bool trigerType = false;
        private bool asABool = false;
        public AnimationState(string animationName, float speed,bool trigerType)
        {
            
            this.animationName = animationName;
            this.speed = speed;
            this.TrigerType = trigerType;
        }
        

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public bool TrigerType
        {
            get => trigerType;
            set => trigerType = value;
        }

        public string AnimationName
        {
            get => animationName;
            set => animationName = value;
        }
        public bool AsABool
        {
            get => asABool;
            set => asABool = value;
        }
}
