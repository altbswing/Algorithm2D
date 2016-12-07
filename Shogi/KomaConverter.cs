using Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shogi {

    /// <summary>
    /// 駒表示用
    /// </summary>
    public class KomaConverter : IValueConverter {

        /// <summary>
        /// 駒画像
        /// </summary>
        private static readonly ImageBrush[,] _KomaSprite = _InitSprite();

        /// <summary>
        /// 駒画像初期化
        /// </summary>
        /// <returns></returns>
        private static ImageBrush[,] _InitSprite() {
            var bitmap = BitmapFrame.Create(new Uri("pack://Application:,,,/Shogi;component/Image/Koma.png"));
            var w = bitmap.PixelWidth / 8;
            var h = bitmap.PixelHeight / 4;
            var sprite = new ImageBrush[8, 4];
            for(int y = 0; y < sprite.GetLength(1); y++) {
                for(int x = 0; x < sprite.GetLength(0); x++) {
                    var cut = new Int32Rect(x * w, y * h, w, h);
                    var sub = new CroppedBitmap(bitmap, cut);
                    sprite[x, y] = new ImageBrush(sub);
                }
            }
            return sprite;
        }

        /// <summary>
        /// 駒の画像座標X
        /// </summary>
        private static Dictionary<Type, int> _KomaX = new Dictionary<Type, int> {
            { typeof(KomaHi), 0 },
            { typeof(KomaKaku), 1 },
            { typeof(KomaKin), 2 },
            { typeof(KomaGin), 3 },
            { typeof(KomaKei), 4 },
            { typeof(KomaKyou), 5 },
            { typeof(KomaHu), 6 },
            { typeof(KomaGyoku), 7 },
        };

        /// <summary>
        /// 駒を画像にする
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var koma = value as Koma;
            if(koma == null) {
                return null;
            }
            var sx = _KomaX[koma.GetType()];
            var sy = 0;
            if(koma.Direction == Direction.Up) {
                sy += 1;
            }
            if(koma.IsNari) {
                sy += 2;
            }
            return _KomaSprite[sx, sy];
        }

        /// <summary>
        /// 使わない
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
