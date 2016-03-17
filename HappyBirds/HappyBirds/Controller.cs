using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyBirds
{
    static class Controller
    {
        static MouseState oldMouseState = Mouse.GetState();
        static MouseState newMouseState;

        static KeyboardState oldKeyboardState = Keyboard.GetState();
        static KeyboardState newKeyboardState;

        public static bool LeftClick = false;
        public static bool LeftHold = false;
        public static bool LeftReleased = false;
        public static Point MousePos = new Point(0, 0);

        public static bool KeyPressed(Keys key)
        {
            return newKeyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key);
        }



        public static void Update(GameTime gameTime)
        {
            oldKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();
            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();

            MousePos.X = newMouseState.X;
            MousePos.Y = newMouseState.Y;

           

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                //LeftClick
                LeftClick = true;
                LeftHold = false;
                LeftReleased = false;
                
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed) 
            {
                //LeftHold
                LeftClick = false;
                LeftHold = true;
                LeftReleased = false;
                //Console.WriteLine("Hold");
                //will need a counter here
            }
            else if (newMouseState.LeftButton == ButtonState.Released && (LeftClick || LeftHold))
            {
                //LeftHold
                LeftClick = false;
                LeftHold = false;
                LeftReleased = true;
                //Console.WriteLine("Hold");
                //will need a counter here
            }
            else 
            {
                //Hover
                LeftClick = false;
                LeftHold = false;
                LeftReleased = false;

                //tempcounter++;
                //tempcounter %= 10;
                //if (tempcounter == 0)
                //{
                //    HandleMouseHover(newMouseState);
                //}
            }

        }
        
    }
}
