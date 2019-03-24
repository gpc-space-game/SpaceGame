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
	public partial class EnemySpinner
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

        double mLastSpawnTime;
        private bool IsTimeToSpawn
        {
            get
            {
                return FlatRedBall.Screens.ScreenManager.CurrentScreen.PauseAdjustedSecondsSince(mLastSpawnTime) > 1.0/BulletsPerSecond;
            }
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
            TurningActivity();
            ShootingActivity();

		}

		private void CustomDestroy()
		{

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void MovementActivity()
        {
            //no movement yet
        }

        private void TurningActivity()
        {
            this.RotationZVelocity = System.Convert.ToSingle(TurningSpeed/100.0);
        }

        private void ShootingActivity()
        {
            if (IsTimeToSpawn)
            {
                SpawnBullet();
                mLastSpawnTime = FlatRedBall.Screens.ScreenManager.CurrentScreen.PauseAdjustedCurrentTime;
            }
        }

        private void SpawnBullet()
        {
            Bullet bullet = BulletFactory.CreateNew();
            bullet.IsFromPlayer = false;
            bullet.Damage = this.Damage;
            bullet.Position = this.Position + this.RotationMatrix.Up * 12;
            bullet.RotationZ = this.RotationZ;
            bullet.Velocity = this.RotationMatrix.Up * bullet.MovementSpeed;
        }

        public void Explode()
        {
            //Spinner explodes, creating an explosion and getting destroyed
            Explosion explosion = ExplosionFactory.CreateNew();
            explosion.Position = this.Position;
            explosion.Velocity = this.Velocity;
            this.Destroy();
        }
	}
}
