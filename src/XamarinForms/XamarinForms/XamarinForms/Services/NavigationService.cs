using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.ViewModel;
using XamarinForms.Views;

namespace XamarinForms.Services
{
    public interface INavigationService
    {
        Task NavigateToPageAsync<T>() where T : Page;
        Task NavigateToPageAsync<T>(object parameter) where T : Page;
        Task NavigateBack();
    }
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _services;

        static NavigationService _instance;

        public NavigationService(IServiceProvider services) => _services = services;

        protected INavigation Navigation
        {
            get
            {
                INavigation navigation = Application.Current?.MainPage?.Navigation;
                if (navigation != null)
                    return navigation;
                else
                {
                    //This != good!
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        }

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public Task NavigateToPageAsync<T>() where T : Page
        {
            return NavigateTo<T>();
        }

        public Task NavigateToPageAsync<T>(object parameter) where T : Page
        {
            return NavigateTo<T>(parameter);
        }

        public Task NavigateBack()
        {
            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();

            throw new InvalidOperationException("No pages to navigate back to!");
        }

        private async Task NavigateTo<T>(object parameter = null) where T : Page
        {
            var toPage = ResolvePage<T>();

            if (toPage != null)
            {
                var vm = GetPageViewModelBase(toPage);
                if (vm != null)
                    await vm.OnNavigatingTo(parameter);
                await Navigation.PushAsync(toPage, true);
            }
            else
                throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        }

        //private async void Page_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        //{
        //    //To determine forward navigation, we look at the 2nd to last item on the NavigationStack
        //    //If that entry equals the sender, it means we navigated forward from the sender to another page
        //    bool isForwardNavigation = Navigation.NavigationStack.Count > 1
        //        && Navigation.NavigationStack[^2] == sender;

        //    if (sender is Page thisPage)
        //    {
        //        if (!isForwardNavigation)
        //        {
        //            thisPage.NavigatedTo -= Page_NavigatedTo;
        //            thisPage.NavigatedFrom -= Page_NavigatedFrom;
        //        }

        //        await CallNavigatedFrom(thisPage, isForwardNavigation);
        //    }
        //}

        //private Task CallNavigatedFrom(Page p, bool isForward)
        //{
        //    var fromViewModel = GetPageViewModelBase(p);

        //    if (fromViewModel != null)
        //        return fromViewModel.OnNavigatedFrom(isForward);
        //    return Task.CompletedTask;
        //}

        //private async void Page_NavigatedTo(object? sender, NavigatedToEventArgs e)
        //    => await CallNavigatedTo(sender as Page);

        //private Task CallNavigatedTo(Page? p)
        //{
        //    var fromViewModel = GetPageViewModelBase(p);

        //    if (fromViewModel != null)
        //        return fromViewModel.OnNavigatedTo();
        //    return Task.CompletedTask;
        //}

        private T ResolvePage<T>() where T : Page
        {
            var page = Startup.ServiceProvider.GetService<T>();
            if (page == null)
                return null;
            return page;
        }

        private BaseViewModel GetPageViewModelBase(Page p) => p?.BindingContext as BaseViewModel;
    }

    //public class NavigationService
    //{
    //    protected readonly Dictionary<Type, Type> _mappings;

    //    static NavigationService _instance;

    //    public NavigationService()
    //    {
    //        _mappings = new Dictionary<Type, Type>();

    //        CreatePageViewModelMappings();
    //    }

    //    public static NavigationService Instance
    //    {
    //        get
    //        {
    //            if (_instance == null)
    //                _instance = new NavigationService();

    //            return _instance;
    //        }
    //    }

    //    protected Application CurrentApplication
    //    {
    //        get { return Application.Current; }
    //    }

    //    public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
    //    {
    //        return InternalNavigateToAsync(typeof(TViewModel), null);
    //    }

    //    public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
    //    {
    //        return InternalNavigateToAsync(typeof(TViewModel), parameter);
    //    }

    //    public Task NavigateToAsync(Type viewModelType)
    //    {
    //        return InternalNavigateToAsync(viewModelType, null);
    //    }

    //    public Task NavigateToAsync(Type viewModelType, object parameter)
    //    {
    //        return InternalNavigateToAsync(viewModelType, parameter);
    //    }

    //    public async Task NavigateBackAsync()
    //    {
    //        await CurrentApplication.MainPage.Navigation.PopAsync();
    //    }

    //    protected Type GetPageTypeForViewModel(Type viewModelType)
    //    {
    //        if (!_mappings.ContainsKey(viewModelType))
    //        {
    //            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
    //        }

    //        return _mappings[viewModelType];
    //    }

    //    protected Page CreateAndBindPage(Type viewModelType, object parameter)
    //    {
    //        Type pageType = GetPageTypeForViewModel(viewModelType);

    //        if (pageType == null)
    //        {
    //            throw new Exception($"Mapping type for {viewModelType} != a page");
    //        }

    //        Page page = Activator.CreateInstance(pageType) as Page;

    //        return page;
    //    }

    //    protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
    //    {
    //        Page page = CreateAndBindPage(viewModelType, parameter);

    //        if (page is Rivers)
    //        {
    //            CurrentApplication.MainPage = page;
    //        }
    //        else
    //        {
    //            if (CurrentApplication.MainPage is NavigationPage navigationPage)
    //            {
    //                await navigationPage.PushAsync(page);
    //            }
    //        }

    //        await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
    //    }

    //    void CreatePageViewModelMappings()
    //    {
    //        _mappings.Add(typeof(RiversViewModel), typeof(Rivers));
    //        _mappings.Add(typeof(RiverViewModel), typeof(River));
    //    }
    //}

}
