using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WJH
{
    public class FoodTrigger : MonoBehaviour
    {


        private float _starFoodMass;
        public float splitFoodMass = 50;

        public float maxStarFoodMass = 100;
        public float minStarFoodMass = 8;

        private FoodType curFoodType = FoodType.StarFood;

        public float starFoodMass
        {
            get
            {
                return _starFoodMass;
            }
            set
            {
                _starFoodMass = value;
            }
        }
        public void SelectCurFoodType(string tag)
        {
            switch (tag)
            {
                case "SplitBallFood":
                    curFoodType = FoodType.SplitFood;
                    break;
                case "StarFood":
                    curFoodType = FoodType.StarFood;
                    break;
                default:
                    curFoodType = FoodType.StarFood;
                    break;

            }
        }

        private void Start()
        {

            _starFoodMass = Random.Range(minStarFoodMass, maxStarFoodMass);
            SelectCurFoodType(gameObject.tag);


        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                other.transform.parent.GetComponent<Cells>().isInvincible = false;
                if (curFoodType == FoodType.SplitFood)
                {
                    other.gameObject.GetComponent<BallProperty>().BallDevourFood(splitFoodMass, 1);
                    Destroy(gameObject);
                }
                else if (curFoodType == FoodType.StarFood)
                {
                    other.gameObject.GetComponent<BallProperty>().BallDevourFood(_starFoodMass, 1);
                    transform.parent.GetComponent<FoodManager>().MoveFoodPos(gameObject);
                }

            }
        }

        enum FoodType
        {
            StarFood,
            SplitFood
        }



    }
}

