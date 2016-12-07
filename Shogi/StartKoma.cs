using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {

    /// <summary>
    /// 初期データ
    /// </summary>
    public static class StartKoma {

        /// <summary>
        /// 標準
        /// </summary>
        public static List<Koma> Standard {
            get {
                return new List<Koma> {
                    // 1
                    new KomaKyou { Direction = Direction.Down }, new KomaKei { Direction = Direction.Down },
                    new KomaGin { Direction = Direction.Down }, new KomaKin { Direction = Direction.Down },
                    new KomaGyoku { Direction = Direction.Down },
                    new KomaKin { Direction = Direction.Down }, new KomaGin { Direction = Direction.Down },
                    new KomaKei { Direction = Direction.Down }, new KomaKyou { Direction = Direction.Down },
                    // 2
                    null,
                    new KomaHi { Direction = Direction.Down },
                    null,null,null,null,null,
                    new KomaKaku { Direction = Direction.Down },
                    null,
                    // 3
                    new KomaHu { Direction = Direction.Down }, new KomaHu { Direction = Direction.Down },
                    new KomaHu { Direction = Direction.Down }, new KomaHu { Direction = Direction.Down },
                    new KomaHu { Direction = Direction.Down }, new KomaHu { Direction = Direction.Down },
                    new KomaHu { Direction = Direction.Down }, new KomaHu { Direction = Direction.Down },
                    new KomaHu { Direction = Direction.Down },
                    // 4-6
                    null,null,null,null,null,null,null,null,null,
                    null,null,null,null,null,null,null,null,null,
                    null,null,null,null,null,null,null,null,null,
                    // 7
                    new KomaHu { Direction = Direction.Up }, new KomaHu { Direction = Direction.Up },
                    new KomaHu { Direction = Direction.Up }, new KomaHu { Direction = Direction.Up },
                    new KomaHu { Direction = Direction.Up }, new KomaHu { Direction = Direction.Up },
                    new KomaHu { Direction = Direction.Up }, new KomaHu { Direction = Direction.Up },
                    new KomaHu { Direction = Direction.Up },
                    // 8
                    null,
                    new KomaKaku { Direction = Direction.Up },
                    null,null,null,null,null,
                    new KomaHi { Direction = Direction.Up },
                    null,
                    // 9
                    new KomaKyou { Direction = Direction.Up }, new KomaKei { Direction = Direction.Up },
                    new KomaGin { Direction = Direction.Up }, new KomaKin { Direction = Direction.Up },
                    new KomaGyoku { Direction = Direction.Up },
                    new KomaKin { Direction = Direction.Up }, new KomaGin { Direction = Direction.Up },
                    new KomaKei { Direction = Direction.Up }, new KomaKyou { Direction = Direction.Up },
                };
            }
        }

        /// <summary>
        /// なりテスト
        /// </summary>
        public static List<Koma> NariTest {
            get {
                return new List<Koma> {
                    // 1
                    null,null,null,null,null,null,null,null,null,
                    // 2
                    null,
                    new KomaHi { Direction = Direction.Down, IsNari = true },
                    null,null,null,null,null,
                    new KomaKaku { Direction = Direction.Down, IsNari = true },
                    null,
                    // 3
                    new KomaKyou { Direction = Direction.Down, IsNari = true }, null,
                    new KomaKei { Direction = Direction.Down, IsNari = true }, null,
                    new KomaGin { Direction = Direction.Down, IsNari = true }, null,
                    new KomaHu { Direction = Direction.Down, IsNari = true }, null, null,
                    // 4-6
                    null,null,null,null,null,null,null,null,null,
                    null,null,null,null,null,null,null,null,null,
                    null,null,null,null,null,null,null,null,null,
                    // 7
                    new KomaHu { Direction = Direction.Up, IsNari = true }, null, null,
                    new KomaGin { Direction = Direction.Up, IsNari = true }, null,
                    new KomaKei { Direction = Direction.Up, IsNari = true }, null,
                    new KomaKyou { Direction = Direction.Up, IsNari = true }, null,
                    // 8
                    null,
                    new KomaKaku { Direction = Direction.Up, IsNari = true },
                    null,null,null,null,null,
                    new KomaHi { Direction = Direction.Up, IsNari = true },
                    null,
                    // 9
                    null,null,null,null,null,null,null,null,null,
                };
            }
        }

    }
}
