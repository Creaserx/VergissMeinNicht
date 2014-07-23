using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using VergissMeinNicht.DataTypes;
#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using FlatRedBall.Screens;
using VergissMeinNicht.Screens;
#elif FRB_MDX
using Keys = Microsoft.DirectX.DirectInput.Key;


#endif



namespace VergissMeinNicht.Entities
{
    public enum MovementType
    {
        Ground,
        Air,
        AfterDoubleJump
    }

	public partial class PlatformerCharacterBase
    {
        #region Fields

        bool mIsOnGround = false;
        bool mHitHead = false;

        bool mHasDoubleJumped = false;

        double mTimeJumpPushed = double.NegativeInfinity;
        MovementValues mValuesJumpedWith;

        MovementValues mCurrentMovement;

        MovementType mMovementType;

        double mLastCollisionTime = -1;
        #endregion

        #region Properties

        public Xbox360GamePad GamePad
        {
            get;
            set;
        }

        double CurrentTime
        {
            get
            {
                if (ScreenManager.CurrentScreen != null)
                {
                    return ScreenManager.CurrentScreen.PauseAdjustedCurrentTime;
                }
                else
                {
                    return TimeManager.CurrentTime;
                }
            }
        }

        MovementValues CurrentMovement
        {
            get
            {
                return mCurrentMovement;
            }
        }

        protected virtual bool JumpDown
        {
            get
            {
                if (GamePad != null && GamePad.IsConnected)
                {
                    return GamePad.ButtonDown(Xbox360GamePad.Button.A);
                }
                else
                {
                    return InputManager.Keyboard.KeyDown(Keys.Space);
                }
            }
        }

        protected virtual bool JumpPushed
        {
            get
            {
                if (GamePad != null && GamePad.IsConnected)
                {
                    return GamePad.ButtonPushed(Xbox360GamePad.Button.A);
                }
                else
                {
                    return InputManager.Keyboard.KeyPushed(Keys.Space);
                }
            }
        }

        protected virtual float HorizontalRatio
        {
            get
            {
                if (GamePad != null && GamePad.IsConnected)
                {
                    return GamePad.LeftStick.Position.X;
                }
                else
                {
                    if (InputManager.Keyboard.KeyDown(Keys.Left))
                    {
                        return -1;
                    }
                    else if (InputManager.Keyboard.KeyDown(Keys.Right))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

        }

        public bool IsOnGround
        {
            get
            {
                return mIsOnGround;
            }
        }

        public MovementType CurrentMovementType
        {
            get
            {
                return mMovementType;
            }
            set
            {
                mMovementType = value;

                switch (mMovementType)
                {
                    case MovementType.Ground:
                        mCurrentMovement = GroundMovement;
                        break;
                    case MovementType.Air:
                        mCurrentMovement = AirMovement;
                        break;
                    case MovementType.AfterDoubleJump:
                        mCurrentMovement = AfterDoubleJump;
                        break;
                }
            }
        }

        public bool InputEnabled
        {
            get;
            set;
        }

        #endregion        

        private void CustomInitialize()
		{
            InputEnabled = true;

            CurrentMovementType = MovementType.Ground;

            YAcceleration = this.CurrentMovement.Gravity;

		}

        public enum LeftOrRight
        {
            Left,
            Right
        }

        public LeftOrRight DirectionFacing
        {
            get;
            set;
        }

        private static PlatformerCharacterBase instance;

        public static PlatformerCharacterBase getInstance()
        {
            return instance;
        }        

        public static void updateinstance(PlatformerCharacterBase newInstance)
        {
            if (instance != null)
            {
                instance.Destroy();
            }
            instance = newInstance;
        }

        public static bool isChild()
        {
            if (PlatformerCharacterBase.getInstance() is TheodorChild) return true;
            else return false;
        }
 

		private void CustomActivity()
		{
            InputActivity();

		}

        private void InputActivity()
        {
            HorizontalMovementActivity();

            JumpVelocity();
        }

        public static void UpdateCharacterValues()
        {
            LevelBase.CollisionHeightLayerFront = getInstance().Collision.Height * 0.5f;
            LevelBase.CollisionHeightLayerMid = getInstance().Collision.Height * 0.45f;
            LevelBase.CollisionHeightLayerBack = getInstance().Collision.Height * 0.4f;
            getInstance().Collision.Height = LevelBase.CollisionHeightLayerFront;

            LevelBase.CollisionWidthLayerFront = getInstance().Collision.Width * 0.5f;
            LevelBase.CollisionWidthLayerMid = getInstance().Collision.Width * 0.45f;
            LevelBase.CollisionWidthLayerBack = getInstance().Collision.Width * 0.4f;
            getInstance().Collision.Width = LevelBase.CollisionWidthLayerFront;

            switch (LevelBase.CurrentLayer)
            {
                case 1:
                    PlatformerCharacterBase.getInstance().MoveToLayer(LevelBase.LayerFront);

                    break;

                case 2:
                    PlatformerCharacterBase.getInstance().MoveToLayer(LevelBase.LayerMid);

                    break;

                case 3:
                    PlatformerCharacterBase.getInstance().MoveToLayer(LevelBase.LayerBack);

                    break;

                default:

                    break;
            }
        }        

        private void JumpVelocity()
        {
            bool jumpPushed = JumpPushed && InputEnabled;
            bool jumpDown = JumpDown && InputEnabled;


            if (jumpPushed && 
                CurrentMovement.JumpVelocity > 0 &&
                (mIsOnGround || AfterDoubleJump == null || (AfterDoubleJump != null && mHasDoubleJumped == false))
                
                )
            {

                mTimeJumpPushed = CurrentTime;
                this.YVelocity = CurrentMovement.JumpVelocity;
                mValuesJumpedWith = CurrentMovement;

                if (CurrentMovementType == MovementType.Air)
                {
                    mHasDoubleJumped = true ;
                }
            }

            double secondsSincePush = CurrentTime - mTimeJumpPushed;

            if (mValuesJumpedWith != null && 
                secondsSincePush < mValuesJumpedWith.JumpApplyLength &&
                (mValuesJumpedWith.JumpApplyByButtonHold == false || JumpDown)
                )
            {
                this.YVelocity = mValuesJumpedWith.JumpVelocity;

            }

            if (mValuesJumpedWith != null && mValuesJumpedWith.JumpApplyByButtonHold &&
                (!JumpDown || mHitHead)
                )
            {
                mValuesJumpedWith = null;
            }

            this.YVelocity = Math.Max(-CurrentMovement.MaxFallSpeed, this.YVelocity);
        }


        private void HorizontalMovementActivity()
        {
            float horizontalRatio = HorizontalRatio;
            if (!InputEnabled)
            {
                horizontalRatio = 0;
            }

            if (this.CurrentMovement.AccelerationTimeX <= 0)
            {
                this.XVelocity = horizontalRatio * CurrentMovement.MaxSpeedX;
            }
            else
            {
                float acceleration = CurrentMovement.MaxSpeedX / CurrentMovement.AccelerationTimeX;

                float sign = Math.Sign(horizontalRatio);
                float magnitude = Math.Abs(horizontalRatio);

                if(sign == 0)
                {
                    sign = -Math.Sign(XVelocity);
                    magnitude = 1;
                }

                if (XVelocity == 0 || sign == Math.Sign(XVelocity))
                {
                    this.XAcceleration = sign * magnitude * CurrentMovement.MaxSpeedX / CurrentMovement.AccelerationTimeX;
                }
                else
                {
                    float accelerationValue = magnitude * CurrentMovement.MaxSpeedX / CurrentMovement.DecelerationTimeX;


                    if (Math.Abs(XVelocity) < accelerationValue * TimeManager.SecondDifference)
                    {
                        this.XAcceleration = 0;
                        this.XVelocity = 0;
                    }
                    else
                    {

                        // slowing down
                        this.XAcceleration = sign * accelerationValue;
                    }

                }

                XVelocity = Math.Min(XVelocity, CurrentMovement.MaxSpeedX);
                XVelocity = Math.Max(XVelocity, -CurrentMovement.MaxSpeedX);
            }
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void CollideAgainst(ShapeCollection shapeCollection)
        {
            CollideAgainst(shapeCollection, false);
        }

        public void CollideAgainst(ShapeCollection shapeCollection, bool isCloudCollision)
        {
            CollideAgainst(() => shapeCollection.CollideAgainstBounceWithoutSnag(this.Collision, 0), isCloudCollision);
        }

        public void CollideAgainst(Func<bool> collisionFunction, bool isCloudCollision)
        {
            Vector3 positionBeforeCollision = this.Position;
            Vector3 velocityBeforeCollision = this.Velocity;

            float lastY = this.Y;

            bool isFirstCollisionOfTheFrame = TimeManager.CurrentTime != mLastCollisionTime;

            if (isFirstCollisionOfTheFrame)
            {
                mLastCollisionTime = TimeManager.CurrentTime;
                mIsOnGround = false;
                mHitHead = false;
            }

            if(isCloudCollision == false || velocityBeforeCollision.Y < 0)
            {

                if (collisionFunction())
                {
                    // make sure that we've been moved up, and that we're falling
                    bool shouldApplyCollision = true;
                    if (isCloudCollision)
                    {
                        if (this.Y <= positionBeforeCollision.Y)
                        {
                            shouldApplyCollision = false;
                        }
                    }

                    if (shouldApplyCollision)
                    {

                        if (this.Y > lastY)
                        {
                            mIsOnGround = true;
                        }
                        if (this.Y < lastY)
                        {
                            mHitHead = true;
                        }
                    }
                    else
                    {
                        Position = positionBeforeCollision;
                        Velocity = velocityBeforeCollision;
                    }
                }
            }
        }


        public void DetermineMovementValues()
        {
            if (mIsOnGround)
            {
                mHasDoubleJumped = true;
                if (CurrentMovementType == MovementType.Air ||
                    CurrentMovementType == MovementType.AfterDoubleJump)
                {
                    CurrentMovementType = MovementType.Ground;
                }
            }
            else
            {
                if (CurrentMovementType == MovementType.Ground)
                {
                    CurrentMovementType = MovementType.Air;
                }

            }

            if (CurrentMovementType == MovementType.Air && mHasDoubleJumped)
            {
                CurrentMovementType = MovementType.AfterDoubleJump;
            }
        


        }



	}
}
