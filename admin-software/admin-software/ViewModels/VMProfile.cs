using CommunityToolkit.Mvvm.ComponentModel;
using admintickets.Models.DBModels;
using System.Windows;
using admintickets.Context;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using admintickets.Helpers;

namespace admintickets.ViewModels
{
    public class VMProfile : ObservableObject
    {
        private BestTicketContext db = new BestTicketContext();

        private User _user;
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public string Email => User.Email;
        public string FullName => User.FullName;

        public string Phone
        {
            get => User.Phone;
            set
            {
                User.Phone = value;
                OnPropertyChanged();
                Update();
            }
        }

        // Profile picture stored as Base64
        public string Base64Picture
        {
            get => User.Picture;
            set
            {
                User.Picture = value;
                OnPropertyChanged();
                Update();
            }
        }

        public BitmapImage ProfilePicture
        {
            get
            {
                if (string.IsNullOrEmpty(Base64Picture))
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/DefaultProfilePicture.webp"));
                }
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(Convert.FromBase64String(Base64Picture));
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        public ICommand UploadPictureCommand { get; }

        // Properties for password change
        private string _currentPassword;
        public string CurrentPassword
        {
            get => _currentPassword;
            set => SetProperty(ref _currentPassword, value);
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand ChangePasswordCommand { get; }

        private void UploadPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Load, resize, and compress the image
                using (var image = new Bitmap(filePath))
                {
                    var resizedImage = ResizeImage(image, 200, 200);
                    using (var ms = new MemoryStream())
                    {
                        resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] imageBytes = ms.ToArray();
                        Base64Picture = Convert.ToBase64String(imageBytes);
                    }
                }

                OnPropertyChanged(nameof(ProfilePicture));
            }
        }

        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Updates the user information in the database.
        /// </summary>
        public void Update()
        {
            db.Users.Update(User);
            db.SaveChanges();
        }

        /// <summary>
        /// Changes the user password after validating the current password and matching new passwords.
        /// </summary>
        private void ChangePassword()
        {
            if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CryptographyHelper.VerifyPassword(CurrentPassword, User.Password) == false)
            {
                MessageBox.Show("Current password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("New passwords do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User.Password = CryptographyHelper.HashPassword(NewPassword);
            Update();
            MessageBox.Show("Password changed successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public VMProfile(User user)
        {
            User = user;
            UploadPictureCommand = new RelayCommand(UploadPicture);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
        }
    
        public VMProfile()
        {
            if (App.Current.ConnectedUser == null)
            {
                throw new Exception("No user found");
            }
            User = App.Current.ConnectedUser;
            UploadPictureCommand = new RelayCommand(UploadPicture);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
        }
    }
}
