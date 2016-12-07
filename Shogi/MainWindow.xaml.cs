using Common;
using System;
using System.Collections.Generic;
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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        /// <summary>手持ち駒</summary>
        private Button _Hand { get; set; }

        /// <summary>現在</summary>
        private Direction _NowDirection { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            // マスク初期化
            MaskMap.Children.Clear();
            for(int y = 0; y < MaskMap.Rows; y++) {
                for(int x = 0; x < MaskMap.Columns; x++) {
                    MaskMap.Children.Add(new Grid());
                }
            }
            // 初期化
            _Reset();
        }

        /// <summary>
        /// 再開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e) {
            _Reset();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void _Reset() {
            // 先手
            _NowDirection = Direction.Up;
            _Hand = null;
            // マスク解除
            _MaskOff();
            // 初期化データ：標準
            var initData = StartKoma.Standard;
            // 初期化データ：なりテスト
            //var initData = StartKoma.NariTest;
            // 盤面クリア
            MainMap.Children.Clear();
            // 駒を入れる
            for(int y = 0; y < MainMap.Rows; y++) {
                for(int x = 0; x < MainMap.Columns; x++) {
                    // ボタン
                    var komaButton = new Button {
                        // 駒
                        Content = initData[y * MainMap.Columns + x],
                        // 座標
                        Tag = new Location { X = x, Y = y },
                    };
                    // イベント追加
                    komaButton.Click += Koma_Click;
                    MainMap.Children.Add(komaButton);
                }
            }
        }

        /// <summary>
        /// 盤状態作成
        /// </summary>
        /// <returns></returns>
        private Koma[,] _BanMap {
            get{
                var map = new Koma[MainMap.Columns, MainMap.Rows];
                foreach(Button btn in MainMap.Children) {
                    var point = (Location)btn.Tag;
                    map[point.X, point.Y] = btn.Content as Koma;
                }
                return map;
            }
        }

        /// <summary>
        /// 駒をクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Koma_Click(object sender, RoutedEventArgs e) {
            if(_Hand == null) {
                _GetKoma(sender as Button);
            } else {
                _SetKoma(sender as Button);
            }
        }

        /// <summary>
        /// 持っている駒を移動させ
        /// </summary>
        /// <param name="button"></param>
        private void _SetKoma(Button clicked) {
            try {
                // 同じ場所をクリック
                if(clicked == _Hand) {
                    _Hand = null;
                    return;
                }
                // クリックしている駒
                var koma = clicked.Content as Koma;
                // 持ち上げている駒
                var handKoma = _Hand.Content as Koma;
                if(koma != null && koma.Direction == handKoma.Direction) {
                    return;
                }
                // 移動処理
                _Move(_Hand, clicked);
                // 手番交換
                _Hand = null;
                _NowDirection = _NowDirection == Direction.Up ? Direction.Down : Direction.Up;
            } finally {
                _MaskOff();
            }
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void _Move(Button from, Button to) {
            // 駒取得
            var koma = from.Content as Koma;
            from.Content = null;
            to.Content = koma;
            // なり処理
            var fromY = ((Location)from.Tag).Y;
            var toY = ((Location)to.Tag).Y;
            // なり判断
            if(koma.WillNari(fromY, toY)) {
                // プレーヤー確認
                var res = MessageBox.Show("なり？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(res == MessageBoxResult.Yes) {
                    koma.IsNari = true;
                }
            }
            // 画面表示更新
            to.Content = null;
            to.Content = koma;
        }

        /// <summary>
        /// 駒を持ち上げ
        /// </summary>
        /// <param name="button"></param>
        private void _GetKoma(Button clicked) {
            var koma = clicked.Content as Koma;
            if(koma == null) {
                return;
            }
            // マスク起動
            var self = (Location)clicked.Tag;
            var canMoveList = koma.GetCanMove(self, _BanMap);
            canMoveList.Add(self);
            _MaskOn(canMoveList);
            // 手持ち設定
            _Hand = clicked;
        }

        #region マスク制御

        /// <summary>
        /// マスクを非表示
        /// </summary>
        private void _MaskOff() {
            foreach(UIElement mask in MaskMap.Children) {
                mask.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// マスクを表示
        /// </summary>
        /// <param name="cannotMoveList"></param>
        private void _MaskOn(HashSet<Location> cannotMoveList) {
            foreach(UIElement mask in MaskMap.Children) {
                mask.Visibility = Visibility.Visible;
            }
            // 移動可能表示
            foreach(var p in cannotMoveList) {
                var idx = p.Y * MaskMap.Columns + p.X;
                (MaskMap.Children[idx] as UIElement).Visibility = Visibility.Hidden;
            }
        }

        #endregion
    }
}
