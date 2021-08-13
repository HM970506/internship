using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GSIntaller
{
    public class InputManager
    {
        List<string> input = new List<string>();

        /// <summary>
        /// input에서 받은 subsystem에 중복이 있는지 확인
        /// </summary>
        /// <returns></returns>
       public int SubsystemCheck()
        {
            input = new List<string>();
            foreach(string now_input in installation_infor.openfile)
            {
                if (installation_infor.purpose == 3 && !now_input.Contains("Backup"))return 1;
                else if (installation_infor.purpose != 3 && now_input.Contains("Backup"))return 2;
                string subsystem = installation_infor.Getsubsystem(Path.GetFileNameWithoutExtension(now_input));
                input.Add(subsystem);
            }

            for(int x=0; x<input.Count; x++)
            {
                for(int y=0; y<input.Count; y++)
                {
                    if (input[x].Equals(input[y]) && x != y) return 3;
                }
            }
            return 0;
        }

    }

}
