using Eventador.APP.V2.Common;
using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eventador.APP.V2
{
    public sealed class NavigationService
    {
        private static readonly Lazy<NavigationService> LazyInstance = new Lazy<NavigationService>(() => new NavigationService(), true);

        private readonly Dictionary<string, Type> _pageTypes;
        private readonly Dictionary<string, Type> _viewModelTypes;

        private NavigationService()
        {
            _pageTypes = GetAssemblyPageTypes();
            _viewModelTypes = GetAssemblyViewModelTypes();
            MessagingCenter.Subscribe<MessageBus, NavigationPushInfo>(this, Consts.NavigationPushMessage, NavigationPushCallback);
            MessagingCenter.Subscribe<MessageBus, NavigationPopInfo>(this, Consts.NavigationPopMessage, NavigationPopCallback);
        }

        public static Task Init(Pages page)
        {
            return Instance.Initialize(page);
        }

        private Task Initialize(Pages page)
        {
            var tks = new TaskCompletionSource<bool>();
            var initPage = GetInitializedPage(page.ToString());
            RootPush(initPage, tks);
            return tks.Task;
        }

        public static NavigationService Instance => LazyInstance.Value;

        private void NavigationPushCallback(MessageBus bus, NavigationPushInfo navigationPushInfo)
        {
            if (navigationPushInfo == null) throw new ArgumentNullException(nameof(navigationPushInfo));
            if (string.IsNullOrEmpty(navigationPushInfo.To)) throw new FieldAccessException(@"'To' page value should be set");

            Push(navigationPushInfo);
        }

        private void NavigationPopCallback(MessageBus bus, NavigationPopInfo navigationPopInfo)
        {
            if (navigationPopInfo == null) throw new ArgumentNullException(nameof(navigationPopInfo));
            Pop(navigationPopInfo);
        }

        #region NavigationService internals

        private INavigation GetTopNavigation()
        {
            var mainPage = Application.Current.MainPage;
            if (mainPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.Detail is NavigationPage navigationPage)
                {
                    var modalStack = navigationPage.Navigation.ModalStack.OfType<NavigationPage>().ToList();
                    if (modalStack.Any())
                    {
                        return modalStack.LastOrDefault()?.Navigation;
                    }
                    return navigationPage.Navigation;
                }
            }
            return (mainPage as NavigationPage)?.Navigation;
        }

        private void Push(NavigationPushInfo pushInfo)
        {
            var newPage = GetInitializedPage(pushInfo);

            switch (pushInfo.Mode)
            {
                case NavigationMode.Normal:
                    NormalPush(newPage, pushInfo.OnCompletedTask);
                    break;

                case NavigationMode.Modal:
                    ModalPush(newPage, pushInfo.OnCompletedTask, pushInfo.NewNavigationStack);
                    break;

                case NavigationMode.RootPage:
                    RootPush(newPage, pushInfo.OnCompletedTask);
                    break;

                case NavigationMode.Master:
                    MasterPush(newPage, pushInfo.OnCompletedTask);
                    break;

                case NavigationMode.Custom:
                    CustomPush(newPage, pushInfo.OnCompletedTask);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void RootPush(Page newPage, TaskCompletionSource<bool> pushInfoOnCompletedTask)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Application.Current.MainPage = new NavigationPage(newPage);
                    pushInfoOnCompletedTask.SetResult(true);
                }
                catch
                {
                    pushInfoOnCompletedTask.SetResult(false);
                }
            });
        }

        private void NormalPush(Page newPage, TaskCompletionSource<bool> completed)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await GetTopNavigation().PushAsync(newPage, true);
                    completed.SetResult(true);
                }
                catch
                {
                    completed.SetResult(false);
                }
            });
        }

        private void ModalPush(Page newPage, TaskCompletionSource<bool> completed, bool newNavigationStack = true)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (newNavigationStack) newPage = new NavigationPage(newPage);
                    await GetTopNavigation().PushModalAsync(newPage, true);
                    completed.SetResult(true);
                }
                catch
                {
                    completed.SetResult(false);
                }
            });
        }

        private void MasterPush(Page newPage, TaskCompletionSource<bool> pushInfoOnCompletedTask = null)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (Application.Current.MainPage is MasterDetailPage mp)
                    {
                        mp.IsPresented = false;
                        await Task.Delay(250);
                        if (mp.Detail is NavigationPage navigationPage)
                        {
                            var navigation = navigationPage.Navigation;
                            var navigationStack = navigationPage.Navigation.NavigationStack;
                            if (navigationStack.Any())
                            {
                                navigation.InsertPageBefore(newPage, navigationStack.FirstOrDefault());
                                await navigation.PopToRootAsync();
                            }
                        }
                        pushInfoOnCompletedTask?.SetResult(true);
                    }
                    else
                    {
                        var masterPage = GetInitializedPage(Pages.Menu.ToString());
                        //Xamarin.Forms return exception when master page title is null
                        //this title not visible in app
                        masterPage.Title = nameof(masterPage);
                        var detailPage = new NavigationPage(newPage);
                        Application.Current.MainPage = new MasterDetailPage
                        {
                            Master = masterPage,
                            Detail = detailPage
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    pushInfoOnCompletedTask?.SetResult(false);
                }
            });
        }

        private void CustomPush(Page newPage, TaskCompletionSource<bool> pushInfoOnCompletedTask)
        {
            // TODO: Implement your own navigation stack manipulation using popInfo
        }

        private void Pop(NavigationPopInfo popInfo)
        {
            switch (popInfo.Mode)
            {
                case NavigationMode.Normal:
                    NormalPop(popInfo.OnCompletedTask);
                    break;

                case NavigationMode.Modal:
                    ModalPop(popInfo.OnCompletedTask);
                    break;

                case NavigationMode.Custom:
                    CustomPop(popInfo.OnCompletedTask);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void ModalPop(TaskCompletionSource<bool> completed)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await GetTopNavigation().PopModalAsync();
                    completed.SetResult(true);
                }
                catch
                {
                    completed.SetResult(false);
                }
            });
        }

        private void CustomPop(TaskCompletionSource<bool> completed)
        {
            // TODO: Implement your own navigation stack manipulation using popInfo
        }

        private void NormalPop(TaskCompletionSource<bool> completed)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await GetTopNavigation().PopAsync();
                    completed.SetResult(true);
                }
                catch
                {
                    completed.SetResult(false);
                }
            });
        }

        private static string GetTypeBaseName(MemberInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.Name.Replace(@"Page", "").Replace(@"ViewModel", "");
        }

        private static Dictionary<string, Type> GetAssemblyPageTypes()
        {
            return typeof(BasePage).GetTypeInfo().Assembly.DefinedTypes
                .Where(ti => ti.IsClass && !ti.IsAbstract && ti.Name.Contains(@"Page") && ti.BaseType.Name.Contains(@"Page"))
                .ToDictionary(GetTypeBaseName, ti => ti.AsType());
        }

        private static Dictionary<string, Type> GetAssemblyViewModelTypes()
        {
            return typeof(BaseViewModel).GetTypeInfo().Assembly.DefinedTypes
                                        .Where(ti => ti.IsClass && !ti.IsAbstract && ti.Name.Contains(@"ViewModel") &&
                                                     ti.BaseType.Name.Contains(@"ViewModel"))
                                        .ToDictionary(GetTypeBaseName, ti => ti.AsType());
        }

        private Page GetInitializedPage(string toName, Dictionary<string, object> navParams = null)
        {
            var page = GetPage(toName);
            var viewModel = GetViewModel(toName);
            viewModel.SetNavigationParams(navParams);
            page.BindingContext = viewModel;
            return page;
        }

        private Page GetInitializedPage(NavigationPushInfo navigationPushInfo)
        {
            return GetInitializedPage(navigationPushInfo.To, navigationPushInfo.NavigationParams);
        }

        private Page GetPage(string pageName)
        {
            if (!_pageTypes.ContainsKey(pageName)) throw new KeyNotFoundException($@"Page for {pageName} not found");
            Page page;
            try
            {
                var pageType = _pageTypes[pageName];
                var pageObject = Activator.CreateInstance(pageType);
                page = pageObject as Page;
            }
            catch (Exception e)
            {
                throw new TypeLoadException($@"Unable create instance for {pageName}Page", e);
            }

            return page;
        }

        private BaseViewModel GetViewModel(string pageName)
        {
            if (!_viewModelTypes.ContainsKey(pageName)) throw new KeyNotFoundException($@"ViewModel for {pageName} not found");
            BaseViewModel viewModel;
            try
            {
                viewModel = Activator.CreateInstance(_viewModelTypes[pageName]) as BaseViewModel;
            }
            catch (Exception e)
            {
                throw new TypeLoadException($@"Unable create instance for {pageName}ViewModel", e);
            }

            return viewModel;
        }

        #endregion NavigationService internals
    }

    public class NavigationPushInfo
    {
        public string From { get; set; }
        public string To { get; set; }
        public Dictionary<string, object> NavigationParams { get; set; }
        public NavigationMode Mode { get; set; } = NavigationMode.Normal;
        public bool NewNavigationStack { get; set; }
        public TaskCompletionSource<bool> OnCompletedTask { get; set; }
    }

    public class NavigationPopInfo
    {
        public NavigationMode Mode { get; set; } = NavigationMode.Normal;
        public TaskCompletionSource<bool> OnCompletedTask { get; set; }
        public string To { get; set; }
    }
}