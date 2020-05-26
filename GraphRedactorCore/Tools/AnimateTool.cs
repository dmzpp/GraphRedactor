using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphRedactorCore.Figures;
using System.Windows.Media;
using GraphRedactorCore.Pens;
using GraphRedactorCore.Brushes;
using GraphRedactorCore.ToolsParams;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using GraphRedactorCore.ToolsParams.AnimateToolParams;
using YamlDotNet.Core.Tokens;

namespace GraphRedactorCore.Tools
{
    public class AnimateTool : Tool
    {
        private List<DrawableElement> _selectedElements = new List<DrawableElement>();
        private DispatcherTimer timer = new DispatcherTimer();
        private Rectangle _rectangle = null;
        private bool isSelecting = false;

        public RotationParam RotationParam { get => _rotationParam; set => _rotationParam = value; }
        public ScalingParam ScalingParam { get => _scalingParam; set => _scalingParam = value; }
        public MovingParam MovingParam { get => _movingParam; set => _movingParam = value; }

        private MovingParam _movingParam;
        private ScalingParam _scalingParam;
        private RotationParam _rotationParam;
        public AnimateTool()
        {
            _rotationParam = new RotationParam(1);
            _movingParam = new MovingParam(1);
            _scalingParam = new ScalingParam(1);

            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Animate",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            if (!isSelecting)
            {
                return;
            }
            _rectangle.ChangeLastPoint(point);
            graphData.canvas.RenderAdditionalElement(_rectangle, graphData.viewPorts.Last());
        }

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            _rectangle = new Rectangle(point, Colors.Blue, typeof(SolidPen), Colors.Transparent, typeof(SolidBrush), 2, graphData.drawables.Count + 1, graphData.viewPorts.Last().Scale);
            graphData.canvas.RenderAdditionalElement(_rectangle, graphData.viewPorts.Last());
            isSelecting = true;
        }

        private Rectangle rect;
        private GraphData graphData;

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            this.graphData = graphData;
            graphData.canvas.RemoveLast();
            rect = (Rectangle)graphData.drawables.collection.First.Value;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(100000);
            timer.Start();

            graphData.canvas.Render(graphData.drawables.collection, graphData.viewPorts.Last());
            isSelecting = false;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            rect.RotateAngle += 1;
            
            rect.OffsetX = Math.Sin(rect.RotateAngle / Math.PI) * 100;
            rect.OffsetY = Math.Cos(rect.RotateAngle / Math.PI) * 100;
            graphData.canvas.Render(graphData.drawables.collection, graphData.viewPorts.Last());
        }
    }
}
