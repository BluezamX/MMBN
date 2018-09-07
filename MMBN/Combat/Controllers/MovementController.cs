using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMBN.Combat.Objects;

namespace MMBN.Combat.Controllers
{
  class MovementController
  {
    private Dictionary<ValueTuple<int, int>, iSpace> fieldTracker = new Dictionary<ValueTuple<int, int>, iSpace>();

    public MovementController()
    {
      for (int i = 0; i < 5; i++)
      {
        for (int j = 0; j < 3; j++)
          fieldTracker.Add((i, j), new Empty());
      }
      fieldTracker[(1, 1)] = new NotEmpty();
    }
    
    public bool updateFieldSpace(ValueTuple<int, int> oldPosition, ValueTuple<int, int> newPosition)
    {
      if(fieldTracker.ContainsKey(newPosition) && fieldTracker[newPosition].isEmpty())
      {
        fieldTracker[oldPosition] = new Empty();
        return true;
      }
      return false;
    }
  }
}
