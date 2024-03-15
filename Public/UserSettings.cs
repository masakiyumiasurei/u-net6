using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace u_net.Public
{
    public class UserSettings
    {
        public int ClientPreference { get; set; }

        /// <summary>
        /// userSettings.jsonに設定を保存する  
        /// </summary>
        /// <param name="preference"></param>         
        
        public static bool SaveClientPreference(int preference)
        {
            try{
                string fileName = "userSettings.json";
                var options = new JsonSerializerOptions { WriteIndented = true };
                var settings = new
                {
                    ClientSettings = new { Preference = preference }
                };
                string jsonString = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(fileName, jsonString);
                return true;
            }
            catch(Exception ex)
            {
                string message = "接続先の変更に失敗しました。：" + ex.Message;
                MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        /// <summary>
        /// userSettings.jsonから設定を読み込む 　ファイルが見つからない時は本番環境を返す
        /// </summary>
        /// <returns></returns>         
        public static int LoadClientPreference()
        {
            string fileName = "userSettings.json";
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = doc.RootElement;
                    int preference = root.GetProperty("ClientSettings").GetProperty("Preference").GetInt32();
                    return preference;
                }
            }
            else
            {
                //ファイルが存在しない時は本番環境を返す
                return 1;
            }
        }

    }
}
