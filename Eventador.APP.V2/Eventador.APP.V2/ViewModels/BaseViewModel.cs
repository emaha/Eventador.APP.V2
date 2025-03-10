﻿using Eventador.APP.V2.Common;
using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Common.Helpers;
using Plugin.Connectivity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class BaseViewModel : Bindable, IDisposable
    {
        private string title = string.Empty;

        public string PageTitle
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        private readonly CancellationTokenSource _networkTokenSource = new CancellationTokenSource();
        private readonly ConcurrentDictionary<string, ICommand> _cachedCommands = new ConcurrentDictionary<string, ICommand>();

        public Dictionary<string, object> NavigationParams
        {
            get => Get<Dictionary<string, object>>();
            private set
            {
                Set(value);
                OnSetNavigationParams(value ?? new Dictionary<string, object>());
            }
        }

        public PageState State
        {
            get => Get(PageState.Clean);
            set => Set(value);
        }

        public bool IsLoadDataStarted
        {
            get => Get<bool>();
            protected internal set => Set(value);
        }

        public bool IsConnected => !CrossConnectivity.IsSupported || CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected;
        public CancellationToken CancellationToken => _networkTokenSource?.Token ?? CancellationToken.None;

        public ICommand GoBackCommand => MakeCommand(GoBackCommandExecute);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            ClearDialogs();
            CancelNetworkRequests();
        }

        public void SetNavigationParams(Dictionary<string, object> navParams)
        {
            NavigationParams = navParams;
        }

        public void CancelNetworkRequests()
        {
            _networkTokenSource.Cancel();
        }

        public virtual Task OnPageAppearing()
        {
            return Task.FromResult(0);
        }

        public virtual Task OnPageDisappearing()
        {
            return Task.FromResult(0);
        }

        public void StartLoadData()
        {
            if (IsLoadDataStarted) return;
            IsLoadDataStarted = true;

            Task.Run(LoadDataAsync, CancellationToken);
        }

        //override this method for load data
        protected virtual Task LoadDataAsync()
        {
            return Task.FromResult(0);
        }

        //override this method for sets viewmodel properties before page appearing
        public virtual void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            // do nothing
        }

        protected static Task ChangeTabPage(int number)
        {
            MessageBus.SendMessage(Consts.NavigationTabPageMessage, number);
            return Task.CompletedTask;
        }

        protected static Task<bool> NavigateTo(object toName,
            object fromName = null,
            NavigationMode mode = NavigationMode.Normal,
            string toTitle = null,
            Dictionary<string, object> navParams = null,
            bool newNavigationStack = false,
            bool withAnimation = true,
            bool withBackButton = false)
        {
            MessageBus.SendMessage(Consts.DialogHideLoadingMessage);

            var completedTask = new TaskCompletionSource<bool>();
            MessageBus.SendMessage(Consts.NavigationPushMessage,
                new NavigationPushInfo
                {
                    To = toName.ToString(),
                    From = fromName?.ToString(),
                    Mode = mode,
                    NavigationParams = navParams,
                    NewNavigationStack = newNavigationStack,
                    OnCompletedTask = completedTask,
                });
            return completedTask.Task;
        }

        protected static ICommand MakeNavigateToCommand(object toName,
            NavigationMode mode = NavigationMode.Normal,
            string toTitle = null,
            bool newNavigationStack = false,
            bool withAnimation = true,
            bool withBackButton = true,
            Dictionary<string, object> navParams = null)
        {
            return new Command(() => NavigateTo(toName, null, mode, toTitle, navParams, newNavigationStack, withAnimation, withBackButton));
        }

        protected ICommand MakeCommand(Action commandAction, [CallerMemberName] string propertyName = null)
        {
            return GetCommand(propertyName) ?? SaveCommand(new Command(commandAction), propertyName);
        }

        protected ICommand MakeCommand(Action<object> commandAction, [CallerMemberName] string propertyName = null)
        {
            return GetCommand(propertyName) ?? SaveCommand(new Command(commandAction), propertyName);
        }

        protected Task<bool> NavigateBack(NavigationMode mode = NavigationMode.Normal, bool withAnimation = true, bool force = false)
        {
            ClearDialogs();
            var taskCompletionSource = new TaskCompletionSource<bool>();
            MessageBus.SendMessage(Consts.NavigationPopMessage, new NavigationPopInfo
            {
                Mode = mode,
                OnCompletedTask = taskCompletionSource
            });
            return taskCompletionSource.Task;
        }

        public void ClearDialogs()
        {
            HideLoading();
        }

        private void GoBackCommandExecute(object mode)
        {
            if (mode is NavigationMode navigationMode)
            {
                NavigateBack(navigationMode);
                return;
            }
            NavigateBack();
        }

        protected void ShowLoading(string message = null, bool useDelay = true)
        {
            MessageBus.SendMessage(Consts.DialogShowLoadingMessage, message);
        }

        protected void HideLoading()
        {
            MessageBus.SendMessage(Consts.DialogHideLoadingMessage);
        }

        protected static Task ShowAlert(string title, string message, string cancel)
        {
            var tcs = new TaskCompletionSource<bool>();
            MessageBus.SendMessage(Consts.DialogAlertMessage,
                new DialogAlertInfo
                {
                    Title = title,
                    Message = message,
                    Cancel = cancel,
                    OnCompleted = () => tcs.SetResult(true)
                });
            return tcs.Task;
        }

        protected static Task<string> ShowSheet(string title, string cancel, string destruction, string[] items)
        {
            var tcs = new TaskCompletionSource<string>();
            MessageBus.SendMessage(Consts.DialogSheetMessage,
                new DialogSheetInfo
                {
                    Title = title,
                    Cancel = cancel,
                    Destruction = destruction,
                    Items = items,
                    OnCompleted = s => tcs.SetResult(s)
                });
            return tcs.Task;
        }

        protected static Task<bool> ShowQuestion(string title, string question, string positive, string negative)
        {
            var tcs = new TaskCompletionSource<bool>();
            MessageBus.SendMessage(Consts.DialogQuestionMessage,
                new DialogQuestionInfo
                {
                    Title = title,
                    Question = question,
                    Positive = positive,
                    Negative = negative,
                    OnCompleted = b => tcs.SetResult(b)
                });
            return tcs.Task;
        }

        protected static Task<string> ShowEntryAlert(string title, string message, string cancel, string ok, string placeholder)
        {
            var tcs = new TaskCompletionSource<string>();
            MessageBus.SendMessage(Consts.DialogEntryMessage,
                new DialogEntryInfo
                {
                    Title = title,
                    Message = message,
                    Cancel = cancel,
                    Ok = ok,
                    Placeholder = placeholder,
                    OnCompleted = s => tcs.SetResult(s),
                    OnCancelled = () => tcs.SetResult(null)
                });
            return tcs.Task;
        }

        protected static void ShowToast(string text, bool isLongTime = false, bool isCenter = false)
        {
            MessageBus.SendMessage(Consts.DialogToastMessage,
                new DialogToastInfo
                {
                    Text = text,
                    IsCenter = isCenter,
                    IsLongTime = isLongTime
                });
        }

        private ICommand SaveCommand(ICommand command, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            if (!_cachedCommands.ContainsKey(propertyName))
                _cachedCommands.TryAdd(propertyName, command);

            return command;
        }

        private ICommand GetCommand(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            return _cachedCommands.TryGetValue(propertyName, out var cachedCommand)
                ? cachedCommand
                : null;
        }
    }
}