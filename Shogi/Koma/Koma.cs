using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {

    /// <summary>
    /// ベースの駒
    /// </summary>
    public abstract class Koma {

        /// <summary>方向</summary>
        public Direction Direction { get; set; } = Direction.Up;

        /// <summary>なり</summary>
        public virtual bool IsNari { get; set; } = false;

        /// <summary>
        /// 移動できる範囲を求める
        /// 共通処理含める
        /// </summary>
        /// <param name="now"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public HashSet<Location> GetCanMove(Location now, Koma[,] map) {
            // 駒別処理
            var canMoves = _MoveRule(now, map);
            // 範囲外のマスを除く
            canMoves.RemoveWhere(p => !p.In(map));
            // 味方のいるマスを除く
            canMoves.RemoveWhere(p => map[p.X, p.Y] != null && map[p.X, p.Y].Direction == Direction);

            return canMoves;
        }

        /// <summary>
        /// 駒の移動ルールを求める
        /// 駒別で実装
        /// </summary>
        /// <param name="now">今クリックいるマスの座標</param>
        /// <param name="map">盤面状況</param>
        /// <returns>
        /// 移動可能のマスの座標の集合
        /// </returns>
        protected abstract HashSet<Location> _MoveRule(Location now, Koma[,] map);

        /// <summary>
        /// 駒台から持ち上げた時設定可能な箇所
        /// </summary>
        /// <param name="map">盤面状況</param>
        /// <returns></returns>
        public virtual HashSet<Location> CanSetFromDai(Koma[,] map) {
            // 戻り用のリスト
            var canSets = new HashSet<Location>();
            // 空白マスの集合を作成
            for(int x = 0; x < map.GetLength(0); x++) {
                for(int y = 0; y < map.GetLength(1); y++) {
                    if(map[x, y] != null) {
                        canSets.Add(new Location { X = x, Y = y });
                    }
                }
            }
            return canSets;
        }

        /// <summary>
        /// なり可能判断
        /// </summary>
        /// <param name="fromY">移動元のY</param>
        /// <param name="toY">移動先のY</param>
        /// <returns>
        /// プレーヤーに問い合わせの場合true
        /// </returns>
        public virtual bool WillNari(int fromY, int toY) {
            // 既になりの場合
            if(IsNari) {
                return false;
            }
            // 敵陣の範囲
            int min = Direction == Direction.Up ? 0 : 6;
            int max = min + 2;
            // なりロジック
            return (min <= fromY && fromY <= max)
                || (min <= toY && toY <= max);
        }

        /// <summary>
        /// 共通：金の移動ルール
        /// </summary>
        /// <param name="now"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        protected HashSet<Location> _KinMoveRule (
                Location now, Koma[,] map) {
            // 十字型
            var canMoves = _Cross(now);
            // 前斜め
            canMoves.Add(new Location {
                X = now.X + 1,
                Y = now.Y + Direction.OffsetY
            });
            // 前斜め
            canMoves.Add(new Location {
                X = now.X - 1,
                Y = now.Y + Direction.OffsetY
            });
            return canMoves;
        }

        /// <summary>
        /// 共通：一マス十字型
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        protected HashSet<Location> _Cross(Location now) {
            return new HashSet<Location> {
                now + Direction.Up,
                now + Direction.Down,
                now + Direction.Left,
                now + Direction.Right,
            };
        }

        /// <summary>
        /// 共通：一マス✕型
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        protected HashSet<Location> _Batsu(Location now) {
            return new HashSet<Location> {
                now + Direction.LeftUp,
                now + Direction.LeftDown,
                now + Direction.RightUp,
                now + Direction.RightDown,
            };
        }
    }
}
