using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedySnakeGame
{
    public interface IEntity
    {
        Color EntityColor { set; get; }

        int Location_X { set; get; }
        int Location_Y { set; get; }
        void Clear();

    }
}
