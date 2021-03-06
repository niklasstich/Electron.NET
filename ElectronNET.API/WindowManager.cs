using ElectronNET.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ElectronNET.API.Interfaces;

namespace ElectronNET.API
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WindowManager : IWindowManager
    {
        private static WindowManager _windowManager;
        private static object _syncRoot = new object();

        internal WindowManager() { }

        internal static WindowManager Instance
        {
            get
            {
                if (_windowManager == null)
                {
                    lock (_syncRoot)
                    {
                        if (_windowManager == null)
                        {
                            _windowManager = new WindowManager();
                        }
                    }
                }

                return _windowManager;
            }
        }

        /// <summary>
        /// Quit when all windows are closed. (Default is true)
        /// </summary>
        /// <value>
        ///   <c>true</c> if [quit window all closed]; otherwise, <c>false</c>.
        /// </value>
        public bool IsQuitOnWindowAllClosed
        {
            get { return _isQuitOnWindowAllClosed; }
            set
            {
                BridgeConnector.Socket.Emit("quit-app-window-all-closed-event", value);
                _isQuitOnWindowAllClosed = value;
            }
        }
        private bool _isQuitOnWindowAllClosed = true;

        /// <summary>
        /// Gets the browser windows.
        /// </summary>
        /// <value>
        /// The browser windows.
        /// </value>
        public IReadOnlyCollection<IBrowserWindow> BrowserWindows { get { return _browserWindows.AsReadOnly(); } }
        private List<BrowserWindow> _browserWindows = new List<BrowserWindow>();

        /// <summary>
        /// Gets the browser views.
        /// </summary>
        /// <value>
        /// The browser view.
        /// </value>
        public IReadOnlyCollection<BrowserView> BrowserViews { get { return _browserViews.AsReadOnly(); } }
        private List<BrowserView> _browserViews = new List<BrowserView>();

        /// <summary>
        /// Creates the window asynchronous.
        /// </summary>
        /// <param name="loadUrl">The load URL.</param>
        /// <returns></returns>
        public async Task<BrowserWindow> CreateWindowAsync(string loadUrl = "http://localhost")
        {
            return await CreateWindowAsync(new BrowserWindowOptions(), loadUrl);
        }

        /// <summary>
        /// Creates the window asynchronous.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="loadUrl">The load URL.</param>
        /// <returns></returns>
        public Task<BrowserWindow> CreateWindowAsync(BrowserWindowOptions options, string loadUrl = "http://localhost")
        {
            var taskCompletionSource = new TaskCompletionSource<BrowserWindow>();

            BridgeConnector.Socket.On("BrowserWindowCreated", (id) =>
            {
                BridgeConnector.Socket.Off("BrowserWindowCreated");

                string windowId = id.ToString();
                BrowserWindow browserWindow = new BrowserWindow(int.Parse(windowId));
                _browserWindows.Add(browserWindow);

                taskCompletionSource.SetResult(browserWindow);
            });

            BridgeConnector.Socket.Off("BrowserWindowClosed");
            BridgeConnector.Socket.On("BrowserWindowClosed", (ids) =>
            {
                var browserWindowIds = ((JArray)ids).ToObject<int[]>();

                for (int index = 0; index < _browserWindows.Count; index++)
                {
                    if (!browserWindowIds.Contains(_browserWindows[index].Id))
                    {
                        _browserWindows.RemoveAt(index);
                    }
                }
            });

            if (loadUrl.ToUpper() == "HTTP://LOCALHOST")
            {
                loadUrl = $"{loadUrl}:{BridgeSettings.WebPort}";
            }

            // Workaround Windows 10 / Electron Bug
            // https://github.com/electron/electron/issues/4045
            if (isWindows10())
            {
                options.Width = options.Width + 14;
                options.Height = options.Height + 7;
            }

            if (options.X == -1 && options.Y == -1)
            {
                options.X = 0;
                options.Y = 0;

                BridgeConnector.Socket.Emit("createBrowserWindow", JObject.FromObject(options, _jsonSerializer), loadUrl);
            }
            else
            {
                // Workaround Windows 10 / Electron Bug
                // https://github.com/electron/electron/issues/4045
                if (isWindows10())
                {
                    options.X = options.X - 7;
                }

                var ownjsonSerializer = new JsonSerializer()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };
                BridgeConnector.Socket.Emit("createBrowserWindow", JObject.FromObject(options, ownjsonSerializer), loadUrl);
            }

            return taskCompletionSource.Task;
        }

        private bool isWindows10()
        {
            return RuntimeInformation.OSDescription.Contains("Windows 10");
        }

        /// <summary>
        /// A BrowserView can be used to embed additional web content into a BrowserWindow. 
        /// It is like a child window, except that it is positioned relative to its owning window. 
        /// It is meant to be an alternative to the webview tag.
        /// </summary>
        /// <returns></returns>
        public Task<BrowserView> CreateBrowserViewAsync()
        {
            return CreateBrowserViewAsync(new BrowserViewConstructorOptions());
        }

        /// <summary>
        /// A BrowserView can be used to embed additional web content into a BrowserWindow. 
        /// It is like a child window, except that it is positioned relative to its owning window. 
        /// It is meant to be an alternative to the webview tag.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Task<BrowserView> CreateBrowserViewAsync(BrowserViewConstructorOptions options)
        {
            var taskCompletionSource = new TaskCompletionSource<BrowserView>();

            BridgeConnector.Socket.On("BrowserViewCreated", (id) =>
            {
                BridgeConnector.Socket.Off("BrowserViewCreated");

                string browserViewId = id.ToString();
                BrowserView browserView = new BrowserView(int.Parse(browserViewId));

                _browserViews.Add(browserView);

                taskCompletionSource.SetResult(browserView);
            });

            var ownjsonSerializer = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            BridgeConnector.Socket.Emit("createBrowserView", JObject.FromObject(options, ownjsonSerializer));

            return taskCompletionSource.Task;
        }

        private JsonSerializer _jsonSerializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
    }
}
