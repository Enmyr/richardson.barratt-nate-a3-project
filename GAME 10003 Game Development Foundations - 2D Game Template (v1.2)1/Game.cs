// Include code libraries you need below (use the namespace).
using GAME_10003_Game_Development_Foundations___2D_Game_Template__v1._2_1;
using System;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

// The namespace your code is in.
namespace Game10003
{
    public class Game
    {
        // Place your variables here:
        Control_Screen ShowControls = new Control_Screen();
        bool Show = true;
        Tiles Ground = new Tiles();
        Tiles Obs1 = new Tiles();
        Tiles Obs2 = new Tiles();
        Tiles Obs3 = new Tiles();
        Tiles Obs4 = new Tiles();
        Tiles Obs5 = new Tiles();
        Vector2 GroundPos;
        Vector2 GroundScale;
        Vector2 Obs1Pos;
        Vector2 Obs1Scale;
        int CurrentLevel;
        bool IsOutOfBounds = false;
        bool IsDead = false;
        bool SecretPassage = false;
        bool IsStanding = false;
        bool CollidingLeft = false;
        bool CollidingRight = false;
        int MovementFactor = 1;
        int JumpForce;
        int RunBoost;
        //bool MoveRight = false;
        //bool MoveLeft = false;
        //bool Jump = false;
        //bool Crouch = false;
        //bool Sprint = false;
        bool UpPressed = false;
        bool LeftPressed = false;
        bool DownPressed = false;
        bool RightPressed = false;
        bool RunPressed = false;
        Vector2 PlayerPosA = new Vector2(410,103);
        Vector2 PlayerPos = new Vector2(400, 85);
        Vector2 PlayerVelocity = new Vector2(0,0);
        Vector2 Gravity = new Vector2(0, 3);
        ///     Setup runs once before the game loop begins.
        public void Setup()
        {
            Window.SetTitle("Lonesome Venture");
            Window.SetSize(800, 540);
            Window.ClearBackground(Color.OffWhite);
            Window.TargetFPS = 30;
            CurrentLevel = 1;
        }
        ///     Update runs every frame.
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            DrawPlayer();

            PlayerPosA.X = PlayerPos.X + 10;
            PlayerPosA.Y = PlayerPos.Y + 18;

            /// Important Stuff for Collision Later
            float GroundLeftEdge = GroundPos.X;
            float GroundRightEdge = GroundPos.X + GroundScale.X;
            float GroundTopEdge = GroundPos.Y;

            float Obs1LeftEdge = Obs1Pos.X;
            float Obs1RightEdge = Obs1Pos.X + Obs1Scale.X;
            float Obs1TopEdge = Obs1Pos.Y;
            float Obs1BottomEdge = Obs1Pos.Y + Obs1Scale.Y;

            if (PlayerPos.Y >= Window.Height)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPos.Y <= 0)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPos.X >= Window.Width)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPos.X <= 0)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPosA.Y >= Window.Height)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPosA.Y <= 0)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPosA.X >= Window.Width)
            {
                IsOutOfBounds = true;
            }
            else if (PlayerPosA.X <= 0)
            {
                IsOutOfBounds = true;
            }
            else
            {
                IsOutOfBounds = false;
            }
            if (IsOutOfBounds == true)
            {
                if (SecretPassage == true)
                {
                    IsDead = false;
                }
                else
                {
                    IsDead = true;
                }
            }
            if (IsDead == true)
            {
                PlayerPos = new Vector2(400, 85);
                IsStanding = false;
                IsDead = false;
            }
            else
            {
                IsDead = false;
            }
            /// Show Controls
            ShowControls.ControlScreen(Show);
            /// Show Toggle
            if (Input.IsKeyboardKeyPressed(KeyboardInput.C))
            {
                if (Show == true) { Show = false; }
                else if (Show == false) { Show = true; }
            }
            /// Player Input
            if (Input.IsKeyboardKeyPressed(KeyboardInput.W) || Input.IsKeyboardKeyDown(KeyboardInput.W))
            {
                UpPressed = true;
            }
            else
            {
                UpPressed = false;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.A) || Input.IsKeyboardKeyDown(KeyboardInput.A))
            {
                LeftPressed = true;
            }
            else
            {
                LeftPressed = false;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.S) || Input.IsKeyboardKeyDown(KeyboardInput.S))
            {
                DownPressed = true;
            }
            else
            {
                DownPressed = false;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.D) || Input.IsKeyboardKeyDown(KeyboardInput.D))
            {
                RightPressed = true;
            }
            else
            {
                RightPressed = false;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.LeftShift) || Input.IsKeyboardKeyDown(KeyboardInput.LeftShift))
            {
                RunPressed = true;
            }
            else
            {
                RunPressed = false;
            }
            if (RunPressed == true)
            {
                MovementFactor = 3;
            }
            else if (DownPressed == true)
            {
                MovementFactor = 1;
            }
            else
            {
                MovementFactor = 2;
            }
            if (LeftPressed == true)
            {
                CollidingRight = false;
                RightPressed = false;
            }
            if (RightPressed == true)
            {
                CollidingLeft = false;
                LeftPressed = false;
            }
            if (LeftPressed && RightPressed == true)
            {
                CollidingLeft = true;
                CollidingRight = true;
            }
            JumpForce = -24 + ((MovementFactor - 2) * -8);
            /// THE FUCKING OBSTACLES
            if (CurrentLevel == 1)
            {
                GroundScale = new Vector2(800, 80);
                GroundPos = new Vector2(0, 460);
                Ground.DrawTile(GroundPos, GroundScale);

                Obs1Scale = new Vector2(60, 60);
                Obs1Pos = new Vector2(260, 400);
                Obs1.DrawTile(Obs1Pos, Obs1Scale);
            }
            /// Collision
            float PlayerBottom = PlayerPosA.Y;
            float PlayerLeft = PlayerPos.X;
            float PlayerRight = PlayerPosA.X;
            float PlayerTop = PlayerPos.Y;
            if (PlayerBottom >= GroundTopEdge && PlayerRight > GroundLeftEdge && PlayerLeft < GroundRightEdge)
            {
                if (UpPressed == false)
                {
                    IsStanding = true;
                    PlayerPos.Y = GroundTopEdge - 18;
                }
                else
                {
                    PlayerVelocity.Y += JumpForce;
                }
            }
            else if (PlayerBottom >= Obs1TopEdge && PlayerRight > Obs1LeftEdge && PlayerLeft < Obs1RightEdge)
            {
                if (UpPressed == false)
                {
                    IsStanding = true;
                    PlayerPos.Y = Obs1TopEdge - 18;
                }
                else
                {
                    PlayerVelocity.Y += JumpForce;
                }
            }
            else
            {
                IsStanding = false;
            }
            if (IsStanding == false)
            {

            }
            if (PlayerVelocity.X <= 0)
            {
                if (PlayerLeft <= Obs1RightEdge && PlayerBottom > (Obs1TopEdge + 15) && PlayerTop < Obs1BottomEdge && PlayerRight > (Obs1LeftEdge + 15))
                {
                    if (RightPressed == false)
                    {

                        CollidingLeft = true;
                        PlayerPos.X = Obs1RightEdge;
                    }
                    else
                    {
                        PlayerPos.X += 1;
                    }
                }
                else
                {
                    CollidingLeft = false;
                }
            }
            if (PlayerVelocity.X >= 0)
            {
                if (PlayerRight >= Obs1LeftEdge && PlayerBottom > (Obs1TopEdge + 15) && PlayerLeft < (Obs1RightEdge - 15) && PlayerTop < Obs1BottomEdge)
                {
                    if (LeftPressed == false)
                    {

                        CollidingRight = true;
                        PlayerPos.X = Obs1LeftEdge - 10;
                    }
                    else
                    {
                        PlayerPos.X += -1;
                    }
                }
                else
                {
                    CollidingRight = false;
                }
            }
            /// Player Movement
            if (PlayerVelocity.Y < 0)
            {
                IsStanding = false;
            }
            if (MovementFactor == 3)
            {
                RunBoost = 6;
            }
            else
            {
                RunBoost = 0;
            }
            if (PlayerVelocity.X != 0)
            {
                PlayerPos.X = PlayerPos.X + PlayerVelocity.X;
            }
            if (PlayerVelocity.X < -(MovementFactor * 6) - RunBoost)
            {
                PlayerVelocity.X = -(MovementFactor * 6) - RunBoost;
            }
            if (PlayerVelocity.X > (MovementFactor * 6) + RunBoost)
            {
                PlayerVelocity.X = (MovementFactor * 6) + RunBoost;
            }
            if (PlayerVelocity.X > 16)
            {
                PlayerVelocity.X = 16;
            }
            if (PlayerVelocity.X < -16)
            {
                PlayerVelocity.X = -16;
            }
            /// Movement Detection
            if (LeftPressed == true)
            {
                PlayerVelocity.X = PlayerVelocity.X - 4;
            }
            else if (IsStanding == true)
            {
                if (RightPressed == false && PlayerVelocity.X < 0)
                {
                    PlayerVelocity.X = PlayerVelocity.X + 2;
                }
                else if (RightPressed == false && PlayerVelocity.X > 0)
                {
                    PlayerVelocity.X = PlayerVelocity.X - 2;
                }
            }
            else
            {
                if (RightPressed == false && PlayerVelocity.X < 0)
                {
                    PlayerVelocity.X = PlayerVelocity.X + 1;
                }
                else if (RightPressed == false && PlayerVelocity.X > 0)
                {
                    PlayerVelocity.X = PlayerVelocity.X - 1;
                }
            }
            if (RightPressed == true)
            {
                PlayerVelocity.X = PlayerVelocity.X + 4;
            }
            /// Gravity and Velocity
            if (PlayerVelocity.Y != 0)
            {
                PlayerPos.Y = PlayerPos.Y + PlayerVelocity.Y;
            }
            if (UpPressed == true && IsStanding == true)
            {
                IsStanding = false;
                PlayerPos.Y = PlayerPos.Y + 3;
                PlayerVelocity.Y = JumpForce;
            }
            if (IsStanding == true)
            {
                PlayerVelocity.Y = 0;
            }
            else
            {
                PlayerVelocity.Y = PlayerVelocity.Y + Gravity.Y;
                if (PlayerVelocity.Y > 15)
                {
                    PlayerVelocity.Y = 15;
                }
            }
            if (CollidingLeft == true)
            {
                PlayerVelocity.X = 0;
            }
            if (CollidingRight == true)
            {
                PlayerVelocity.X = 0;
            }
        }
        public void DrawPlayer()
        {
            Draw.FillColor = Color.Black;
            Draw.Rectangle(PlayerPos.X,PlayerPos.Y,10,18);
        }
    }
}