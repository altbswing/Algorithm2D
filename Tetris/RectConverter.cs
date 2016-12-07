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

namespace Tetris {

    /// <summary>
    /// 画像
    /// </summary>
    public class RectConverter : IValueConverter {

        private Dictionary<string, ImageBrush> _Cashe = new Dictionary<string, ImageBrush>();

        /// <summary>
        /// 画像
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null) {
                return null;
            }
            var path = string.Format("pack://Application:,,,/Tetris;component/Image/{0}.png", value);
            if(!_Cashe.ContainsKey(path)) {
                _Cashe[path] = new ImageBrush(new BitmapImage(new Uri(path)));
            }
            return _Cashe[path];
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
