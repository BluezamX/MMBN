using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMBN.Combat.Objects
{
  class Player : iSpace
  {
    private ValueTuple<int, int> playerPosition;
    private Texture2D playerTexture;

    public Player()
    {
      this.playerPosition = (1, 1);
    }

    public bool isEmpty()
    {
      return false;
    }

    // Getters / Setters

    public Texture2D getTexture()
    {
      return playerTexture;
    }

    public void setTexture(Texture2D new_texture)
    {
      this.playerTexture = new_texture;
    }

    public ValueTuple<int, int> getPosition()
    {
      return playerPosition;
    }

    public void setPosition(ValueTuple<int, int> new_position)
    {
      this.playerPosition = new_position;
    }
  }
}
