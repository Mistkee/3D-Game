using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
    public class AnimationController : MonoBehaviour
    {
        public Animator animator;
        public string walkForwardAnimation = "walk_forward";


        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {

            animator.Play(walkForwardAnimation);

        }
    }
}
