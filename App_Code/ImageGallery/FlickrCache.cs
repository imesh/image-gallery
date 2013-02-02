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
using FlickrNet;

namespace Textpencil.ImageGallery
{
    public static class FlickrCache
    {
        #region Private Attributes
        private static Flickr flickr;
        private static PhotoCollection photoCollection;
        private static List<KeyValuePair<string, string>> imageDescriptions;
        #endregion

        #region Public Properties
        public static List<KeyValuePair<string, string>> ImageDescriptions
        {
            get { return imageDescriptions; }
            set { imageDescriptions = value; }
        }

        public static PhotoCollection PhotoCollection
        {
            get { return photoCollection; }
            set { photoCollection = value; }
        }

        public static int Count
        {
            get
            {
                if (photoCollection != null)
                    return photoCollection.Count;
                else
                    return 0;
            }
        }
        #endregion

        #region Private Methods
        private static void initializeFlickr()
        {
            if (flickr == null)
            {
                string flickrApiKey = DefaultSettings.FlickrApiKey;
                if (flickrApiKey != null)
                    flickr = new Flickr(flickrApiKey);
            }
        }

        private static string GetImageDescription(Photo image)
        {
            try
            {
                initializeFlickr();

                PhotoInfo photoInfo = flickr.PhotosGetInfo(image.PhotoId);
                return photoInfo.Description;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        #endregion

        #region Public Methods
        public static void UpdateFlickrImageCache()
        {
            initializeFlickr();
            if (flickr != null)
            {
                PhotoSearchOptions options = new PhotoSearchOptions();
                options.UserId = DefaultSettings.FlickrUserID;
                options.PerPage = 1000;
                options.Page = 1;
                                
                PhotoCollection = flickr.PhotosSearch(options);

                if (PhotoCollection != null)
                {
                    ImageDescriptions = new List<KeyValuePair<string, string>>();
                    foreach (Photo photo in PhotoCollection)
                    {
                        KeyValuePair<string, string> pair = new KeyValuePair<string, string>(photo.PhotoId, GetImageDescription(photo));
                        ImageDescriptions.Add(pair);
                    }
                }
            }
        }

        public static string GetDescription(string photoId)
        {
            if(Textpencil.ImageGallery.FlickrCache.ImageDescriptions != null)
            {
                foreach (KeyValuePair<string, string> pair in Textpencil.ImageGallery.FlickrCache.ImageDescriptions)
                {
                    if ((pair.Key != null) && (pair.Key.Equals(photoId)))
                        return pair.Value;
                }
            }
            return null;
        }
        #endregion
    }
}
