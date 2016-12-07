using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Othello {

    /// <summary>アルゴリズム</summary>
    public class Algorithm {

        /// <summary>
        /// x,yを中心としてして、反転処理
        /// </summary>
        /// <param name="map">盤面</param>
        /// <param name="now">開始地点</param>
        /// <returns>反転対象の集合</returns>
        public HashSet<Location> ReverseList(Koma[,] map, Location now) {
            // セットした色
            var nowKoma = map[now.X, now.Y];
            // 反対側の色
            var otherKoma = nowKoma == Koma.White ? Koma.Black : Koma.White;
            // 反転の座標を記録する
            var reverseList = new HashSet<Location>();
            // 8方向解析
            foreach(var direction in Direction.All8) {
                // 一つ方向の反転対象
                var reverse = _DirectionReverse(map, now, direction);
                reverseList.UnionWith(reverse);
            }
            return reverseList;
        }

        /// <summary>
        /// 一つ方向の反転対象
        /// </summary>
        /// <param name="map">盤面</param>
        /// <param name="now">開始地点</param>
        /// <param name="direction">方向</param>
        /// <returns>一つ方向の反転対象</returns>
        private HashSet<Location> _DirectionReverse(Koma[,] map, Location now, Direction direction) {
            // セットした色
            var nowKoma = map[now.X, now.Y];
            // 反対側の色
            var otherKoma = nowKoma == Koma.White ? Koma.Black : Koma.White;
            // 反転対象の集合
            var reverses = new HashSet<Location>();
            // 現在座標の次のから解析
            for(var p = now + direction; p.In(map); p += direction) {
                // 現在の解析座標
                var checkKoma = map[p.X, p.Y];
                // 自分のコマに当たった
                if(checkKoma == nowKoma) {
                    return reverses;
                }
                // 空白に当たった
                if(checkKoma == Koma.None) {
                    // 反転しない
                    return new HashSet<Location>();
                }
                // 相手の石を反転対象に加える
                reverses.Add(p);
            }
            // 最後のコマをチェック
            if(reverses.Count > 0) {
                var last = reverses.Last();
                var lastKoma = map[last.X, last.Y];
                if(lastKoma != nowKoma) {
                    reverses.Clear();
                }
            }
            return reverses;
        }

        /// <summary>
        /// 設置可能な場所を返す
        /// </summary>
        /// <param name="map">盤面状況</param>
        /// <param name="nowKoma">手番のコマ</param>
        /// <returns></returns>
        public HashSet<Location> CanSets(Koma[,] map, Koma nowKoma) {
            // 戻り用の集合
            var canSets = new HashSet<Location>();
            // 全箇所チェック
            for(int y = 0; y < map.GetLength(1); y++) {
                for(int x = 0; x < map.GetLength(0); x++) {
                    // コマあり
                    if(map[x, y] != Koma.None) {
                        continue;
                    }
                    // コピー作成
                    var copy = (Koma[,])map.Clone();
                    // 1箇所チェック
                    var p = new Location { X = x, Y = y };
                    copy[p.X, p.Y] = nowKoma;
                    if(_CanSetPoint(copy, p)) {
                        canSets.Add(p);
                    }
                }
            }
            return canSets;
        }

        /// <summary>
        /// 該当ポイントのチェック
        /// </summary>
        /// <param name="map">盤面</param>
        /// <param name="check">確認箇所</param>
        /// <returns>反転対象の集合</returns>
        private bool _CanSetPoint(Koma[,] map, Location check) {
            var nowKoma = map[check.X, check.Y];
            // 反対側の色
            var otherKoma = nowKoma == Koma.White ? Koma.Black : Koma.White;
            // 8方向解析
            foreach(var direction in Direction.All8) {
                var reverses = _DirectionReverse(map, check, direction);
                // 一つ方向の反転になるか
                if(reverses.Count > 0) {
                    return true;
                }
            }
            return false;
        }
    }
}
