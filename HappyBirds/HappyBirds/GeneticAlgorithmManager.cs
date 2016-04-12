using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        const int generations = 800;
        public int currentGeneration { get; private set; }

        int numberOfMembers = 20;
        public int numberOfMembersTried { get; private set; }
        public List<Member> memberList { get; private set; }
        public AI ai { get; private set; }
        bool IsDoneWithGeneration;
        bool IsDoneWithTest;
        bool printing = true;

        public int lastGenTopFitness { get; private set; }
        public float lastGenAverageFitness { get; private set; }
        public int lastGenMedianFitness { get; private set; }
        public float lastGenLowestFitness { get; private set; }

        private List<String> TopFitnessList;
        private List<String> AverageFitnessList;
        private List<String> MedianFitnessList;
        private List<String> LowestFitnessList;
        public bool playall { get; set; }


        public GeneticAlgorithmManager()
        {
            memberList = new List<Member>(numberOfMembers);
            ai = new AI();

            currentGeneration = 0;
            numberOfMembersTried = 0;
            lastGenTopFitness = 0;
            lastGenAverageFitness = 0;
            IsDoneWithGeneration = false;
            IsDoneWithTest = false;
            TopFitnessList = new List<String>();
            AverageFitnessList = new List<String>();
            MedianFitnessList = new List<String>();
            LowestFitnessList = new List<String>();

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

                    if (numberOfMembersTried >= (numberOfMembers-1)/2)
                    {
                        numberOfMembersTried++;
                        if(memberList[numberOfMembersTried].fitness != 0)
                        {
                            if (!playall)
                            {
                                IsDoneWithGeneration = true;
                                numberOfMembersTried = 0;
                            }
                        }
                    }
                    else
                    {
                        numberOfMembersTried++;
                    }
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
                lastGenTopFitness = CurrentTopFitness();
                lastGenAverageFitness = CurrentAverageFitness();
                lastGenMedianFitness = CurrentMedianFitness();
                lastGenLowestFitness = CurrentLowestFitness();
                TopFitnessList.Add("Gen " + currentGeneration + " Top: " + lastGenTopFitness);
                AverageFitnessList.Add("Gen " + currentGeneration + " Average: " + lastGenAverageFitness);
                MedianFitnessList.Add("Gen " + currentGeneration + " Median: " + lastGenMedianFitness);
                LowestFitnessList.Add("Gen " + currentGeneration + " Lowest: " + lastGenLowestFitness);
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
                PrintThings();

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
            TwentyPercentRandom(SortedList);


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
            for (int i = 0; i < SortedList.Count / 4; i++)
            {
                int randPart = Globals.rand.Next(0, 3);
                //SortedList[i].CrossPartStats(SortedList[SortedList.Count - i - 1], randPart); old
                SortedList[i].CrossPartStats(SortedList[(SortedList.Count / 2) - i - 1], randPart);
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
        /// Makes Twenty percent near middle Random
        /// </summary>
        /// <param name="SortedList"></param>
        private void TwentyPercentRandom(List<Member> SortedList)
        {
            int twentyPercent;
            if(SortedList.Count < 10) 
            {
                twentyPercent = 2;
            }
            else 
            {
                twentyPercent = SortedList.Count / 2;
            }

            for (int i = SortedList.Count / 2 - twentyPercent; i < SortedList.Count / 2; i++)
            {
                SortedList[i].Randomize();
            }
        }



        private void resetAI()
        {
            ai.CreateDefaultAI();
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

        public int CurrentTopFitness()
        {
            int top = 0;
            for (int i = 0; i < memberList.Count; i++)
            {
                if (memberList[i].fitness > top)
                {
                    top = memberList[i].fitness;
                }
            }
            return top;
        }

        public int CurrentLowestFitness()
        {
            int lowest = Int32.MaxValue;
            for (int i = 0; i < memberList.Count; i++)
            {
                if (memberList[i].fitness < lowest)
                {
                    lowest = memberList[i].fitness;
                }
            }
            return lowest;
        }

        public float CurrentAverageFitness()
        {
            float total = 0.0f;
            for (int i = 0; i < memberList.Count; i++)
            {
                total += memberList[i].fitness;
            }
            total = total/memberList.Count;

            return total;
        }

        public int CurrentMedianFitness()
        {
            List<Member> SortedList = memberList.OrderBy(o => o.fitness).ToList();
            int index = SortedList.Count/2;
            return SortedList[index].fitness;
        }




        public void PrintThings()
        {
            List<Member> SortedList = memberList.OrderBy(o => o.fitness).ToList();
            Console.WriteLine("Printing Things!");

            Console.WriteLine("Members Sorted Best first:");
            for (int i = SortedList.Count; i-- > 0; )
            {
                Console.WriteLine("    " + (SortedList.Count - i) + ": " + SortedList[i].ToString());
            }

            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Generations top fitness:");

            for (int i = 0; i < TopFitnessList.Count; i++)
            {
                Console.WriteLine("    " + TopFitnessList[i]);
            }


            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Generations average fitness:");

            for (int i = 0; i < AverageFitnessList.Count; i++)
            {
                Console.WriteLine("    "+AverageFitnessList[i]);
            }

            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Generations median fitness:");

            for (int i = 0; i < MedianFitnessList.Count; i++)
            {
                Console.WriteLine("    " + MedianFitnessList[i]);
            }

            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Generations lowest fitness:");

            for (int i = 0; i < LowestFitnessList.Count; i++)
            {
                Console.WriteLine("    " + LowestFitnessList[i]);
            }

            Console.WriteLine("//////////////////////////////////////////////////");
            Console.WriteLine("//////////////////////////////////////////////////");

        }


    }
}
