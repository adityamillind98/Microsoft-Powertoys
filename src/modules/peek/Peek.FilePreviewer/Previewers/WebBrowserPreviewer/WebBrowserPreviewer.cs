﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;
using Peek.Common.Extensions;
using Peek.FilePreviewer.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using File = Peek.Common.Models.File;

namespace Peek.FilePreviewer.Previewers
{
    public partial class WebBrowserPreviewer : ObservableObject, IBrowserPreviewer
    {
        private static readonly HashSet<string> _supportedFileTypes = new HashSet<string>
        {
            // Web
            ".html",
            ".htm",

            // Document
            ".pdf",
        };

        [ObservableProperty]
        private Uri? preview;

        [ObservableProperty]
        private PreviewState state;

        public WebBrowserPreviewer(File file)
        {
            File = file;
            Dispatcher = DispatcherQueue.GetForCurrentThread();
        }

        private File File { get; }

        private DispatcherQueue Dispatcher { get; }

        public Task<Size?> GetPreviewSizeAsync(CancellationToken cancellationToken)
        {
            Size? size = null;
            return Task.FromResult(size);
        }

        public Task LoadPreviewAsync(CancellationToken cancellationToken)
        {
            State = PreviewState.Loading;

            Preview = new Uri(File.Path);

            return Task.CompletedTask;
        }

        public async Task CopyAsync()
        {
            await Dispatcher.RunOnUiThread(async () =>
            {
                var storageFile = await File.GetStorageFileAsync();

                var dataPackage = new DataPackage();
                dataPackage.SetStorageItems(new StorageFile[1] { storageFile }, false);

                Clipboard.SetContent(dataPackage);
            });
        }

        public static bool IsFileTypeSupported(string fileExt)
        {
            return _supportedFileTypes.Contains(fileExt);
        }
    }
}
