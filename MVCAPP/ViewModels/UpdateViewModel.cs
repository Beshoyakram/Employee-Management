namespace MVCAPP.ViewModels
{
    public class UpdateViewModel :CreateViewModel
    {
        public int id { get; set; }

        public string? ExistingPhotoPath { get; set; }
    }
}
