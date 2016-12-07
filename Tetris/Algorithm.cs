using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris {

    /// <summary>アルゴリズム</summary>
    public class Algorithm {

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="map">(y, x)</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Location> Move(bool[,] map, List<Location> list, Direction direction) {
            var query = list.Select(p => 
                new Location {
                    X = p.X + direction.OffsetX,
                    Y = p.Y + direction.OffsetY
                }
            );
            return query.ToList();
        }

        /// <summary>
        /// 回転
        /// X = O.y + O.x - P.y
        /// Y = O.y - O.x + P.x
        /// </summary>
        /// <param name="map">(y, x)</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Location> Round(bool[,] map, List<Location> list) {
            return list;
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="map">(y, x)</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool[,] Add(bool[,] map, List<Location> list) {
            return map;
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="map">(y, x)</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool[,] Remove(bool[,] map) {
            return map;
        }
    }
}
