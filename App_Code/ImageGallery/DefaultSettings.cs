/*
 *  Copyright 2013 Textpencil
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 *  History: 
 *  2008/10/07 Created.
 */

using System;
using System.Collections.Generic;
using System.Web;

namespace Textpencil.ImageGallery
{
    /// <summary>
    /// Default Settings of the Textpencil.ImageGallery
    /// </summary>
    public class DefaultSettings
    {
        #region Private Attributes
        private static string defaultTheme = "Default";
       
        private static string siteTitle = "Your Site Title";
        private static string siteDescription = "Your Site Description";
        private static string flickrApiKey = null; // Flickr API Key
        private static string flickrUserId = null; // Flickr User ID
        private static string flickrUrl = "http://www.flickr.com/" + flickrUserId;
        private static string cacheSecretKey = "key";
        private static int    imagesPerPage = 12;
        private static string footerText = "Copyright © 2013 " + siteTitle;
        private static string projectSiteUrl = "http://code.google.com/p/imagegallery";
        private static string version = "Textpencil.ImageGallery 1.0";
        private static string unknownError = "Oops an unknown error occurred. Please try again shortly.";
        #endregion

        #region Public Properties
        public static string DefaultTheme
        {
            get { return DefaultSettings.defaultTheme; }            
        }

        public static string SiteTitle
        {
            get { return DefaultSettings.siteTitle; }            
        }

        public static string SiteDescription
        {
            get { return DefaultSettings.siteDescription; }
        }

        public static string FlickrApiKey
        {
            get { return DefaultSettings.flickrApiKey; }
        }

        public static string FlickrUserID
        {
            get { return DefaultSettings.flickrUserId; }
        }

        public static string FlickrUrl
        {
            get { return DefaultSettings.flickrUrl ; }
        }

        public static string CacheSecretKey
        {
            get { return DefaultSettings.cacheSecretKey; }
        }

        public static int ImagesPerPage
        {
            get { return DefaultSettings.imagesPerPage; }
            set { DefaultSettings.imagesPerPage = value; }
        }

        public static string FooterText
        {
            get { return DefaultSettings.footerText; }
        }

        public static string ProjectSiteUrl
        {
            get { return DefaultSettings.projectSiteUrl; }
        }

        public static string Version
        {
            get { return DefaultSettings.version; }
        }

        public static string UnknownError
        {
            get { return DefaultSettings.unknownError; }
        }
        #endregion
    }
}
