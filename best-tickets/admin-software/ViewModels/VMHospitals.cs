using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Controls; // For MessageBox
using System.Windows.Data;
using admintickets.Helpers;
using Microsoft.EntityFrameworkCore;

namespace admintickets.ViewModels
{
    public class VMHospitals : ObservableObject
    {
        private BestTicketContext context;

        // List of hospitals from the DB.
        public ObservableCollection<Hospital> Hospitals { get; set; } = new ObservableCollection<Hospital>();

        // Fields for the new hospital
        private string _newHospitalName;
        public string NewHospitalName
        {
            get => _newHospitalName;
            set => SetProperty(ref _newHospitalName, value);
        }

        private string _newHospitalAddress;
        public string NewHospitalAddress
        {
            get => _newHospitalAddress;
            set => SetProperty(ref _newHospitalAddress, value);
        }

        private string _newHospitalPassword;
        public string NewHospitalPassword
        {
            get => _newHospitalPassword;
            set => SetProperty(ref _newHospitalPassword, value);
        }

        // New hospital logo as a Base64 string.
        private string _newHospitalLogo;
        public string NewHospitalLogo
        {
            get => _newHospitalLogo;
            set
            {
                SetProperty(ref _newHospitalLogo, value);
                OnPropertyChanged(nameof(NewHospitalLogoImage));
            }
        }

        // Helper property to display the logo image.
        public BitmapImage? NewHospitalLogoImage
        {
            get
            {
                if (string.IsNullOrEmpty(NewHospitalLogo))
                {
                    return null;
                }
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(Convert.FromBase64String(NewHospitalLogo));
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        // Commands
        public ICommand AddHospitalCommand { get; }
        public ICommand DeleteHospitalCommand { get; }
        public ICommand UploadLogoCommand { get; }
        public ICommand EditHospitalPasswordCommand { get; }

        public VMHospitals()
        {
            context = new BestTicketContext();
            AddHospitalCommand = new RelayCommand(async () => await AddHospital());
            DeleteHospitalCommand = new RelayCommand<Hospital>(async (hospital) => await DeleteHospital(hospital));
            UploadLogoCommand = new RelayCommand(UploadLogo);
            EditHospitalPasswordCommand = new RelayCommand<Hospital>(async (hospital) => await EditHospitalPassword(hospital));

            Refresh();
        }

        private async Task Refresh() 
        {
            Hospitals.Clear();
            Hospitals = new ObservableCollection<Hospital>(await context.Hospital.ToListAsync());
            OnPropertyChanged(nameof(Hospitals));
        }

        private async Task AddHospital()
        {
            if (string.IsNullOrWhiteSpace(NewHospitalName) ||
                string.IsNullOrWhiteSpace(NewHospitalAddress) ||
                string.IsNullOrWhiteSpace(NewHospitalPassword))
            {
                MessageBox.Show("Please fill in all required fields (Name, Address, and Password).");
                return;
            }

            // Hash the password using the helper.
            var hashedPassword = CryptographyHelper.HashPassword(NewHospitalPassword);
            var newHospital = new Hospital()
            {
                Name = NewHospitalName,
                Address = NewHospitalAddress,
                Password = hashedPassword,
                Logo = NewHospitalLogo ?? string.Empty
            };

            context.Hospital.Add(newHospital);
            await context.SaveChangesAsync();

            // Clear new hospital fields.
            NewHospitalName = string.Empty;
            NewHospitalAddress = string.Empty;
            NewHospitalPassword = string.Empty;
            NewHospitalLogo = string.Empty;

            await Refresh();
        }

        private async Task DeleteHospital(Hospital? hospital)
        {
            if (hospital == null)
                return;
            if (MessageBox.Show($"Are you sure you want to delete hospital \"{hospital.Name}\"?",
                "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                context.Hospital.Remove(hospital);
                await context.SaveChangesAsync();
                await Refresh();
            }
        }

        private void UploadLogo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                using (var image = new Bitmap(filePath))
                {
                    // Resize the image to 200x200 pixels.
                    var resizedImage = ResizeImage(image, 200, 200);

                    using (var ms = new MemoryStream())
                    {
                        resizedImage.Save(ms, ImageFormat.Jpeg);
                        byte[] imageBytes = ms.ToArray();
                        NewHospitalLogo = Convert.ToBase64String(imageBytes);
                    }
                }
            }
        }

        // Helper to resize an image.
        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        private async Task EditHospitalPassword(Hospital hospital)
        {
            if (hospital == null || string.IsNullOrWhiteSpace(hospital.Password))
            {
                MessageBox.Show("Please enter a valid password.");
                return;
            }

            // Hash the new password using the helper.
            hospital.Password = CryptographyHelper.HashPassword(hospital.Password);

            context.Hospital.Update(hospital);
            await context.SaveChangesAsync();

            MessageBox.Show($"Password for hospital \"{hospital.Name}\" has been updated.");
        }
    }
}
