using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamSQLite.Database;
using XamSQLite.Models;

namespace XamSQLite.ViewModels
{
    public class VMProducts : INotifyPropertyChanged
    {
        private ObservableCollection<Products> _lstProducts { get; set; }

        public ObservableCollection<Products> lstProducts
        {
            get { return _lstProducts; }
            set
            {
                _lstProducts = value;
                OnPropertyChanged();
            }
        }


        public Command btnAddProduct { get; set; }

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

        public VMProducts()
        {
            lstProducts = new ObservableCollection<Products>();

        }

        public void GetProducts()
        {
            try
            {
                ProductDatabase productDatabase = new ProductDatabase();
                var products = productDatabase.getProducts().Result;

                if (products != null && products.Count > 0)
                {
                    lstProducts = new ObservableCollection<Products>();

                    foreach (var prod in products)
                    {
                        lstProducts.Add(new Products
                        {
                            ID = prod.ID,
                            Name = prod.Name,
                            Price = prod.Price,
                            Quantity = prod.Quantity
                        });
                    }

                    lblInfo = "Total " + products.Count.ToString() + " record(s) found";
                }
                else
                    lblInfo = "No product records found. Please add one";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void DeleteProduct(Products product)
        {
            try
            {
                ProductDatabase productDatabase = new ProductDatabase();
                var result = productDatabase.deleteProduct(product).Result;

                if (result == 1)
                    GetProducts();
                else
                    lblInfo = "Cannot Delete this Product.";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
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