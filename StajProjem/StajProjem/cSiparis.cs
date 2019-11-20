using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StajProjem
{

    class cSiparis
    {

        cGenel gnl = new cGenel();

        private int _Id;
        private int _adisyonID;
        private int _urunId;
        private int _adet;
        private int _masaId;

        #region Properties
        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public int AdisyonID
        {
            get
            {
                return _adisyonID;
            }

            set
            {
                _adisyonID = value;
            }
        }

        public int UrunId
        {
            get
            {
                return _urunId;
            }

            set
            {
                _urunId = value;
            }
        }

        public int Adet
        {
            get
            {
                return _adet;
            }

            set
            {
                _adet = value;
            }
        }

        public int MasaId
        {
            get
            {
                return _masaId;
            }

            set
            {
                _masaId = value;
            }
        }
        #endregion

        //Siparisleri Getir
        public void getByOrder(ListView lv, int AdisyonId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select URUNAD,FIYAT,Satislar.ID,Satislar.URUNID,Satislar.ADET from Satislar Inner Join Urunler on Satislar.URUNID=Urunler.ID where ADISYONID=@AdisyonId", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@AdisyonId", SqlDbType.Int).Value = AdisyonId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(Convert.ToString(Convert.ToDecimal(dr["FIYAT"]) * Convert.ToDecimal(dr["ADET"])));
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());

                    sayac++;

                }

            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }

        }
        public bool setSaveOrder(cSiparis Bilgiler)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into satislar(ADISYONID,URUNID,ADET,MASAID) values(@AdisyonNo,@UrunId,@Adet,@masaId)", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = Bilgiler._adisyonID;
                cmd.Parameters.Add("@UrunId", SqlDbType.Int).Value = Bilgiler._urunId;
                cmd.Parameters.Add("@Adet", SqlDbType.Int).Value = Bilgiler._adet;
                cmd.Parameters.Add("@masaId", SqlDbType.Int).Value = Bilgiler._masaId;

                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());


            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }

            return sonuc;

        }
        public void setDeleteOrder(int UrunId)
        {

            SqlConnection con = new SqlConnection(gnl.conString);

            SqlCommand cmd = new SqlCommand("Delete from satislar where URUNID=@UrunId", con);
            cmd.Parameters.Add("@UrunId", SqlDbType.Int).Value = UrunId;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();

        }
        public decimal GenelToplamBul(int musteriId)
        {

            decimal geneltoplam = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            /*
             * 
             * */
            //select sum(TOPLAMTUTAR) from hesapOdemeleri where MUSTERIID=@musteriId
            SqlCommand cmd = new SqlCommand("Select Sum(dbo.satislar.ADET * dbo.urunler.FIYAT) as fiyat from dbo.musteriler INNER JOIN dbo.paketSiparis on dbo.musteriler.ID=paketSiparis.MUSTERIID Inner Join adisyonlar on adisyonlar.ID=paketSiparis.ADISYONID Inner Join dbo.satislar on dbo.adisyonlar.ID=dbo.satislar.ADISYONID Inner Join dbo.urunler on dbo.satislar.URUNID=dbo.urunler.ID where(dbo.paketSiparis.MUSTERIID=@musteriId) and (dbo.paketSiparis.Durum=0)", con);
            cmd.Parameters.Add("@musteriId", SqlDbType.Int).Value = musteriId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                geneltoplam = Convert.ToDecimal(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return geneltoplam;
        }

        public void adisyonpaketsiparisDetaylari(ListView lv, int adisyonID)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("select satislar.ID as satisID,urunler.URUNAD,urunler.FIYAT,satislar.ADET from satislar Inner Join adisyonlar on adisyonlar.ID=satislar.ADISYONID Inner Join urunler on urunler.ID=satislar.URUNID where satislar.ADISYONID=@adisyonID", con);
            cmd.Parameters.Add("@adisyonID", SqlDbType.Int).Value = adisyonID;
            SqlDataReader dr = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                int i = 0;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lv.Items.Add(dr["satisID"].ToString());
                    lv.Items[i].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[i].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[i].SubItems.Add(dr["FIYAT"].ToString());

                    i++;
                }

            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }

        }



    }


}
