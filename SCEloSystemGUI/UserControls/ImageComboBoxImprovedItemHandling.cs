using CustomControls;
using EloSystem;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    public partial class ImageComboBoxImprovedItemHandling : ImageComboBox
    {
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
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="getter"></param>
        /// <param name="includeEmptyItem">Should the first item in the combobox hold an empty item.</param>
        public void AddItems<T>(T[] items, Func<int, Image> getter, bool includeEmptyItem) where T : class, IHasName, IHasImageID
        {
            var currentSelection = this.SelectedValue as T;

            this.DisplayMember = "Item1";
            this.ValueMember = "Item2";
            this.ImageMember = "Item3";

            var comboBoxItems = includeEmptyItem ?

                (new Tuple<string, T, Image>[] { Tuple.Create<string, T, Image>("none", null, null) }).Concat(items.OrderBy(item => item.Name).Select(item =>
                Tuple.Create<string, T, Image>(item.Name, item, getter(item.ImageID)))).ToList()

                : (new Tuple<string, T, Image>[] { }).Concat(items.OrderBy(item => item.Name).Select(item => Tuple.Create<string, T, Image>(item.Name, item, getter(item.ImageID)))).ToList();


            this.DataSource = comboBoxItems;

            if (currentSelection != null && comboBoxItems.Any(item => item.Item2 == currentSelection)) { this.SelectedIndex = comboBoxItems.IndexOf(comboBoxItems.First(item => item.Item2 == currentSelection)); }
            else { this.SelectedIndex = -1; }
        }
    }
}
