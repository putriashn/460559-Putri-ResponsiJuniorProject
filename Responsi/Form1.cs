using Npgsql;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Responsi2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=Responsi2_Putri";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                dgvData.DataSource = null;
                sql = "select * from st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "FAIL!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from st_insert(:_id_karyawan,:_nama)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", cbKaryawan.Text);
                cmd.Parameters.AddWithValue("_nama", txtNamaKaryawan.Text);
                if ((int) cmd = ExecuteScalar() == 1);
                {
                    MessageBox.Show("Berhasil", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    btnLoadData.PerformClick();
                    txtNamaKaryawan.Text = cbKaryawan.Text = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvdata_CellClick (object sender_DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dgvDataRows[e.RowIndex];
                txtNamaKaryawan.Text = r.Cells["_nama"].Value.ToString();
                cbKaryawan.Text = r.Cells["_id_karyawan"].Value.ToString();
            }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (r = null)
                {
                    MessageBpx.Show("Pilih baris terlebih dahulu");
                    return;
                }
            try
                {
                    conn.Open();
                    sql = @"select * from st_update (:_id_karyawan,:_nama,_id_dept)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id_dept", r.Cells["_id_dept"].Value.ToString());
                    cmd.Parameters.AddWithValue("_id_karyawan", cbKaryawan.Text);
                    cmd.Parameters.AddWithValue("_nama", txtNamaKaryawan.Text);
                    if ((int)cmd = ExecuteScalar() == 1) ;
                    {
                        MessageBox.Show("Berhasil", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        btnLoadData.PerformClick();
                        txtNamaKaryawan.Text = cbKaryawan.Text = null;
                        r = null;
                    }
                }
               catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r = null)
            {
                MessageBpx.Show("Pilih baris terlebih dahulu");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from st_delete (_id_dept)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_dept", r.Cells["_id_dept"].Value.ToString());
                cmd.Parameters.AddWithValue("_id_karyawan", cbKaryawan.Text);
                cmd.Parameters.AddWithValue("_nama", txtNamaKaryawan.Text);
                if ((int)cmd = ExecuteScalar() == 1) ;
                {
                    MessageBox.Show("Berhasil", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    btnLoadData.PerformClick();
                    txtNamaKaryawan.Text = cbKaryawan.Text = null;
                    r = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
}