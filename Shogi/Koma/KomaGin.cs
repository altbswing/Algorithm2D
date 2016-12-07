using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {

    /// <summary>
    /// 銀
    /// </summary>
    public class KomaGin : Koma {

        /// <summary>
        /// 移動できる範囲を求めるd
        /// </summary>
        /// <param name="now">今クリックいるマスの座標</param>
        /// <param name="map">盤面状況</param>
        /// <returns>
        /// 移動可能のマスの座標の集合
        /// </returns>
        protected override HashSet<Location> _MoveRule(Location now, Koma[,] map) {
            // なりの場合
            if(IsNari) {
                return _KinMoveRule(now, map);
            }
            // 戻り用
            var canMoves = new HashSet<Location>();
            // バツ
            canMoves.UnionWith(_Batsu(now));
            // 前一歩
            canMoves.Add(now + Direction);

            return canMoves;
        }
    }
}
