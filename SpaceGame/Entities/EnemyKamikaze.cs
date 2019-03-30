using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using SpaceGame.Factories;

namespace SpaceGame.Entities
{
	public partial class EnemyKamikaze
	{

        //Get player location, used for AI
        private float playerLocationX;
        private float playerLocationY;

        public float PlayerLocationX
        {
            set { playerLocationX = value; }
        }

        public float PlayerLocationY
        {
            set { playerLocationY = value; }
        }

        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{

        }

		private void CustomActivity()
		{
            MovementActivity();

		}

		private void CustomDestroy()
		{

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void MovementActivity()
        {
            //Check where player ship is and the distance to it
            double distanceToPlayer = Math.Sqrt(
                Math.Pow(System.Convert.ToDouble(this.X - playerLocationX),2) +
                Math.Pow(System.Convert.ToDouble(this.Y - playerLocationY),2));

            //If player ship is close enough to kamikaze then become hostile
            if (distanceToPlayer < Range)
            {
                this.CurrentState = VariableState.Hostile;

                //Start moving towards player ship
                this.XVelocity = Convert.ToSingle((playerLocationX - this.X) / distanceToPlayer) * MovementSpeed;
                this.YVelocity = Convert.ToSingle((playerLocationY - this.Y) / distanceToPlayer) * MovementSpeed;
            } else
            {
                //Become dormant when player is far away
                this.CurrentState = VariableState.Dormant;
                this.XVelocity = 0;
                this.YVelocity = 0;
            }
        }

        public void Explode()
        {
            //Kamikaze explodes, creating an explosion and getting destroyed
            Explosion explosion = ExplosionFactory.CreateNew();
            explosion.Position = this.Position;
            explosion.Velocity = this.Velocity;
            this.Destroy();
        }
	}
}
