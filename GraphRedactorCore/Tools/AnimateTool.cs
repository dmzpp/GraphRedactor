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
using GraphRedactorCore.Tools.Animations;

namespace GraphRedactorCore.Tools
{
    public class AnimateTool : Tool
    {
        private List<DrawableElement> _selectedElements = new List<DrawableElement>();
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
           // graphData.drawables.Last.Value = _rectangle;
        }

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            _rectangle = new Rectangle(point, Colors.Blue, typeof(SolidPen), Colors.Transparent, typeof(SolidBrush), 2, graphData.drawables.Count + 1, graphData.viewPorts.Last().Scale);
            // graphData.canvas.RenderAdditionalElement(_rectangle, graphData.viewPorts.Last());
            graphData.drawables.AddLast(_rectangle);
            isSelecting = true;
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            isSelecting = false;
            graphData.drawables.Remove(graphData.drawables.Last);
            var selectedElements = graphData.drawables.SelectElements(_rectangle.ToRect());
            if(selectedElements.Count() == 0)
            {
                _rectangle = null;
                return;
            }

            foreach(var item in selectedElements)
            {
                if (RotationParam.Value != 0)
                {
                    graphData.animations.Add(ApplyAnimation(item, typeof(RotationAnimation), RotationParam.Value));
                }
                if (MovingParam.Value != 0)
                {
                    //ApplyAnimation(item, typeof(Animation));
                }
                if (ScalingParam.Value != 0)
                {
                    //ApplyAnimation(item, typeof(RotationAnimation));
                }
            }
            _rectangle = null;
        }

        private Animation ApplyAnimation(DrawableElement drawable, Type animation, object additionalArg)
        {
            return animation.GetConstructor(new Type[] { typeof(DrawableElement), additionalArg.GetType() }).Invoke(new object[] {drawable, additionalArg}) as Animation;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
          /*  rect.RotateAngle += 1;
            
            rect.OffsetX = Math.Sin(rect.RotateAngle / Math.PI) * 100;
            rect.OffsetY = Math.Cos(rect.RotateAngle / Math.PI) * 100;
            graphData.canvas.Render(graphData.drawables.collection, graphData.viewPorts.Last());*/
        }

    }
}
