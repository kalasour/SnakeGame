using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SnakeGame
{

    public class SnakeGameController : Controller
    {
        Timer timer;
        Timer pause;
        private bool canPress = true;

        public SnakeGameController()
        {
            // update the board every one second;
            Timer = new Timer(SnakeGameModel.TIME_BASE / SnakeGameModel.Speed);
            Timer.Enabled = false;
            Timer.Elapsed += this.OnTimedEvent;
            pause = new Timer(500);
            pause.Enabled = false;
            pause.Elapsed += OnTimedEvent2;
        }

        public Timer Timer { get => timer; set => timer = value; }

        public void KeyUpHandled(KeyboardState ks)
        {
            int direction = -1;
            Keys[] keys = ks.GetPressedKeys();

            if (keys.Contains(Keys.Up))
            {
                direction = SnakeGameModel.MOVE_UP;
            }
            else if (keys.Contains(Keys.Down))
            {
                direction = SnakeGameModel.MOVE_DOWN;
            }
            else if (keys.Contains(Keys.Left))
            {
                direction = SnakeGameModel.MOVE_LEFT;
            }
            else if (keys.Contains(Keys.Right))
            {
                direction = SnakeGameModel.MOVE_RIGHT;
            }
            else if (keys.Contains(Keys.Space))
            {

                if (canPress&&timer.Enabled)
                {
                    Stop();
                    pause.Enabled = true;
                    canPress = false;
                }
                else if(canPress && !timer.Enabled)
                {
                    Start();
                    canPress = false;
                    pause.Enabled = true;
                }

            }
            // Find all snakeboard model we know
            if (direction == -1) return;
            foreach (Model m in mList)
            {
                if (m is SnakeGameModel)
                {
                    // Tell the model to update
                    SnakeGameModel sbm = (SnakeGameModel)m;
                    sbm.SetDirection(direction);
                }
            }

        }


        public void Start()
        {
            Timer.Enabled = true;
        }


        public void Stop()
        {
            // Stop the game
            Timer.Enabled = false;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Snake.Debug("TE");
            foreach (Model m in mList)
            {
                if (m is SnakeGameModel)
                {
                    SnakeGameModel sbm = (SnakeGameModel)m;
                    sbm.Move();
                    sbm.Update();
                }
            }
            Timer.Interval = SnakeGameModel.TIME_BASE / SnakeGameModel.Speed;
        }
        private void OnTimedEvent2(Object source, ElapsedEventArgs e)
        {
            Snake.Debug("Can switch!");
            canPress = true;
            pause.Enabled = false;
        }

    }
}
