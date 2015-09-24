﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Physics2D.Common;
using Physics2D.Object;

namespace Physics2D.Collision
{
    public class ParticleRod : ParticleLink
    {
        /// <summary>
        /// 长度
        /// </summary>
        public double Length { get; }

        public ParticleRod(Particle pA, Particle pB)
        {
            Length = (pB.Position - pA.Position).Length();
            PA = pA;
            PB = pB;
        }


        public override IEnumerator<ParticleContact> GetEnumerator()
        {
            double length = CurrentLength();
            double penetration = length - Length;

            if (penetration == 0) yield break;

            var normal = (PB.Position - PA.Position).Normalize();

            if (length <= Length)
            {
                normal *= -1;
                penetration *= -1;
            }

            yield return new ParticleContact
            {
                PA = PA,
                PB = PB,
                ContactNormal = normal,
                Restitution = 0,
                Penetration = penetration
            };
        }
    }
}
