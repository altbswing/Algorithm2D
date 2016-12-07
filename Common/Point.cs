using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {

    /// <summary>
    /// 座標
    /// 雑談：Pointと言う名前はSystemライブラリと色々と競合しているので
    /// 長いけど、Locationを使う
    /// </summary>
    public struct Location {

        /// <summary>座標X</summary>
        public int X { get; set; }

        /// <summary>座標Y</summary>
        public int Y { get; set; }

        /// <summary>
        /// 指定している方向に移動した後の座標
        /// </summary>
        /// <param name="p">座標</param>
        /// <param name="d">方向</param>
        /// <returns>1マス前進した結果</returns>
        public static Location operator +(Location p, Direction d) {
            return new Location {
                X = p.X + d.OffsetX,
                Y = p.Y + d.OffsetY
            };
        }

        /// <summary>
        /// 指定している方向の逆に移動した後の座標
        /// </summary>
        /// <param name="p">座標</param>
        /// <param name="d">方向</param>
        /// <returns>1マス後退した結果</returns>
        public static Location operator -(Location p, Direction d) {
            return new Location {
                X = p.X - d.OffsetX,
                Y = p.Y - d.OffsetY
            };
        }

        /// <summary>
        /// 範囲内にあるか
        /// </summary>
        /// <returns></returns>
        public bool In<T>(T[,] map) {
            if(map == null) {
                return false;
            }
            // 幅
            var w = map.GetLength(0);
            // 高さ
            var h = map.GetLength(1);
            // 範囲確認
            return 0 <= X && X < w && 0 <= Y && Y < h;
        }
    }
}
