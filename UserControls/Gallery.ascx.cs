/*
 * Textpencil.ImageGallery 
 * An ASP.NET control for publishing Image Galleries on the web.
 * Copyright (C) 2008 Imesh Gunaratne
 * http://code.google.com/p/imagegallery
 * 
 * This file is part of Textpencil.ImageGallery.
 *
 * Textpencil.ImageGallery is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Textpencil.ImageGallery is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Textpencil.ImageGallery.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * History: 2008/10/07 Created.
 *          
 */

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlickrNet;

namespace Textpencil.ImageGallery.UserControls
{
    public partial class Gallery : System.Web.UI.UserControl
    {
        #region Private Attributes
        #region Page Controls
        //private Label lblPageTitle;
        //private Table tableImages;
        //private Table tablePageNumbers;
        //private Label lblErrorDesc;
        //private LinkButton linkThumbnail;
        //private LinkButton linkMosaic;
        //private LinkButton linkFlickr;
        #endregion

        private string gallery = "thumbnail";
        private int selectedPageNo = 1;

        private List<int[]> rangeList;
        #endregion

        #region Page Events
        protected void Page_PreInit(object sender, EventArgs e)
        {            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();
                ResetErrorLabel();
                SetPageTitle();
                ReadPageArguments();
                LoadGallery();
            }
            catch (Exception e1)
            {
                ShowError(e1);
            }
        }
        #endregion

        #region Private Methods
        private void ReadPageArguments()
        {
            ReadGallery();
            ReadPageNo();
        }

        private void LoadGallery()
        {
            if(gallery != null)
            {
                if (gallery.Equals("mosaic"))
                    AddMosaic();
                else
                    AddThumbnails();
            }
        }

        private void initializeRangeList()
        {
            if (FlickrCache.Count > 0)
            {
                int pageCount = GetPageCount();
                if (pageCount > 10)
                {
                    if (rangeList == null)
                        rangeList = new List<int[]>();

                    for (int i = 4; i <= (pageCount - 5); i=(i+3))
                    {
                        int[] array = new int[5];
                        int k = 0;
                        for (int j = i; j < (i + 5); j++)
                        {
                            array[k++] = j;
                        }
                        rangeList.Add(array);
                    }
                }
            }
        }

        private void SetPageTitle()
        {
            Page.Title = "Image Gallery | " + DefaultSettings.SiteTitle;
            if (this.lblPageTitle != null)
                this.lblPageTitle.Text = "Image Gallery";
        }

        private int[] GetRangeArray()
        {
            foreach (int[] array in rangeList)
            {
                if((selectedPageNo > array[0]) && (selectedPageNo < array[4]))
                    return array;
            }
            return null;
        }

        private bool InRange(int i)
        {
            int pageCount = GetPageCount();
            int[] array = GetRangeArray();
            if (array != null)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == array[j])
                        return true;
                }
            }
            return false;
        }

        private void ReadGallery()
        {
            Object galleryObject = Request["gallery"];
            if (galleryObject != null)
            {
                    gallery = galleryObject.ToString();
                    if ((!gallery.Equals("thumbnail")) && (!gallery.Equals("mosaic")))
                        gallery = "thumbnail";
            }
        }

        private void ReadPageNo()
        {
            Object pageNoObject = Request["page"];
            if (pageNoObject != null)
            {
                try
                {
                    selectedPageNo = int.Parse(pageNoObject.ToString());
                    if ((selectedPageNo < 0) || ((Textpencil.ImageGallery.FlickrCache.PhotoCollection != null) && (selectedPageNo > GetPageCount())))
                        selectedPageNo = 1;
                }
                catch (FormatException)
                {
                    selectedPageNo = 1;
                }
                catch (OverflowException)
                {
                    selectedPageNo = 1;
                }
            }            
        }
        
        private void InitializeControls()
        {
            if(this.tableImages != null)
                this.tableImages.HorizontalAlign = HorizontalAlign.Center;
            if (this.tablePageNumbers != null)
                this.tablePageNumbers.HorizontalAlign = HorizontalAlign.Center;

            if (this.linkThumbnail != null)
                this.linkThumbnail.PostBackUrl = "~/default.aspx?gallery=thumbnail&page=1";
            if (this.linkMosaic != null)
                this.linkMosaic.PostBackUrl = "~/default.aspx?gallery=mosaic&page=1";
            if (this.linkFlickr != null)
                this.linkFlickr.PostBackUrl = DefaultSettings.FlickrUrl;
        }

        private void AddMedium()
        {
            try
            {
                if ((Textpencil.ImageGallery.FlickrCache.PhotoCollection != null) && (Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count > 0))
                {
                    DefaultSettings.ImagesPerPage = 4;

                    TableRow row = new TableRow();
                    tableImages.Rows.Add(row);

                    TableCell cell = null;
                    Image image = null;
                    Label lblImageTitle = null;
                    Label lblImageDesc = null;
                    HyperLink link = null;
                    Photo photo = null;
                    int cellCount = 0;

                    int startIndex = GetStartIndex(selectedPageNo);
                    int endIndex = startIndex + (DefaultSettings.ImagesPerPage - 1);

                    for (int i = startIndex; ((i <= endIndex) && (i < Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count)); i++)
                    {
                        photo = Textpencil.ImageGallery.FlickrCache.PhotoCollection[i];

                        if (cellCount == 1)
                        {
                            row = new TableRow();
                            tableImages.Rows.Add(row);
                            cellCount = 0;
                        }

                        lblImageTitle = new Label();
                        lblImageTitle.Text = "<p><b>" + photo.Title + "</b><br /></p>";

                        image = new Image();
                        image.ImageUrl = photo.MediumUrl;

                        link = new HyperLink();
                        link.NavigateUrl = photo.MediumUrl;
                        link.Controls.Add(image);
                        link.Attributes.Add("rel", "lightbox[flickr]");
                        link.Attributes.Add("title", photo.Title);
                        link.Attributes.Add("alt", photo.Title);

                        lblImageDesc = new Label();                       
                        lblImageDesc.Text = "<div id=\"FlickrImageDesc\"><p>" + Textpencil.ImageGallery.FlickrCache.GetDescription(photo.PhotoId) + "</p></div>";

                        cell = new TableCell();
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.Controls.Add(lblImageTitle);
                        cell.Controls.Add(link);
                        cell.Controls.Add(lblImageDesc);
                        row.Cells.Add(cell);

                        cellCount++;
                    }

                    AddPageNumbers();
                }
            }
            catch (Exception e)
            {
                this.ShowError(e);
            }
        }

        private void AddThumbnails()
        {
            try
            {
                if ((tableImages != null) && (Textpencil.ImageGallery.FlickrCache.PhotoCollection != null) && (Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count > 0))
                {
                    tableImages.CssClass = "FlickrPostThumbnail";
                    DefaultSettings.ImagesPerPage = 12;

                    TableRow row = new TableRow();
                    tableImages.Rows.Add(row);

                    TableCell cell = null;
                    Image image = null;
                    Label lblImageTitle = null;
                    Label lblImageDesc = null;
                    HyperLink link = null;
                    Photo photo = null;
                    int cellCount = 0;

                    int startIndex = GetStartIndex(selectedPageNo);
                    int endIndex = startIndex + (DefaultSettings.ImagesPerPage - 1);

                    for (int i = startIndex; ((i <= endIndex) && (i < Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count)); i++)
                    {
                        photo = Textpencil.ImageGallery.FlickrCache.PhotoCollection[i];

                        if (cellCount == 3)
                        {
                            row = new TableRow();
                            tableImages.Rows.Add(row);
                            cellCount = 0;
                        }

                        lblImageTitle = new Label();
                        lblImageTitle.Text = "<p><b>" + photo.Title + "</b><br /></p>";

                        image = new Image();
                        image.ImageUrl = photo.SmallUrl;

                        link = new HyperLink();
                        link.NavigateUrl = photo.MediumUrl;
                        link.Controls.Add(image);
                        link.Attributes.Add("rel", "lightbox[flickr]");
                        link.Attributes.Add("title", photo.Title);
                        link.Attributes.Add("alt", photo.Title);

                        lblImageDesc = new Label();
                        lblImageDesc.Text = "<div id=\"FlickrImageDesc\"><p>" + Textpencil.ImageGallery.FlickrCache.GetDescription(photo.PhotoId) + "</p></div>";

                        cell = new TableCell();
                        cell.Controls.Add(lblImageTitle);
                        cell.Controls.Add(link);
                        cell.Controls.Add(lblImageDesc);
                        row.Cells.Add(cell);

                        cellCount++;                        
                    }

                    AddPageNumbers();
                }
            }
            catch (Exception e)
            {
                this.ShowError(e);
            }
        }

        private void AddMosaic()
        {
            try
            {
                if ((tableImages != null) && (Textpencil.ImageGallery.FlickrCache.PhotoCollection != null) && (Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count > 0))
                {
                    tableImages.CssClass = "FlickrPostMosaic";
                    DefaultSettings.ImagesPerPage = 256;
                    
                    TableRow row = new TableRow();
                    tableImages.Rows.Add(row);

                    TableCell cell = null;
                    Image image = null;
                    HyperLink link = null;
                    Photo photo = null;
                    int cellCount = 0;

                    int startIndex = GetStartIndex(selectedPageNo);
                    int endIndex = startIndex + (DefaultSettings.ImagesPerPage - 1);

                    for (int i = startIndex; ((i <= endIndex) && (i < Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count)); i++)
                    {
                        photo = Textpencil.ImageGallery.FlickrCache.PhotoCollection[i];

                        if (cellCount == 16)
                        {
                            row = new TableRow();
                            tableImages.Rows.Add(row);
                            cellCount = 0;
                        }

                        image = new Image();
                        image.ImageUrl = photo.SquareThumbnailUrl;

                        link = new HyperLink();
                        link.NavigateUrl = photo.MediumUrl;
                        link.Controls.Add(image);
                        link.Attributes.Add("rel", "lightbox[flickr]");
                        link.Attributes.Add("title", photo.Title);
                        link.Attributes.Add("alt", photo.Title);

                        cell = new TableCell();
                        cell.Controls.Add(link);
                        row.Cells.Add(cell);

                        cellCount++;
                    }
                    AddPageNumbers();
                }
            }
            catch (Exception e)
            {
                this.ShowError(e);
            }
        }

        private int GetPageNumber(int startIndex)
        {
            if (startIndex == 0)
                return 1;
            else if (startIndex > 0)
            {
                return ((startIndex / DefaultSettings.ImagesPerPage) + 1);
            }
            return -1;
        }

        private int GetStartIndex(int pageNo)
        {
            return (pageNo - 1) * DefaultSettings.ImagesPerPage;
        }

        private int GetPageCount()
        {
            if ((Textpencil.ImageGallery.FlickrCache.PhotoCollection != null) && (Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count > 0))
            {
                double value = (Textpencil.ImageGallery.FlickrCache.PhotoCollection.Count / (double)DefaultSettings.ImagesPerPage);
                return (int)Math.Ceiling(value);
            }
            else
                return 0;
        }

        private string GetNavigateUrl(int pageNo)
        {
            return "~/default.aspx?gallery=" + gallery + "&page=" + pageNo;
        }

        private void AddPageNumbers()
        {
            initializeRangeList();

            if (tablePageNumbers != null)
            {
                TableCell cell = null;
                HyperLink link = null;

                TableRow row = new TableRow();

                // Add Previous '<'
                if ((selectedPageNo - 1) > 0)
                {
                    link = new HyperLink();
                    link.Text = "<div id=\"PageNumber\">" + "<" + "</div>";
                    link.NavigateUrl = GetNavigateUrl(selectedPageNo - 1);
                    cell = new TableCell();
                    cell.Controls.Add(link);
                    row.Cells.Add(cell);
                }
                else
                {
                    cell = new TableCell();
                    cell.Text = "<div id=\"PageNumberNoCursor\">" + "<" + "</div>";
                    row.Cells.Add(cell);
                }

                int pageCount = GetPageCount();
                for (int i = 1; i <= pageCount; i++)
                {
                    if ((pageCount < 10) || // Condition 1
                        (((pageCount > 10) && (selectedPageNo < 5)) && // Condition 2
                            (((i >= 1) && (i <= 5)) || (i == pageCount)) || (i == (pageCount - 1)))  ||
                        (((pageCount > 10) && (selectedPageNo >= 5) && (selectedPageNo <= (pageCount - 5))) &&  // Condition 3
                            ((i == 1) || (i == 2) || (InRange(i)  || (i == pageCount) || (i == (pageCount - 1))))) ||
                        (((pageCount > 10) && (selectedPageNo >= 5) && (selectedPageNo > (pageCount - 5))) && // Condition 4
                            ((i == 1) || (i == 2) || (i >= (pageCount - 5))))
                        
                        )
                    {
                        // Add Number
                        link = new HyperLink();
                        if (selectedPageNo == i)
                            link.Text = "<div id=\"PageNumberSelected\">" + i + "</div>";
                        else
                            link.Text = "<div id=\"PageNumber\">" + i + "</div>";
                        link.NavigateUrl = GetNavigateUrl(i);                        
                        cell = new TableCell();
                        cell.VerticalAlign = VerticalAlign.Top;
                        cell.Controls.Add(link);
                        row.Cells.Add(cell);
                    }
                    else if ((((pageCount > 10) && (selectedPageNo < 5)) && 
                                (i == (pageCount - 3))) ||
                             (((pageCount > 10) && (selectedPageNo >= 5) && (selectedPageNo <= (pageCount - 5))) && 
                                ((i == 3) || (i == (pageCount - 2)))) ||
                             (((pageCount > 10) && (selectedPageNo >= 5) && (selectedPageNo > (pageCount - 5))) && 
                                (i == 3))
                        )
                    {
                        // Add More "..."
                        cell = new TableCell();
                        cell.Text = "<div id=\"PageNumberMore\">" + "..." + "</div>";                        
                        row.Cells.Add(cell);
                    }
                }
                // Add Next '>'
                if ((selectedPageNo + 1) <= pageCount)
                {
                    link = new HyperLink();
                    link.Text = "<div id=\"PageNumber\">" + ">" + "</div>";
                    link.NavigateUrl = GetNavigateUrl(selectedPageNo + 1);
                    cell = new TableCell();
                    cell.Controls.Add(link);
                    row.Cells.Add(cell);
                }
                else
                {
                    cell = new TableCell();
                    cell.Text = "<div id=\"PageNumberNoCursor\">" + ">" + "</div>";
                    row.Cells.Add(cell);
                }

                tablePageNumbers.HorizontalAlign = HorizontalAlign.Center;
                tablePageNumbers.Rows.Add(row);
            }
        }

        private void ResetErrorLabel()
        {
            if (this.lblErrorDesc != null)
            {
                this.lblErrorDesc.Visible = false;
                this.lblErrorDesc.Text = String.Empty;
            }
        }

        private void ShowError(Exception e)
        {
            if (this.lblErrorDesc != null)
            {
                this.lblErrorDesc.Text = DefaultSettings.UnknownError;
                this.lblErrorDesc.Visible = true;
            }
        }
        #endregion
    }
}
