﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace LiteDB
{
    /// <summary>
    /// FileStream disk implementation of disk factory
    /// </summary>
    public class FileStreamDiskFactory : IDiskFactory
    {
        private string _filename;
        private bool _readOnly;

        public FileStreamDiskFactory(string filename, bool readOnly)
        {
            _filename = filename;
            _readOnly = readOnly;
        }

        /// <summary>
        /// Get filename
        /// </summary>
        public string Filename => _filename;

        /// <summary>
        /// Create new FileStream instance based on dataFilename
        /// </summary>
        public Stream GetStream()
        {
            return new FileStream(_filename,
                _readOnly ? FileMode.Open : FileMode.OpenOrCreate,
                _readOnly ? FileAccess.Read : FileAccess.ReadWrite,
                FileShare.ReadWrite,
                BasePage.PAGE_SIZE,
                FileOptions.RandomAccess);
        }

        /// <summary>
        /// Delete file from file system (only if readOnly = false)
        /// </summary>
        public void Delete()
        {
            if (_readOnly == false)
            {
                File.Delete(_filename);
            }
        }

        /// <summary>
        /// Check if file exists
        /// </summary>
        public bool Exists() => File.Exists(_filename);

        /// <summary>
        /// Close all stream on end
        /// </summary>
        public bool Dispose => true;
    }
}