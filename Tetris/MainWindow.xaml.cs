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

namespace Tetris {

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        private Algorithm algorithm = new Algorithm();

        /// <summary>
        /// 初期化
        /// </summary>
        public MainWindow() {
            InitializeComponent();
        }

        /// <summary>キー</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            var map = new Dictionary<Key, Action> {
                { Key.Up, _Key_Up },
                { Key.Down, _Key_Down },
                { Key.Left, _Key_Left },
                { Key.Right, _Key_Right },
                { Key.Space, _Key_Space },
            };
            if(_Now == null) {
                return;
            }
            if(map.ContainsKey(e.Key)) {
                map[e.Key]();
            }
        }

        /// <summary>空白</summary>
        private void _Key_Space() {

        }

        /// <summary>上</summary>
        private void _Key_Up() {
            var map = _GetMap();
            _Now.PointList = algorithm.Round(map, _Now.PointList);
            _Reflesh();
        }

        /// <summary>下</summary>
        private void _Key_Down() {
            var map = _GetMap();
            _Now.PointList = algorithm.Move(map, _Now.PointList, Direction.Down);
            _Reflesh();
        }

        /// <summary>
        /// マップ作成
        /// </summary>
        /// <returns></returns>
        private bool[,] _GetMap() {
            var map = new bool[MainMap.Columns, MainMap.Rows];
            foreach(CheckBox check in MainMap.Children) {
                var p = (Location)check.Tag;
                map[p.X, p.Y] = (bool)check.IsChecked;
            }
            return map;
        }

        /// <summary>左</summary>
        private void _Key_Left() {
            var map = _GetMap();
            _Now.PointList = algorithm.Move(map, _Now.PointList, Direction.Left);
            _Reflesh();
        }

        /// <summary>右</summary>
        private void _Key_Right() {
            var map = _GetMap();
            _Now.PointList = algorithm.Move(map, _Now.PointList, Direction.Right);
            _Reflesh();
        }

        public GameEntity _Now = null;

        /// <summary>
        /// 再開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e) {
            MainMap.Children.Clear();
            for(int y = 0; y < MainMap.Rows; y++) {
                for(int x = 0; x < MainMap.Columns; x++) {
                    MainMap.Children.Add(new CheckBox {
                        Tag = new Location { X = x, Y = y },
                        IsChecked = true
                    });
                }
            }
            _Now = _CreateNew();
            _Reflesh();
            Focus();
        }

        /// <summary></summary>
        private void _Reflesh() {
            foreach(CheckBox check in MainMap.Children) {
                check.Content = null;
                check.IsChecked = false;
            }
            foreach(var p in _Now.PointList) {
                var idx = p.Y * MainMap.Columns + p.X;
                (MainMap.Children[idx] as CheckBox).Content = _Now.Id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static Location[][] _InitData = new Location[][] {
            new Location[] { new Location { X=4, Y=0},new Location { X=3, Y=0},new Location { X=5, Y=0},new Location { X=6, Y=0}},
            new Location[] { new Location { X=4, Y=0},new Location { X=3, Y=0},new Location { X=5, Y=0},new Location { X=4, Y=1} },
            new Location[] { new Location { X=4, Y=0},new Location { X=3, Y=0},new Location { X=5, Y=0},new Location { X=3, Y=1}},
            new Location[] { new Location { X=4, Y=0},new Location { X=5, Y=0},new Location { X=3, Y=1},new Location { X=4, Y=1},},
            new Location[] { new Location { X=4, Y=0},new Location { X=5, Y=0},new Location { X=4, Y=1},new Location { X=5, Y=1}, },
            new Location[] { new Location { X=4, Y=0},new Location { X=3, Y=0},new Location { X=5, Y=0},new Location { X=5, Y=1},},
            new Location[] { new Location { X=4, Y=0},new Location { X=3, Y=0},new Location { X=4, Y=1},new Location { X=5, Y=1},}
        };

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <returns></returns>
        private GameEntity _CreateNew() {
            var random = new Random();
            var entity = new GameEntity();
            entity.Id = random.Next(_InitData.Length);
            entity.PointList = _InitData[entity.Id].ToList();
            return entity;
        }
    }
}
