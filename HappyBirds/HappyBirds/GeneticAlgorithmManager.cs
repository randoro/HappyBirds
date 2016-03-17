using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{

    struct Generation
    {
        float FirstAngleVectX;
        float FirstAngleVectY;
        float FirstPower;

        float SecondAngleVectX;
        float SecondAngleVectY;
        float SecondPower;

        float ThirdAngleVectX;
        float ThirdAngleVectY;
        float ThirdPower;
    }


    class GeneticAlgorithmManager
    {

        const int generations = 10;
        int numberOfMembers = 5;
        List<Generation> memberList;


        public GeneticAlgorithmManager()
        {
            memberList = new List<Generation>(numberOfMembers);
        }


        private void InitializeGenerations()
        {
            for (int i = 0; i < numberOfMembers; i++)
            {
                //generate random generation
            }
        }
    }
}
