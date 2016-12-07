using Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Othello {

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        /// <summary>アルゴリズム</summary>
        private Algorithm _Algorithm { get; } = new Algorithm();

        /// <summary>手番制御</summary>
        private Koma _Now { get; set; } = Koma.Black;

        /// <summary>
        /// コンスタント
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            // 初期化処理
            _Init();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void _Init() {
            // 盤面初期化
            MainMap.Children.Clear();
            for(int y = 0; y < MainMap.Columns; y++) {
                for(int x = 0; x < MainMap.Rows; x++) {
                    MainMap.Children.Add(new CheckBox { Tag = new Location { X = x, Y = y } });
                }
            }
            // 初期コマ
            (MainMap.Children[28] as CheckBox).IsEnabled = false;
            (MainMap.Children[28] as CheckBox).IsChecked = true;
            (MainMap.Children[27] as CheckBox).IsEnabled = false;
            (MainMap.Children[27] as CheckBox).IsChecked = false;
            (MainMap.Children[35] as CheckBox).IsEnabled = false;
            (MainMap.Children[35] as CheckBox).IsChecked = true;
            (MainMap.Children[36] as CheckBox).IsEnabled = false;
            (MainMap.Children[36] as CheckBox).IsChecked = false;
            // 先手設定
            _Now = Koma.Black;
            // 次の設置可能な場所を算出
            _CanSets = _Algorithm.CanSets(_KomaMap, _Now);
            // 情報設定
            _UpdateInfo();
        }

        /// <summary>
        /// クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e) {
            // コマ追加
            var cilcked = sender as CheckBox;
            cilcked.IsEnabled = false;
            cilcked.IsChecked = _Now == Koma.Black;
            // 画面から盤面配列を取得
            var map = _KomaMap;
            // 全て反転の場所を算出
            var reverseList = _Algorithm.ReverseList(_KomaMap, (Location)cilcked.Tag);
            // 反転処理
            foreach(var reverse in reverseList) {
                map[reverse.X, reverse.Y] = _Now;
            }
            // 画面更新
            _KomaMap = map;
            // 手番交換
            _Now = (Koma)(Koma.None - _Now);
            // 次の設置可能な場所を算出
            _CanSets = _Algorithm.CanSets(map, _Now);
            // 情報設定
            _UpdateInfo();
        }

        /// <summary>
        /// 情報設定
        /// </summary>
        private void _UpdateInfo() {
            // 黒の番
            lblBlackMark.Visibility = _Now == Koma.Black ? Visibility.Visible : Visibility.Hidden;
            // 白の番
            lblWhiteMark.Visibility = _Now == Koma.White ? Visibility.Visible : Visibility.Hidden;
            // 集計用リスト
            var list = MainMap.Children.Cast<CheckBox>().ToList();
            // 黒の合計
            lblBlackCount.Content = list.Count(c => c.IsChecked == true);
            // 白の合計
            lblWhiteCount.Content = list.Count(c => c.IsChecked == false);
        }

        /// <summary>置ける場所</summary>
        private HashSet<Location> _CanSets {
            get {
                var query = from check in MainMap.Children.Cast<CheckBox>()
                            where check.IsEnabled
                            select (Location)check.Tag;
                return new HashSet<Location>(query);
            }
            set {
                foreach(CheckBox check in MainMap.Children) {
                    check.IsEnabled = false;
                }
                foreach(var p in value) {
                    var idx = MainMap.Columns * p.Y + p.X;
                    (MainMap.Children[idx] as CheckBox).IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// コマ配列
        /// </summary>
        private Koma[,] _KomaMap {
            get {
                // コントローラーの行列数で配列新規
                var map = new Koma[MainMap.Columns, MainMap.Rows];
                // コントローラーからコマの配列を作成
                foreach(CheckBox check in MainMap.Children) {
                    var p = (Location)check.Tag;
                    map[p.X, p.Y] = check.IsChecked == true ? Koma.Black
                                  : check.IsChecked == false ? Koma.White
                                  : Koma.None;
                }
                return map;
            }
            set {
                // コントローラーからコマの配列を作成
                foreach(CheckBox check in MainMap.Children) {
                    var p = (Location)check.Tag;
                    var newKoma = value[p.X, p.Y];
                    check.IsChecked = newKoma == Koma.Black ? true
                                    : newKoma == Koma.White ? (bool?)false
                                    : null;
                }
            }
        }

        /// <summary>
        /// 再開ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e) {
            _Init();
        }
    }
}
