using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Game
    {

        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        enum GameState{
            Play,
            Pause
        }
        GameState state;
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        private PlayerSpaceship playerShip;

        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
            playerShip = new PlayerSpaceship(new Vecteur2D((gameSize.Width / 2)-SpaceInvaders.Properties.Resources.ship3.Width/2, gameSize.Height - SpaceInvaders.Properties.Resources.ship3.Height), 3, SpaceInvaders.Properties.Resources.ship3);
            AddNewGameObject(playerShip);

            int decalage = ((gameSize.Width / 3)/2) - SpaceInvaders.Properties.Resources.bunker.Width/2;

            Bunker b1 = new Bunker(new Vecteur2D(decalage, gameSize.Height-(gameSize.Height/4)));
            AddNewGameObject(b1);

            Bunker b2 = new Bunker(new Vecteur2D((gameSize.Width / 3)+ decalage, gameSize.Height - (gameSize.Height / 4)));
            AddNewGameObject(b2);

            Bunker b3 = new Bunker(new Vecteur2D(((gameSize.Width / 3) * 2)+ decalage, gameSize.Height - (gameSize.Height / 4)));
            AddNewGameObject(b3);

            EnemyBlock ennemieBlock = new EnemyBlock(new Vecteur2D(0, 0), gameSize.Width/2);
            ennemieBlock.AddLine(5, 1, SpaceInvaders.Properties.Resources.ship2);
            ennemieBlock.AddLine(5, 1, SpaceInvaders.Properties.Resources.ship4);
            ennemieBlock.AddLine(5, 1, SpaceInvaders.Properties.Resources.ship3);
            AddNewGameObject(ennemieBlock);

        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if (state == GameState.Play)
            {
                foreach (GameObject gameObject in gameObjects)
                    gameObject.Draw(this, g);
            }else if(state == GameState.Pause)
            {
                g.DrawString("Pause", defaultFont, blackBrush, new PointF(gameSize.Width / 2, gameSize.Height / 2));
            }
     
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            if (keyPressed.Contains(Keys.P))
            {
                if (state == GameState.Play)
                {
                    state = GameState.Pause;
                    ReleaseKey(Keys.P);
                }
                else if (state == GameState.Pause)
                {
                    state = GameState.Play;
                    ReleaseKey(Keys.P);
                }
            }

            if (state == GameState.Play)
            {
                // add new game objects
                gameObjects.UnionWith(pendingNewGameObjects);
                pendingNewGameObjects.Clear();

                // update each game object
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(this, deltaT);
                }

                // remove dead objects
                gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
            }
            
        }
        #endregion
    }
}
