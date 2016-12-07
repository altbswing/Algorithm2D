using Common;
using System;
using System.Collections.Generic;
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

namespace Maze {

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        /// <summary>
        /// 開始時点設定
        /// </summary>
        private bool _isSetStart = true;

        /// <summary>
        /// アルゴリズムモジュール
        /// </summary>
        private Algorithm algorithm = new Algorithm();

        /// <summary>
        /// 初期化
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            _Reset();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void _Reset() {
            var trueSet = new HashSet<int>();
            if(File.Exists("save.txt")) {
                var saves = File.ReadAllText("save.txt").Split(',').Select(s => int.Parse(s));
                trueSet.UnionWith(saves);
            }
            MainMap.Children.Clear();
            for(int y = 0; y < MainMap.Rows; y++) {
                for(int x = 0; x < MainMap.Columns; x++) {
                    var idx = y * MainMap.Columns + x;
                    MainMap.Children.Add(new CheckBox {
                        IsChecked = trueSet.Contains(idx),
                        Tag = new Location { X = x, Y = y }
                    });
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Search_Click(object sender, RoutedEventArgs e) {
            try {
                Mask.Visibility = Visibility.Visible;
                var start = _GetPoint("S");
                var goal = _GetPoint("G");
                if(start == null || goal == null) {
                    return;
                }
                var rootMap = new bool[MainMap.Columns, MainMap.Rows];
                foreach(CheckBox check in MainMap.Children) {
                    var point = (Location)check.Tag;
                    rootMap[point.X, point.Y] = (bool)check.IsChecked;
                    if(Equals(check.Content, "R")) {
                        check.Content = null;
                    }
                }
                await Task.Delay(500);
                var root = await Task.Run(() => algorithm.GetRoot(rootMap, (Location)start, (Location)goal));
                if(root == null) {
                    return;
                }
                foreach(var loc in root) {
                    var idx = loc.Y * MainMap.Columns + loc.X;
                    var check = (MainMap.Children[idx] as CheckBox);
                    if(Equals(check.Content, "S") || Equals(check.Content, "G")) {
                        continue;
                    }
                    check.Content = "R";
                }
            } finally {
                Mask.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 座標オブジェクトを取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Location? _GetPoint(string key) {
            foreach(CheckBox check in MainMap.Children) {
                var str = check.Content as string;
                if(str == key) {
                    return (Location)check.Tag;
                }
            }
            return null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e) {
            var set = new HashSet<int>();
            for(int i = 0; i < MainMap.Columns * MainMap.Rows; i++) {
                var check = MainMap.Children[i] as CheckBox;
                if(check.IsChecked == true) {
                    set.Add(i);
                }
            }
            File.WriteAllText("save.txt", string.Join(",", set));
            (sender as Button).IsEnabled = false;
        }

        /// <summary>
        /// 壁設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            var now = (sender as CheckBox);
            if(now.IsChecked == true) {
                return;
            }
            var key = _isSetStart ? "S" : "G";
            foreach(CheckBox check in MainMap.Children) {
                if((check.Content as string) == key) {
                    check.Content = null;
                }
            }
            now.Content = key;
            _isSetStart = !_isSetStart;
        }

        /// <summary>
        /// 開始終了地点設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e) {
            btnSave.IsEnabled = true;
        }
    }
}
