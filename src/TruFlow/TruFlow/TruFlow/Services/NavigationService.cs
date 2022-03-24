using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TruFlow.ViewModel;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace TruFlow.Services
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
}
