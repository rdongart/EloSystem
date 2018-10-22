using System.Drawing;

namespace EloSystem.ResourceManagement
{
    public struct EloImage
    {
        public Image Image
        {
            get
            {
                return this._image;
            }
        }
        private Image _image;

        internal EloImage(Image image)
        {
            _image = new Bitmap(image);
        }
    }
}
