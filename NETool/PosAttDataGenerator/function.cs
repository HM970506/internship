using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PosAttDataGenerator
{
    public class function
    {

        /// <summary>
        /// input파일을 읽는 함수
        /// </summary>
        /// <param name="now_path"></param>
        /// <returns></returns>
        public string inputfile_open(string now_path)
        {
            return Path.GetDirectoryName(now_path) + "\\Input\\" + "PosAttData_noise_SPC_001.json";
        }

        /// <summary>
        /// output 경로를 출력하는 함수
        /// </summary>
        /// <param name="now_path"></param>
        /// <returns></returns>
        public string outputpath_load(string now_path)
        {
            return Path.GetDirectoryName(now_path) + "\\Output\\" + "PosAttData_noise_SPC_”yyyyMMddHHmmss”.json";
        }

        /// <summary>
        /// transfer함수와 save함수를 조정
        /// </summary>
        /// <param name="json_path"></param>
        /// <param name="save_path"></param>
        /// <param name="start_time"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public bool transfer_and_save(string json_path, string save_path, string start_time, string interval)
        {
            string transfer = transfer_function(json_path, start_time, interval);
            return save_fucntion(save_path, transfer);

        }

        /// <summary>
        /// parsing및 수정 함수
        /// </summary>
        /// <param name="json_path"></param>
        /// <param name="start_time"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public string transfer_function(string json_path, string start_time, string interval)
        {
            string json_string = File.ReadAllText(json_path);
            JArray json = JArray.Parse(json_string);

            JObject json_update = new JObject();

            float hz = float.Parse(interval) / 8;

            for (int x = 0; x < json.Count; x++)
            {
                JToken ordinary_value = json[x]["Time"];

                json[x]["Time"] = DateTime.Parse(start_time).AddSeconds(x * hz);
                json[x]["TimeJD"] = transferring(start_time);

            }

            json_string = json.ToString();

            return json_string;
        }


        /// <summary>
        /// 저장 담당 함수
        /// </summary>
        /// <param name="save_path"></param>
        /// <param name="transfer_json"></param>
        /// <returns></returns>
        public bool save_fucntion(string save_path, string transfer_json)
        {

            if (File.Exists(save_path)) File.Delete(save_path);
            File.WriteAllText(save_path, transfer_json);

            return true;
        }

        /// <summary>
        /// timejd를 구하기 위한 밑작업
        /// </summary>
        /// <param name="start_time"></param>
        /// <returns></returns>
        public string transferring(string start_time)
        {
            string[] array = start_time.Split('-');

            int _iYear = int.Parse(array[0]);
            int _iMonth = int.Parse(array[1]);

            array = array[2].Split('T');

            int _iDay = int.Parse(array[0]);

            array = array[1].Split(':');

            int _iHour = int.Parse(array[0]);
            int _iMin = int.Parse(array[1]);
            double _dSec = double.Parse(array[2].Remove(array[2].Length - 2));
            double result = GetJD(_iYear, _iMonth, _iDay, _iHour, _iMin, _dSec);
            return result.ToString();

        }


        /// <summary>
        /// timejd를 구하는 함수
        /// </summary>
        /// <param name="_iYear"></param>
        /// <param name="_iMonth"></param>
        /// <param name="_iDay"></param>
        /// <param name="_iHour"></param>
        /// <param name="_iMin"></param>
        /// <param name="_dSec"></param>
        /// <returns></returns>
        public double GetJD(int _iYear, int _iMonth, int _iDay, int _iHour, int _iMin, double _dSec)

        {

            double dFracday = _iHour / 24.0 + _iMin / 1440.0 + _dSec / 86400.0;

            double dJD = (double)(367 * _iYear - 7 * (_iYear + (_iMonth + 9) / 12) / 4 + 275 * _iMonth / 9 + _iDay) + 1721013.5;

            dJD += dFracday;

            return (dJD);

        }
    }

}
