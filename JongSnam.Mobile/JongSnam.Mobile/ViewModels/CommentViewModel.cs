namespace JongSnam.Mobile.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public CommentViewModel()
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
