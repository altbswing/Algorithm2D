using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze {

    /// <summary>アルゴリズム</summary>
    public class Algorithm {

        /// <summary>
        /// 経路を求める
        /// </summary>
        /// <remarks>
        /// このアルゴリズムはまだまだ
        /// 決して最善ではない
        /// </remarks>
        /// <param name="rootMap"></param>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public List<Location> GetRoot(bool[,] rootMap, Location start, Location goal) {
            var stepMap = _ToStepMap(rootMap);
            stepMap[start.X, start.Y] = 0;
            _FullStepMap(stepMap, start);
            var root = _ParseRootMap(stepMap, new List<Location> { goal });
            return root;
        }

        /// <summary>
        /// 準備
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private int[,] _ToStepMap(bool[,] map) {
            var w = map.GetLength(0);
            var h = map.GetLength(1);
            var rootMap = new int[w, h];
            for(int x = 0; x < w; x++) {
                for(int y = 0; y < h; y++) {
                    rootMap[x, y] = map[x, y] ? -1 : int.MaxValue;
                }
            }
            return rootMap;
        }

        /// <summary>
        /// マップの全ての通路に必要最小歩数を塗りつぶす
        /// </summary>
        /// <param name="stepMap"></param>
        /// <param name="now"></param>
        private static void _FullStepMap(int[,] stepMap, Location now) {
            var step = stepMap[now.X, now.Y];
            foreach(var dir in Direction.All4) {
                // 次
                var next = now + dir;
                // 枠の外
                if(!next.In(stepMap)) {
                    continue;
                }
                // 壁
                if(stepMap[next.X, next.Y] < 0) {
                    continue;
                }
                // もっと短いルートがある
                if(stepMap[next.X, next.Y] < step + 1) {
                    continue;
                }
                stepMap[next.X, next.Y] = step + 1;
                _FullStepMap(stepMap, next);
            }
        }

        /// <summary>
        /// 経路解析
        /// </summary>
        /// <param name="rootMap"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<Location> _ParseRootMap(int[,] rootMap, List<Location> root) {
            var now = root.Last();
            if(rootMap[now.X, now.Y] == 0) {
                return root;
            }
            foreach(var dir in Direction.All4) {
                // 次
                var next = now + dir;
                // 枠の外
                if(!next.In(rootMap)) {
                    continue;
                }
                if(rootMap[next.X, next.Y] == rootMap[now.X, now.Y] - 1) {
                    root.Add(next);
                    return _ParseRootMap(rootMap, root);
                }
            }
            return null;
        }
    }
}
