using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {

    /// <summary>
    /// 金
    /// </summary>
    public class KomaKin : Koma {

        /// <summary>なりしない</summary>
        public override bool IsNari { get { return false; } }

        /// <summary>
        /// 移動できる範囲を求めるd
        /// </summary>
        /// <param name="now">今クリックいるマスの座標</param>
        /// <param name="map">盤面状況</param>
        /// <returns>
        /// 移動可能のマスの座標の集合
        /// </returns>
        protected override HashSet<Location> _MoveRule(Location now, Koma[,] map) {
            return _KinMoveRule(now, map);
        }

        /// <summary>
        /// なり可能判断
        /// </summary>
        /// <param name="fromY">移動元のY</param>
        /// <param name="toY">移動先のY</param>
        /// <returns>
        /// プレーヤーに問い合わせの場合true
        /// </returns>
        public override bool WillNari(int fromY, int toY) {
            return false;
        }
    }
}
