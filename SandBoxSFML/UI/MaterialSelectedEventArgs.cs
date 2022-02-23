using SandBoxSFML.Materials;

namespace SandBoxSFML.UI
{
    public class MaterialSelectedEventArgs
    {
        public MaterialSelectedEventArgs(MaterialType material)
        {
            Material = material;
        }

        public MaterialType Material { get; set; }
    }
}
