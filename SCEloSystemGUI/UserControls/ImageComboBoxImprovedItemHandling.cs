using CustomControls;
using EloSystem;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CustomExtensionMethods;

namespace SCEloSystemGUI.UserControls
{
    public delegate Image ImageGetter<T>(T item);
    public delegate string NameGetter<T>(T item);

    public partial class ImageComboBoxImprovedItemHandling<T> : ImageComboBox where T : class
    {
        public ImageGetter<T> ImageGetter { get; set; }
        public NameGetter<T> NameGetter { get; set; }

        public ImageComboBoxImprovedItemHandling() : base()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="imageGetter"></param>
        /// <param name="nameGetter"></param>
        /// <param name="includeEmptyItem">Should the first item in the combobox hold an empty item.</param>
        public void AddItems(T[] items, ImageGetter<T> imageGetter, NameGetter<T> nameGetter, bool includeEmptyItem)
        {
            var currentSelection = this.SelectedValue as T;

            this.DisplayMember = "Item1";
            this.ValueMember = "Item2";
            this.ImageMember = "Item3";

            var comboBoxItems = includeEmptyItem ?

                (new Tuple<string, T, Image>[] { Tuple.Create<string, T, Image>("none", null, null) }).Concat(items.OrderBy(item => nameGetter(item)).Select(item =>
                    Tuple.Create<string, T, Image>(nameGetter(item), item, imageGetter(item)))).ToList()

                : (new Tuple<string, T, Image>[] { }).Concat(items.OrderBy(item => nameGetter(item)).Select(item => Tuple.Create<string, T, Image>(nameGetter(item), item, imageGetter(item)))).ToList();


            this.DataSource = comboBoxItems;

            if (currentSelection != null && comboBoxItems.Any(item => item.Item2 == currentSelection)) { this.SelectedIndex = comboBoxItems.IndexOf(comboBoxItems.First(item => item.Item2 == currentSelection)); }
            else { this.SelectedIndex = -1; }
        }

        public void AddItems(T[] items, bool includeEmptyItem)
        {
            this.AddItems(items, this.NameGetter ?? new NameGetter<T>((i) => i.ToString()), includeEmptyItem);
        }

        public void AddItems(T[] items, ImageGetter<T> imageGetter, bool includeEmptyItem)
        {
            this.AddItems(items, imageGetter, this.NameGetter ?? new NameGetter<T>((i) => i.ToString()), includeEmptyItem);
        }

        public void AddItems(T[] items, NameGetter<T> nameGetter, bool includeEmptyItem)
        {
            this.AddItems(items, this.ImageGetter ?? new ImageGetter<T>((i) => { return null; }), nameGetter, includeEmptyItem);
        }

        public bool TrySetSelectedIndex(T valueMemeber)
        {
            if (this.Items.Cast<Tuple<string, T, Image>>().Any(item => item.Item2 == valueMemeber))
            {
                this.SelectedIndex = this.Items.Cast<Tuple<string, T, Image>>().IndexOf(this.Items.Cast<Tuple<string, T, Image>>().First(item => item.Item2 == valueMemeber));

                return true;
            }
            else { return false; }
        }
    }
}
