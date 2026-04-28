namespace CHECKERS.Models
{
    public class ResolutionOption
    {
        public string Label { get; }
        public double Width { get; }
        public double Height { get; }

        public ResolutionOption(string label, double width, double height)
        {
            Label = label;
            Width = width;
            Height = height;
        }

        public override string ToString() => Label;
    }
}