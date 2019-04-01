using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YiSoFramwork.Test
{
    public class ColorFader
    {
        private readonly Color _From;
        private readonly Color _To;

        private readonly double _StepR;
        private readonly double _StepG;
        private readonly double _StepB;

        private readonly uint _Steps;

        public ColorFader(Color from, Color to, uint steps)
        {
            if (steps == 0)
                throw new ArgumentException("steps must be a positive number");

            _From = from;
            _To = to;
            _Steps = steps;

            _StepR = (double)(_To.R - _From.R) / _Steps;
            _StepG = (double)(_To.G - _From.G) / _Steps;
            _StepB = (double)(_To.B - _From.B) / _Steps;
        }

        public IEnumerable<Color> Fade()
        {
            for (uint i = 0; i < _Steps; ++i)
            {
                yield return Color.FromArgb((int)(_From.R + i * _StepR), (int)(_From.G + i * _StepG), (int)(_From.B + i * _StepB));
            }
            yield return _To; // make sure we always return the exact target color last
        }

        public static void ChangeControlColour(Control activeControl, Color eventColour)
        {
            uint intervals = 20;

            var colorFader = new ColorFader(eventColour, activeControl.BackColor, intervals);

            SetControlBackColor(activeControl, eventColour);

            Task t = Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(500);
                foreach (var color in colorFader.Fade())
                {
                    SetControlBackColor(activeControl, color);
                    System.Threading.Thread.Sleep(50);
                }
            });
        }

        private static void SetControlBackColor(Control activeControl, Color color)
        {
            if (activeControl.InvokeRequired)
                activeControl.Invoke((MethodInvoker)delegate { activeControl.BackColor = color; });
            else
                activeControl.BackColor = color;
        }
    }
}
