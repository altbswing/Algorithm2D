using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {

    public class GameEntity {

        public int Id { get; set; }

        public List<Location> PointList { get; set; }

        public bool IsRound { get { return Id != 4; } }
    }
}
