using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Lastwork3_team4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //スレッドの作成
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            textBox3.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button8.Visible = true;



            string connectionString = "Data Source=BBS;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // テーブルに新しいカラムを追加する ALTER TABLE 文の実行
                using (SQLiteCommand alterCommand = new SQLiteCommand(connection))
                {
                    alterCommand.CommandText = "CREATE TABLE IF NOT EXISTS Bbs (title TEXT);";
                    alterCommand.ExecuteNonQuery();

                   // alterCommand.CommandText = "ALTER TABLE Bbs ADD COLUMN title TEXT;";
                   // alterCommand.ExecuteNonQuery();
                }

                // テキストボックスから入力された値を挿入する INSERT 文の実行
                string columnName = "title";
                string columnValue = textBox2.Text;

                using (SQLiteCommand insertCommand = new SQLiteCommand(connection))
                {
                    insertCommand.CommandText = "INSERT INTO Bbs (" + columnName + ") VALUES (@value);";
                    insertCommand.Parameters.AddWithValue("@value", columnValue);
                    insertCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
            //明日の自分へ
            //上のコードはタイトルしか出来てない、しかもエラー出る。タイトル：textbox2、本文：textbox3



            /*
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = "BBS.db" };
            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();
                using (var cmd = new SQLiteCommand(cn))
                {
                    //テーブル追加
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Bbs(" + "name TEXT," + "birthday TEXT" + ")";
                    cmd.ExecuteNonQuery();
                    //データ追加
                    string a = textBox1.Text;
                    cmd.CommandText = "INSERT INTO Bbs VALUES(@name);";
                    cmd.Parameters.AddWithValue("@name", a);
                    cmd.ExecuteNonQuery();
                }
            }
            */
        }

        //返信する
        private void button2_Click(object sender, EventArgs e)
        {
            label4.Visible = true;

            //タイトル一覧出すコード
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = "BBS.db" };
            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();
                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = "SELECT * FROM Bbs;";

                    label5.Text = cmd.CommandText;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var sb = new StringBuilder();
                        while (reader.Read())
                        {
                            sb.AppendLine(reader.GetString(0)); // title 列の値を取得する
                        }
                        label5.Text = sb.ToString();
                    }
                }
            }    

            label5.Visible = true;
        }
        //スレッドの表示
        private void button3_Click(object sender, EventArgs e)
        {
        }
        //ログイン
        private void button4_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox1.Visible = true;
            button7.Visible = true;
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = "BBS.db" };
            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();
                using (var cmd = new SQLiteCommand(cn))
                {
                    //テーブル作成
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Bbs(" + "name TEXT" + ")";
                    cmd.ExecuteNonQuery();
                    //データ追加
                    string a = textBox1.Text;
                    cmd.CommandText = "INSERT INTO Bbs VALUES(@name);";
                    cmd.Parameters.AddWithValue("@name", a);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //ログアウト
        private void button5_Click(object sender, EventArgs e)
        {
        }
        //終了
        private void button6_Click(object sender, EventArgs e)
        {
        }
        private void button7_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = false;
            button7.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox3.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            button8.Visible = false;
        }
    }
}