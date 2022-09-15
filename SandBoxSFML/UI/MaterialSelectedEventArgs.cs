using SandBoxSFML.Materials;

namespace SandBoxSFML.UI
{
    public sealed class MaterialSelectedEventArgs
    {
        public MaterialSelectedEventArgs(MaterialType material)
        {
            Material = material;
        }

        public MaterialType Material { get; }
    }
}
