using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using CustomXmlViewer;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            Data.DataSetting(table, columns, columns_type, column, PK, PK_index, PK_check, cmd, da, conn, ds, dt, now_value,
                this.dataGridView1, this.ucXmlRichTextBox1, this.textBox1);
            Buttons.ButtonsSetting(connect_button, unconnect_button, up_button, down_button, search_button, save_button);
            Labels.LabelsSetting(isconnect_label);

            Input.InputSetting(this.server_input, this.table_input, this.db_input, this.id_input, this.pw_input);
            Input.InputStart();
            Data.DataStart();
            Buttons.WhenStartButtons();

            dba = new DBAccessor();
            sm = new SQLConnecitonManager();

        }

        public string table=null;
        public List<string> columns = new List<string>();
        public List<string> columns_type = new List<string>();
        public string column=null;
        public List<string> PK = new List<string>();
        public List<int> PK_index = new List<int>();
        public List<string> PK_check = new List<string>();
        public SqlCommand cmd = null;
        public SqlDataAdapter da = null;
        public SqlConnection conn = null;
        public DataSet ds = null;
        public DataTable dt = null;
        public string now_value = null;

        //인터페이스로 끊어서! 기능과 view를 가능한 철저하게 분리

        DBAccessor dba = null;
        SQLConnecitonManager sm = null;


        private void connect_button_Click(object sender, EventArgs e)
        {
            bool isConnect=sm.Connect(); //connect interface

            if (isConnect)
            {
                Buttons.WhenConnectingSuccessButtons();
                Labels.WhenConnectingSuccessLabels();
                Data.gridview(); //출력 함수
                Data.columns = Data.dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
                sm.getColumntype();
                sm.getPK();

            }
            else Buttons.WhenConnectingFailButtons();
            
        }

        private void save_button_Click(object sender, EventArgs e)
        {
           bool isSave=dba.Save(); //save interface
            if (isSave)
            {
                Data.dataGridView1.DataSource = null;
                Data.gridview(); // 출력 함수
                Data.ucXmlRichTextBox1.Text = "";
                Data.ucXmlRichTextBox1.ReadOnly = true;
                Data.ucXmlRichTextBox1.Enabled = false;
            }
        }

        private void unconnect_button_Click(object sender, EventArgs e)
        {
            bool isUnconnect = sm.Unconnect(); //unconnect interface

            if (isUnconnect) //모든 값 초기화
            {
                Data.dataGridView1.DataSource = null;
                Labels.isconnect_label.Text = "unconnect";
                Data.ucXmlRichTextBox1.Text = "";
                Data.ucXmlRichTextBox1.ReadOnly = true;
                Data.ucXmlRichTextBox1.Enabled = false;

                Data.textBox1.Enabled = false;

                Buttons.WhenUnconnectingSuccessButtons();
            }
        }


        private void Datagridview1_cellclick(object sender, DataGridViewCellEventArgs e)
        {
            Data.data_output(); //출력 함수

        }


        private void up_button_Click(object sender, EventArgs e)
        {
            ucXmlRichTextBox1.Font = new Font(ucXmlRichTextBox1.Font.FontFamily, ucXmlRichTextBox1.Font.Size + 1);
            Data.data_output();
        }

        private void down_button_Click(object sender, EventArgs e)
        {
            ucXmlRichTextBox1.Font = new Font(ucXmlRichTextBox1.Font.FontFamily, ucXmlRichTextBox1.Font.Size - 1);
            Data.data_output();
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            dba.Search(); //검색 결과 인덱스는 일회성 값들이기 때문에 메모리를 할애하지 않도록 함수와 출력을 따로 분리하지 않았습니다!
        }
    }


    public class Input
    {
        public static TextBox server_input;
        public static TextBox table_input;
        public static TextBox db_input;
        public static TextBox id_input;
        public static TextBox pw_input;

        /// <summary>
        /// Form1의 TextBox를 sttatic value로 연결
        /// </summary>
        /// <param name="server_input_"></param>
        /// <param name="table_input_"></param>
        /// <param name="db_input_"></param>
        /// <param name="id_input_"></param>
        /// <param name="pw_input_"></param>
        public static void InputSetting(TextBox server_input_, TextBox table_input_, TextBox db_input_, TextBox id_input_, TextBox pw_input_)
        {
            server_input = server_input_;
            table_input = table_input_;
            db_input = db_input_;
            id_input = id_input_;
            pw_input = pw_input_;
        }

        /// <summary>
        /// 프로그램 시작시 config값으로 textbox setting
        /// </summary>
        public static void InputStart()
        {
            server_input.Text = Properties.Settings.Default.server_input.ToString();
            table_input.Text = Properties.Settings.Default.table_input.ToString();
            db_input.Text = Properties.Settings.Default.db_input.ToString();
            id_input.Text = Properties.Settings.Default.id_input.ToString();
            pw_input.Text = Properties.Settings.Default.pw_input.ToString();
            pw_input.PasswordChar = '*';
        }

        /// <summary>
        /// textbox에 null이 있는지 check, null이 없다면 하나의 string으로 묶을 수 있도록 각 값 수정
        /// </summary>
        /// <returns></returns>
        public static string[] check_textbox()
        {
            string[] input_array = new string[5];
            input_array[0] = string.Format(server_input.Text);
            input_array[1] = string.Format(table_input.Text);
            input_array[2] = string.Format(db_input.Text);
            input_array[3] = string.Format(id_input.Text);
            input_array[4] = string.Format(pw_input.Text);

            Data.SetTable(input_array[1]);

            string[] false_return = { "Server", "Table", "Database", "uid", "pwd" };

            for (int x = 0; x < input_array.Length; x++)
            {
                if (input_array[x] == "")
                {
                    MessageBox.Show(false_return[x] + "를 채워주세요!", "ERROR");
                    return null;
                }
                else input_array[x] = false_return[x] + "=" + input_array[x] + ";";
            }

            Properties.Settings.Default.server_input = server_input.Text;
            Properties.Settings.Default.table_input = table_input.Text;
            Properties.Settings.Default.db_input = db_input.Text;
            Properties.Settings.Default.id_input = id_input.Text;
            Properties.Settings.Default.pw_input = pw_input.Text;
            Properties.Settings.Default.Save();

            input_array[1] = "";
            return input_array;
        }

    }

    public class Buttons
    {
        public static Button connect_button;
        public static Button unconnect_button;
        public static Button up_button;
        public static Button down_button;
        public static Button search_button;
        public static Button save_button;

        /// <summary>
        /// form1의 button을 static variable에 배정
        /// </summary>
        /// <param name="connect_button_"></param>
        /// <param name="unconnect_button_"></param>
        /// <param name="up_button_"></param>
        /// <param name="down_button_"></param>
        /// <param name="search_button_"></param>
        /// <param name="save_button_"></param>
        public static void ButtonsSetting(Button connect_button_, Button unconnect_button_, Button up_button_, Button down_button_, Button search_button_, Button save_button_)
        {
            connect_button = connect_button_;
            unconnect_button = unconnect_button_;
            up_button = up_button_;
            down_button = down_button_;
            search_button = search_button_;
            save_button = save_button_;
        }

        /// <summary>
        /// 프로그램 시작시 초기 button setting
        /// </summary>
        public static void WhenStartButtons()
        {
            unconnect_button.Enabled = false;
            save_button.Enabled = false;
        }

        /// <summary>
        /// connect 시도시 button setting
        /// </summary>
        public static void WhenConnectButtons()
        {
            up_button.Enabled = true;
            down_button.Enabled = true;
        }

        /// <summary>
        /// connecting 성공시 button setting
        /// </summary>
        public static void WhenConnectingSuccessButtons()
        {
            unconnect_button.Enabled = true;
        }

        public static void WhenConnectingFailButtons()
        {
            connect_button.Enabled = true;
            unconnect_button.Enabled = false;
        }

        /// <summary>
        /// unconnect 성공시 button setting
        /// </summary>
        public static void WhenUnconnectingSuccessButtons()
        {
            connect_button.Enabled = true;
            unconnect_button.Enabled = false;
            search_button.Enabled = false;
            up_button.Enabled = false;
            down_button.Enabled = false;
            save_button.Enabled = false;
        }

        /// <summary>
        /// xml data out시 button setting
        /// </summary>
        public static void WhenDataOutputXmlButtons()
        {
            search_button.Enabled = true;
            save_button.Enabled = true;
        }


        /// <summary>
        /// xml이 아닌 data out시 button setting
        /// </summary>
        public static void WhenDataOutputOthersButtons()
        {

            save_button.Enabled = false;
            search_button.Enabled = false;
        }

    }

    public class Labels
    {
        public static Label isconnect_label;
        public static void LabelsSetting(Label isconnect_label_)
        {
            isconnect_label = isconnect_label_;

        }

        /// <summary>
        /// connect 성공시 label setting
        /// </summary>
        public static void WhenConnectingSuccessLabels()
        {
            isconnect_label.Text = "connected..";
        }
    }
    public class Data
    {

        public static string table;
        public static List<string> columns;
        public static List<string> columns_type;
        public static string column;
        public static List<string> PK;
        public static List<int> PK_index;
        public static List<string> PK_check;
        public static SqlCommand cmd;
        public static SqlDataAdapter da;
        public static SqlConnection conn;
        public static DataSet ds;
        public static DataTable dt;
        public static string now_value;
        public static DataGridView dataGridView1;
        public static ucXmlRichTextBox ucXmlRichTextBox1;
        public static TextBox textBox1;

        public static void DataSetting(
        string table_,
        List<string> columns_,
        List<string> columns_type_,
        string column_,
        List<string> PK_,
        List<int> PK_index_,
        List<string> PK_check_,
        SqlCommand cmd_,
        SqlDataAdapter da_,
        SqlConnection conn_,
        DataSet ds_,
        DataTable dt_,
        string now_value_,
        DataGridView dataGridView1_,
        ucXmlRichTextBox ucXmlRichTextBox1_,
        TextBox textBox1_)
        {
            table = table_;
            columns = columns_;
            column = column_;
            columns_type = columns_type_;
            PK = PK_;
            PK_check = PK_check_;
            PK_index = PK_index_;
            cmd = cmd_;
            da = da_;
            conn = conn_;
            ds = ds_;
            dt = dt_;
            now_value = now_value_;
            dataGridView1 = dataGridView1_;
            textBox1 = textBox1_;
            ucXmlRichTextBox1 = ucXmlRichTextBox1_;
        }

        ~Data()
        { }
        
        /// <summary>
        /// 프로그램 시작시 초기 textbox setting
        /// </summary>
        public static void DataStart()
        {
            ucXmlRichTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            ucXmlRichTextBox1.Enabled = false;
        }

        /// <summary>
        /// table 이름 setting
        /// </summary>
        /// <param name="table_"></param>
        public static void SetTable(string table_)
        {
            table = table_;
        }


        /// <summary>
        /// datagridview에 초기 table 표 출력
        /// </summary>
        public static void gridview()
        {
            cmd = new SqlCommand();
            cmd.Connection = conn;

            try
            {
                cmd.CommandText = "SELECT * From " + table;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, table);
                dt = ds.Tables[table];

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = table;

            }
            catch
            {
                MessageBox.Show("database error: 테이블을 불러오는 데 실패하였습니다.");
                Buttons.connect_button.Enabled = true;
                Buttons.unconnect_button.Enabled = false;
            }
        }

        /// <summary>
        /// data의 상세값을 오른쪽 textbox에 출력. xml이라면 수정 가능, xml이 아니라면 수정 불가능.
        /// </summary>
        public static void data_output()
        {
            DataGridViewCell now_cell = dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[dataGridView1.CurrentCellAddress.X];
            now_value = now_cell.Value.ToString();
            if (columns_type[dataGridView1.CurrentCellAddress.X].ToLower() == "xml")
            {
                ucXmlRichTextBox1.Enabled = true;
                ucXmlRichTextBox1.ReadOnly = false;

                PK_check.Clear();
                for (int x = 0; x < PK.Count(); x++)
                {
                    PK_check.Add(dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[PK_index[x]].Value.ToString());
                }
                column = columns[dataGridView1.CurrentCellAddress.X];
                ucXmlRichTextBox1.Xml = now_value;

                textBox1.Enabled = true;
                Buttons.WhenDataOutputXmlButtons();

            }
            else
            {
                ucXmlRichTextBox1.Enabled = false;
                ucXmlRichTextBox1.ReadOnly = true;

                ucXmlRichTextBox1.Text = now_value;

                textBox1.Enabled = false;
                Buttons.WhenDataOutputOthersButtons();

            }

        }
    }


    public class SQLConnecitonManager
    {
        public bool Connect()
        {

            string[] input_array = Input.check_textbox();

            if (input_array != null)
            {
                //DESKTOP-LHA0FDV\SQLEXPRESS
                //TB_SYSTEM_CONFIGURATION
                string conn_input = string.Join("", input_array);

                Data.conn = new SqlConnection(conn_input);
                try
                {
                    Buttons.connect_button.Enabled = false;
                    Data.conn.Open();
                    Buttons.WhenConnectButtons();
                }
                catch
                {
                    MessageBox.Show("linking error: 연결에 실패하였습니다.");
                    Buttons.connect_button.Enabled = true;
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// input_array를 하나의 string으로 만들어 MSSQL에 연결
        /// </summary>
        /// <param name="connect_button"></param>

        /// <summary>
        /// 스키마 테이블을 이용해 PK column의 목록을 구하는 함수. 값 save시 쿼리문 작성을 위해 필요.
        /// </summary>
        public void getPK()
        {
            string pk_query = "select column_name from information_schema.KEY_COLUMN_USAGE where CONSTRAINT_NAME LIKE '%" + Data.table + "'";

            Data.PK.Clear();
            Data.PK_index.Clear();

            Data.cmd = new SqlCommand();
            Data.cmd.Connection = Data.conn;

            try
            {
                Data.cmd.CommandText = pk_query;
                Data.da = new SqlDataAdapter(Data.cmd);
                Data.ds = new DataSet();
                Data.da.Fill(Data.ds, Data.table);
                Data.dt = Data.ds.Tables[Data.table];

                foreach (DataRow now in Data.dt.Rows)
                {
                    string pk_column = now["column_name"].ToString();
                    Data.PK.Add(pk_column);
                    Data.PK_index.Add(Data.columns.IndexOf(pk_column));
                }

            }
            catch
            {
                MessageBox.Show("PK error: PK를 불러오는 데 실패하였습니다.");
            }
        }

        /// <summary>
        /// 스키마 테이블을 이용해 각 column의 type을 구하는 함수. 해당 column이 xml인지 구분하기 위해 필요.
        /// </summary>
        public void getColumntype()
        {
            string query = "select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where table_name = '" + Data.table + "'";

            Data.columns_type.Clear();

            Data.cmd = new SqlCommand();
            Data.cmd.Connection = Data.conn;

            try
            {
                Data.cmd.CommandText = query;
                Data.da = new SqlDataAdapter(Data.cmd);
                Data.ds = new DataSet();
                Data.da.Fill(Data.ds, Data.table);
                Data.dt = Data.ds.Tables[Data.table];

                foreach (DataRow now in Data.dt.Rows)
                {
                    string column_type = now["DATA_TYPE"].ToString();
                    Data.columns_type.Add(column_type);
                }

            }
            catch
            {
                MessageBox.Show("type error: column type을 불러오는 데 실패하였습니다.");
            }
        }


        /// <summary>
        /// 연결을 끊고 모든 값을 초기화
        /// </summary>
        public bool Unconnect()
        {
            try
            {
                Data.conn.Close();
            }
            catch
            {
                MessageBox.Show("unconnect error: unconnect에 실패하였습니다.", "ERROR");
                return false;
            }

            return true;
        }
    }
   
    public class DBAccessor
    {

        public bool Save()
        {
            string input = Data.ucXmlRichTextBox1.Text;
            try
            {
                XDocument xdoc = XDocument.Parse(input);
            }
            catch
            {
                MessageBox.Show("syntax error: xml문법에 맞지 않습니다.", "ERROR");
                return false;
            }
            Data.cmd = new SqlCommand();
            Data.cmd = Data.conn.CreateCommand();
            string update_query = "UPDATE " + Data.table + " SET " + Data.column + "='" + Data.ucXmlRichTextBox1.Text + "' WHERE ";

            for (int x = 0; x < Data.PK.Count; x++)
            {
                update_query += Data.PK[x] + "='" + Data.PK_check[x] + "'";
                if (x != Data.PK.Count - 1) update_query += " and ";
            }
            SqlTransaction trans = Data.conn.BeginTransaction();

            try
            {
                Data.cmd.Connection = Data.conn;
                Data.cmd.Transaction = trans;
                Data.cmd.CommandText = update_query;

                try
                {
                    Data.cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    MessageBox.Show("commit error: commit에 실패하였습니다.", "ERROR");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("update error: 쿼리문을 입력하는 데 실패하였습니다.", "ERROR");
                return false;
            }

            return true;
        }


        /// <summary>
        /// textbox에서 검색한 내용을 highlight
        /// </summary>
        public void Search()
        {
            Data.ucXmlRichTextBox1.Select(0, Data.ucXmlRichTextBox1.TextLength);
            Data.ucXmlRichTextBox1.SelectionBackColor = Color.White;
            Data.ucXmlRichTextBox1.Select(0, 0);

            string search_input = Data.textBox1.Text;
            int find_index = 0;
            int start_index = 0;
            if (search_input != null && search_input != "" && search_input != " ")
            {

                while (find_index != -1)
                {
                    try
                    {
                        find_index = Data.ucXmlRichTextBox1.Find(search_input, start_index + 1, Data.ucXmlRichTextBox1.TextLength, RichTextBoxFinds.MatchCase);
                    }
                    catch
                    {
                        find_index = -1;
                    }

                    if (find_index != -1)
                    {
                        start_index = find_index;
                        Data.ucXmlRichTextBox1.Select(start_index, search_input.Length);
                        Data.ucXmlRichTextBox1.SelectionBackColor = Color.Aquamarine;
                        Data.ucXmlRichTextBox1.Select(0, 0);
                    }
                }

            }
        }

    }



}