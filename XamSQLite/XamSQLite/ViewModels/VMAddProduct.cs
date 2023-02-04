using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamSQLite.Database;
using XamSQLite.Models;

namespace XamSQLite.ViewModels
{
    public class VMAddProduct : INotifyPropertyChanged
    {
        private Products _products { get; set; }

        public Products product
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private string _lblInfo { get; set; }
        public string lblInfo
        {
            get { return _lblInfo; }
            set
            {
                _lblInfo = value;
                OnPropertyChanged();
            }
        }

        private string _btnSaveLabel { get; set; }
        public string btnSaveLabel
        {
            get { return _btnSaveLabel; }
            set
            {
                _btnSaveLabel = value;
                OnPropertyChanged();
            }
        }

        public Command btnSaveProduct { get; set; }
        public Command btnClearProduct { get; set; }
        public VMAddProduct(Products obj)
        {
            if (obj == null || obj.ID == 0)
                ClearProduct();
            
            else
            {
                product = obj;
                btnSaveLabel = "UPDATE";
            }
            btnSaveProduct = new Command(SaveProduct);
            btnClearProduct = new Command(ClearProduct);
        }

        public void SaveProduct()
        {
            try
            {
                ProductDatabase productDatabase = new ProductDatabase();
                int i = productDatabase.saveProduct(product).Result;

                if (i == 1)
                {

                    if (btnSaveLabel.Equals("ADD"))
                    {
                        ClearProduct();
                        lblInfo = "Your Product saved successfully.";
                    }
                    else
                    {
                        ClearProduct();
                        lblInfo = "Your Product updated successfully.";
                    }
                }
                else
                    lblInfo = "Cannot save Product Information";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void ClearProduct()
        {
            product = new Products();
            product.ID = 0;
            product.Name = "";
            product.Price = null;
            product.Quantity = null;
            lblInfo = "";
            btnSaveLabel = "ADD";
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}