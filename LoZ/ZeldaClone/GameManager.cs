using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaClone.UI;

namespace ZeldaClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameManager : Game
    {
        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1024,
                PreferredBackBufferHeight = 960,
                IsFullScreen = false
            };

            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _uiSpriteBatch = new SpriteBatch(GraphicsDevice);
            _worldBatch = new SpriteBatch(GraphicsDevice);

            _rupeeIcon = Content.Load<Texture2D>("hud/hud_rupee_16x16");
            _hudBombIcon = Content.Load<Texture2D>("hud/hud_bomb_16x16");
            _hudKeyIcon = Content.Load<Texture2D>("hud/hud_key_16x16");
            _borderView = new BorderView
            {
                TopRight = Content.Load<Texture2D>("box_ne"),
                TopLeft = Content.Load<Texture2D>("box_nw"),
                BottomLeft = Content.Load<Texture2D>("box_sw"),
                BottomRight = Content.Load<Texture2D>("box_se"),
                Vertical = Content.Load<Texture2D>("box_v"),
                Horizontal = Content.Load<Texture2D>("box_h"),
            };

            _hudView = new HudView
            {
                HeartFull = Content.Load<Texture2D>("hud/hud_heart_16x16"),
                HeartHalf = Content.Load<Texture2D>("hud/hud_heart_half_16x16"),
                HeartEmpty = Content.Load<Texture2D>("hud/hud_heart_empty_16x16"),
                IconBomb = Content.Load<Texture2D>("hud/hud_bomb_16x16"),
                IconRupee = Content.Load<Texture2D>("hud/hud_rupee_16x16"),
                IconKey = Content.Load<Texture2D>("hud/hud_key_16x16")
            };

            

            _itemView = new ItemView
            {
                ItemIcons = new Dictionary<int, List<Texture2D>>
                {
                    {0, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("boomerang_16x32"),
                            Content.Load<Texture2D>("boomerang_blue_16x32"),
                        }
                    },
                    {1, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("bomb_16x32"),
                        }
                    },
                    {2, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("arrow_16x32"),
                            Content.Load<Texture2D>("arrow_silver_16x32")
                            
                        }
                    },
                    {3, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("bow_16x32"),
                            Content.Load<Texture2D>("bow_blue_16x32")
                        }
                    },
                    {4, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("candle_blue_32x32"),
                            Content.Load<Texture2D>("candle_red_32x32")
                        }
                    },
                    {5, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("whistle_16x32")
                        }
                    },
                    {6, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("meat_32x32")
                        }
                    },
                    {7, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("map_blue_16x32"),
                            Content.Load<Texture2D>("potion_blue_16x32"),
                            Content.Load<Texture2D>("potion_red_16x32")
                        }
                    },
                    {8, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("magic_rod_32x32"),
                        }
                    },
                    {9, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("raft_32x32"),
                        }
                    },
                    {10, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("book_16x32"),
                        }
                    },
                    {11, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("ring_blue_16x32"),
                            Content.Load<Texture2D>("ring_red_16x32")
                        }
                    },
                    {12, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("ladder_32x32")
                        }
                    },
                    {13, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("key_magic_16x32")
                        }
                    },
                    {14, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("bracelet_16x32")
                        }
                    },
                    {15, new List<Texture2D>
                        {
                            Content.Load<Texture2D>("sword_wood_16x32"),
                            Content.Load<Texture2D>("sword_magic_16x32"),
                            Content.Load<Texture2D>("sword_super_16x32"),
                        }
                    }
                }
            };

            _bButtonBorder = new Border(_borderView, 16, 3, 4);
            

            _pngFontTexture = Content.Load<Texture2D>("nesfont_black");
            _textSound = Content.Load<SoundEffect>("sound/text");

            var font = new NesSprteText(_pngFontTexture);

            _pngFont = new TypedSpriteText("IT'S DANGEROUS TO GO\n  ALONE! TAKE THIS.", 
                new Vector2(96f, 208f), _textSound, _pngFontTexture);

            _rupeeBankLabel = new NesBankNumber(font, _rupeeIcon);
            _bombBankLabel = new NesBankNumber(font, _hudBombIcon);
            _keyBankLabel = new NesBankNumber(font, _hudKeyIcon);

            _worldCamera = new Camera2D(1024, 960)
            {
                FocusVector = _worldCameraPlaying,
                Zoom = 2.0f
            };

            _uiCamera = new Camera2D(1024, 960)
            {
                FocusVector = _uiCameraPlaying,
                Zoom = 2.0f
            };

            _hud = new HeadsUpDisplay(_borderView, _hudView, _itemView, _pngFontTexture, 16)
            {
                Origin = new Vector2(0, 368),
                PlayerData =  new PlayerData
                {
                    Rupees = 197,
                    Bombs = 8,
                    Keys = 2
                }
            };
        }

        private ItemView _itemView;

        private Border _bButtonBorder;
        private BorderView _borderView;

        private HeadsUpDisplay _hud;

        private NesBankNumber _rupeeBankLabel;
        private NesBankNumber _bombBankLabel;
        private NesBankNumber _keyBankLabel;

        private Vector2 _uiCameraPlaying = new Vector2(256, 592);
        private Vector2 _uiCameraPause = new Vector2(256, 240);

        private Vector2 _worldCameraPlaying = new Vector2(256, 240);
        private Vector2 _worldCameraPause = new Vector2(256, -112);

        private bool _isPaused;

        private Vector2 NextUICamera
        {
            get { return _isPaused ? _uiCameraPause : _uiCameraPlaying; }
        }

        private Vector2 NextWorldCamera
        {
            get { return _isPaused ? _worldCameraPause : _worldCameraPlaying; }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gamePadState.Buttons.Start == ButtonState.Pressed &&
                _lastGamePadState.Buttons.Start == ButtonState.Released)
            {
                if (!_uiCamera.IsMoving)
                {
                    _isPaused = !_isPaused;
                    _uiCamera.MoveTo(NextUICamera);
                }

                if (!_worldCamera.IsMoving)
                {
                    _worldCamera.MoveTo(NextWorldCamera);
                }
            }

            _uiCamera.Update(gameTime);
            _worldCamera.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.LeftControl) &&
                _lastKeyboardState.IsKeyUp(Keys.LeftControl))
            {
                _pngFont.Reset();
            }

//            if (!_isPaused && !_uiCamera.IsMoving)
//            {
//                _pngFont.Update(gameTime);
//            }

            _lastGamePadState = gamePadState;
            _lastKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Cyan);
            _worldBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _worldCamera.Transform);
            _uiSpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _uiCamera.Transform);

            _worldBatch.FillRectangle(new Rectangle(0, 128, 512, 480), Color.Black);

            _worldBatch.FillRectangle(new Rectangle(0, 128, 512, 64), Color.Brown);
            _worldBatch.FillRectangle(new Rectangle(0, 128, 64, 336), Color.Brown);
            _worldBatch.FillRectangle(new Rectangle(448, 128, 64, 336), Color.Brown);
            _worldBatch.FillRectangle(new Rectangle(0, 416, 192, 64), Color.Brown);
            _worldBatch.FillRectangle(new Rectangle(320, 416, 192, 64), Color.Brown);

            _uiSpriteBatch.FillRectangle(new Rectangle(0, 0, 512, 480), Color.Black);

            _hud.Draw(_uiSpriteBatch);

            _uiSpriteBatch.End();
            _worldBatch.End();
            base.Draw(gameTime);
        }

        private GraphicsDeviceManager _graphics;

        private Camera2D _worldCamera;
        private SpriteBatch _worldBatch;

        private Camera2D _uiCamera;
        private SpriteBatch _uiSpriteBatch;

        private SoundEffect _textSound;
        private Texture2D _pngFontTexture;
        private TypedSpriteText _pngFont;

        private GamePadState _lastGamePadState;
        private KeyboardState _lastKeyboardState;
        private Texture2D _rupeeIcon;
        private Texture2D _hudBombIcon;
        private Texture2D _hudKeyIcon;
        private HudView _hudView;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }
    }
}
