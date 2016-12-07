using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {

    /// <summary>
    /// 方向
    /// </summary>
    public class Direction {

        public static readonly Direction Up = new Direction(0, -1);

        public static readonly Direction RightUp = new Direction(1, -1);

        public static readonly Direction Right = new Direction(1, 0);

        public static readonly Direction RightDown = new Direction(1, 1);

        public static readonly Direction Down = new Direction(0, 1);

        public static readonly Direction LeftDown = new Direction(-1, 1);

        public static readonly Direction Left = new Direction(-1, 0);

        public static readonly Direction LeftUp = new Direction(-1, -1);

        public static readonly Direction[] All4 = new Direction[] {
            Up, Right, Down, Left
        };

        public static readonly Direction[] All4Naname = new Direction[] {
            RightUp, RightDown, LeftDown, LeftUp
        };

        public static readonly Direction[] All8 = new Direction[] {
            Up, RightUp, Right, RightDown, Down, LeftDown, Left, LeftUp
        };

        /// <summary>xの移動補正</summary>
        public readonly int OffsetX;

        /// <summary>yの移動補正</summary>
        public readonly int OffsetY;

        /// <summary>
        /// 私的コンストラクタ
        /// </summary>
        /// <param name="ox"></param>
        /// <param name="oy"></param>
        private Direction(int ox, int oy) {
            OffsetX = ox;
            OffsetY = oy;
        }
    }
}
