using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace CompareWindows {
    public partial class PictureForm : Form {
        public PictureForm() {
            InitializeComponent();
        }

        public unsafe void ShowPricture(string picture1, string picture2) {
            pictureBox1.ImageLocation = picture1;
            pictureBox2.ImageLocation = picture2;
            if (string.IsNullOrEmpty(picture2)) {
                pictureBox3.ImageLocation = picture1;
                return;
            } // end if
            Bitmap bitmap1 = new Bitmap(picture1);
            Bitmap bitmap2 = new Bitmap(picture2);
            int width = bitmap1.Width > bitmap2.Width ? bitmap1.Width : bitmap2.Width;
            int height = bitmap1.Width > bitmap2.Height ? bitmap1.Height : bitmap2.Height;
            Bitmap bitmap = new Bitmap(width, height);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Rectangle rect1 = new Rectangle(0, 0, bitmap1.Width, bitmap1.Height);
            BitmapData bmpData1 = bitmap1.LockBits(rect1, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Rectangle rect2 = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
            BitmapData bmpData2 = bitmap2.LockBits(rect2, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte* ptr = (byte*)(bmpData.Scan0);
            byte* ptr1 = (byte*)(bmpData1.Scan0);
            byte* ptr2 = (byte*)(bmpData2.Scan0);
            byte[] result = new byte[1];
            for (int h = 0; h < bmpData.Height; ++h) {
                for (int w = 0; w < bmpData.Width; ++w) {
                    if (CheckPixel(bmpData1, ref w, ref h)) {
                        if (CheckPixel(bmpData2, ref w, ref h)) {
                            if (MixPixel(ptr, ptr1, ptr2)) {
                                GrayPixel(ptr);
                            } else {
                                RedPixel(ptr);
                            } // end if
                            ptr2 += 4;
                        } else {
                            CopyPixel(ptr, ptr1);
                            BluePixel(ptr);
                        }// end if
                        ptr1 += 4;
                    } else if(CheckPixel(bmpData2, ref w, ref h)) {
                        CopyPixel(ptr, ptr2);
                        BluePixel(ptr);
                        ptr2 += 4;
                    } // end if
                    ptr += 4;
                } // end for
            } // end for
            bitmap.UnlockBits(bmpData);
            bitmap2.UnlockBits(bmpData1);
            bitmap1.UnlockBits(bmpData2);
            pictureBox3.Image = bitmap;
        } // end ShowPricture

        private bool CheckPixel(BitmapData bitmapData, ref int w, ref int h) {
            if (bitmapData.Width <= w) return false;
            // end if
            if (bitmapData.Height <= h) return false;
            // end if
            return true;
        } // end CheckPixel

        private unsafe void CopyPixel(byte* ptr, byte* source) {
            for (int i = 0; i < 4; ++i) {
                ptr[i] = source[i];
            } // end for
        } // end CopyPixel

        private unsafe bool MixPixel(byte* ptr, byte* ptr1, byte* ptr2) {
            bool isSame = true;
            for (int i = 0; i < 4; ++i) {
                if (ptr1[i] != ptr2[i]) {
                    ptr[i] = (byte)((ptr1[i] + ptr2[i]) * 0.5);
                    isSame = false;
                } else {
                    ptr[i] = ptr1[i];
                }// end if
            } // end for
            return isSame;
        } // end MixPixel

        private unsafe void GrayPixel(byte* ptr) {
            ptr[0] = ptr[1] = ptr[2] = (byte)(0.229 * ptr[2] + 0.587 * ptr[1] + 0.114 * ptr[0]);
        } // end GrayPixel

        private unsafe void RedPixel(byte* ptr) {
            ptr[1] = ptr[2] = (byte)(0.229 * ptr[2] + 0.587 * ptr[1] + 0.114 * ptr[0]);
            ptr[2] = 255;
        } // end RedPixel

        private unsafe void BluePixel(byte* ptr) {
            ptr[0] = ptr[1] = (byte)(0.229 * ptr[2] + 0.587 * ptr[1] + 0.114 * ptr[0]);
            ptr[0] = 255;
        } // end BluePixel

        public unsafe Bitmap MethodBaseOnMemory(Bitmap bitmap, int degree) {
            if (bitmap == null) {
                return null;
            }
            double Deg = (100.0 + degree) / 100.0;
            int width = bitmap.Width;
            int height = bitmap.Height;
            int length = height * 3 * width;
            byte[] RGB = new byte[length];
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr Scan0 = data.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Scan0, RGB, 0, length);

            double gray = 0;
            for (int i = 0; i < RGB.Length; i += 3) {
                for (int j = 0; j < 3; j++) {
                    gray = (((RGB[i + j] / 255.0 - 0.5) * Deg + 0.5)) * 255.0;
                    if (gray > 255)
                        gray = 255;
                    if (gray < 0)
                        gray = 0;
                    RGB[i + j] = (byte)gray;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(RGB, 0, Scan0, length);// 此处Copy是之前Copy的逆操作
            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}