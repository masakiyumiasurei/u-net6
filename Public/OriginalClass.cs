using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using TextBox = System.Windows.Forms.TextBox;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace u_net.Public
{
    internal class OriginalClass
    {


        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }
        }


        public static bool IsValidUrl(string url)
        {
            // URL の妥当性を確認するためのカスタムロジックを実装
            // このロジックは URL の形式に合ったものである必要があります
            // 有効な URL であるかどうかの判定を行います
            try
            {
                Uri uri = new Uri(url);
                return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
            }
            catch (UriFormatException)
            {
                return false;
            }
        }



        public const string ApiUrl = "https://zipcloud.ibsnet.co.jp/api/search";

        public static async Task<string> GetAddressFromZipCode(string zipCode)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"{ApiUrl}?zipcode={zipCode}";

                try
                {
                    var response = await httpClient.GetStringAsync(apiUrl);
                    var result = JObject.Parse(response);
                    if (result["results"][0]["address1"].ToString() != null)
                    {
                        return result["results"][0]["address1"].ToString() + result["results"][0]["address2"].ToString() + result["results"][0]["address3"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    return "エラー: " + ex.Message;
                }
            }
        }

        public static bool IsValidZipCode(string zipCode)
        {
            // 郵便番号の正規表現パターン (例: "123-4567")
            string pattern = @"^\d{3}-?\d{4}$";

            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, pattern);
        }






        public static void CaptureScreen(string outputPath)
        {
            // 画面のサイズを取得
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            // スクリーンショットを撮るためのBitmapオブジェクトを作成
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                // デスクトップに保存
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        public static void CaptureActiveForm(string outputPath)
        {            // アクティブなフォームを取得
            Form activeForm = Form.ActiveForm;

            if (activeForm != null)
            {
                // アクティブなフォームのサイズを取得
                Rectangle bounds = activeForm.Bounds;

                // スクリーンショットを撮るためのBitmapオブジェクトを作成
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(activeForm.Location, Point.Empty, bounds.Size);
                    }

                    // ファイルに保存
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }
            else
            {
                // アクティブなフォームがない場合のエラーハンドリング
                MessageBox.Show("アクティブなフォームが見つかりません。");
            }
        }

        public static void PrintScreen(string imagePath)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                Image image = Image.FromFile(imagePath);
                e.Graphics.DrawImage(image, e.MarginBounds);
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }


        //指定された値がnullまたはDBNull.Valueの場合に、デフォルトの値（通常は0や空の文字列など）を返す
        public static T Nz<T>(T value, T defaultValue)
        {
            return (value == null || value.Equals(DBNull.Value)) ? defaultValue : value;
        }


    }
}
