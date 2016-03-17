using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{

    //struct Member
    //{
    //    public float FirstAngleVectX { get; set; }
    //    public float FirstAngleVectY { get; set; }
    //    public float FirstPower { get; set; }

    //    public float SecondAngleVectX { get; set; }
    //    public float SecondAngleVectY { get; set; }
    //    public float SecondPower { get; set; }

    //    public float ThirdAngleVectX { get; set; }
    //    public float ThirdAngleVectY { get; set; }
    //    public float ThirdPower { get; set; }

    //    public int fitness { get; set; }

    //    public void SetFitness(int fitness)
    //    {
    //        this.fitness = fitness;
    //    }


    //    public override string ToString() 
    //    {
    //        return "Fitness:" + fitness + " FirstAngleVectX:" + FirstAngleVectX + " FirstAngleVectY:" + FirstAngleVectY + " FirstPower:" + FirstPower + " || SecondAngleVectX:" + SecondAngleVectX + " SecondAngleVectY:" + SecondAngleVectY + " SecondPower:" + SecondPower + " || ThirdAngleVectX:" + ThirdAngleVectX + " ThirdAngleVectY:" + ThirdAngleVectY + " ThirdPower:" + ThirdPower;
    //    }
    //}


    class GeneticAlgorithmManager
    {

        const int generations = 400;
        int currentGeneration = 0;

        int numberOfMembers = 20;
        int numberOfMembersTried = 0;
        List<Member> memberList;
        AI ai;
        bool IsDoneWithGeneration;
        bool IsDoneWithTest;
        bool printing = true;

        public GeneticAlgorithmManager()
        {
            memberList = new List<Member>(numberOfMembers);
            ai = new AI();

            IsDoneWithGeneration = false;
            IsDoneWithTest = false;

            InitializeGenerations();
        }

        public void Update(GameTime gameTime)
        {
            if (ai.isDone)
            {
                //fitness is saved in memberObject after all 3 birds are thrown
                fitnessFunction();
                //AI is reset (now has 3 birds again)
                resetAI();
                Game1.level.CreateDefaultLevel();
                //Next memberObject is set to current OR generation is done set to true
                if (numberOfMembersTried < numberOfMembers - 1)
                {
                    numberOfMembersTried++;
                }
                else
                {
                    IsDoneWithGeneration = true;
                    numberOfMembersTried = 0;
                }
            }
            else
            {
                if (!IsDoneWithTest)
                {
                    //AI shoots his 1-3 birds
                    ai.Shoot(memberList[numberOfMembersTried]);
                }
            }


            if (IsDoneWithGeneration)
            {
                Console.WriteLine("Generation:" + currentGeneration);
                if (currentGeneration < generations - 1)
                {
                    currentGeneration++;
                    AgeGeneration();

                }
                else
                {
                    IsDoneWithTest = true;
                }
            }


            if (IsDoneWithTest && printing)
            {
                List<Member> SortedList = memberList.OrderBy(o => o.fitness).ToList();
                Console.WriteLine("Test Done!");
                Console.WriteLine("Printing List:");
                for (int i = SortedList.Count; i-- > 0; )
                {
                    Console.WriteLine(SortedList.Count - i+": "+SortedList[i].ToString());
                }

                printing = false;
            }
        }


        private void AgeGeneration()
        {
            //Sort by fitness
            List<Member> SortedList = memberList.OrderBy(o => o.fitness).ToList();

            BreedTopGeneration(SortedList);
            MutateZeros(SortedList);
            CrossOverHalfGeneration(SortedList);
            MutateHalfGeneration(SortedList);
            TenPercentRandom(SortedList);


            memberList = SortedList;
            IsDoneWithGeneration = false;
        }

        private void BreedTopGeneration(List<Member> SortedList)
        {
            for (int i = 0; i < SortedList.Count / 2; i++)
            {
                SortedList[i].CopyStats(SortedList[SortedList.Count - i - 1]);
            }
        }

        private void MutateHalfGeneration(List<Member> SortedList)
        {
            for (int i = 0; i < SortedList.Count / 2; i++)
            {
                int val = Globals.rand.Next(0, 9);
                SortedList[i].MutateValue(val);
            }
        }

        private void CrossOverHalfGeneration(List<Member> SortedList)
        {
            for (int i = 0; i < SortedList.Count / 2; i++)
            {
                int randPart = Globals.rand.Next(0, 3);
                SortedList[i].CopyPartStats(SortedList[SortedList.Count - i - 1], randPart);
            }
        }




        private void MutateZeros(List<Member> SortedList)
        {
            for (int i = 0; i < SortedList.Count; i++)
            {
                int fitness = SortedList[i].fitness;
                if (fitness == 0)
                {
                    SortedList[i].Randomize();
                }
            }
        }

        /// <summary>
        /// Makes Ten percent near middle Random
        /// </summary>
        /// <param name="SortedList"></param>
        private void TenPercentRandom(List<Member> SortedList)
        {
            int tenPercent;
            if(SortedList.Count < 10) 
            {
                tenPercent = 1;
            }
            else 
            {
                tenPercent = SortedList.Count / 10;
            }

            for (int i = SortedList.Count / 2 - tenPercent; i < SortedList.Count / 2; i++)
            {
                SortedList[i].Randomize();
            }
        }



        private void resetAI()
        {
            ai.CreateDefaultAI();
        }

        public void StartTest()
        {

            for (int i = 0; i < generations; i++)
            {
                
            }
        }


        private void InitializeGenerations()
        {
            for (int i = 0; i < numberOfMembers; i++)
            {
                //generate random generation
                Member newGen = new Member();
                CreateGeneration(ref newGen);
                memberList.Add(newGen);
                //Console.WriteLine("Added new Random Generation:" + newGen.ToString());
            }
        }

        private void CreateGeneration(ref Member gen)
        {
            gen.FirstAngleVectX = CreateRandomMinusOneToOne();
            gen.FirstAngleVectY = CreateRandomMinusOneToOne();
            gen.FirstPower = CreateRandomPower();

            gen.SecondAngleVectX = CreateRandomMinusOneToOne();
            gen.SecondAngleVectY = CreateRandomMinusOneToOne();
            gen.SecondPower = CreateRandomPower();

            gen.ThirdAngleVectX = CreateRandomMinusOneToOne();
            gen.ThirdAngleVectY = CreateRandomMinusOneToOne();
            gen.ThirdPower = CreateRandomPower();

        }

        public static float CreateRandomMinusOneToOne()
        {
            double minusOneToOne = (Globals.rand.NextDouble() * 2.0D) - 1.0D;
            return (float)minusOneToOne;
        }

        public static float CreateRandomPower()
        {
            double randomPower = Globals.rand.NextDouble() * Globals.maxPower;
            return (float)randomPower;
        }


        private void fitnessFunction()
        {
            memberList[numberOfMembersTried].fitness = Game1.level.removedBlocks;
        }





    }
}
