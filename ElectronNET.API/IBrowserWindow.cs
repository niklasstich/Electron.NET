using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronNET.API.Entities;

namespace ElectronNET.API
{
    public interface IBrowserWindow
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; }

        /// <summary>
        /// Gets the menu items.
        /// </summary>
        /// <value>
        /// The menu items.
        /// </value>
        IReadOnlyCollection<MenuItem> MenuItems { get; }

        /// <summary>
        /// Gets the thumbar buttons.
        /// </summary>
        /// <value>
        /// The thumbar buttons.
        /// </value>
        IReadOnlyCollection<ThumbarButton> ThumbarButtons { get; }

        /// <summary>
        /// Render and control web pages.
        /// </summary>
        WebContents WebContents { get; }

        /// <summary>
        /// Emitted when the web page has been rendered (while not being shown) and 
        /// window can be displayed without a visual flash.
        /// </summary>
        event Action OnReadyToShow;

        /// <summary>
        /// Emitted when the document changed its title.
        /// </summary>
        event Action<string> OnPageTitleUpdated;

        /// <summary>
        /// Emitted when the window is going to be closed.
        /// </summary>
        event Action OnClose;

        /// <summary>
        /// Emitted when the window is closed. 
        /// After you have received this event you should remove the 
        /// reference to the window and avoid using it any more.
        /// </summary>
        event Action OnClosed;

        /// <summary>
        /// Emitted when window session is going to end due to force shutdown or machine restart or session log off.
        /// </summary>
        event Action OnSessionEnd;

        /// <summary>
        /// Emitted when the web page becomes unresponsive.
        /// </summary>
        event Action OnUnresponsive;

        /// <summary>
        /// Emitted when the unresponsive web page becomes responsive again.
        /// </summary>
        event Action OnResponsive;

        /// <summary>
        /// Emitted when the window loses focus.
        /// </summary>
        event Action OnBlur;

        /// <summary>
        /// Emitted when the window gains focus.
        /// </summary>
        event Action OnFocus;

        /// <summary>
        /// Emitted when the window is shown.
        /// </summary>
        event Action OnShow;

        /// <summary>
        /// Emitted when the window is hidden.
        /// </summary>
        event Action OnHide;

        /// <summary>
        /// Emitted when window is maximized.
        /// </summary>
        event Action OnMaximize;

        /// <summary>
        /// Emitted when the window exits from a maximized state.
        /// </summary>
        event Action OnUnmaximize;

        /// <summary>
        /// Emitted when the window is minimized.
        /// </summary>
        event Action OnMinimize;

        /// <summary>
        /// Emitted when the window is restored from a minimized state.
        /// </summary>
        event Action OnRestore;

        /// <summary>
        /// Emitted when the window is being resized.
        /// </summary>
        event Action OnResize;

        /// <summary>
        /// Emitted when the window is being moved to a new position.
        /// 
        /// Note: On macOS this event is just an alias of moved.
        /// </summary>
        event Action OnMove;

        /// <summary>
        /// macOS: Emitted once when the window is moved to a new position.
        /// </summary>
        event Action OnMoved;

        /// <summary>
        /// Emitted when the window enters a full-screen state.
        /// </summary>
        event Action OnEnterFullScreen;

        /// <summary>
        /// Emitted when the window leaves a full-screen state.
        /// </summary>
        event Action OnLeaveFullScreen;

        /// <summary>
        /// Emitted when the window enters a full-screen state triggered by HTML API.
        /// </summary>
        event Action OnEnterHtmlFullScreen;

        /// <summary>
        /// Emitted when the window leaves a full-screen state triggered by HTML API.
        /// </summary>
        event Action OnLeaveHtmlFullScreen;

        /// <summary>
        /// Emitted when an App Command is invoked. These are typically related to 
        /// keyboard media keys or browser commands, as well as the “Back” button 
        /// built into some mice on Windows.
        /// 
        /// Commands are lowercased, underscores are replaced with hyphens, 
        /// and the APPCOMMAND_ prefix is stripped off.e.g.APPCOMMAND_BROWSER_BACKWARD 
        /// is emitted as browser-backward.
        /// </summary>
        event Action<string> OnAppCommand;

        /// <summary>
        /// Emitted when scroll wheel event phase has begun.
        /// </summary>
        event Action OnScrollTouchBegin;

        /// <summary>
        /// Emitted when scroll wheel event phase has ended.
        /// </summary>
        event Action OnScrollTouchEnd;

        /// <summary>
        /// Emitted when scroll wheel event phase filed upon reaching the edge of element.
        /// </summary>
        event Action OnScrollTouchEdge;

        /// <summary>
        /// Emitted on 3-finger swipe. Possible directions are up, right, down, left.
        /// </summary>
        event Action<string> OnSwipe;

        /// <summary>
        /// Emitted when the window opens a sheet.
        /// </summary>
        event Action OnSheetBegin;

        /// <summary>
        /// Emitted when the window has closed a sheet.
        /// </summary>
        event Action OnSheetEnd;

        /// <summary>
        /// Emitted when the native new tab button is clicked.
        /// </summary>
        event Action OnNewWindowForTab;

        /// <summary>
        /// Force closing the window, the unload and beforeunload event won’t be 
        /// emitted for the web page, and close event will also not be emitted 
        /// for this window, but it guarantees the closed event will be emitted.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Try to close the window. This has the same effect as a user manually 
        /// clicking the close button of the window. The web page may cancel the close though. 
        /// </summary>
        void Close();

        /// <summary>
        /// Focuses on the window.
        /// </summary>
        void Focus();

        /// <summary>
        /// Removes focus from the window.
        /// </summary>
        void Blur();

        /// <summary>
        /// Whether the window is focused.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsFocusedAsync();

        /// <summary>
        /// Whether the window is destroyed.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsDestroyedAsync();

        /// <summary>
        /// Shows and gives focus to the window.
        /// </summary>
        void Show();

        /// <summary>
        /// Shows the window but doesn’t focus on it.
        /// </summary>
        void ShowInactive();

        /// <summary>
        /// Hides the window.
        /// </summary>
        void Hide();

        /// <summary>
        /// Whether the window is visible to the user.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsVisibleAsync();

        /// <summary>
        /// Whether current window is a modal window.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsModalAsync();

        /// <summary>
        /// Maximizes the window. This will also show (but not focus) the window if it isn’t being displayed already.
        /// </summary>
        void Maximize();

        /// <summary>
        /// Unmaximizes the window.
        /// </summary>
        void Unmaximize();

        /// <summary>
        /// Whether the window is maximized.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsMaximizedAsync();

        /// <summary>
        /// Minimizes the window. On some platforms the minimized window will be shown in the Dock.
        /// </summary>
        void Minimize();

        /// <summary>
        /// Restores the window from minimized state to its previous state.
        /// </summary>
        void Restore();

        /// <summary>
        /// Whether the window is minimized.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsMinimizedAsync();

        /// <summary>
        /// Sets whether the window should be in fullscreen mode.
        /// </summary>
        void SetFullScreen(bool flag);

        /// <summary>
        /// Whether the window is in fullscreen mode.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsFullScreenAsync();

        /// <summary>
        /// This will make a window maintain an aspect ratio. The extra size allows a developer to have space, 
        /// specified in pixels, not included within the aspect ratio calculations. This API already takes into
        /// account the difference between a window’s size and its content size.
        ///
        /// Consider a normal window with an HD video player and associated controls.Perhaps there are 15 pixels
        /// of controls on the left edge, 25 pixels of controls on the right edge and 50 pixels of controls below
        /// the player. In order to maintain a 16:9 aspect ratio (standard aspect ratio for HD @1920x1080) within
        /// the player itself we would call this function with arguments of 16/9 and[40, 50]. The second argument
        /// doesn’t care where the extra width and height are within the content view–only that they exist. Just 
        /// sum any extra width and height areas you have within the overall content view.
        /// </summary>
        /// <param name="aspectRatio">The aspect ratio to maintain for some portion of the content view.</param>
        /// <param name="extraSize">The extra size not to be included while maintaining the aspect ratio.</param>
        void SetAspectRatio(int aspectRatio, Size extraSize);

        /// <summary>
        /// Uses Quick Look to preview a file at a given path.
        /// </summary>
        /// <param name="path">The absolute path to the file to preview with QuickLook. This is important as 
        /// Quick Look uses the file name and file extension on the path to determine the content type of the 
        /// file to open.</param>
        void PreviewFile(string path);

        /// <summary>
        /// Uses Quick Look to preview a file at a given path.
        /// </summary>
        /// <param name="path">The absolute path to the file to preview with QuickLook. This is important as 
        /// Quick Look uses the file name and file extension on the path to determine the content type of the 
        /// file to open.</param>
        /// <param name="displayname">The name of the file to display on the Quick Look modal view. This is 
        /// purely visual and does not affect the content type of the file. Defaults to path.</param>
        void PreviewFile(string path, string displayname);

        /// <summary>
        /// Closes the currently open Quick Look panel.
        /// </summary>
        void CloseFilePreview();

        /// <summary>
        /// Resizes and moves the window to the supplied bounds
        /// </summary>
        /// <param name="bounds"></param>
        void SetBounds(Rectangle bounds);

        /// <summary>
        /// Resizes and moves the window to the supplied bounds
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="animate"></param>
        void SetBounds(Rectangle bounds, bool animate);

        /// <summary>
        /// Gets the bounds asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<Rectangle> GetBoundsAsync();

        /// <summary>
        /// Resizes and moves the window’s client area (e.g. the web page) to the supplied bounds.
        /// </summary>
        /// <param name="bounds"></param>
        void SetContentBounds(Rectangle bounds);

        /// <summary>
        /// Resizes and moves the window’s client area (e.g. the web page) to the supplied bounds.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="animate"></param>
        void SetContentBounds(Rectangle bounds, bool animate);

        /// <summary>
        /// Gets the content bounds asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<Rectangle> GetContentBoundsAsync();

        /// <summary>
        /// Resizes the window to width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void SetSize(int width, int height);

        /// <summary>
        /// Resizes the window to width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="animate"></param>
        void SetSize(int width, int height, bool animate);

        /// <summary>
        /// Contains the window’s width and height.
        /// </summary>
        /// <returns></returns>
        Task<int[]> GetSizeAsync();

        /// <summary>
        /// Resizes the window’s client area (e.g. the web page) to width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void SetContentSize(int width, int height);

        /// <summary>
        /// Resizes the window’s client area (e.g. the web page) to width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="animate"></param>
        void SetContentSize(int width, int height, bool animate);

        /// <summary>
        /// Contains the window’s client area’s width and height.
        /// </summary>
        /// <returns></returns>
        Task<int[]> GetContentSizeAsync();

        /// <summary>
        /// Sets the minimum size of window to width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void SetMinimumSize(int width, int height);

        /// <summary>
        /// Contains the window’s minimum width and height.
        /// </summary>
        /// <returns></returns>
        Task<int[]> GetMinimumSizeAsync();

        /// <summary>
        /// Sets the maximum size of window to width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void SetMaximumSize(int width, int height);

        /// <summary>
        /// Contains the window’s maximum width and height.
        /// </summary>
        /// <returns></returns>
        Task<int[]> GetMaximumSizeAsync();

        /// <summary>
        /// Sets whether the window can be manually resized by user.
        /// </summary>
        /// <param name="resizable"></param>
        void SetResizable(bool resizable);

        /// <summary>
        /// Whether the window can be manually resized by user.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsResizableAsync();

        /// <summary>
        /// Sets whether the window can be moved by user. On Linux does nothing.
        /// </summary>
        /// <param name="movable"></param>
        void SetMovable(bool movable);

        /// <summary>
        /// Whether the window can be moved by user.
        /// 
        /// On Linux always returns true.
        /// </summary>
        /// <returns>On Linux always returns true.</returns>
        Task<bool> IsMovableAsync();

        /// <summary>
        /// Sets whether the window can be manually minimized by user. On Linux does nothing.
        /// </summary>
        /// <param name="minimizable"></param>
        void SetMinimizable(bool minimizable);

        /// <summary>
        /// Whether the window can be manually minimized by user.
        /// 
        /// On Linux always returns true.
        /// </summary>
        /// <returns>On Linux always returns true.</returns>
        Task<bool> IsMinimizableAsync();

        /// <summary>
        /// Sets whether the window can be manually maximized by user. On Linux does nothing.
        /// </summary>
        /// <param name="maximizable"></param>
        void SetMaximizable(bool maximizable);

        /// <summary>
        /// Whether the window can be manually maximized by user.
        /// 
        /// On Linux always returns true.
        /// </summary>
        /// <returns>On Linux always returns true.</returns>
        Task<bool> IsMaximizableAsync();

        /// <summary>
        /// Sets whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
        /// </summary>
        /// <param name="fullscreenable"></param>
        void SetFullScreenable(bool fullscreenable);

        /// <summary>
        /// Whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsFullScreenableAsync();

        /// <summary>
        /// Sets whether the window can be manually closed by user. On Linux does nothing.
        /// </summary>
        /// <param name="closable"></param>
        void SetClosable(bool closable);

        /// <summary>
        /// Whether the window can be manually closed by user.
        /// 
        /// On Linux always returns true.
        /// </summary>
        /// <returns>On Linux always returns true.</returns>
        Task<bool> IsClosableAsync();

        /// <summary>
        /// Sets whether the window should show always on top of other windows. 
        /// After setting this, the window is still a normal window, not a toolbox 
        /// window which can not be focused on.
        /// </summary>
        /// <param name="flag"></param>
        void SetAlwaysOnTop(bool flag);

        /// <summary>
        /// Sets whether the window should show always on top of other windows. 
        /// After setting this, the window is still a normal window, not a toolbox 
        /// window which can not be focused on.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="level">Values include normal, floating, torn-off-menu, modal-panel, main-menu, 
        /// status, pop-up-menu and screen-saver. The default is floating. 
        /// See the macOS docs</param>
        void SetAlwaysOnTop(bool flag, OnTopLevel level);

        /// <summary>
        /// Sets whether the window should show always on top of other windows. 
        /// After setting this, the window is still a normal window, not a toolbox 
        /// window which can not be focused on.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="level">Values include normal, floating, torn-off-menu, modal-panel, main-menu, 
        /// status, pop-up-menu and screen-saver. The default is floating. 
        /// See the macOS docs</param>
        /// <param name="relativeLevel">The number of layers higher to set this window relative to the given level. 
        /// The default is 0. Note that Apple discourages setting levels higher than 1 above screen-saver.</param>
        void SetAlwaysOnTop(bool flag, OnTopLevel level, int relativeLevel);

        /// <summary>
        /// Whether the window is always on top of other windows.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsAlwaysOnTopAsync();

        /// <summary>
        /// Moves window to the center of the screen.
        /// </summary>
        void Center();

        /// <summary>
        /// Moves window to x and y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SetPosition(int x, int y);

        /// <summary>
        /// Moves window to x and y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="animate"></param>
        void SetPosition(int x, int y, bool animate);

        /// <summary>
        /// Contains the window’s current position.
        /// </summary>
        /// <returns></returns>
        Task<int[]> GetPositionAsync();

        /// <summary>
        /// Changes the title of native window to title.
        /// </summary>
        /// <param name="title"></param>
        void SetTitle(string title);

        /// <summary>
        /// The title of the native window.
        /// 
        /// Note: The title of web page can be different from the title of the native window.
        /// </summary>
        /// <returns></returns>
        Task<string> GetTitleAsync();

        /// <summary>
        /// Changes the attachment point for sheets on macOS. 
        /// By default, sheets are attached just below the window frame, 
        /// but you may want to display them beneath a HTML-rendered toolbar.
        /// </summary>
        /// <param name="offsetY"></param>
        void SetSheetOffset(float offsetY);

        /// <summary>
        /// Changes the attachment point for sheets on macOS. 
        /// By default, sheets are attached just below the window frame, 
        /// but you may want to display them beneath a HTML-rendered toolbar.
        /// </summary>
        /// <param name="offsetY"></param>
        /// <param name="offsetX"></param>
        void SetSheetOffset(float offsetY, float offsetX);

        /// <summary>
        /// Starts or stops flashing the window to attract user’s attention.
        /// </summary>
        /// <param name="flag"></param>
        void FlashFrame(bool flag);

        /// <summary>
        /// Makes the window not show in the taskbar.
        /// </summary>
        /// <param name="skip"></param>
        void SetSkipTaskbar(bool skip);

        /// <summary>
        /// Enters or leaves the kiosk mode.
        /// </summary>
        /// <param name="flag"></param>
        void SetKiosk(bool flag);

        /// <summary>
        /// Whether the window is in kiosk mode.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsKioskAsync();

        /// <summary>
        /// Returns the native type of the handle is HWND on Windows, NSView* on macOS, and Window (unsigned long) on Linux.
        /// </summary>
        /// <returns>string of the native handle obtained, HWND on Windows, NSView* on macOS, and Window (unsigned long) on Linux.</returns>
        Task<string> GetNativeWindowHandle();

        /// <summary>
        /// Sets the pathname of the file the window represents, 
        /// and the icon of the file will show in window’s title bar.
        /// </summary>
        /// <param name="filename"></param>
        void SetRepresentedFilename(string filename);

        /// <summary>
        /// The pathname of the file the window represents.
        /// </summary>
        /// <returns></returns>
        Task<string> GetRepresentedFilenameAsync();

        /// <summary>
        /// Specifies whether the window’s document has been edited, 
        /// and the icon in title bar will become gray when set to true.
        /// </summary>
        /// <param name="edited"></param>
        void SetDocumentEdited(bool edited);

        /// <summary>
        /// Whether the window’s document has been edited.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsDocumentEditedAsync();

        /// <summary>
        /// Focuses the on web view.
        /// </summary>
        void FocusOnWebView();

        /// <summary>
        /// Blurs the web view.
        /// </summary>
        void BlurWebView();

        /// <summary>
        /// The url can be a remote address (e.g. http://) or 
        /// a path to a local HTML file using the file:// protocol.
        /// </summary>
        /// <param name="url"></param>
        void LoadURL(string url);

        /// <summary>
        /// The url can be a remote address (e.g. http://) or 
        /// a path to a local HTML file using the file:// protocol.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="options"></param>
        void LoadURL(string url, LoadURLOptions options);

        /// <summary>
        /// Same as webContents.reload.
        /// </summary>
        void Reload();

        /// <summary>
        /// Sets the menu as the window’s menu bar, 
        /// setting it to null will remove the menu bar.
        /// </summary>
        /// <param name="menuItems"></param>
        void SetMenu(MenuItem[] menuItems);

        /// <summary>
        /// Remove the window's menu bar.
        /// </summary>
        void RemoveMenu();

        /// <summary>
        /// Sets progress value in progress bar. Valid range is [0, 1.0]. Remove progress
        /// bar when progress smaler as 0; Change to indeterminate mode when progress bigger as 1. On Linux
        /// platform, only supports Unity desktop environment, you need to specify the
        /// .desktop file name to desktopName field in package.json.By default, it will
        /// assume app.getName().desktop.On Windows, a mode can be passed.Accepted values
        /// are none, normal, indeterminate, error, and paused. If you call setProgressBar
        /// without a mode set (but with a value within the valid range), normal will be
        /// assumed.
        /// </summary>
        /// <param name="progress"></param>
        void SetProgressBar(double progress);

        /// <summary>
        /// Sets progress value in progress bar. Valid range is [0, 1.0]. Remove progress
        /// bar when progress smaler as 0; Change to indeterminate mode when progress bigger as 1. On Linux
        /// platform, only supports Unity desktop environment, you need to specify the
        /// .desktop file name to desktopName field in package.json.By default, it will
        /// assume app.getName().desktop.On Windows, a mode can be passed.Accepted values
        /// are none, normal, indeterminate, error, and paused. If you call setProgressBar
        /// without a mode set (but with a value within the valid range), normal will be
        /// assumed.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="progressBarOptions"></param>
        void SetProgressBar(double progress, ProgressBarOptions progressBarOptions);

        /// <summary>
        /// Sets whether the window should have a shadow. On Windows and Linux does nothing.
        /// </summary>
        /// <param name="hasShadow"></param>
        void SetHasShadow(bool hasShadow);

        /// <summary>
        /// Whether the window has a shadow.
        /// 
        /// On Windows and Linux always returns true.
        /// </summary>
        /// <returns></returns>
        Task<bool> HasShadowAsync();

        /// <summary>
        /// Add a thumbnail toolbar with a specified set of buttons to the thumbnail 
        /// image of a window in a taskbar button layout. Returns a Boolean object 
        /// indicates whether the thumbnail has been added successfully.
        /// 
        /// The number of buttons in thumbnail toolbar should be no greater than 7 due 
        /// to the limited room.Once you setup the thumbnail toolbar, the toolbar cannot
        /// be removed due to the platform’s limitation.But you can call the API with an
        /// empty array to clean the buttons.
        /// </summary>
        /// <param name="thumbarButtons"></param>
        /// <returns>Whether the buttons were added successfully.</returns>
        Task<bool> SetThumbarButtonsAsync(ThumbarButton[] thumbarButtons);

        /// <summary>
        /// Sets the region of the window to show as the thumbnail image displayed when hovering over
        /// the window in the taskbar. You can reset the thumbnail to be the entire window by specifying
        /// an empty region: {x: 0, y: 0, width: 0, height: 0}.
        /// </summary>
        /// <param name="rectangle"></param>
        void SetThumbnailClip(Rectangle rectangle);

        /// <summary>
        /// Sets the toolTip that is displayed when hovering over the window thumbnail in the taskbar.
        /// </summary>
        /// <param name="tooltip"></param>
        void SetThumbnailToolTip(string tooltip);

        /// <summary>
        /// Sets the properties for the window’s taskbar button.
        /// 
        /// Note: relaunchCommand and relaunchDisplayName must always be set together. 
        /// If one of those properties is not set, then neither will be used.
        /// </summary>
        /// <param name="options"></param>
        void SetAppDetails(AppDetailsOptions options);

        /// <summary>
        /// Same as webContents.showDefinitionForSelection().
        /// </summary>
        void ShowDefinitionForSelection();

        /// <summary>
        /// Sets whether the window menu bar should hide itself automatically. 
        /// Once set the menu bar will only show when users press the single Alt key.
        /// 
        /// If the menu bar is already visible, calling setAutoHideMenuBar(true) won’t hide it immediately.
        /// </summary>
        /// <param name="hide"></param>
        void SetAutoHideMenuBar(bool hide);

        /// <summary>
        /// Whether menu bar automatically hides itself.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsMenuBarAutoHideAsync();

        /// <summary>
        /// Sets whether the menu bar should be visible. If the menu bar is auto-hide,
        /// users can still bring up the menu bar by pressing the single Alt key.
        /// </summary>
        /// <param name="visible"></param>
        void SetMenuBarVisibility(bool visible);

        /// <summary>
        /// Whether the menu bar is visible.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsMenuBarVisibleAsync();

        /// <summary>
        /// Sets whether the window should be visible on all workspaces.
        /// 
        /// Note: This API does nothing on Windows.
        /// </summary>
        /// <param name="visible"></param>
        void SetVisibleOnAllWorkspaces(bool visible);

        /// <summary>
        /// Whether the window is visible on all workspaces.
        /// 
        /// Note: This API always returns false on Windows.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsVisibleOnAllWorkspacesAsync();

        /// <summary>
        /// Makes the window ignore all mouse events.
        /// 
        /// All mouse events happened in this window will be passed to the window 
        /// below this window, but if this window has focus, it will still receive keyboard events.
        /// </summary>
        /// <param name="ignore"></param>
        void SetIgnoreMouseEvents(bool ignore);

        /// <summary>
        /// Prevents the window contents from being captured by other apps.
        /// 
        /// On macOS it sets the NSWindow’s sharingType to NSWindowSharingNone. 
        /// On Windows it calls SetWindowDisplayAffinity with WDA_MONITOR.
        /// </summary>
        /// <param name="enable"></param>
        void SetContentProtection(bool enable);

        /// <summary>
        /// Changes whether the window can be focused.
        /// </summary>
        /// <param name="focusable"></param>
        void SetFocusable(bool focusable);

        /// <summary>
        /// Sets parent as current window’s parent window, 
        /// passing null will turn current window into a top-level window.
        /// </summary>
        /// <param name="parent"></param>
        void SetParentWindow(BrowserWindow parent);

        /// <summary>
        /// The parent window.
        /// </summary>
        /// <returns></returns>
        Task<IBrowserWindow> GetParentWindowAsync();

        /// <summary>
        /// All child windows.
        /// </summary>
        /// <returns></returns>
        Task<List<IBrowserWindow>> GetChildWindowsAsync();

        /// <summary>
        /// Controls whether to hide cursor when typing.
        /// </summary>
        /// <param name="autoHide"></param>
        void SetAutoHideCursor(bool autoHide);

        /// <summary>
        /// Adds a vibrancy effect to the browser window. 
        /// Passing null or an empty string will remove the vibrancy effect on the window.
        /// </summary>
        /// <param name="type">Can be appearance-based, light, dark, titlebar, selection, 
        /// menu, popover, sidebar, medium-light or ultra-dark. 
        /// See the macOS documentation for more details.</param>
        void SetVibrancy(Vibrancy type);

        /// <summary>
        /// A BrowserView can be used to embed additional web content into a BrowserWindow. 
        /// It is like a child window, except that it is positioned relative to its owning window. 
        /// It is meant to be an alternative to the webview tag.
        /// </summary>
        /// <param name="browserView"></param>
        void SetBrowserView(BrowserView browserView);
    }
}