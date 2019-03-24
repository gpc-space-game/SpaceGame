using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Localization;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using SpaceGame.Factories;
using SpaceGame.Entities;



namespace SpaceGame.Screens
{
	public partial class SpinnerTest
	{

		void CustomInitialize()
		{
            TargetShipInstance.MovementInput =
                InputManager.Keyboard.Get2DInput(Keys.A, Keys.D, Keys.W, Keys.S);
            SpawnEnemies();
        }

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();
            RemovalActivity();
            PlayerLocationActivity();

        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public float PlayerLocationX()
        {
            return TargetShipInstance.X;
        }

        public float PlayerLocationY()
        {
            return TargetShipInstance.Y;
        }

        private void SpawnEnemies()
        {
            EnemySpinner spinner1 = EnemySpinnerFactory.CreateNew();
            spinner1.TurningSpeed = 50;
            spinner1.Position = new Microsoft.Xna.Framework.Vector3(-200, 200, 0);
            EnemySpinner spinner2 = EnemySpinnerFactory.CreateNew();
            spinner2.TurningSpeed = 70;
            spinner2.Position = new Microsoft.Xna.Framework.Vector3(0, 200, 0);
            EnemySpinner spinner3 = EnemySpinnerFactory.CreateNew();
            spinner3.TurningSpeed = 90;
            spinner3.Position = new Microsoft.Xna.Framework.Vector3(200, 200, 0);
        }

        private void PlayerLocationActivity()
        {
            for (int i = 0; i < EnemySpinnerList.Count; i++)
            {
                EnemySpinner spinner = EnemySpinnerList[i];
                spinner.PlayerLocationX = TargetShipInstance.X;
                spinner.PlayerLocationY = TargetShipInstance.Y;
            }
        }

        private void CollisionActivity()
        {
            SpinnerVsPlayerShipCollisionActivity();
            BulletVsPlayerShipCollisionActivity();
        }

        private void SpinnerVsPlayerShipCollisionActivity()
        {
            for (int i = EnemySpinnerList.Count - 1; i > -1; i--)
            {
                EnemySpinner spinner = EnemySpinnerList[i];
                if (spinner.PolygonInstance.CollideAgainst(TargetShipInstance.PolygonInstance))
                {
                    //Change this to PlayerShip.TakeDamage() later?
                    TargetShipInstance.HealthPoints -= spinner.Damage;
                    spinner.Explode();
                    break;
                }
            }
        }

        private void BulletVsPlayerShipCollisionActivity()
        {
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                Bullet bullet = BulletList[i];
                if (bullet.PolygonInstance.CollideAgainst(TargetShipInstance.PolygonInstance))
                {
                    //Change this to PlayerShip.TakeDamage() later as well..
                    TargetShipInstance.HealthPoints -= bullet.Damage;
                    bullet.Destroy();
                    break;
                }
            }
        }

        private void RemovalActivity()
        {
            RemoveExplosions();
            RemoveBullets();
        }

        private void RemoveExplosions()
        {
            //remove explosions after their time is up
            for (int i = ExplosionList.Count - 1; i > -1; i--)
            {
                Explosion explosion = ExplosionList[i];
                explosion.DestroyExplosion();
            }
        }

        private void RemoveBullets()
        {

        }
    }
}
