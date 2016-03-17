using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    class Member
    {
        public float FirstAngleVectX { get; set; }
        public float FirstAngleVectY { get; set; }
        public float FirstPower { get; set; }

        public float SecondAngleVectX { get; set; }
        public float SecondAngleVectY { get; set; }
        public float SecondPower { get; set; }

        public float ThirdAngleVectX { get; set; }
        public float ThirdAngleVectY { get; set; }
        public float ThirdPower { get; set; }

        public int fitness { get; set; }

        public override string ToString()
        {
            return "Fitness:" + fitness + " FirstAngleVectX:" + FirstAngleVectX + " FirstAngleVectY:" + FirstAngleVectY + " FirstPower:" + FirstPower + " || SecondAngleVectX:" + SecondAngleVectX + " SecondAngleVectY:" + SecondAngleVectY + " SecondPower:" + SecondPower + " || ThirdAngleVectX:" + ThirdAngleVectX + " ThirdAngleVectY:" + ThirdAngleVectY + " ThirdPower:" + ThirdPower;
        }

        public void CopyStats(Member otherMember)
        {
            this.FirstAngleVectX = otherMember.FirstAngleVectX;
            this.FirstAngleVectY = otherMember.FirstAngleVectY;
            this.FirstPower = otherMember.FirstPower;

            this.SecondAngleVectX = otherMember.SecondAngleVectX;
            this.SecondAngleVectY = otherMember.SecondAngleVectY;
            this.SecondPower = otherMember.SecondPower;

            this.ThirdAngleVectX = otherMember.ThirdAngleVectX;
            this.ThirdAngleVectY = otherMember.ThirdAngleVectY;
            this.ThirdPower = otherMember.ThirdPower;

            this.fitness = otherMember.fitness;
        }

        public void CopyPartStats(Member otherMember, int part)
        {
            switch (part)
            {
                case 0:
                    this.FirstAngleVectX = otherMember.FirstAngleVectX;
                    this.FirstAngleVectY = otherMember.FirstAngleVectY;
                    this.FirstPower = otherMember.FirstPower;
                    break;
                case 1:
                    this.SecondAngleVectX = otherMember.SecondAngleVectX;
                    this.SecondAngleVectY = otherMember.SecondAngleVectY;
                    this.SecondPower = otherMember.SecondPower;
                    break;
                case 2:
                    this.ThirdAngleVectX = otherMember.ThirdAngleVectX;
                    this.ThirdAngleVectY = otherMember.ThirdAngleVectY;
                    this.ThirdPower = otherMember.ThirdPower;
                    break;
                default:
                    break;
            }

            this.fitness = 0;
        }

        public void MutateValue(int val)
        {
            switch (val)
            {
                case 0:
                    this.FirstAngleVectX = MutateMinusOneToOne(FirstAngleVectX);
                    break;
                case 1:
                    this.FirstAngleVectY = MutateMinusOneToOne(FirstAngleVectY);
                    break;
                case 2:
                    this.FirstPower = MutatePower(FirstPower);
                    break;
                case 3:
                    this.SecondAngleVectX = MutateMinusOneToOne(SecondAngleVectX);
                    break;
                case 4:
                    this.SecondAngleVectY = MutateMinusOneToOne(SecondAngleVectY);
                    break;
                case 5:
                    this.SecondPower = MutatePower(SecondPower);
                    break;
                case 6:
                    this.ThirdAngleVectX = MutateMinusOneToOne(ThirdAngleVectX);
                    break;
                case 7:
                    this.ThirdAngleVectY = MutateMinusOneToOne(ThirdAngleVectY);
                    break;
                case 8:
                    this.ThirdPower = MutatePower(ThirdPower);
                    break;
                default:
                    break;
            }

            fitness = 0;

        }

        public float MutateMinusOneToOne(float old)
        {
            float generated = GeneticAlgorithmManager.CreateRandomMinusOneToOne() * 0.5f;
            float newGen = old + generated;
            if (newGen < -1f)
            {
                newGen = -1f;
            }

            if (newGen > 1f)
            {
                newGen = 1f;
            }
            return newGen;
        }

        public float MutatePower(float old)
        {
            float generated = GeneticAlgorithmManager.CreateRandomPower() * 0.5f;
            float newGen = old + generated;
            if (newGen < 0f)
            {
                newGen = 0f;
            }

            if (newGen > Globals.maxPower)
            {
                newGen = Globals.maxPower;
            }
            return newGen;
        }


        public void Randomize()
        {
            MutateValue(0);
            MutateValue(1);
            MutateValue(2);
            MutateValue(3);
            MutateValue(4);
            MutateValue(5);
            MutateValue(6);
            MutateValue(7);
            MutateValue(8);
        }

    }
}
