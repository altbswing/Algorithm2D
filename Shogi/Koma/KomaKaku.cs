using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {

    /// <summary>
    /// 角
    /// </summary>
    public class KomaKaku : Koma {

        /// <summary>
        /// 移動できる範囲を求めるd
        /// </summary>
        /// <param name="now">今クリックいるマスの座標</param>
        /// <param name="map">盤面状況</param>
        /// <returns>
        /// 移動可能のマスの座標の集合
        /// </returns>
        protected override HashSet<Location> _MoveRule(Location now, Koma[,] map) {
            // 戻り用の集合
            var canMoves = new HashSet<Location>();
            // 4方向チェック
            foreach(var dir in Direction.All4Naname) {
                // 単方向直進
                for(var p = now + dir; p.In(map); p += dir) {
                    var koma = map[p.X, p.Y];
                    // 空白マス、あるいは相手のコマ
                    if(koma == null || Direction != koma.Direction) {
                        canMoves.Add(p);
                    }
                    // コマに当たった場合
                    if(koma != null) {
                        break;
                    }
                }
            }
            // なりの場合
            if(IsNari) {
                // 十字を追加
                canMoves.UnionWith(_Cross(now));
            }
            return canMoves;
        }
    }
}
