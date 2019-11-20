using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace StajProjem
{
    class cPaketler
    {
        cGenel gnl = new cGenel();

        #region Fields
        private int _ID;
        private int _AdditionID;
        private int _ClientId;
        private string _Description;
        private int _State;
        private int _Paytypeid;
        #endregion

        #region Properties
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public int AdditionID
        {
            get
            {
                return _AdditionID;
            }

            set
            {
                _AdditionID = value;
            }
        }

        public int ClientId
        {
            get
            {
                return _ClientId;
            }

            set
            {
                _ClientId = value;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }

        public int State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = value;
            }
        }

        public int Paytypeid
        {
            get
            {
                return _Paytypeid;
            }

            set
            {
                _Paytypeid = value;
            }
        }
        #endregion

        //paket servisi açma
        public bool OrderServiceOpen(cPaketler order)
        {
            bool result = false;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into paketSiparis (ADISYONID,MUSTERIID,ODEMETURID,ACIKLAMA) values (@ADISYONID,@MUSTERIID,@ODEMETURID,@ACIKLAMA)", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@ADISYONID", SqlDbType.Int).Value = order._AdditionID;
                cmd.Parameters.Add("@MUSTERIID", SqlDbType.Int).Value = order._ClientId;
                cmd.Parameters.Add("@ODEMETURID", SqlDbType.Int).Value = order._Paytypeid;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = order._Description;
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }

            finally
            {
                con.Dispose();
                con.Close();

            }

            return result;




        }

        //paket servisi kapatma
        public void OrderServiceClose(int AdditionID)
        {


            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update paketSiparis set paketSiparis.Durum=1 where paketSiparis.ADISYONID=@AdditionID", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AdditionID", SqlDbType.Int).Value = AdditionID;

                Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }

            finally
            {
                con.Dispose();
                con.Close();

            }





        }

        //açılan adisyon ve paket siparişe ait ön girilen ödeme tür ıd
        public int OdemeTurIdGetir(int adisyonId)
        {
            int odemeTurID = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select paketSiparis.ODEMETURID from paketSiparis Inner Join adisyonlar on paketSiparis.ADISYONID=adisyonlar.ID where adisyonlar.ID=@adisyonId", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@adisyonId", SqlDbType.Int).Value = adisyonId;
                odemeTurID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                String hata = ex.Message;
                throw;
            }

            finally
            {
                con.Dispose();
                con.Close();

            }
            return odemeTurID;


        }

        //sipariş kontrol için müşteriye ait açık olan en son adisyon nosunu getirme
        //bir müşteriye ait 2 tane siparişin açık olmayacağını belirtiyoruz.

        public int musteriSonAdisyonIDGetir(int musteriID)
        {
            int no;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select adisyonlar.ID from adisyonlar Inner Join paketSiparis on paketSiparis.ADISYONID=adisyonlar.ID where (adisyonlar.DURUM=0) (paketSiparis.DURUM=0) and paketSiparis.MUSTERIID=@musteriID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@musteriID", SqlDbType.Int).Value = musteriID;
                no = Convert.ToInt32(cmd.ExecuteScalar());


            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return no;


        }

        //müşteri arama ekranında adsiyonbul butonu adisyon açık mı değil mi? kontrol
        public bool getCheckOpenAdditionID(int additionID)
        {

            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from adisyonlar where (DURUM=0) and (ID=@additionID)", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.Parameters.Add("@additionID", SqlDbType.Int).Value = additionID;
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();

            }

            return result;

        }














    }
}
