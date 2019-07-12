using System;

namespace AR_Lib.Collections
{
    public struct Interval
    {
        private double _start;
        private double _end;

        public double Start { get => _start; set => _start = value; }
        public double End { get => _end; set => _end = value; }
        public double Domain => _end - _start;
        public bool HasInvertedDirection => Domain < 0 ? true : false;
        
        
        public Interval(double start, double end)
        {
            if(start == end) throw new Exception("Cannot create Interval out of two equal numbers");
            if (Double.IsNaN(start) || Double.IsInfinity(start)) throw new Exception("Start value is invalid");
            if (Double.IsNaN(end) || Double.IsInfinity(end)) throw new Exception("End value is invalid");
            _start = start;
            _end = end;
        }

        public Interval(Interval interval): this(interval.Start, interval.End) { }


        public double Crop(double number) => Interval.CropNumber(number, this);
        public double Remap(double number, Interval toInterval) => Interval.RemapNumber(number, this, toInterval);
        public double RemapToUnit(double number) => this.Remap(number, Interval.Unit );
        public bool Contains(double number) {

            double min = HasInvertedDirection ? _end : _start;
            double max = HasInvertedDirection ? _start : _end;

            return (min < number && number < max) ? true : false;
        }
        public void FlipDirection()
        {
            double temp = _start;
            _start = _end;
            _end = temp;
        }


        public static Interval Unit => new Interval(0, 1);

        public static double CropNumber(double number, Interval interval)
        {
            if(number <= interval.Start) return interval.Start;
            if(number >= interval.End) return interval.End;
            return number;
        }

        public static double RemapNumber(double number, Interval fromInterval, Interval toInterval)
        {
            double cropped = fromInterval.Contains(number) ? number : fromInterval.Crop(number);
            double proportion = (cropped - fromInterval.Start)/Math.Abs(fromInterval.Domain);
            
            return toInterval.Start + toInterval.Domain * proportion;
        }
        
    }
}