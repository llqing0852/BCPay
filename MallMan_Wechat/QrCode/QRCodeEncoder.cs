using QRCodeEncoderDecoderLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QRCodeEncoderDemo
{
    public partial class QRCodeEncoderForm : Form
    {
        private QREncoder QRCodeEncoder;
        private Bitmap QRCodeImage;
        private Rectangle QRCodeImageArea = new Rectangle();
        private string url;

        public QRCodeEncoderForm()
        {
            InitializeComponent();
            return;
        }

        public QRCodeEncoderForm(string url)
        {
            this.url = url;
            InitializeComponent();
            return;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            // create encoder object
            QRCodeEncoder = new QREncoder();

            // load program state
            ProgramState.LoadState();

            // set initial screen
            SetScreen();

            // force resize
            OnResize(sender, e);

            DataTextBox.Text = this.url;

            // get data for QR Code
            string Data = DataTextBox.Text.Trim();
            if (Data.Length == 0)
            {
                MessageBox.Show("Data must not be empty.");
                return;
            }

            // save state
            ProgramState.State.EncodeErrorCorrection = ErrorCorrection.L;
            ProgramState.State.EncodeData = Data;
            ProgramState.SaveState();

            try
            {
                QRCodeEncoder.Encode(ErrorCorrection.L, Data);

                // create bitmap
                QRCodeImage = QRCodeToBitmap.CreateBitmap(QRCodeEncoder, 4, 8);
            }

            catch (Exception Ex)
            {
                MessageBox.Show("Encoding exception.\r\n" + Ex.Message);
            }

            // repaint panel
            Invalidate();

            return;
        }

        /// <summary>
        /// Create QR Code image
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnEncode(object sender, EventArgs e)
        {
            // get error correction code
            ErrorCorrection ErrorCorrection = ErrorCorrection.L;

            // get data for QR Code
            string Data = DataTextBox.Text.Trim();
            if (Data.Length == 0)
            {
                MessageBox.Show("Data must not be empty.");
                return;
            }

            // save state
            ProgramState.State.EncodeErrorCorrection = ErrorCorrection;
            ProgramState.State.EncodeData = Data;
            ProgramState.SaveState();

            try
            {
                QRCodeEncoder.Encode(ErrorCorrection, Data);

                // create bitmap
                QRCodeImage = QRCodeToBitmap.CreateBitmap(QRCodeEncoder, 4, 8);
            }

            catch (Exception Ex)
            {
                MessageBox.Show("Encoding exception.\r\n" + Ex.Message);
            }

            // repaint panel
            Invalidate();
            return;
        }

        /// <summary>
        /// Restore program defaults
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnDefault(object sender, EventArgs e)
        {
            ProgramState.SetDefaultState();
            QRCodeImage = null;
            SetScreen();
            Invalidate();
            return;
        }

        /// <summary>
        /// Paint QR Code image
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            // no image
            if (QRCodeImage == null) return;

            // calculate image area width and height to preserve aspect ratio
            Rectangle ImageRect = new Rectangle
            {
                Height = (QRCodeImageArea.Width * QRCodeImage.Height) / QRCodeImage.Width
            };
            if (ImageRect.Height <= QRCodeImageArea.Height)
            {
                ImageRect.Width = QRCodeImageArea.Width;
            }
            else
            {
                ImageRect.Width = (QRCodeImageArea.Height * QRCodeImage.Width) / QRCodeImage.Height;
                ImageRect.Height = QRCodeImageArea.Height;
            }

            // calculate position
            ImageRect.X = QRCodeImageArea.X + (QRCodeImageArea.Width - ImageRect.Width) / 2;
            ImageRect.Y = QRCodeImageArea.Y + (QRCodeImageArea.Height - ImageRect.Height) / 2;
            e.Graphics.DrawImage(QRCodeImage, ImageRect);
            return;
        }

        /// <summary>
        /// Set screen based on program state
        /// </summary>
        private void SetScreen()
        {
            DataTextBox.Text = ProgramState.State.EncodeData;
            return;
        }

        /// <summary>
        /// Resize frame
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnResize(object sender, EventArgs e)
        {
            if (ClientSize.Width == 0) return;

            // put data text box at the bottom of client area
            DataTextBox.Top = ClientSize.Height - DataTextBox.Height - 8;
            DataTextBox.Width = ClientSize.Width - 2 * DataTextBox.Left;

            // image area
            QRCodeImageArea.X = 4;
            QRCodeImageArea.Y = 4;
            QRCodeImageArea.Width = ClientSize.Width - QRCodeImageArea.X - 4;
            QRCodeImageArea.Height = DataTextBox.Top - QRCodeImageArea.Y - 4;

            // force re-paint
            Invalidate();
            return;
        }
    }
}
