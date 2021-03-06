﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {

    /// <summary>
    /// 歩
    /// </summary>
    public class KomaHu : Koma {

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
            // 前一歩
            return new HashSet<Location> { now + Direction };
        }

        /// <summary>
        /// 駒台から持ち上げた時設定可能な箇所
        /// </summary>
        /// <param name="map">盤面状況</param>
        /// <returns></returns>
        public override HashSet<Location> CanSetFromDai(Koma[,] map) {
            // 
            var canSets = base.CanSetFromDai(map);

            // TODO 特別ロジック

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
        public override bool WillNari(int fromY, int toY) {
            // なり判断
            var willNari = base.WillNari(fromY, toY);
            // 底についたたら問い合わせしない
            if(willNari && (toY == 0 || toY == 8)) {
                IsNari = true;
                return false;
            }
            return willNari;
        }
    }
}
