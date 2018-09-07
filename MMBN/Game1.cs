using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MMBN.Combat.Objects;
using MMBN.Combat.Controllers;

namespace MMBN
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    //      Variables
    //    Player
    Player player;
    Vector2 playerVector = new Vector2(0, 0);

    // Field
    MovementController movementController;
    float waitLeft = 0;
    float waitRight = 0;
    float waitUp = 0;
    float waitDown = 0;
    float waitAttack = 0;

    //      Textures
    //    Field
    Texture2D tilesTexture;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here
      player = new Player();
      movementController = new MovementController();

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      tilesTexture = Content.Load<Texture2D>("Graphics/Battlefield/Tiles");
      player.setTexture(Content.Load<Texture2D>("Graphics/Character/Character"));
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // TODO: Add your update logic here
      // Check if the player is allowed to move yet
      waitUp -= (float)gameTime.ElapsedGameTime.Milliseconds;
      waitDown -= (float)gameTime.ElapsedGameTime.Milliseconds;
      waitLeft -= (float)gameTime.ElapsedGameTime.Milliseconds;
      waitRight -= (float)gameTime.ElapsedGameTime.Milliseconds;
      waitAttack -= (float)gameTime.ElapsedGameTime.Milliseconds;
      handleMovement();

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // TODO: Add your drawing code here
      spriteBatch.Begin();
      spriteBatch.Draw(tilesTexture, new Vector2(0, 0), Color.White);
      spriteBatch.Draw(player.getTexture(), playerVector, null, Color.White, 0f, new Vector2(player.getTexture().Width / 2, player.getTexture().Height / 2), Vector2.One, SpriteEffects.None, 0f);
      spriteBatch.End();

      base.Draw(gameTime);
    }

    // TODO separate file
    private void handleMovement()
    {
      var kstate = Keyboard.GetState();
      if (kstate.IsKeyDown(Keys.Up) && waitUp <= 0)
      {
        // Check if the movement is possible, check if the new space is available
        if ((player.getPosition().Item2 > 0) && (movementController.updateFieldSpace(player.getPosition(), (player.getPosition().Item1, player.getPosition().Item2 - 1))))
        {
          player.setPosition((player.getPosition().Item1, player.getPosition().Item2 - 1));
          waitUp = 150;
        }
      }
      else if (kstate.IsKeyDown(Keys.Down) && waitDown <= 0)
      {
        if ((player.getPosition().Item2 < 5) && (movementController.updateFieldSpace(player.getPosition(), (player.getPosition().Item1, player.getPosition().Item2 + 1))))
        {
          player.setPosition((player.getPosition().Item1, player.getPosition().Item2 + 1));
          waitDown = 150;
        }
      }
      else if (kstate.IsKeyDown(Keys.Left) && waitLeft <= 0)
      {
        if ((player.getPosition().Item1 > 0) && (movementController.updateFieldSpace(player.getPosition(), (player.getPosition().Item1 - 1, player.getPosition().Item2))))
        {
          player.setPosition((player.getPosition().Item1 - 1, player.getPosition().Item2));
          waitLeft = 150;
        }
      }
      else if (kstate.IsKeyDown(Keys.Right) && waitRight <= 0)
      {
        if ((player.getPosition().Item1 < 2) && (movementController.updateFieldSpace(player.getPosition(), (player.getPosition().Item1 + 1, player.getPosition().Item2))))
        {
          player.setPosition((player.getPosition().Item1 + 1, player.getPosition().Item2));
          waitRight = 150;
        }
      }

      if (kstate.IsKeyDown(Keys.Space) && waitAttack <= 0)
      {
        if (player.getTexture() == Content.Load<Texture2D>("Graphics/Character/Character"))
        {
          player.setTexture(Content.Load<Texture2D>("Graphics/Character/Character_Attack_1"));
        }
        else
        {
          waitAttack = 150;
          player.setTexture(Content.Load<Texture2D>("Graphics/Character/Character_Attack_2"));
        }
      }
      else if (waitAttack <= 0)
      {
        player.setTexture(Content.Load<Texture2D>("Graphics/Character/Character"));
      }

      playerVector.Y = 200 + (player.getPosition().Item2 * 96);
      playerVector.X = 58 + (player.getPosition().Item1 * 106);
    }
  }
}
