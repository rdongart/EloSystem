﻿using BrightIdeasSoftware;
using CustomControls;
using CustomExtensionMethods;
using CustomExtensionMethods.Drawing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCEloSystemGUI.UserControls
{
    internal static partial class Styles
    {
        internal const string NUMBER_FORMAT = "#,#";

        internal static class ObjectListViewStyles
        {
            internal static void SetHotItemStyle(ObjectListView olv)
            {
                olv.FullRowSelect = true;
                olv.UseHotItem = true;
                olv.MouseMove += EloGUIControlsStaticMembers.ShowCurserHandOnMouseMove;
                olv.HotItemStyle = new HotItemStyle();
                olv.HotItemStyle.Decoration = EloSystemGUIStaticMembers.OlvListViewRowBorderDecoration();
            }

            internal static void DeselectItemsOnMousUp(ObjectListView olv)
            {
                olv.MouseUp += Olv_MouseUp;
            }

            private static void Olv_MouseUp(object sender, MouseEventArgs e)
            {
                var senderOLV = sender as ObjectListView;
                               
                if (senderOLV != null) { senderOLV.SelectedItems.Clear(); }

            }

            internal static void AvoidFocus(ObjectListView olv)
            {
                olv.GotFocus += ObjectListViewStyles.Olv_GotFocus;
            }

            private static void Olv_GotFocus(object sender, EventArgs e)
            {
                var senderOLV = sender as ObjectListView;

                Form form = senderOLV.FindForm();

                if (form != null) { form.Focus(); }
                else if (senderOLV.Parent != null) { senderOLV.Parent.Focus(); }
            }
        }

        internal static class PageSelecterStyles
        {
            internal static void SetSpaceStyle(PageSelecter ps)
            {
                ps.Font = new Font("Calibri", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                ps.ForeColorCurrentPage = Color.WhiteSmoke;
                ps.ForeColorMaxPages = Color.WhiteSmoke;
                ps.BackColorCurrentPage = Color.Black;
                ps.UseDefaultButtonBorderColorScheme = false;
                ps.ButtonBorderColor = Color.WhiteSmoke;
                ps.Anchor = AnchorStyles.Right;
            }

        }

        internal static class PictureBoxStyles
        {
            internal static void SetPictureBoxStyleAndImage(PictureBox picBx, Image img)
            {
                const double IMAGE_BOUNDS_PROPORTION = 0.22;

                int imageBoundsX = (picBx.Width * IMAGE_BOUNDS_PROPORTION).RoundToInt();
                int imageBoundsY = (picBx.Height * IMAGE_BOUNDS_PROPORTION).RoundToInt();

                picBx.BackColor = EloGUIControlsStaticMembers.RaceImageBackgroundColor;
                picBx.BorderStyle = BorderStyle.FixedSingle;
                picBx.SizeMode = PictureBoxSizeMode.CenterImage;
                picBx.BackgroundImage = EloGUIControlsStaticMembers.BackGroundFrame(picBx.Size, Color.Black, (Math.Min(picBx.Size.Width, picBx.Size.Height) * 0.12).RoundToInt());

                if (img != null) { picBx.Image = img.ResizeSARWithinBounds(picBx.Width - imageBoundsX, picBx.Height - imageBoundsY); }
                else { picBx.Image = null; }
            }


        }

        internal static class StringStyles
        {
            internal static string ConvertRatingChangeString(string ratingChangeTxt)
            {
                int ratingChangeValue = 0;

                bool hasRatingValue = int.TryParse(ratingChangeTxt, out ratingChangeValue);

                return String.Format("{0}{1}", ratingChangeValue >= 0 ? "+" : "",
                    hasRatingValue ?
                    (ratingChangeValue == 0 ? "0" : ratingChangeValue.ToString(Styles.NUMBER_FORMAT))
                    : ratingChangeTxt);
            }
        }
    }
}
