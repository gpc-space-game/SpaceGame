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
	public partial class KamikazeTest
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

        //these codes should be changed to be about the actual playership later
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
            EnemyKamikaze kamikaze1 = EnemyKamikazeFactory.CreateNew();
            kamikaze1.Position = new Microsoft.Xna.Framework.Vector3(-200, 200, 0);
            EnemyKamikaze kamikaze2 = EnemyKamikazeFactory.CreateNew();
            kamikaze2.Position = new Microsoft.Xna.Framework.Vector3(0, 200, 0);
            EnemyKamikaze kamikaze3 = EnemyKamikazeFactory.CreateNew();
            kamikaze3.Position = new Microsoft.Xna.Framework.Vector3(200, 200, 0);
        }

        private void PlayerLocationActivity()
        {
            for (int i = 0; i < EnemyKamikazeList.Count; i++)
            //for (int i = EnemyKamikazeList.Count - 1; i > -1; i--)
            {
                EnemyKamikaze kamikaze = EnemyKamikazeList[i];
                //EnemyKamikazeList[i].PlayerLocationX = PlayerLocationX();
                kamikaze.PlayerLocationX = TargetShipInstance.X;
                kamikaze.PlayerLocationY = TargetShipInstance.Y;
                //EnemyKamikazeList[i].PlayerLocationY = PlayerLocationY();
            }
        }

        private void CollisionActivity()
        {
            KamikazeVsPlayerShipCollisionActivity();
        }

        private void KamikazeVsPlayerShipCollisionActivity()
        {
            for (int i = EnemyKamikazeList.Count -1; i > -1; i--)
            {
                EnemyKamikaze kamikaze = EnemyKamikazeList[i];
                if (kamikaze.PolygonInstance.CollideAgainst(TargetShipInstance.PolygonInstance))
                {
                    //Change this to PlayerShip.TakeDamage() later?
                    TargetShipInstance.HealthPoints -= kamikaze.Damage;
                    kamikaze.Explode();
                }
            }
        }

        private void RemovalActivity()
        {
            RemoveExplosions();
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
	}
}
