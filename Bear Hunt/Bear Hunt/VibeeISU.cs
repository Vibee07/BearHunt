///Author: Vibeesshanan Thevamanoharan
///File Name: Game 1    
///Project Name: Bear Hunt
///Creation Date: December 20th 2017
///Modified Date: January 22th 2018
///Description: The player's goal is to defend his fresh fish for as long as he can until all the animals take his food, 
///the more animals that are killed the higher the score the player gets. The player is going to be motivated to try and beat their previous score, 
///and even challenge friends to try and beat their scores.
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bear_Hunt
{
    public class VibeeISU : Microsoft.Xna.Framework.Game
    {
        #region Global Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        #region Gamestates
       //5 different Gamestates
        enum GameState
        {
            GameScreen,
            MenuScreen,
            EndScreen,
            InstructScreen,
            InstructScreen2,
            StatsScreen
        }

        //Sets current game state to the Menu
        GameState currentGameState = GameState.MenuScreen;
        #endregion

        #region Endscreen
        Texture2D EndScreen;
        Rectangle EndScreenRec;
        SpriteFont EndFont;
        Vector2 EndFontLoc = new Vector2(140, 400);
        Texture2D MainBtn;
        Rectangle MainBtnRec;
        #endregion

        #region Mouse and Keyboard
        //Keyboard and Mouse
        KeyboardState kb;
        KeyboardState prevkb;
        MouseState mouse;
        MouseState prevmouse;
        //Crosshair
        Texture2D crosshair;
        Rectangle crosshairRec;
        #endregion

        #region MenuScreen
        //Menu screen background
        Texture2D MenuScreen;
        Rectangle MenuScreenRec;
        //Main Menu Buttons
        Texture2D StartButton;
        Rectangle StartButtonRec;
        Texture2D InstructButton;
        Rectangle InstructButtonRec;
        Texture2D LeaderBoardButton;
        Rectangle LeaderBoardButtonRec;
        Texture2D ExitButton;
        Rectangle ExitButtonRec;
        bool isMenu;
        #endregion

        #region InstructionScreens
        //Background for instruct screens
        Texture2D InstructScreen;
        Rectangle InstructScreenRec;
        Texture2D InstructScreen2;
        Rectangle InstructScreen2Rec;
        //Buttons for Instruct screens
        Texture2D BackButton1;
        Rectangle BackButton1Rec;
        Rectangle BackButton2Rec;
        Texture2D NextButton;
        Rectangle NextButtonRec;
        #endregion

        #region Statistics
        Texture2D StatsScreen;
        Rectangle StatsScreenRec;
        Rectangle BackButton3Rec;
        Vector2 AccuracyLoc = new Vector2(470, 210);
        Vector2 GamesPlayedLoc = new Vector2(730, 375);
        Vector2 BearsKilledLoc = new Vector2(680, 550);
        Vector2 BirdsKilledLoc = new Vector2(675, 720);
        Vector2 WolvesKilledLoc = new Vector2(750, 890);
        double ShotsTaken;
        double ShotsCount;
        double Accuracy;
        int BearsKilled;
        int BirdsKilled;
        int WolvesKilled;
        int GamesPlayed;

        SpriteFont StatsFont;
        #endregion

        #region HUD
        //Background for the gamplay screen
        Texture2D GameScreen;
        Rectangle GameScreenRec;
        //Score
        int score = 0;
        SpriteFont scoreText;
        Vector2 scoreTextLoc = new Vector2(1390, 10);
        Texture2D Whiteline;
        Rectangle ScoreBorder1;
        Rectangle ScoreBorder2;

        //Reloading and Bullets
        Texture2D BulletImg;
        Rectangle[] Bullet = new Rectangle[6];
        int BulletCount = 0;
        bool isbullet = true;
        float reloadTime = 1000f;
        bool isRealoding = false;

        //Health
        Texture2D Fish;
        Rectangle [] FishRec = new Rectangle [5]; 
        int PlayerHealth = 5;
        //Objective of the game
        Texture2D FishPot;
        Rectangle FishPotRec;
        
       
       
        //Time played
        int MinsPlayed = 0;
        double GameTimer;
        Vector2 TimeTextLoc = new Vector2(30, 10);
        
        //Homebutton
        Texture2D HomeButton;
        Rectangle HomeButtonRec;
        #endregion

        #region Borders
        //Borders around the screen
        const int RightBorder = 1700;
        const int LeftBorder = 0;
        const int TopBorder = 0;
        const int BottomBorder = 1000;
        #endregion

        #region AnimalVARIABLES

        #region BearVariables
        //Sprites and Images
        Texture2D[] CrntBearDirect = new Texture2D[5];
        Texture2D BearWalking;
        Texture2D BearWalkingBack;
        Texture2D BearDead;
        //Rectangles
        Rectangle BearWalkingSourceRec;
        Rectangle [] BearWalkingRec = new Rectangle[5];
        //Bear Status
        bool [] isBearAlive = new bool [5] {true,true,true,true,true};
        bool [] isBearShot = new bool[5];
        int [] BearHealth = new int [5] {3,3,3,3,3};
        //Action Timers
        float [] BearTimer = new float [5] {1,1,1,1,1};
        float [] BearSpawn = new float [5] {5,5,5,5,5};
        int bearLocation;
        //BearMovement
        bool [] BearMovement = new bool [5] {true,true,true,true,true};
        int[] BearDirect = new int[5];
        //Speed of the bear
        float BearSpeed = 2f;
        #endregion

        #region BirdVariables
        //Sprites and Images
        Texture2D BirdFlying;
        Texture2D BirdFlyingBack;
        Texture2D [] CrntBirdDirect = new Texture2D [5];
        Texture2D BirdShot;
        Texture2D BirdDead;
        //Rectangles
        Rectangle[] BirdFlyingRec = new Rectangle[5];
        Rectangle BirdFlyingSourceRec;
        //Birds Status
        bool[] isBirdAlive = new bool[5] { true, true, true, true, true };
        bool[] isBirdShot = new bool[5];
        bool[] isBirdDead = new bool[5];
        //Action Timers
        float [] BirdTimer = new float [5] {1,1,1,1,1};
        float [] BirdSpawn = new float [5] {10,10,10,10,10};
        //Bird Movement
        bool[] BirdMovement = new bool[5] { true, true, true, true, true };
        int [] FlyDirectX = new int [5];
        int[] FlyDirectY = new int[5];
        //Speed of the Bird
        float BirdSpeedY = 3.0f;
        float BirdSpeedX = 5.0f;
        #endregion

        #region WolfVariables
        //Sprites and Images
        Texture2D WolfRunning;
        Texture2D WolfRunningBack;
        Texture2D WolfDead;
        Texture2D[] CrntWolfDirect = new Texture2D[5];
        //Rectangles
        Rectangle[] WolfRunningRec = new Rectangle[5];
        Rectangle WolfRunningSourceRec;
        //Wolf Status
        bool[] isWolfAlive = new bool[5] {true,true,true,true,true};
        bool[] isWolfShot = new bool[5];
        int[] WolfHealth = new int[5] { 3, 3, 3, 3, 3 };
        //Action Timers
        float[] WolfTimer = new float[5] {1,1,1,1,1};
        float[] WolfSpawn = new float[5] {5,5,5,5,5};
        int WolfLoction;
        //BearMovement
        bool[] WolfMovement = new bool[5] { true, true, true, true, true };
        int[] WolfDirect = new int[5];
        //Speed of the Wolf
        float WolfSpeed = 3f;
        #endregion
        
        //Randomizing variables, for moevemnet, spawn, and offset
        Random Movement = new Random();
        Random rngSpawner = new Random();
        Random rngOffset = new Random();

        #endregion

        #region Animations
        //Timer for animation
        double AnimTime;
        //Frame aniamtion
        float Elapsed = 0;
        float Delay = 800;
        int frames = 0;
        #endregion

        #region Audio
        SoundEffect GunShot;
        SoundEffectInstance GunShotInst;
        SoundEffect BearGrowl;
        SoundEffectInstance BearGrowlInst;
        SoundEffect WolfHowl;
        SoundEffectInstance WolfHowlInst;
        SoundEffect CrowCall;
        SoundEffectInstance CrowCallInst;
        SoundEffect Reloading;
        SoundEffectInstance ReloadingInst;
        Song RockMusic;
        #endregion

        #region Nightmaremode
        const int BearNoseX1 = 490;
        const int BearNoseY = 465;
        const int BearNoseX2 = 595;
        const int BearNoseY2 = 515;

        bool isNightmare = false;
        #endregion

        #endregion
        public VibeeISU()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            //Changes the dimension of the program
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1500;
            graphics.ApplyChanges();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Width and Height variables for display
            int width = (this.graphics.GraphicsDevice.Viewport.Width);
            int height = (this.graphics.GraphicsDevice.Viewport.Height);

            #region Screens
            //Loads the different screens: Menu, Main, Instruct, LeaderBoards
            GameScreen = Content.Load<Texture2D>(@"Images\Backgrounds\BearHuntBackground");
            GameScreenRec = new Rectangle(0, 0, (int)(width), (int)(height));
            MenuScreen = Content.Load<Texture2D>(@"Images\Backgrounds\AngryBear");
            MenuScreenRec = new Rectangle(0, 0, (int)(width), (int)(height));
            InstructScreen = Content.Load<Texture2D>(@"Images\Backgrounds\InstructionScreen");
            InstructScreenRec = new Rectangle(0, 0, (int)(width), (int)(height));
            InstructScreen2 = Content.Load<Texture2D>(@"Images\Backgrounds\InstructScreen2");
            InstructScreen2Rec = new Rectangle(0, 0, (int)(width), (int)(height));
            EndScreen = Content.Load<Texture2D>(@"Images\Backgrounds\EndScreen");
            EndScreenRec = new Rectangle(0, 0, (int)(width), (int)(height));
            StatsScreen = Content.Load<Texture2D>(@"Images\Backgrounds\StatsScreen");
            StatsScreenRec = new Rectangle(0, 0, (int)(width), (int)(height));
            #endregion

            #region Buttons
            //Loads All the buttons used in the Menu
            StartButton = Content.Load<Texture2D>(@"Images\Pictures\StartButton");
            StartButtonRec = new Rectangle(1170, 270, (int)(width * 0.215), (int)(height * 0.135));
            InstructButton = Content.Load<Texture2D>(@"Images\Pictures\InstructButton");
            InstructButtonRec = new Rectangle(1075, 420, (int)(width * 0.300), (int)(height * 0.110));
            LeaderBoardButton = Content.Load<Texture2D>(@"Images\Pictures\LeaderBoardButton");
            LeaderBoardButtonRec = new Rectangle(1075, 560, (int)(width * 0.300), (int)(height * 0.110));
            ExitButton = Content.Load<Texture2D>(@"Images\Pictures\ExitButton");
            ExitButtonRec = new Rectangle(1300, 815, (int)(width * 0.215), (int)(height * 0.135));
            //Loads all the buttons used in the Instructions screens
            BackButton1 = Content.Load<Texture2D>(@"Images\Pictures\BackButton");
            BackButton1Rec = new Rectangle(30, 920, (int)(width * 0.065), (int)(height * 0.075));
            BackButton2Rec = new Rectangle(30, 920, (int)(width * 0.065), (int)(height * 0.075));
            NextButton = Content.Load<Texture2D>(@"Images\Pictures\NextButton");
            NextButtonRec = new Rectangle(1575, 920, (int)(width * 0.065), (int)(height * 0.075));
            //Loads the button in the exit screen
            MainBtn = Content.Load<Texture2D>(@"Images\Pictures\MainBtn");
            MainBtnRec = new Rectangle(490, 700, (int)(width * 0.35), (int)(height * 0.25));
            EndFont = Content.Load<SpriteFont>(@"Font\EndFont");
            //Loads the button and font for stats screen
            BackButton3Rec = new Rectangle(30, 30, (int)(width * 0.065), (int)(height * 0.075));
            StatsFont = Content.Load<SpriteFont>(@"Font\Statistics");
            #endregion

            #region HUD
            //Loads the crosshair used as the mouse
            crosshair = Content.Load<Texture2D>(@"Images\Pictures\CrossHair");
            crosshairRec =  new Rectangle(200, 400, (int)(width * 0.04), (int)(height * 0.05));

            //Loads the borders around the score
            Whiteline = Content.Load<Texture2D>(@"Images\Pictures\Whiteline");
            ScoreBorder1 = new Rectangle (1370, 70, (int)(width * 0.20), (int)(height* 0.003));
            ScoreBorder2 = new Rectangle(1370, 0, (int)(width * 0.0023), (int)(height * 0.072));
            //Loads the font for the Score and Timer, in the HUD
            scoreText = Content.Load<SpriteFont>(@"Font\ScoreFont");
            //Loads image and rectangle for the homebutton
            HomeButton = Content.Load<Texture2D>(@"Images\Pictures\Homebutton");
            HomeButtonRec = new Rectangle(1580,920, (int)(width * 0.065), (int)(height * 0.075));

            //Loads all 6 bullets, in the HUD
            BulletImg = Content.Load<Texture2D>(@"Images\Pictures\Bullet");
            Bullet[0] = new Rectangle(1375, 75, (int)(width * 0.023), (int)(height * 0.070));
            Bullet[1] = new Rectangle(1425, 75, (int)(width * 0.023), (int)(height * 0.070));
            Bullet[2] = new Rectangle(1475, 75, (int)(width * 0.023), (int)(height * 0.070));
            Bullet[3] = new Rectangle(1525, 75, (int)(width * 0.023), (int)(height * 0.070));
            Bullet[4] = new Rectangle(1575, 75, (int)(width * 0.023), (int)(height * 0.070));
            Bullet[5] = new Rectangle(1625, 75, (int)(width * 0.023), (int)(height * 0.070));

            //Loads the fish/health, in the HUD
            Fish = Content.Load<Texture2D>(@"Images\Pictures\Fish");
            FishRec[0] = new Rectangle(10, 900, (int)(width * 0.07), (int)(height * 0.08));
            FishRec[1] = new Rectangle(130, 900, (int)(width * 0.07), (int)(height * 0.08));
            FishRec[2] = new Rectangle(250, 900, (int)(width * 0.07), (int)(height * 0.08));
            FishRec[3] = new Rectangle(370, 900, (int)(width * 0.07), (int)(height * 0.08));
            FishRec[4] = new Rectangle(490, 900, (int)(width * 0.07), (int)(height * 0.08));
            //Lods the image and rectangle for the fishpot, main objective of the game
            FishPot = Content.Load<Texture2D>(@"Images\Pictures\Fishpot");
            FishPotRec = new Rectangle(830, 610, (int)(width * 0.125), (int)(height * 0.125));
            #endregion
            
            #region BearLoadContent
            //Loads the status animations for the bear
            BearWalking = Content.Load<Texture2D>(@"Sprites\BearWalkingSpriteSheet");
            BearDead = Content.Load<Texture2D>(@"Sprites\BearDead");
            BearWalkingBack = Content.Load<Texture2D>(@"Sprites\BearWalkingSpriteSheetRev");
            //Loads the current status of the bear as the foward walking motion
            CrntBearDirect[0] = BearWalking;
            CrntBearDirect[1] = BearWalking;
            CrntBearDirect[2] = BearWalking;
            CrntBearDirect[3] = BearWalking;
            CrntBearDirect[4] = BearWalking;
            //Loads the rectangles for the bear
            BearWalkingRec[0] = new Rectangle(20, 600, (BearWalking.Width / 6), (BearWalking.Height));
            BearWalkingRec[1] = new Rectangle(-1300, 600, (BearWalking.Width / 6), (BearWalking.Height));
            BearWalkingRec[2] = new Rectangle(-2000, 600, (BearWalking.Width / 6), (BearWalking.Height));
            BearWalkingRec[3] = new Rectangle(2000, 600, (BearWalking.Width / 6), (BearWalking.Height));
            BearWalkingRec[4] = new Rectangle(4000, 600, (BearWalking.Width / 6), (BearWalking.Height));
            BearWalkingSourceRec = new Rectangle(0, 0, (BearWalking.Width / 6), (BearWalking.Height));
            #endregion

            #region BirdLoadContent
            //Loads the status animations for the bird
            BirdFlying = Content.Load<Texture2D>(@"Sprites\BirdFlyingSpriteSheetRev");
            BirdShot = Content.Load<Texture2D>(@"Sprites\BirdShot");
            BirdDead = Content.Load<Texture2D>(@"Sprites\BirdDead");
            BirdFlyingBack = Content.Load<Texture2D>(@"Sprites\BirdFlyingSpriteSheet");
            //Loads the current status of the bird as the foward flying
            CrntBirdDirect[0] = BirdFlying;
            CrntBirdDirect[1] = BirdFlying;
            CrntBirdDirect[2] = BirdFlying;
            CrntBirdDirect[3] = BirdFlying;
            CrntBirdDirect[4] = BirdFlying;
            //Loads the rectangles for the bird
            BirdFlyingRec[0] = new Rectangle(-100, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
            BirdFlyingRec[1] = new Rectangle(-4500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
            BirdFlyingRec[2] = new Rectangle(7500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
            BirdFlyingRec[3] = new Rectangle(-7500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
            BirdFlyingRec[4] = new Rectangle(10500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
            BirdFlyingSourceRec = new Rectangle(0, 0, (BirdFlying.Width / 6), (BirdFlying.Height));
            #endregion

            #region WolfLoadContent
            //Loads the status animation for the bear
            WolfRunning = Content.Load<Texture2D>(@"Sprites\WolfRunningSpriteSheet");
            WolfRunningBack = Content.Load<Texture2D>(@"Sprites\WolfRunningSpriteSheetRev");
            WolfDead = Content.Load<Texture2D>(@"Sprites\WolfDead");
            //Loads the current status of the wolf as the foward running
            CrntWolfDirect[0] = WolfRunning;
            CrntWolfDirect[1] = WolfRunning;
            CrntWolfDirect[2] = WolfRunning;
            CrntWolfDirect[3] = WolfRunning;
            CrntWolfDirect[4] = WolfRunning;
            //Loads the rectangles for the wolf
            WolfRunningRec[0] = new Rectangle(2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
            WolfRunningRec[1] = new Rectangle(4000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
            WolfRunningRec[2] = new Rectangle(3000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
            WolfRunningRec[3] = new Rectangle(-2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
            WolfRunningRec[4] = new Rectangle(-3000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
            WolfRunningSourceRec = new Rectangle(0, 0, (WolfRunning.Width / 6), (WolfRunning.Height));
            #endregion

            #region AudioLoadContent
            //Loads all audio, such as sound effects, and songs
            GunShot = Content.Load<SoundEffect>(@"Audio\SoundEffects\GunShot");
            GunShotInst = GunShot.CreateInstance();
            BearGrowl = Content.Load <SoundEffect>(@"Audio\SoundEffects\BearGrowl");
            BearGrowlInst = BearGrowl.CreateInstance();
            WolfHowl = Content.Load<SoundEffect>(@"Audio\SoundEffects\WolfHowl");
            WolfHowlInst = WolfHowl.CreateInstance();
            CrowCall = Content.Load<SoundEffect>(@"Audio\SoundEffects\CrowCall");
            CrowCallInst = CrowCall.CreateInstance();
            Reloading = Content.Load<SoundEffect>(@"Audio\SoundEffects\Reloading");
            ReloadingInst = Reloading.CreateInstance();

            RockMusic = Content.Load<Song>(@"Audio\Songs\RockMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(RockMusic);
            #endregion

        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            //Gets the state of the keyboard
            prevkb = kb;
            kb = Keyboard.GetState();

            //Gets the state of the mouse
            prevmouse = mouse;
            mouse = Mouse.GetState();

            //Sets the mouse to the center of the crosshair
            crosshairRec.X = mouse.X - (crosshairRec.Width/2);
            crosshairRec.Y = mouse.Y - (crosshairRec.Height / 2);

            //Whenever the mouse is clicked a gunshot sound is made
            if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && isRealoding == false)
            {
                GunShotInst.Play();
            }
            //This allows the user to exit the program
            if (kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            
            if (ShotsTaken >= 1)
            {
                Accuracy = ShotsCount / ShotsTaken;
                Accuracy = Accuracy * 100;
            }

            //When "+" is pressed increase volume
            if (kb.IsKeyDown(Keys.Add))
            {
                MediaPlayer.Volume = Math.Min(1.0f, MediaPlayer.Volume + 0.01f);
            }
            //Wehn "-" is pressed decrease volume
            else if (kb.IsKeyDown(Keys.Subtract))
            {
                MediaPlayer.Volume = Math.Min(1.0f, MediaPlayer.Volume - 0.01f);
            }

            //All the gamestates 
            switch (currentGameState)
            {
                #region MenuScreen
                case GameState.MenuScreen:
                    MainMenuButtons();
                    isMenu = true;
                    NewGame();
                    NightMare();
                    
                    break;
                #endregion

                #region Instruction Screens
                case GameState.InstructScreen:
                    //If the back button is pressed return to the menu screen
                    if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > BackButton1Rec.X && mouse.X < (BackButton1Rec.X + BackButton1Rec.Width)
                      && mouse.Y > BackButton1Rec.Y && mouse.Y < (BackButton1Rec.Y + BackButton1Rec.Height))
                    {
                        currentGameState = GameState.MenuScreen;
                    }
                    //If the next button is pressed go to the second instructions screen
                    if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > NextButtonRec.X && mouse.X < (NextButtonRec.X + NextButtonRec.Width)
                      && mouse.Y > NextButtonRec.Y && mouse.Y < (NextButtonRec.Y + NextButtonRec.Height))
                    {
                        currentGameState = GameState.InstructScreen2;
                    }
                    break;

                case GameState.InstructScreen2:
                    //If the back button is pressed return to the first instructions screen
                    if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > BackButton2Rec.X && mouse.X < (BackButton2Rec.X + BackButton2Rec.Width)
                      && mouse.Y > BackButton2Rec.Y && mouse.Y < (BackButton2Rec.Y + BackButton2Rec.Height))
                    {
                        currentGameState = GameState.InstructScreen;
                    }
                    break;
                #endregion

                #region Endscreen
                case GameState.EndScreen:
                    //If the "return to main menu" button is pressed return to the Menu Screen
                    if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > MainBtnRec.X && mouse.X < (MainBtnRec.X + MainBtnRec.Width)
                      && mouse.Y > MainBtnRec.Y && mouse.Y < (MainBtnRec.Y + MainBtnRec.Height))
                    {
                        currentGameState = GameState.MenuScreen;
                        GamesPlayed++;
                    }
                    break;
                #endregion

                #region StatsScreen
                case GameState.StatsScreen:
                    //If the back button is pressed return to the menu screen
                    if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > BackButton3Rec.X && mouse.X < (BackButton3Rec.X + BackButton3Rec.Width)
                      && mouse.Y > BackButton3Rec.Y && mouse.Y < (BackButton3Rec.Y + BackButton3Rec.Height))
                    {
                        currentGameState = GameState.MenuScreen;
                    }
                    break;
                #endregion

                #region Gameplay Screen
                case GameState.GameScreen:
                    isMenu = false;
                    Bear(gameTime);
                    AnimalAnimation(gameTime);
                    Bird(gameTime);
                    Wolf(gameTime);
                    Ammunition(gameTime);
                    AnimalSpeed();
                    Health();
                //Tracks the amount of time played in the game
                GameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (GameTimer >= 59)
                {
                    GameTimer = 0;
                    MinsPlayed++;
                }
                //If the home button is pressed, the gamescreen is switched to the Menu
                if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > HomeButtonRec.X && mouse.X < (HomeButtonRec.X + HomeButtonRec.Width)
                     && mouse.Y > HomeButtonRec.Y && mouse.Y < (HomeButtonRec.Y + HomeButtonRec.Height))
                    {
                        currentGameState = GameState.MenuScreen;
                        GamesPlayed++;
                        isMenu = true;
                    }

                if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && isRealoding == false)
                {
                    ShotsTaken++;
                }
                    break;
                #endregion
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {   
            spriteBatch.Begin();

            switch (currentGameState)
            {
                //Everything drawn on the menuscreen
                #region MenuScreen
                case GameState.MenuScreen:
                 //Draws the image for the main menu: Title, and Bear Logo
                 spriteBatch.Draw(MenuScreen, MenuScreenRec, Color.White);
                 //Draws all buttons in the Menu Screen
                 spriteBatch.Draw(StartButton, StartButtonRec, Color.White);
                 spriteBatch.Draw(InstructButton, InstructButtonRec, Color.White);
                 spriteBatch.Draw(LeaderBoardButton, LeaderBoardButtonRec, Color.White);
                 spriteBatch.Draw(ExitButton, ExitButtonRec, Color.White);
                    break;
                #endregion

                //Everthing drawn on the instruction screens
                #region Instruction Screen
                case GameState.InstructScreen:
                    spriteBatch.Draw(InstructScreen, InstructScreenRec, Color.White);
                    spriteBatch.Draw(BackButton1, BackButton1Rec, Color.White);
                    spriteBatch.Draw(NextButton, NextButtonRec, Color.White);
                    break;
                case GameState.InstructScreen2:
                    spriteBatch.Draw(InstructScreen2, InstructScreen2Rec, Color.White);
                    spriteBatch.Draw(BackButton1, BackButton2Rec, Color.White);
                    break;
                #endregion

                //Everything drawn on the end screen
                #region EndScreen
                case GameState.EndScreen:
                    spriteBatch.Draw(EndScreen, EndScreenRec, Color.White);
                    spriteBatch.DrawString(EndFont, "FINAL SCORE: " + score, EndFontLoc, Color.Red);
                    spriteBatch.Draw(MainBtn, MainBtnRec, Color.White);
                    break;
                #endregion

                //Everything drawn on the statistics screen
                #region StatsScren
                //Everything drawn on the LeaderBoard Screen
                case GameState.StatsScreen:
                    spriteBatch.Draw(StatsScreen, StatsScreenRec, Color.White);
                    spriteBatch.Draw(BackButton1, BackButton3Rec, Color.White);
                    spriteBatch.DrawString(StatsFont," " + Math.Round(Accuracy,2) + " %  ", AccuracyLoc, Color.Red);
                    spriteBatch.DrawString(StatsFont, " " + BearsKilled , BearsKilledLoc, Color.Red);
                    spriteBatch.DrawString(StatsFont, " " + BirdsKilled , BirdsKilledLoc, Color.Red);
                    spriteBatch.DrawString(StatsFont, " " + WolvesKilled , WolvesKilledLoc, Color.Red);
                    spriteBatch.DrawString(StatsFont, " " + GamesPlayed, GamesPlayedLoc, Color.Red);

                    break;
                #endregion

                //Everything drawn on the Main gameplay screen
                #region Gameplay Screen
                case GameState.GameScreen:
                    //Draws the Background for the gameplay
                    spriteBatch.Draw(GameScreen, GameScreenRec, Color.White);

                    //Draws the score and the timer at the top
                    spriteBatch.DrawString(scoreText, "Score: " + score, scoreTextLoc, Color.White);
                    spriteBatch.DrawString(scoreText, "Time: " + MinsPlayed + " Mins " + Math.Round(GameTimer) + " Secs", TimeTextLoc, Color.White);
                    spriteBatch.Draw(Whiteline, ScoreBorder1, Color.White);
                    spriteBatch.Draw(Whiteline, ScoreBorder2, Color.White);

                    //Draws all 6 bulets
                    while (isbullet == true)
                    {
                        for (int i = 0; i < Bullet.Length; i++)
                        {
                            spriteBatch.Draw(BulletImg, Bullet[i], Color.White);
                        }
                        break;
                    }
                    //Draws the homebutton at the bottom right
                    spriteBatch.Draw(HomeButton, HomeButtonRec, Color.White);

                    //Draws pot in the center
                    spriteBatch.Draw(FishPot, FishPotRec, Color.White);
                    //Draws the 5 fish as health in the 
                    for (int f = 0; f < FishRec.Length; f++)
                    {
                        spriteBatch.Draw(Fish, FishRec[f], Color.White);                        
                    }

                    //Draws the Wolves
                    for (int i = 0; i < 5; i++)
                    {
                        if (isWolfAlive[i] == true)
                        {
                            spriteBatch.Draw(CrntWolfDirect[i], WolfRunningRec[i], WolfRunningSourceRec, Color.White);
                        }
                    }
                    //Draws the Bears
                    for (int i = 0; i < 5; i++)
                    {
                        if (isBearAlive[i] == true)
                        {
                            spriteBatch.Draw(CrntBearDirect[i], BearWalkingRec[i], BearWalkingSourceRec, Color.White);
                        }
                    }
                    //Draws the Birds
                    for (int i = 0; i < 5; i++)
                    {
                        if (isBirdAlive[i] == true)
                        {
                            spriteBatch.Draw(CrntBirdDirect[i], BirdFlyingRec[i], BirdFlyingSourceRec, Color.White);
                        }
                    }
                    break;
                #endregion
            }

            //Draws the mouse as the crosshair
            spriteBatch.Draw(crosshair, crosshairRec, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        //All the buttons on the Menu screen
        private void MainMenuButtons() 
        {       
                //Start Button
                    if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > StartButtonRec.X && mouse.X < (StartButtonRec.X + StartButtonRec.Width)
                              && mouse.Y > StartButtonRec.Y && mouse.Y < (StartButtonRec.Y + StartButtonRec.Height))
                {
                    //Switches to the gamplay screen
                    currentGameState = GameState.GameScreen;
                    isNightmare = false;
                }
            //Exit Button
            if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > ExitButtonRec.X && mouse.X < (ExitButtonRec.X + ExitButtonRec.Width)
                      && mouse.Y > ExitButtonRec.Y && mouse.Y < (ExitButtonRec.Y + ExitButtonRec.Height))
            {
                //Exits the game
                this.Exit();
            }
            //Instructions Button
            if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > InstructButtonRec.X && mouse.X < (InstructButtonRec.X + InstructButtonRec.Width)
                      && mouse.Y > InstructButtonRec.Y && mouse.Y < (InstructButtonRec.Y + InstructButtonRec.Height))
            {
                //Switches the Intructions screen
                currentGameState = GameState.InstructScreen;
            }
            //LeaderBoards button
            if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && mouse.X > LeaderBoardButtonRec.X && mouse.X < (LeaderBoardButtonRec.X + LeaderBoardButtonRec.Width)
                      && mouse.Y > LeaderBoardButtonRec.Y && mouse.Y < (LeaderBoardButtonRec.Y + LeaderBoardButtonRec.Height))
            {
                //Switches the Intructions screen
                currentGameState = GameState.StatsScreen;
            }

            //Sets nighmare mode to true
            if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released)
            {
                if (mouse.X >= BearNoseX1 && mouse.X <= BearNoseX2 && mouse.Y >= BearNoseY && mouse.Y <= BearNoseY2)
                {
                    currentGameState = GameState.GameScreen;
                    isNightmare = true;
                }
            }
        }
        //All bullets and reloading mechanics
        private void Ammunition(GameTime gameTime)
        {
            //While the user is not reloading
            if (isRealoding == false)
            {
                //If the user clicks/shoots during gameplay
                if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released)
                {
                    BulletCount++;
                }

                //If no bullets are shot sets bullets to original locations
                if (BulletCount == 0)
                {
                    Bullet[0].X = 1375;
                    Bullet[1].X = 1425;
                    Bullet[2].X = 1475;
                    Bullet[3].X = 1525;
                    Bullet[4].X = 1575;
                    Bullet[5].X = 1625;
                }

                //When first bullet is shot remove it
                if (BulletCount == 1)
                {
                    Bullet[0].X = Bullet[5].X;
                }
                //2nd bullet
                else if (BulletCount == 2)
                {
                    Bullet[1].X = Bullet[5].X;
                }
                //3rd bullet
                else if (BulletCount == 3)
                {
                    Bullet[2].X = Bullet[5].X;
                }
                //4th bullet
                else if (BulletCount == 4)
                {
                    Bullet[3].X = Bullet[5].X;
                }
                //5 bulltet
                else if (BulletCount == 5)
                {
                    Bullet[4].X = Bullet[5].X;
                }
                //When the 6th bullet is shot the user will start reloading
                if (BulletCount == 6 || kb.IsKeyDown(Keys.R))
                {
                    BulletCount = 0;
                    isbullet = false;
                    //Starts the reloading
                    isRealoding = true;
                }
            }
            //Starts the timer taken to reload
            if (isRealoding == true)
            {
                reloadTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                ReloadingInst.Play();
            }
            //Once reloading is done sets everything back to the original value, given the user 6 bullets again
            if (reloadTime <= 0)
            {
                isRealoding = false;
                reloadTime = 1000;
                isbullet = true;
            }
        }
        //All 3 animal's movement animations
        private void AnimalAnimation(GameTime gameTime)
        {
            Elapsed += (float)(gameTime.ElapsedGameTime.TotalMilliseconds);
            //Sets the animations to change between the sprites
            if (Elapsed >= Delay)
            {
                 frames = (frames + 1) % 6;
                 Elapsed = 0; 
            }
            //Sets the source rectangle for the three animal's animations
            BearWalkingSourceRec = new Rectangle((BearWalking.Width / 6) * frames, 0, (BearWalking.Width / 6), (BearWalking.Height));
            BirdFlyingSourceRec = new Rectangle((BirdFlying.Width / 6) * frames, 0, (BirdFlying.Width / 6), (BirdFlying.Height));
            WolfRunningSourceRec = new Rectangle((WolfRunning.Width / 6) * frames, 0, (WolfRunning.Width / 6), (WolfRunning.Height));
        }
        //All gameplayer related to the Bird
        private void Bird(GameTime gameTime)
        {
            Elapsed += (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            for (int i = 0; i < 5; i++)
            {
                //Controls the rate of direction change
                AnimTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (AnimTime >= 0.8)
                {
                    if (BirdMovement[i] == true)
                    {
                        FlyDirectX[i] = Movement.Next(1, 3);
                        FlyDirectY[i] = Movement.Next(1, 3);
                    }
                    //Sets the total time back to 0
                    AnimTime = 0;
                }

                //If the bird is moving
                if (BirdMovement[i] == true)
                {
                    //Allows the bird to the Left
                    if (FlyDirectX[i] == 1)
                    {
                        BirdFlyingRec[i].X -= (int)(BirdSpeedX);
                        CrntBirdDirect[i] = BirdFlyingBack;
                    }
                    //Right
                    else if (FlyDirectX[i] == 2)
                    {
                        BirdFlyingRec[i].X += (int)(BirdSpeedX);
                        CrntBirdDirect[i] = BirdFlying;
                    }
                    //Down
                    if (FlyDirectY[i] == 1)
                    {
                        BirdFlyingRec[i].Y -= (int)(BirdSpeedY);
                    }
                    //Up
                    else if (FlyDirectY[i] == 2)
                    {
                        BirdFlyingRec[i].Y += (int)(BirdSpeedY);
                    }
                }
                //Detect for colision between the fish and the fishpot, if collsion occers the following happens
                if (BirdFlyingRec[i].X <= (FishPotRec.X + FishPotRec.Width) && (BirdFlyingRec[i].X + BirdFlyingRec[i].Width) >= FishPotRec.X
                    && (BirdFlyingRec[i].Y <= (FishPotRec.Y + FishPotRec.Height) && (BirdFlyingRec[i].Y + BirdFlyingRec[i].Height) >= FishPotRec.Y && isBirdShot[i] == false))
                {
                    //Bear "dies" and is set away from pot
                    isBirdAlive[i] = false;
                    //Player loses 1 health
                    PlayerHealth--;
                }
                //Checks for if a bullet is shot, while the gun is not reloading, and the bear is not shot
                if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && isRealoding == false && isBirdShot[i] == false) 
                {
                    //Checks for collision betweenn the crosshair and the rectangle of the bird
                    if (mouse.X >= BirdFlyingRec[i].X && mouse.X <= BirdFlyingRec[i].X + BirdFlyingRec[i].Width
                    && mouse.Y >= BirdFlyingRec[i].Y && mouse.Y <= BirdFlyingRec[i].Y + BirdFlyingRec[i].Height)
                    {
                        //Score increases by 25
                        score = score + 25;
                        BirdsKilled++;
                        //The bird status is set to shot
                        isBirdShot[i] = true;
                        CrowCallInst.Play();
                        ShotsCount++;
                    }
                }
                //When the bird is shot, it will start falling
                if (isBirdShot[i] == true)
                {
                    CrntBirdDirect[i] = BirdShot;
                    FlyDirectX[i] = 3;
                    FlyDirectY[i] = 2;
                    BirdSpeedY = 7;
                }
                //When the vird hits the ground, the bird dies, and movement stops
                if (isBirdShot[i] == true && BirdFlyingRec[i].Y + BirdFlyingRec[i].Height >= BottomBorder)
                {
                    BirdMovement[i] = false;
                    isBirdDead[i] = true;
                }
                //Once the bird is dead, timer for dispearing beings, and current animation changes to a daed bird
                if (isBirdDead[i] == true)
                {
                    BirdTimer[i] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    CrntBirdDirect[i] = BirdDead;
                }
                //Once the dead timer reaches 0, the bird disapears 
                if (BirdTimer[i] <= 0)
                {
                    isBirdAlive[i] = false;
                }
                //When the bird disapears, it is set to a random location, and the respawn timer begings
                if (isBirdAlive[i] == false)
                {
                    BirdSpawn[i] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    BirdFlyingRec[i].X = -1000;
                    BirdFlyingRec[i].Y = -1000;
                    isBirdDead[i] = false;
                    isBirdShot[i] = false;
                }
                //When the respawn timer reaches 0, the bird respawns at one of 2 locations
                if (BirdSpawn[i] <= 0)
                {
                    BirdMovement[i] = true;
                    isBirdAlive[i] = true;
                    BirdTimer[i] = 1.0f;
                    BirdSpawn[i] = 5.0f;
                    BirdSpeedY = 3;
                    CrntBirdDirect[i] = BirdFlying;
                    BirdFlyingRec[i] = new Rectangle(20, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    BirdFlyingRec[i] = new Rectangle(800, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                }
                //Does allow the bird to fly outside the borders
                //Left Border
                if (BirdFlyingRec[i].X <= LeftBorder)
                {
                    FlyDirectX[i] = 2;
                }
                //RightBorder
                else if (BirdFlyingRec[i].X + BirdFlyingRec[0].Width >= RightBorder)
                {
                    FlyDirectX[i] = 1;
                }
                //Top Border
                else if (BirdFlyingRec[i].Y <= TopBorder)
                {
                    FlyDirectY[i] = 2;
                }
                //Bottom Border
                else if (BirdFlyingRec[i].Y + BirdFlyingRec[i].Height >= BottomBorder)
                {
                    FlyDirectY[i] = 1;
                }
            }
        }
        //All gameplay related to the Bear
        private void Bear(GameTime gameTime)
        {
            Elapsed += (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            for (int i = 0; i < 5; i++)
            {
                //Rate of direction change of bear
                if (BearMovement[i] == true)
                {
                    BearDirect[i] = Movement.Next(1, 3);
                }
                //Randomizes the loation of the bears spawn
                bearLocation = rngSpawner.Next(1, 3);
                
                //Creates a vector for the position of the bear and fishpot
                Vector2 bearPt = new Vector2(BearWalkingRec[i].X ,BearWalkingRec[i].Y);
                Vector2 fishPt = new Vector2(FishPotRec.X, FishPotRec.Y);

                //Allows the bears to walktowards the fishpot
                if (BearMovement[i] == true && isBearAlive[i]== true)
                {
                    //Gets the distance between the bear and the fishpot
                    float distance = (bearPt - fishPt).Length();
                    if (distance > BearSpeed)
                    {
                        Vector2 chaseVector = (fishPt - bearPt);
                        chaseVector.Normalize();
                        chaseVector.X = chaseVector.X * (int)BearSpeed;
                        chaseVector.Y = chaseVector.Y * (int)BearSpeed;
                        //If the bear is on the Left side of the fish the foward walking animation is used
                        if (BearWalkingRec[i].X <= FishPotRec.X)
                        {
                            BearWalkingRec[i].X += (int)chaseVector.X;
                            CrntBearDirect[i] = BearWalking;
                        }
                        //Or else the backward animation is used
                        else if (BearWalkingRec[i].X >= FishPotRec.X)
                        {
                            BearWalkingRec[i].X += (int)chaseVector.X;
                            CrntBearDirect[i] = BearWalkingBack;
                        }
                    }
                }
                //Detect for colision between the bear and the fishpot, if collsion occers the following happens
                if (BearWalkingRec[i].X <= (FishPotRec.X + FishPotRec.Width) && (BearWalkingRec[i].X + BearWalkingRec[i].Width) >= FishPotRec.X
                    && (BearWalkingRec[i].Y <= (FishPotRec.Y + FishPotRec.Height) && (BearWalkingRec[i].Y + BearWalkingRec[i].Height) >= FishPotRec.Y))
                {
                    //Bear "dies" and is set away from pot
                    isBearAlive[i] = false;
                    //Player loses 1 health
                    PlayerHealth--;
                }
                //Checks for if a bullet is shot, while the gun is not reloading, and the bear is not shot
                if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && isRealoding == false && isBearShot[i] == false)
                {
                    //Checks for collision betweenn the crosshair and the rectangle of the bear
                    if (mouse.X >= BearWalkingRec[i].X && mouse.X <= BearWalkingRec[i].X + BearWalkingRec[i].Width
                        && mouse.Y >= BearWalkingRec[i].Y && mouse.Y <= BearWalkingRec[i].Y + BearWalkingRec[i].Height)
                    {
                        //Bear loses 1 health
                        BearHealth[i]--;
                        //Score increases by 15
                        score = score + 15;
                        ShotsCount++;
                        //Plays the beargrowl sound effect
                        if (BearHealth[i] == 0)
                        {
                            BearsKilled++;
                            BearGrowlInst.Play();
                        }
                    }
                }
                //When the bears health depletes, the status of the bear is set to shot
                if (BearHealth[i] == 0)
                {
                    isBearShot[i] = true;
                }
                //When the bear status is shot, the animation changes, movement stops, and the timer for disapearing begins
                if (isBearShot[i] == true)
                {
                    BearTimer[i] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    CrntBearDirect[i] = BearDead;
                    BearMovement[i] = false;
                }
                //Once the timer is over, the bear disapears
                if (BearTimer[i] <= 0)
                {
                    isBearAlive[i] = false;
                }
                //When the bear is not alive or "visible", respawn timer begins and bear is set to a random location
                if (isBearAlive[i] == false)
                {
                    BearSpawn[i] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    BearWalkingRec[i].X = -1000;
                    BearWalkingRec[i].Y = -1000;
                    isBearShot[i] = false;
                }
                //When the respawn timer is over, the bear returns, and all status are set back to normal
                if (BearSpawn[i] <= 0)
                {
                    BearMovement[i] = true;
                    CrntBearDirect[i] = BearWalking;
                    BearHealth[i] = 3;
                    isBearAlive[i] = true;
                    BearTimer[i] = 1.0f;
                    BearSpawn[i] = 5.0f;
        
                    //Sets the respawn to a random location
                    if (bearLocation == 1)
                    {
                        BearWalkingRec[i] = new Rectangle(2000 + 200 * rngOffset.Next(1, 4), 600, (BearWalking.Width / 6), (BearWalking.Height));
                    }
                    else if (bearLocation == 2)
                    {
                        BearWalkingRec[i] = new Rectangle(-800 - 200 * rngOffset.Next(1, 4), 600, (BearWalking.Width / 6), (BearWalking.Height));
                    }
                }
            }
        } 
        //All the gameplay related to the Wolf
        private void Wolf(GameTime gameTime)
        {
            Elapsed += (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            for (int i = 0; i < 5; i++)
            {
                //Rate of direction change of wolf
                if (WolfMovement[i] == true)
                {
                    WolfDirect[i] = Movement.Next(1, 3);
                }
                //Randomizes the location of the wolf spawn
                WolfLoction = rngSpawner.Next(1, 3);

                //Creates a vector for the postion of the wolf and fishpot
                Vector2 wolfPt = new Vector2(WolfRunningRec[i].X ,WolfRunningRec[i].Y);
                Vector2 fishPt = new Vector2(FishPotRec.X, FishPotRec.Y);

                //Allows the the wolves to walk towards the fish
                if (WolfMovement[i] == true && isWolfAlive[i] == true)
                {
                    //Gets the distance between the wolf and the fishpot
                    float distance = (wolfPt - fishPt).Length();
                    if (distance > WolfSpeed)
                    {
                        Vector2 chaseVector = (fishPt - wolfPt);

                        chaseVector.Normalize();

                        chaseVector.X = chaseVector.X * (int)WolfSpeed;
                        chaseVector.Y = chaseVector.Y * (int)WolfSpeed;
                        //If the wolf is on the Left side of the fish the foward walking animation is used
                        if (WolfRunningRec[i].X <= FishPotRec.X)
                        {
                            WolfRunningRec[i].X += (int)chaseVector.X;
                            CrntWolfDirect[i] = WolfRunning;
                        }
                        //Or else the backward animation is used
                        if (WolfRunningRec[i].X >= FishPotRec.X)
                        {
                            WolfRunningRec[i].X += (int)chaseVector.X;
                            CrntWolfDirect[i] = WolfRunningBack;
                        }
                    }
                }
                //Detect for colision between the bear and the fishpot, if collsion occers the following happens
                if (WolfRunningRec[i].X <= (FishPotRec.X + FishPotRec.Width) && (WolfRunningRec[i].X + WolfRunningRec[i].Width) >= FishPotRec.X
                    && (WolfRunningRec[i].Y <= (FishPotRec.Y + FishPotRec.Height) && (WolfRunningRec[i].Y + WolfRunningRec[i].Height) >= FishPotRec.Y))
                {
                    //Wolf "dies" and is set away from pot
                    isWolfAlive[i] = false;
                    //Player loses 1 health
                    PlayerHealth--;
                }
                //Checks for if a bullet is shot, while the gun is not reloading, and the wolf is not shot
                if (mouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && isRealoding == false && isWolfShot[i] == false) 
                {
                    //Checks for collision betweenn the crosshair and the rectangle of the wolf
                    if (mouse.X >= WolfRunningRec[i].X && mouse.X <= WolfRunningRec[i].X + WolfRunningRec[i].Width
                        && mouse.Y >= WolfRunningRec[i].Y && mouse.Y <= WolfRunningRec[i].Y + WolfRunningRec[i].Height)
                    {
                        //Wolf loses 1 health
                        WolfHealth[i]--;
                        //Score increases by 15
                        score = score + 15;
                        ShotsCount++;

                        if (WolfHealth[i] == 0)
                        {
                            WolvesKilled++;
                            WolfHowlInst.Play();
                            
                        }
                    }
                }
                //When the wolf health depletes, the status of the wolf is set to shot
                if (WolfHealth[i] == 0)
                {
                    isWolfShot[i] = true;
                }
                //When the wolf status is shot, the animation changes, movement stops, and the timer for disapearing begins
                if (isWolfShot[i] == true)
                {
                    WolfTimer[i] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    CrntWolfDirect[i] = WolfDead;
                    WolfMovement[i] = false;
                }
                //Once the timer is over, the wolf disapears
                if (WolfTimer[i] <= 0)
                {
                    isWolfAlive[i] = false;
                }
                //When the wolf is not alive or "visible", respawn timer begins and wolf is set to a random location
                if (isWolfAlive[i] == false)
                {
                    WolfSpawn[i] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    WolfRunningRec[i].X = -1000;
                    WolfRunningRec[i].Y = -1000;
                    isWolfShot[i] = false;
                }
                //When the respawn timer is over, the bear returns, and all status are set back to normal
                if (WolfSpawn[i] <= 0)
                {
                    WolfMovement[i] = true;
                    CrntWolfDirect[i] = WolfRunning;
                    WolfHealth[i] = 2;
                    isWolfAlive[i] = true;
                    WolfTimer[i] = 1.0f;
                    WolfSpawn[i] = 5.0f;
                    //Sets the respawn to a random location
                    if (WolfLoction == 1)
                    {
                        WolfRunningRec[i] = new Rectangle(2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                    }
                    if (WolfLoction == 2)
                    {
                        WolfRunningRec[i] = new Rectangle(-2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                    }
                }
            }
        }
       //Determines the speed of the animals
        private void AnimalSpeed()
        {
            if (score >=0 && score <= 99)
            {
                BirdSpeedX = 5f;
                BirdSpeedY = 3f;
                BearSpeed = 2;
                WolfSpeed = 3;
            }
            //As the score increases so does the speed
            if (score >= 100 && score <= 199)
            {
                BearSpeed = 2.25f;
                WolfSpeed = 3.25f;
            }
            else if (score >= 200 && score <= 299)
            {
                BearSpeed = 2.5f;
                WolfSpeed = 3.5f;
            }
            else if (score >= 300 && score <= 399)
            {
                BearSpeed = 2.75f;
                WolfSpeed = 3.75f;
            }
            else if (score >= 400 && score <= 499)
            {
                BearSpeed = 3f;
                WolfSpeed = 4f;
            }
            else if (score >= 500 && score <= 699)
            {
                BearSpeed = 3.25f;
                WolfSpeed = 4.25f;
            }
            else if (score >= 700 && score <= 999)
            {
                BearSpeed = 3.5f;
                WolfSpeed = 4.5f;
            }
            else if (score >= 1000 && score <= 1499)
            {
                BearSpeed = 3.75f;
                WolfSpeed = 4.75f;
            }
            else if (score >= 1500 && score <= 1999)
            {
                BearSpeed = 4f;
                WolfSpeed = 5f;
            }
            else if (score >= 2000 && score <= 2500)
            {
                BearSpeed = 4.5f;
                WolfSpeed = 5.5f;
            }
            if (score >= 4499)
            {
                BearSpeed = 5f;
                WolfSpeed = 6f;
            }
        }
        //Player health
        private void Health()
        {
            //If health is full all fish are active
            if (PlayerHealth == 5)
            {
                FishRec[0].X = 10;
                FishRec[1].X = 130;
                FishRec[2].X = 250;
                FishRec[3].X = 370;
                FishRec[4].X = 490;
            }
            //Eachtime a health is lost 1 fish will deplete
            if (PlayerHealth <= 4)
            {
                FishRec[4].X = FishRec[0].X;
            }
            if (PlayerHealth <= 3)
            {
                FishRec[3].X = FishRec[0].X;
            }
            if (PlayerHealth <= 2)
            {
                FishRec[2].X = FishRec[0].X;
            }
            if (PlayerHealth <= 1)
            {
                FishRec[1].X = FishRec[0].X;
            }
            if (PlayerHealth == 0)
            {
                currentGameState = GameState.EndScreen;
            }
        }
        //When the menuscreen is active a new game is automatically started
        private void NewGame()
        {
            if (isMenu == true)
            {
                for (int i =0; i < 5; i ++)
                {
                    //Resets all player info
                    PlayerHealth = 5;
                    score = 0;
                    GameTimer = 0;
                    MinsPlayed = 0;
                    isbullet = true;
                    BulletCount = 0;

                    //Resets all the status of the bear
                    BearHealth[i] = 3;
                    isBearAlive[i] = true;
                    isBearShot[i] = false;
                    BearWalkingRec[0] = new Rectangle(20, 600, (BearWalking.Width / 6), (BearWalking.Height));
                    BearWalkingRec[1] = new Rectangle(-1300, 600, (BearWalking.Width / 6), (BearWalking.Height));
                    BearWalkingRec[2] = new Rectangle(-2000, 600, (BearWalking.Width / 6), (BearWalking.Height));
                    BearWalkingRec[3] = new Rectangle(2000, 600, (BearWalking.Width / 6), (BearWalking.Height));
                    BearWalkingRec[4] = new Rectangle(4000, 600, (BearWalking.Width / 6), (BearWalking.Height));

                    //Resets all the status of the wolves
                    WolfHealth[i] = 2;
                    isWolfAlive[i] = true;
                    isWolfShot[i] = false;
                    WolfRunningRec[0] = new Rectangle(2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                    WolfRunningRec[1] = new Rectangle(4000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                    WolfRunningRec[2] = new Rectangle(3000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                    WolfRunningRec[3] = new Rectangle(-2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                    WolfRunningRec[4] = new Rectangle(-3000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));

                    //Resets all status of the bird
                    isBirdAlive[i] = true;
                    isBirdDead[i] = false;
                    isBirdShot[i] = false;
                    BirdFlyingRec[0] = new Rectangle(20, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    BirdFlyingRec[1] = new Rectangle(-4500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    BirdFlyingRec[2] = new Rectangle(7500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    BirdFlyingRec[3] = new Rectangle(-7500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    BirdFlyingRec[4] = new Rectangle(10500, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    BirdFlyingSourceRec = new Rectangle(0, 0, (BirdFlying.Width / 6), (BirdFlying.Height));
                }
            }
        }
        //NightMare Mode
        private void NightMare()
        {
            if (isNightmare == true)
            {
                //Sets the score to highest setting
                score = 4500;
                for (int i = 0; i < 5; i++)
                {
                    score = 4500;
                    //Sets the wolfspawn timer to 2 seconds instead of the normal 5
                    if (WolfSpawn[i] <= 0)
                    {
                        WolfMovement[i] = true;
                        CrntWolfDirect[i] = WolfRunning;
                        WolfHealth[i] = 2;
                        isWolfAlive[i] = true;
                        WolfTimer[i] = 1.0f;
                        WolfSpawn[i] = 2.0f;
                        //Sets the respawn to a random location
                        if (WolfLoction == 1)
                        {
                            WolfRunningRec[i] = new Rectangle(2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                        }
                        if (WolfLoction == 2)
                        {
                            WolfRunningRec[i] = new Rectangle(-2000, 600, (WolfRunning.Width / 6), (WolfRunning.Height));
                        }
                    }
                    //Sets the bearspawn timer to 2 seconds instead of the normal 5
                    if (BearSpawn[i] <= 0)
                    {
                        BearMovement[i] = true;
                        CrntBearDirect[i] = BearWalking;
                        BearHealth[i] = 3;
                        isBearAlive[i] = true;
                        BearTimer[i] = 1.0f;
                        BearSpawn[i] = 2.0f;
                        //Sets the respawn to a random location
                        if (bearLocation == 1)
                        {
                            BearWalkingRec[i] = new Rectangle(2000 + 200 * rngOffset.Next(1, 4), 600, (BearWalking.Width / 6), (BearWalking.Height));
                        }
                        else if (bearLocation == 2)
                        {
                            BearWalkingRec[i] = new Rectangle(-800 - 200 * rngOffset.Next(1, 4), 600, (BearWalking.Width / 6), (BearWalking.Height));
                        }
                    }
                    //Sets the birdspawn time to 2 seconds instead of the normal 5
                    if (BirdSpawn[i] <= 0)
                    {
                        BirdMovement[i] = true;
                        isBirdAlive[i] = true;
                        BirdTimer[i] = 1.0f;
                        BirdSpawn[i] = 2.0f;
                        BirdSpeedY = 3;
                        CrntBirdDirect[i] = BirdFlying;
                        BirdFlyingRec[i] = new Rectangle(20, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                        BirdFlyingRec[i] = new Rectangle(800, 20, (BirdFlying.Width / 6), (BirdFlying.Height));
                    }
                }
                if (PlayerHealth == 0)
                {
                    currentGameState = GameState.MenuScreen;
                }
            }
        }
    }
}