using CustomExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CustomControls.Utilities;

namespace SCEloSystemGUI.UserControls
{
    public partial class PageSelecter : UserControl
    {
        private const float DEFAULT_BUTTON_WIDTH = 18;
        private const float DEFAULT_BUTTON_HEIGHT = 26;
        private const int TXTBOX_HEIGHT_STANDARD = 24;
        private const double HORIZONTAL_MARGIN_PROPORTION = 0.08;
        private const int TABLELAYOUT_HEIGHT_DEFAULT = 36;
        private const float BUTTONSHAPE_CORNERROUNDING = 0.87F;

        private static Color default_buttonColor_BackColor = Color.FromArgb(0, 0, 179);
        private static Color default_buttonColor_MouseOverBackColor = Color.FromArgb(0, 128, 255);
        private static Color default_buttonColor_MouseDownBackColor = Color.FromArgb(0, 26, 255);
        private static Color default_buttonColor_BackColor_Enabled_False = Color.FromArgb(110, 110, 110);

        private Image imgFirstStandard;
        private Image imgFirstMouseOver;
        private Image imgFirstMouseDown;
        private Image imgFirstDisabled;
        private Image imgPrevStandard;
        private Image imgPrevMouseOver;
        private Image imgPrevMouseDown;
        private Image imgPrevDisabled;
        private Image imgNextStandard;
        private Image imgNextMouseOver;
        private Image imgNextMouseDown;
        private Image imgNextDisabled;
        private Image imgLastStandard;
        private Image imgLastMouseOver;
        private Image imgLastMouseDown;
        private Image imgLastDisabled;
        private Color buttonColor = Color.FromArgb(0, 0, 180);
        public Color ButtonColor { get; set; }
        public Color ButtonBorderColor { get; set; }
        public Color ButtonMouseDownBorderColor { get; set; }
        public Color ButtonMouseDownColor { get; set; }
        public Color ButtonMouseOverBorderColor { get; set; }
        public Color ButtonMouseOverColor { get; set; }
        public EventHandler<EventArgs> PageNumberChanged = delegate { };
        public EventHandler<EventArgs> FirstPageButtonClick = delegate { };
        public EventHandler<EventArgs> LastPageButtonClick = delegate { };
        public EventHandler<EventArgs> NextPageButtonClick = delegate { };
        public EventHandler<EventArgs> PrevPageButtonClick = delegate { };
        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            private set
            {
                int tempCurrent = this._currentPage;

                this._currentPage = value.TruncateToRange(1, this.pageMax);

                if (this._currentPage != tempCurrent)
                {
                    this.HandlePageShift();
                    this.PageNumberChanged(this, new EventArgs());
                }
            }
        }
        public TextBox CurrentPageTxtBx
        {
            get
            {
                return this.txtBxCurrentPage;
            }
            private set
            {
                this.txtBxCurrentPage = value;
            }
        }
        private int pageMax;
        private int _currentPage;

        public PageSelecter(int pagesMax)
        {
            InitializeComponent();

            this.pageMax = pagesMax;
            this._currentPage = 1;

            //this.FirstPageBtn = CreateFirstPageBtn();
            //this.tblLOPnlPageSelecter.Controls.Add(this.FirstPageBtn, 0, 0);

            //this.PrevPageBtn = CreatePrevPageBtn();
            //this.tblLOPnlPageSelecter.Controls.Add(this.PrevPageBtn, 1, 0);

            //this.NextPageBtn = CreateNextPageBtn();
            //this.tblLOPnlPageSelecter.Controls.Add(this.NextPageBtn, 4, 0);

            //this.LastPageBtn = CreateLastPageBtn();
            //this.tblLOPnlPageSelecter.Controls.Add(this.LastPageBtn, 5, 0);

            this.btnNextPage.Click += this.NextPageBtn_Click;
            this.btnPrevPage.Click += this.PrevPageBtn_Click;
            this.btnFirstPage.Click += this.FirstPageBtn_Click;
            this.btnLastPage.Click += this.LastPageBtn_Click;

            foreach (Button btn in new Button[] { this.btnNextPage, this.btnPrevPage, this.btnFirstPage, this.btnLastPage })
            {
                btn.EnabledChanged += PageSelecter.PageBtn_EnabledChanged;
                btn.Click += this.Btn_Click;
            }

            this.SetBtnEnabledStatus();
            this.DisplaySelectedPageStatus();

            this.UpdateControlSizes();
        }

        private void UpdateButtonImages()
        {

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            this.txtBxCurrentPage.Focus();
        }

        private void SetButtonSizes()
        {
            const float BUTTON_HEIGHT_TO_TEXTBX_HEIGHT_PROPORTION = 22F / 24F;

            // for drawing buttons the smallest button sizes are used

            const int PREVPAGEBUTTON_COLUMN_INDEX = 1;

            var buttonSizeStandard = new SizeF(this.tblLOPnlPageSelecter.ColumnStyles[PREVPAGEBUTTON_COLUMN_INDEX].Width, (this.txtBxCurrentPage.Height * BUTTON_HEIGHT_TO_TEXTBX_HEIGHT_PROPORTION));
            //int offSetY = ((this.PrevPageBtn.Height - correctedButtonSize.Height) / 2F).RoundToInt(); ;

            GraphicsPath singleBtnPath = Paths.Triangle(buttonSizeStandard, PageSelecter.BUTTONSHAPE_CORNERROUNDING);
            this.btnPrevPage.Region = new Region(singleBtnPath);
            singleBtnPath.Transform(new Matrix(-1, 0, 0, 1, singleBtnPath.GetBounds().Width, 0));
            this.btnNextPage.Region = new Region(singleBtnPath);

            GraphicsPath doubleBtnPath = PageSelecter.CreateDoubleArrowGPath(buttonSizeStandard);
            this.btnFirstPage.Region = new Region(doubleBtnPath);
            doubleBtnPath.Transform(new Matrix(-1, 0, 0, 1, doubleBtnPath.GetBounds().Width, 0));
            this.btnLastPage.Region = new Region(doubleBtnPath);
        }

        private void CenterTextBox()
        {
            // when the parent container changes size the text box needs to be centered vertically

            const int TEXTBOX_COLUMN_INDEX = 2;
            const int TEXTBOX_ROW_INDEX = 0;

            int horizontalMargin, verticalMargin;

            switch (this.tblLOPnlPageSelecter.ColumnStyles[TEXTBOX_COLUMN_INDEX].SizeType)
            {
                case SizeType.AutoSize:
                case SizeType.Absolute: horizontalMargin = (this.tblLOPnlPageSelecter.ColumnStyles[TEXTBOX_COLUMN_INDEX].Width * PageSelecter.HORIZONTAL_MARGIN_PROPORTION).RoundToInt(); break;
                case SizeType.Percent:
                    horizontalMargin = (this.tblLOPnlPageSelecter.ColumnStyles[TEXTBOX_COLUMN_INDEX].Width * (this.tblLOPnlPageSelecter.Width / 100.0)
                        * PageSelecter.HORIZONTAL_MARGIN_PROPORTION).RoundToInt();
                    break;
                default: throw new Exception(String.Format("Unknown {0} {1}", typeof(SizeType).Name, this.tblLOPnlPageSelecter.ColumnStyles[TEXTBOX_COLUMN_INDEX].SizeType.ToString()));
            }

            switch (this.tblLOPnlPageSelecter.RowStyles[TEXTBOX_ROW_INDEX].SizeType)
            {
                case SizeType.AutoSize:
                case SizeType.Absolute: verticalMargin = ((this.tblLOPnlPageSelecter.RowStyles[TEXTBOX_ROW_INDEX].Height - this.txtBxCurrentPage.Height) / 2.0).RoundToInt(); break;
                case SizeType.Percent:
                    verticalMargin = ((((this.tblLOPnlPageSelecter.RowStyles[TEXTBOX_ROW_INDEX].Height * (this.tblLOPnlPageSelecter.Height / 100.0)) - this.txtBxCurrentPage.Height) / 2.0)).RoundToInt();
                    break;
                default: throw new Exception(String.Format("Unknown {0} {1}", typeof(SizeType).Name, this.tblLOPnlPageSelecter.RowStyles[TEXTBOX_ROW_INDEX].SizeType.ToString()));
            }

            this.txtBxCurrentPage.Margin = new Padding(horizontalMargin, verticalMargin, horizontalMargin, 0);
        }

        private void SetMaxPageLabelMargins()
        {
            const int LABEL_COLUMN_INDEX = 3;
            const int LABEL_ROW_INDEX = 0;

            int horizontalMargin, verticalMargin;

            switch (this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_INDEX].SizeType)
            {
                case SizeType.AutoSize:
                case SizeType.Absolute: horizontalMargin = (this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_INDEX].Width * PageSelecter.HORIZONTAL_MARGIN_PROPORTION).RoundToInt(); break;
                case SizeType.Percent:
                    horizontalMargin = (this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_INDEX].Width * (this.tblLOPnlPageSelecter.Width / 100.0) * PageSelecter.HORIZONTAL_MARGIN_PROPORTION).RoundToInt();
                    break;
                default: throw new Exception(String.Format("Unknown {0} {1}", typeof(SizeType).Name, this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_INDEX].SizeType.ToString()));
            }

            switch (this.tblLOPnlPageSelecter.RowStyles[LABEL_ROW_INDEX].SizeType)
            {
                case SizeType.AutoSize:
                case SizeType.Absolute: verticalMargin = ((this.tblLOPnlPageSelecter.RowStyles[LABEL_ROW_INDEX].Height - this.maxPageNumberDisplay.Height) / 2.0).RoundToInt(); break;
                case SizeType.Percent:
                    verticalMargin = ((((this.tblLOPnlPageSelecter.RowStyles[LABEL_ROW_INDEX].Height * (this.tblLOPnlPageSelecter.Height / 100.0)) - this.maxPageNumberDisplay.Height) / 2.0)).RoundToInt();
                    break;
                default: throw new Exception(String.Format("Unknown {0} {1}", typeof(SizeType).Name, this.tblLOPnlPageSelecter.RowStyles[LABEL_ROW_INDEX].SizeType.ToString()));
            }

            this.maxPageNumberDisplay.Margin = new Padding(horizontalMargin, verticalMargin, horizontalMargin, 0);
        }

        //private static Button CreateFirstPageBtn()
        //{
        //    GraphicsPath gPath = CreateDoubleArrowGPath(new SizeF(PageSelecter.DEFAULT_BUTTON_WIDTH, PageSelecter.DEFAULT_BUTTON_HEIGHT), 0);

        //    Button btn = GetArrowHeadButton();

        //    btn.TabIndex = 1;
        //    btn.Region = new Region(gPath);
        //    btn.Cursor = Cursors.Hand;

        //    return btn;
        //}

        //private static Button CreateLastPageBtn()
        //{
        //    GraphicsPath gPath = CreateDoubleArrowGPath(new SizeF(PageSelecter.DEFAULT_BUTTON_WIDTH, PageSelecter.DEFAULT_BUTTON_HEIGHT), 0);

        //    PageSelecter.HorizontalFlip(gPath);

        //    Button btn = GetArrowHeadButton();

        //    btn.TabIndex = 4;
        //    btn.Region = new Region(gPath);

        //    return btn;
        //}

        //private static Button CreateNextPageBtn()
        //{
        //    GraphicsPath gPath = CreateSingleArrowGPath(new SizeF(PageSelecter.DEFAULT_BUTTON_WIDTH, PageSelecter.DEFAULT_BUTTON_HEIGHT), 0);

        //    PageSelecter.HorizontalFlip(gPath);

        //    Button btn = GetArrowHeadButton();

        //    btn.TabIndex = 3;
        //    btn.Region = new Region(gPath);

        //    return btn;
        //}

        //private static Button CreatePrevPageBtn()
        //{
        //    GraphicsPath gPath = CreateSingleArrowGPath(new SizeF(PageSelecter.DEFAULT_BUTTON_WIDTH, PageSelecter.DEFAULT_BUTTON_HEIGHT), 0);

        //    Button btn = GetArrowHeadButton();

        //    btn.TabIndex = 2;
        //    btn.Region = new Region(gPath);

        //    return btn;
        //}

        //private static Button GetArrowHeadButton()
        //{
        //    var btn = new Button()
        //    {
        //        AutoSize = true,
        //        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        //        Dock = DockStyle.Fill,
        //        Text = "",
        //        FlatStyle = FlatStyle.Flat,
        //        BackgroundImageLayout = ImageLayout.Center,
        //        BackColor = PageSelecter.default_buttonColor_BackColor,
        //        UseVisualStyleBackColor = true,
        //        Cursor = Cursors.Hand
        //    };

        //    btn.FlatAppearance.MouseOverBackColor = PageSelecter.default_buttonColor_MouseOverBackColor;
        //    btn.FlatAppearance.MouseDownBackColor = PageSelecter.default_buttonColor_MouseDownBackColor;
        //    btn.FlatAppearance.BorderSize = 0;

        //    return btn;
        //}

        private static SizeF CorrectSizeForArrowPath(float maxWidth, float maxHeight)
        {
            float widthLimit = (float)Math.Sqrt(Math.Pow(maxHeight, 2) - Math.Pow(maxHeight / 2, 2));
            float heightLimit = (float)Math.Sqrt(Math.Pow(maxWidth, 2) + Math.Pow(maxHeight / 2, 2));

            if (maxWidth > widthLimit) { maxWidth = widthLimit; }
            else if (maxHeight > heightLimit) { maxHeight = heightLimit; }

            return new SizeF(maxWidth, maxHeight);
        }

        private static GraphicsPath CreateDoubleArrowGPath(SizeF maxSize)
        {
            GraphicsPath firstArrow = Paths.Triangle(maxSize, PageSelecter.BUTTONSHAPE_CORNERROUNDING);

            var secondArrow = (GraphicsPath)firstArrow.Clone();
            secondArrow.Transform(new Matrix(1, 0, 0, 1, firstArrow.GetBounds().Width / 2F, 0));

            firstArrow.AddPolygon(secondArrow.PathPoints);
            firstArrow.FillMode = FillMode.Winding;

            return firstArrow;
        }

        private static GraphicsPath CreateSingleArrowGPath(SizeF maxSize, float offSetX = 0, float offSetY = 0)
        {
            SizeF correctedSize = PageSelecter.CorrectSizeForArrowPath(maxSize.Width, maxSize.Height);

            float width = correctedSize.Width;
            float height = correctedSize.Height;

            var path = new GraphicsPath();

            float bezMajor = 0.87F;
            float bezMinor = 1F - bezMajor;

            var bottomBezEnd = new PointF(width * bezMajor + offSetX, (height / 2) * bezMinor + offSetY);
            var arrowBezStart = new PointF(width * bezMinor + offSetX, (height / 2) * bezMajor + offSetY);
            var arrowBezIntersect = new PointF(0 + offSetX, height / 2 + offSetY);
            var arrowBezEnd = new PointF(width * bezMinor + offSetX, height / 2 + (height / 2) * bezMinor + offSetY);
            var topBezStart = new PointF(width * bezMajor + offSetX, height / 2 + (height / 2) * bezMajor + offSetY);
            var topBezIntersect = new PointF(width + offSetX, height + offSetY);
            var topBezEnd = new PointF(width + offSetX, height * bezMajor + offSetY);
            var bottomBezStart = new PointF(width + offSetX, height * bezMinor + offSetY);
            var bottomBezIntersect = new PointF(width + offSetX, 0 + offSetY);

            path.StartFigure();
            path.AddLine(bottomBezEnd, arrowBezStart);
            path.AddBezier(arrowBezStart, arrowBezIntersect, arrowBezEnd, arrowBezEnd);
            path.AddLine(arrowBezEnd, topBezStart);
            path.AddBezier(topBezStart, topBezIntersect, topBezEnd, topBezEnd);
            path.AddLine(topBezEnd, bottomBezStart);
            path.AddBezier(bottomBezStart, bottomBezIntersect, bottomBezEnd, bottomBezEnd);
            path.CloseFigure();

            return path;
        }

        //private static GraphicsPath CreateArrowPath(SizeF maxSize, int offSetY)
        //{
        //    SizeF correctedSize = PageSelecter.CorrectSizeForArrowPath(maxSize.Width, maxSize.Height);

        //    float width = correctedSize.Width;
        //    float height = correctedSize.Height;

        //    var path = new GraphicsPath();

        //    float bezMajor = 0.85F;
        //    float bezMinor = 1F - bezMajor;
        //    float bezMajorIntersect = bezMajor + bezMinor / 1.5F;
        //    float bezMinorIntersect = bezMinor / 3F;

        //    int offSetX = 0;

        //    var bottomBezEnd = new PointF(width * bezMajor + offSetX, (height / 2) * bezMinor + offSetY);
        //    var arrowBezStart = new PointF(width * bezMinor + offSetX, (height / 2) * bezMajor + offSetY);
        //    var arrowBezIntersect = new PointF(width * bezMinorIntersect + offSetX, height / 2 + offSetY);
        //    var arrowBezEnd = new PointF(width * bezMinor + offSetX, height / 2 + (height / 2) * bezMinor + offSetY);
        //    var topBezStart = new PointF(width * bezMajor + offSetX, height / 2 + (height / 2) * bezMajor + offSetY);
        //    var topBezIntersect = new PointF(width * bezMajorIntersect + offSetX, height * bezMajorIntersect + offSetY);
        //    var topBezEnd = new PointF(width + offSetX, height * bezMajor + offSetY);
        //    var bottomBezStart = new PointF(width + offSetX, height * bezMinor + offSetY);
        //    var bottomBezIntersect = new PointF(width * bezMajorIntersect + offSetX, height * bezMinorIntersect + offSetY);

        //    path.StartFigure();
        //    path.AddLine(bottomBezEnd, arrowBezStart);
        //    path.AddBezier(arrowBezStart, arrowBezIntersect, arrowBezEnd, arrowBezEnd);
        //    path.AddLine(arrowBezEnd, topBezStart);
        //    path.AddBezier(topBezStart, topBezIntersect, topBezEnd, topBezEnd);
        //    path.AddLine(topBezEnd, bottomBezStart);
        //    path.AddBezier(bottomBezStart, bottomBezIntersect, bottomBezEnd, bottomBezEnd);

        //    return path;

        //    //new PointF[4] {
        //    //    new PointF(correctedSize.Width, offSetY)
        //    //    , new PointF(0, correctedSize.Height / 2 + offSetY)
        //    //    , new PointF(correctedSize.Width, correctedSize.Height + offSetY)
        //    //    , new PointF(correctedSize.Width, offSetY) };
        //}

        private static PointF[] TranslatePath(PointF[] source, PointF offset)
        {
            return source.Select(p => new PointF(p.X + offset.X, p.Y + offset.Y)).ToArray();
        }

        private static void HorizontalFlip(GraphicsPath gPath)
        {
            List<PointF> points = gPath.PathPoints.Select(p => p).ToList();

            float maxX = points.Max(p => p.X);

            gPath.Reset();
            gPath.FillMode = FillMode.Winding;

            gPath.AddPolygon(points.Select(p => new PointF((p.X - maxX) * -1, p.Y)).ToArray());
        }

        private static void PageBtn_EnabledChanged(object sender, EventArgs e)
        {
            var senderBtn = sender as Button;

            if (senderBtn == null) { return; }

            senderBtn.BackColor = senderBtn.Enabled ? PageSelecter.default_buttonColor_BackColor : PageSelecter.default_buttonColor_BackColor_Enabled_False;
        }

        public void UpdatePageMax(int pageMax)
        {
            this.pageMax = pageMax;
            this._currentPage = 1;

            this.DisplaySelectedPageStatus();
            this.SetBtnEnabledStatus();

            this.UpdateControlSizes();

            this.PageNumberChanged(this, new EventArgs());

            this.txtBxCurrentPage.MaxLength = this.pageMax.ToString().Length;
        }

        private void DisplaySelectedPageStatus()
        {
            this.txtBxCurrentPage.Text = this.CurrentPage.ToString("#,#");
            this.maxPageNumberDisplay.Text = this.MaxPageText();
        }

        private string MaxPageText()
        {
            return String.Format("/ {0}", this.pageMax.ToString("#,#"));
        }

        private void LastPageBtn_Click(object sender, EventArgs e)
        {
            this.CurrentPage = this.pageMax;
            this.LastPageButtonClick.Invoke(sender, e);
        }

        private void FirstPageBtn_Click(object sender, EventArgs e)
        {
            this.CurrentPage = 1;
            this.FirstPageButtonClick.Invoke(sender, e);
        }

        private void PrevPageBtn_Click(object sender, EventArgs e)
        {
            this.CurrentPage--;
            this.PrevPageButtonClick.Invoke(sender, e);
        }

        private void NextPageBtn_Click(object sender, EventArgs e)
        {
            this.CurrentPage++;
            this.NextPageButtonClick.Invoke(sender, e);
        }

        private void HandlePageShift()
        {
            this.SetBtnEnabledStatus();
            this.DisplaySelectedPageStatus();
        }

        private void SetBtnEnabledStatus()
        {
            if (this.CurrentPage == 1)
            {
                this.btnFirstPage.Enabled = false;
                this.btnPrevPage.Enabled = false;
            }
            else
            {
                this.btnFirstPage.Enabled = true;
                this.btnPrevPage.Enabled = true;
            }

            if (this.CurrentPage < this.pageMax)
            {
                this.btnNextPage.Enabled = true;
                this.btnLastPage.Enabled = true;
            }
            else
            {
                this.btnNextPage.Enabled = false;
                this.btnLastPage.Enabled = false;
            }
        }

        private void UpdateControlSizes()
        {
            double sizeFactorFromFont = this.txtBxCurrentPage.PreferredHeight / (double)PageSelecter.TXTBOX_HEIGHT_STANDARD;

            const int STANDARD_WIDTH_DBLARROWCOLUMN = 28;
            const int STANDARD_WIDTH_SINGLEARROWCOLUMN = 21;
            const int STANDARD_WIDTH_TEXTCOLUMN = 46;

            int doubleArrowColumnWidth = (STANDARD_WIDTH_DBLARROWCOLUMN * sizeFactorFromFont).RoundToInt();
            int singleArrowColumnWidth = (STANDARD_WIDTH_SINGLEARROWCOLUMN * sizeFactorFromFont).RoundToInt();
            int textColumnWidth = (STANDARD_WIDTH_TEXTCOLUMN * sizeFactorFromFont).RoundToInt();

            Size maxPagesTxtSize = TextRenderer.MeasureText(this.MaxPageText(), this.Font);

            double labelTextPaddingFactor = 1.25; // a value estimating the amount of padding from label border to text

            int extraWidthNeededForTextDisplay = Math.Max(0, (this.maxPageNumberDisplay.PreferredWidth * labelTextPaddingFactor - textColumnWidth)
                * (1 + PageSelecter.HORIZONTAL_MARGIN_PROPORTION)).RoundToInt();

            textColumnWidth += extraWidthNeededForTextDisplay;

            const int LABEL_COLUMN_FIRSTPAGEBTN = 0;
            const int LABEL_COLUMN_PREVPAGEBTN = 1;
            const int LABEL_COLUMN_INDEX = 2;
            const int LABEL_COLUMN_MAXPAGES = 3;
            const int LABEL_COLUMN_NEXTPAGEBTN = 4;
            const int LABEL_COLUMN_LASTPAGEBTN = 5;

            this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_FIRSTPAGEBTN].Width = doubleArrowColumnWidth;
            this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_PREVPAGEBTN].Width = singleArrowColumnWidth;
            this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_INDEX].Width = textColumnWidth;
            this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_MAXPAGES].Width = textColumnWidth;
            this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_NEXTPAGEBTN].Width = singleArrowColumnWidth;
            this.tblLOPnlPageSelecter.ColumnStyles[LABEL_COLUMN_LASTPAGEBTN].Width = doubleArrowColumnWidth;

            const int CONTROL_BORDER = 1;

            this.tblLOPnlPageSelecter.Size = new Size(this.tblLOPnlPageSelecter.ColumnCount + CONTROL_BORDER + (doubleArrowColumnWidth + singleArrowColumnWidth + textColumnWidth) * 2
                , (PageSelecter.TABLELAYOUT_HEIGHT_DEFAULT * sizeFactorFromFont).RoundToInt());

            if (this.Width < this.tblLOPnlPageSelecter.Width) { this.Width = this.tblLOPnlPageSelecter.Width; }

            if (this.Height < this.tblLOPnlPageSelecter.Height) { this.Height = this.tblLOPnlPageSelecter.Height; }

            this.SetButtonSizes();
            this.CenterTextBox();
            this.SetMaxPageLabelMargins();

        }

        private void txtBxCurrentPage_FontChanged(object sender, EventArgs e)
        {
            this.UpdateControlSizes();
        }

        private void txtBxCurrentPage_TextChanged(object sender, EventArgs e)
        {
            int newPageValue = this.CurrentPage;

            if (int.TryParse(this.txtBxCurrentPage.Text, out newPageValue) && newPageValue <= this.pageMax && newPageValue > 0) { this.CurrentPage = newPageValue; }
            else { this.txtBxCurrentPage.Text = this.CurrentPage.ToString("#,#"); }

        }

        private void PageSelecter_FontChanged(object sender, EventArgs e)
        {
            this.txtBxCurrentPage.Font = this.Font;
            this.maxPageNumberDisplay.Font = this.Font;

            this.UpdateControlSizes();
        }
    }
}
